#service: python_script.set_light_on
#target:
#   light: light.0x04cd15fffe8f1be8
#   color:  [255, 0, 0]
#   brightness: 128


l=data.get("light")
b=data.get("brightness")
c=data.get("color")
if b is not None and l is not None and c is not None:
    service_data = {"entity_id": l, "rgb_color": c, "brightness": b}
    hass.services.call("light", "turn_on", service_data, False)