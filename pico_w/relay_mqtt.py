import json
import mqtt_client_setup as mqtt
import _thread
from time import sleep

connected = False
status = "off"
command_topic = f"command_{mqtt.client_id}"


def on_message_received(topic, message):
    if topic == command_topic:
        message = json.loads(message)
        process_command_message(message)
    else:
        print(f"Unknown topic {topic}")


def process_command_message(message):
    global status
    command = message["command"]

    if command == "on":
        print("Turning on")
        status = "on"
    elif command == "off":
        print("Turning off")
        status = "off"
    else:
        print(f"Unknown command {command}")
        return
    report_status()


def report_status():
    mqtt.publish(
        topic="status",
        message={"client_id": mqtt.client_id, "type": "relay", "status": status},
    )


def continuously_report_status():
    while connected is True:
        report_status()
        sleep(5)


mqtt.init(wlan_init=True, callback=on_message_received, topics=[command_topic])
connected = True
_thread.start_new_thread(continuously_report_status, ())

try:
    while True:
        mqtt.wait_msg()
finally:
    connected = False
    mqtt.__client.disconnect()
