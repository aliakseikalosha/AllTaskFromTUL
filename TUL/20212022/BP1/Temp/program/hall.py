
import RPi.GPIO as GPIO
import time
import json
from threading import Thread
import math
state=0
GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)
vzdalenost=0
rychlost=0
pulse = 0
distance = 0
rpm = 0.00
speed = 0.00
wheel_c = (189)/100
#wheel_c = (189)/100    motorka set
multiplier = 0
speed_mem=0
elapse_mem=0
hall = 14
elapse = 0.00
addme = 0
start = time.time()

GPIO.setup(hall, GPIO.IN, pull_up_down = GPIO.PUD_UP)


def get_rpm():
    
    return rpm

def get_speed():
    return speed

def get_distance():
   
    return distance

def get_elapse():
    return elapse

def get_multiplier():
    return multiplier

KO=True
def get_pulse(number):
    global elapse,distance,start,pulse,speed,rpm,multiplier,elapse_mem,vzdalenost,rychlost,state,speed_mem,elapse,KO,old_speed
    
    #if old_speed<10:
    #    KO=False    
    
    
    if KO==True:
        cycle = 0
        pulse+=1
        cycle+=1
        if pulse > 0:
            elapse = time.time() - start
            pulse -=1
        if cycle > 0:
            distance += wheel_c/1000
            cycle -= 1

        multiplier = 3600/elapse
        speed = (wheel_c*multiplier)/1000       
        rpm = 1/elapse *60
        #print(elapse)
        start = time.time()
        #print(speed-old_speed)
        #if old_speed<10 or speed-old_speed<0:
            
        KO=False
    else:
        KO=True
        
    
old_speed=0   
class speed_simulator(Thread):
    def __init__(self):
        Thread.__init__(self)
        self.daemon = True
        self.start()
        
    def run(self):
        global speedKmh, distance,state, rychlost, vzdalenost,speed_mem,elapse,time_dif,rychlost_f,rpm,old_speed
        while True:
            time.sleep(0.001)

            time_dif=elapse
            #print(get_rpm())
            vzdalenost=get_distance() #metry
            
            #print(vzdalenost)
            if ((time.time()-start)>2):
                rychlost=0
                rychlost_f=0
                rpm=0
                
            else:
                rychlost=int(get_speed()) #Kmh
                rychlost_f=get_speed()
                rpm=get_speed()
            
            old_speed=rychlost
            
            
            
        
GPIO.add_event_detect(hall,GPIO.RISING,callback = get_pulse,bouncetime=1)
# motorka bounce time= 1
#speed_simulator()