#show_phone_battery.py
#service: python_script.show_phone_battery
#target:
#  light: light.0x04cd15fffe8f1be8
#  battery_level: sensor.sony_xz3_battery_level


b=data.get("battery_level")
l=data.get("light")
if b is not None and l is not None:
    b=int(str(hass.states.get(b)).split("=")[1].split(";")[0])/100.0
    rgb_color = [0,255,0]
    if b > 50:
        rgb_color = [int(128*(1-b)),int(255*b),0]
    else:
        rgb_color = [int(255*(1-b)),int(128*b),0]
    service_data = {"entity_id": l, "rgb_color": rgb_color, "brightness": 128}
    hass.services.call("light", "turn_on", service_data, False)
