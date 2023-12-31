import json
import mqtt_client_setup as mqtt
import _thread
import relay
from time import sleep

connected = False
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

    if command == "reset_all":
        print("Resetting all relays")
        relay.reset_all()
    elif command == "on":
        k = int(message["k"])
        print(f"Turning on relay {k}")
        relay.relays[k].switch_on()
    elif command == "off":
        k = int(message["k"])
        print(f"Turning on relay {k}")
        relay.relays[k].switch_off()
    else:
        print(f"Unknown command {command}")
        return
    report_status()


def report_status():
    mqtt.publish(
        topic="status",
        message={
            "client_id": mqtt.client_id,
            "type": "relay",
            "status": relay.get_status(),
        },
    )


def continuously_report_status():
    print("Started reporting status")
    while connected is True:
        report_status()
        sleep(5)
    print("Stopped reporting status")


relay.init()

while True:
    mqtt.init(wlan_init=True, callback=on_message_received, topics=[command_topic])
    connected = True
    _thread.start_new_thread(continuously_report_status, ())

    try:
        while True:
            mqtt.wait_msg()
    except Exception as e:
        print(f"Exception ocurred: {e}")
        connected = False
        sleep(6)
