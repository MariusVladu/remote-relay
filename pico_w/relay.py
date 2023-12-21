from machine import Pin


class Relay:
    def __init__(self, gpio_pin):
        self.pin = Pin(gpio_pin, Pin.OUT)
        self.switch_off()

    def switch_on(self):
        self.pin.value(0)
        self.state = "on"

    def switch_off(self):
        self.pin.value(1)
        self.state = "off"

    def toggle(self):
        if self.state == "off":
            self.switch_on()
        else:
            self.switch_off()


relays: list[Relay] = []


def init():
    if len(relays) > 0:
        raise Exception("Relays already initialized")

    relays.append(Relay(gpio_pin=0))
    relays.append(Relay(gpio_pin=1))
    relays.append(Relay(gpio_pin=2))
    relays.append(Relay(gpio_pin=3))
    relays.append(Relay(gpio_pin=4))
    relays.append(Relay(gpio_pin=5))
    relays.append(Relay(gpio_pin=6))
    relays.append(Relay(gpio_pin=7))


def reset_all():
    for relay in relays:
        relay.switch_off()


def get_status():
    return [relay.state for relay in relays]
