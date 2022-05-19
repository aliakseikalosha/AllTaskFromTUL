# tepLED.py
#service: python_script.templed
#target:
#  entity_id: light.0x04cd15fffe8f1be8
#data:
#  b: 128
#  rgb_color:
#    - 255
#    - 0
#    - 0
entity_id = data.get("entity_id")
rgb_color = data.get("rgb_color", [255, 255, 255])
b = data.get("b")
if entity_id is not None:
    logger.info(b)
    service_data = {"entity_id": entity_id, "rgb_color": rgb_color, "brightness": b}
    hass.services.call("light", "turn_on", service_data, False)
