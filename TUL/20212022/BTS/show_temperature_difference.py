#show_temperature_difference.py
#service: python_script.show_temperature_difference
#target:
#  light: light.0x04cd15fffe8f1be8
#  temp: sensor.0x00158d0007e5033d_temperature
#  outside:sensor.openweathermap_forecast_temperature
l=data.get("light")
t=data.get("temp")
o=data.get("outside")

b_down = -10
b_up = 3
r_up = 10
r_down = -3

def clamp01(x):
    return min(1, max(0,x))

if l is not None:
    t=float(str(hass.states.get(t)).split("=")[1].split(";")[0])
    o=float(str(hass.states.get(o)).split("=")[1].split(";")[0])
    diff = (o - t)
    rgb = [255, 0, 255]
    
    if diff > r_up:
        rgb = [255,0,0]
    elif diff < b_down:
        rgb = [0,0,255]
    else:
        if diff>r_down:
            rgb[0] = int(255*clamp01((diff - r_down)/(r_up-r_down)))
        if diff<b_up:
            rgb[2] = abs(int(255*clamp01((diff - b_up)/(b_down-b_up))))
    
    logger.error("inside:"+str(t))
    logger.error("outside:"+str(o))
    logger.error(diff)
    logger.error(rgb)
    service_data = {"entity_id": l, "rgb_color": rgb, "brightness": 128}
    hass.services.call("light", "turn_on", service_data, False)
