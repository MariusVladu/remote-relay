# https://core-electronics.com.au/guides/getting-started-with-mqtt-on-raspberry-pi-pico-w-connect-to-the-internet-of-things/

import machine
import ubinascii
import network
import secrets
import json
from time import sleep
from umqtt.simple import MQTTClient

client_id = ubinascii.hexlify(machine.unique_id()).decode("utf-8")

__client = None
__on_message_received_callback = None


def init(wlan_init=True, callback=None, topics=[]):
    if wlan_init is True:
        wlan = network.WLAN(network.STA_IF)
        wlan.active(True)
        wlan.connect(secrets.wifi_ssid, secrets.wifi_password)
        while wlan.isconnected() is False:
            print("Waiting for connection...")
            sleep(1)
        print("Connected to WiFi")

    global __client
    __client = MQTTClient(
        client_id=client_id,
        server="raspberrypi.local",
        user="",
        password="",
    )

    if callback is not None:
        global __on_message_received_callback
        __on_message_received_callback = callback

    __client.set_callback(__on_message_received)

    __client.connect()
    print("Connected to MQTT")

    if len(topics) > 0:
        for topic in topics:
            __client.subscribe(topic)
        print(f"Subscribed to topics {topics}")


def publish(topic, message):
    __client.publish(topic, json.dumps(message))


def wait_msg():
    __client.wait_msg()


def __on_message_received(topic, message):
    topic = str(topic, "utf-8")
    message = str(message, "utf-8")

    print(f"Topic: {topic} Message: {message}")

    if __on_message_received_callback is not None:
        __on_message_received_callback(topic, message)
