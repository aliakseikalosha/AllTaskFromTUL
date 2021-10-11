
#!/usr/bin/env python3
import sqlite3
import math
from multiprocessing import Process
from tkinter import *
import tkinter
import tkinter as tk
from tkinter.font import Font
from tkinter import messagebox
import time
import random
import graphic
import datetime
import matplotlib
import hall
import RPi.GPIO as GPIO
# import vláken----
from threading import Thread
import time
import bmsinfo
import gatt
from gpiozero import Button
import itertools
from w1thermsensor import W1ThermSensor
import database
import database_test
#import get_voltages

root = tk.Tk()
root.title("Palubní počítač - motorka")
nasobek_rozliseni=1
height=480*nasobek_rozliseni
width=800*nasobek_rozliseni
geometry = "%dx%d" % (width,height)
root.geometry(geometry)
root.configure(bg="black")
#root.attributes('-fullscreen', True)
predvidac=1
levy_blinkr=0
pravy_blinkr=0

ABS=0
FAIL=0
D=1
potkavacky=1
dalkova=0

start_cyklus=time.time()

out_temp=0
pocitadlo_prumer=0
Watts_mem=0
dojezd=0
rychlost_mem=0
write_speed=0
vzdalenost_mem=0
energie=0
spotreba_mem=0


start_spotreba = time.time()
start_btn=time.time()


klik=0
aktivace=0
cislo_menu=0
info_menu=0
add=0        

class button_detection():
    def info_slider(pin):
        global add
        if pin==up_button: #-----------------------------------------TLAČÍTKO DOLU
            add+=1
            
        elif pin==down_button:   #-----------------------------------------TLAČÍTKO NAHORU
            add-=1
        max_side=math.ceil(bmsinfo.num_cell/8)-1

        if add>max_side:
            add=0
        elif add<0:
            add=max_side
            
            
    def kontrola(cislo_menu):
        if cislo_menu>3:
            cislo_menu=0
        elif cislo_menu<0:
            cislo_menu=3
        return cislo_menu
            
    def button_handler(pin):
        global klik, start_btn, aktivace, info_menu, cislo_menu,start_timer
        if pin==set_button:  #-------------------------------------------TLAČÍTKO SET
            start_btn=time.time()
            klik=1
            
        elif pin==up_button: #-----------------------------------------TLAČÍTKO DOLU
            if pin==up_button:
                cislo_menu+=1
            if pin==down_button:
                cislo_menu-=1
            cislo_menu=button_detection.kontrola(cislo_menu)
            #print("up_button")
            
        elif pin==down_button:   #-----------------------------------------TLAČÍTKO NAHORU
            if pin==up_button:
                cislo_menu+=1
            if pin==down_button:
                cislo_menu-=1
            cislo_menu=button_detection.kontrola(cislo_menu)
            #print("down_button")
  
        elif pin==back_button:   #----------------------------------------TLAČÍTKO ZPET
            klik=0
            start_timer=time.time()

            if aktivace and not info_menu:
                info_menu=0
                aktivace=0
                cislo_menu=0
                GPIO.remove_event_detect(up_button)
                GPIO.remove_event_detect(down_button)
                GPIO.remove_event_detect(set_button)
                
                GPIO.add_event_detect(set_button, GPIO.FALLING,
                            callback=button_detection.button_handler,
                            bouncetime=400)
                
                p2.place(x=130*width/800,y=0)
                database.Databaze.zakoduj_zapis(nabidka)
                
            elif info_menu and not aktivace:
                bmsinfo.AnyDevice.get_vol(0)
                p7.place_forget()
                p12.place_forget()
                GPIO.remove_event_detect(up_button)
                GPIO.remove_event_detect(down_button)
                once=False
                info_menu=0
            
    def blinkr():
        global nacti_pro_dojezd
        if GPIO.input(l_blink_button):
            levy_blinkr=1
            nacti_pro_dojezd=1
        else:
            levy_blinkr=0
            nacti_pro_dojezd=0
        
        if GPIO.input(p_blink_button):
            pravy_blinkr=1
        else:
            pravy_blinkr=0
            
        return(pravy_blinkr,levy_blinkr)
    
    def transmission_f():
        if GPIO.input(transmission):
            D=1      
        else:
            D=0    
        return(D)
    
    def svetla():
        if GPIO.input(potkavaci):
            potkavacky=1
            
        else:
            potkavacky=0
            nacti_pro_dojezd=0
        if GPIO.input(dalkova):
            potkavacky=1 
            dalkovky=1
        else:
            dalkovky=0
        if GPIO.input(ABS):
            abs_=1 
        else:
            abs_=0
        if GPIO.input(FAIL):
            fail=1 
        else:
            fail=0
            
        return(potkavacky,dalkovky,abs_,fail)
        
        
    def set_action():
        global klik, info_menu, aktivace, cislo_menu
        if klik==1:
            done = time.time()
            elapse = done - start_btn
            if elapse>0.2 and GPIO.input(set_button) and not aktivace:
                #print("click", aktivace)
                
                p7.place(x=0*width/800,y=0*width/800)
                p12.place(x=198*width/800,y=78*width/800)
            
                GPIO.remove_event_detect(up_button)
                GPIO.remove_event_detect(down_button)
                GPIO.add_event_detect(up_button, GPIO.FALLING,
                                        callback=button_detection.info_slider,
                                        bouncetime=400)

                GPIO.add_event_detect(down_button, GPIO.FALLING,
                                        callback=button_detection.info_slider,
                                        bouncetime=400)                
                
                bmsinfo.AnyDevice.get_vol(1)
                info_menu=1
                klik=0
                
            elif not GPIO.input(set_button) and elapse>2 and not info_menu:
                #print("drzeni", aktivace)
                aktivace=1
                cislo_menu=0
                klik=0
                GPIO.remove_event_detect(up_button)
                GPIO.remove_event_detect(down_button)
                GPIO.remove_event_detect(set_button)
           
                GPIO.add_event_detect(up_button, GPIO.FALLING,
                                        callback=button_detection.button_handler,
                                        bouncetime=400)

                GPIO.add_event_detect(down_button, GPIO.FALLING,
                                        callback=button_detection.button_handler,
                                        bouncetime=400)
                
                GPIO.add_event_detect(set_button,GPIO.FALLING,callback=lambda x: button_callback2(set_button,cislo_menu))
                p2.place_forget()
            #print(info_menu,elapse,aktivace)

set_button =27
up_button=18
down_button=22
back_button=17
l_blink_button=23
p_blink_button=24
potkavaci=5
dalkova=6
ABS=13
FAIL=19
transmission=25
GPIO.setmode(GPIO.BCM)

GPIO.setup(set_button, GPIO.IN, pull_up_down = GPIO.PUD_UP)
GPIO.setup(back_button, GPIO.IN, pull_up_down = GPIO.PUD_UP)
GPIO.setup(up_button, GPIO.IN, pull_up_down = GPIO.PUD_UP)
GPIO.setup(down_button, GPIO.IN, pull_up_down = GPIO.PUD_UP)
GPIO.setup(l_blink_button, GPIO.IN, pull_up_down = GPIO.PUD_DOWN)
GPIO.setup(p_blink_button, GPIO.IN, pull_up_down = GPIO.PUD_DOWN)
GPIO.setup(potkavaci, GPIO.IN, pull_up_down = GPIO.PUD_DOWN)
GPIO.setup(dalkova, GPIO.IN, pull_up_down = GPIO.PUD_DOWN)
GPIO.setup(ABS, GPIO.IN, pull_up_down = GPIO.PUD_DOWN)
GPIO.setup(FAIL, GPIO.IN, pull_up_down = GPIO.PUD_DOWN)
GPIO.setup(transmission, GPIO.IN, pull_up_down = GPIO.PUD_DOWN)


# GPIO.RISING, GPIO.FALLING, GPIO.
GPIO.add_event_detect(set_button, GPIO.FALLING,
                        callback=button_detection.button_handler,
                        bouncetime=400)

GPIO.add_event_detect(back_button, GPIO.FALLING,
                        callback=button_detection.button_handler,
                        bouncetime=400)


start_menu1=0
def button_callback2(number,cislo_menu):
    global start_menu1,nabidka
    done = time.time()
    elapsed = done - start_menu1
    click=0
    pocet=0
    if elapsed>0.4:
            
            nabidka0=nabidka[0]
            nabidka1=nabidka[1]
            nabidka2=nabidka[2]
            nabidka3=nabidka[3]

            nepouzity=[None]*(len(nabidka0)-len(nabidka))

            for i in range(4):
                if cislo_menu==i:
                    nabidka_vyber=nabidka[i]
                    ypsilon=10+(90*(i+1))
                    
            for i in range(7):
                if nabidka0[i]==1 or nabidka1[i]==1 or nabidka2[i]==1 or nabidka3[i]==1:
                    if nabidka_vyber[i]==1:
                        pouzity=i
                else:
                    nepouzity[pocet]=i
                    pocet+=1
            max_range=len(nepouzity)-1
            #print(pouzity)
            #print(nepouzity)
            
            if(pouzity>nepouzity[0] and pouzity>nepouzity[1] and not pouzity>nepouzity[2]):
                click=1
            elif(pouzity>nepouzity[0] and not pouzity>nepouzity[1] and not pouzity>nepouzity[2]):
                click=0 
            elif(pouzity>nepouzity[0] and pouzity>nepouzity[1] and pouzity>nepouzity[2]):
                click=2
            elif( not pouzity>nepouzity[0] and not pouzity>nepouzity[1] and not pouzity>nepouzity[2]):
                click=2
                    
            if click>max_range:
                click=0
            pocet=0
            #print("Clikc: ",click)
            zapomen=pouzity
            nabidka_vyber[nepouzity[click]]=1
            nabidka_vyber[pouzity]=0
            oba(nepouzity[click],ypsilon,zapomen)

    start_menu1 = done


class out_temperature(Thread):
    def __init__(self):
        Thread.__init__(self)
        self.daemon = True
        self.start()
    def run(self):
        global out_temp
        while True:
            time.sleep(1)
            sensor = W1ThermSensor()
            out_temp = sensor.get_temperature()
            
            
          
def started():
    once=False
    while bmsinfo.start==0:
        if once==False:
            print("blt loading..")
            once=True


odo,trip,spotreba,counter_1=database.Databaze_odo.nacti_trip()
celkova_prumerna=spotreba
bmsinfo.myClassA()
started()
out_temperature()
hall.speed_simulator()
#graph()     
def load():
    global trip,odo,prumerna,celkova_prumerna,index,spotreba,prumerna_sp
    odo,trip,spotreba,index=database.Databaze_odo.nacti_trip()
    prumerna_sp=spotreba
    
  
once=False
def nuluj():
    global once,trip,hall_old,nacti_pro_dojezd,prumerna_sp,prumerna,counter_1,index,spotreba_new,vzdalenost_mem
    if once==False:
        odo,trip,prumerna,index=database.Databaze_odo.nacti_trip()
        database.Databaze_odo.zapis_data((odo),(0),0,0)
        load()
        hall.distance=0
        hall_old=0
        trip=0
        prumerna_sp=0
        prumerna=0
        counter_1=0
        index=0
        spotreba_new=0
        vzdalenost_mem=0
        nacti_pro_dojezd=1
        
        once=True

            
counterr=0
dist_mem=0
slide=0
speed_prumer=0
speed_counter=0
#Voltages=0
counter_1=0
prumerna_spotreba=0
prumerna=0

#---------ukoly-----------
#--grafika dalsich polozek

#--vyberove menu hned naskoci zmena po aktivaci

counteer=0
prumer=0
prumerna=0
def spotrebaa(Watts):
    global pocitadlo_prumer, Watts_mem, start_spotreba,vzdalenost_mem,start_spotreba,counter_1,prumerna_spotreba,prumerna,prumer,energie,scitanec
    #Průměrování výkonu
    pocitadlo_prumer+=1
    Watts=Watts+Watts_mem
    Watts_mem=Watts
    Watts=(Watts/pocitadlo_prumer)
    elapse1 = time.time() - start_spotreba
    vzdalenost=hall.get_distance()
    
#Aktuální spotřeba počítaná každou vteřinu
    if elapse1>1 and hall.rychlost>0:
        
        spotrebazas=Watts/60/60 #Wh/s
        vzdalenost=vzdalenost-vzdalenost_mem
        try:
            #print("try")
            energie=(spotrebazas)/(vzdalenost) #Wh/km
            counter_1+=1
            #print("spotrebazas",spotrebazas,"/vzdalenost",vzdalenost,"= energie",energie)
            scitanec=energie
            prumerna=energie+prumerna_spotreba
            prumerna_spotreba=prumerna
            prumerna=prumerna/counter_1
            prumer=prumerna
            
            #print("aktualni sectena: ",prumerna_spotreba, counter_1, scitanec)
             
        except:
            #print("except")
            energie=0
        #print(counter_1)
        vzdalenost_mem+=vzdalenost
        Watts_mem=0
        pocitadlo_prumer=0
        start_spotreba = time.time()
    elif(hall.rychlost<=0):
        scitanec=None
        #print("None",Watts)
        
    #if hall.rychlost==0:
    #    energie=0
        
hallak=0
hall_old=0
nacti_pro_dojezd=0
databaze_true=False
start_reload=time.time()
start_reload_graph=time.time()
start_reload_test=time.time()
turn_off=False
counter_test=0
start_timer =0
scitanec=0
def read_every_second():
    
    global spotreba_new,turn_off,start_reload_graph,start_spotreba,start_reload,start_cyklus,databaze_true,spotreba,prumerna_spotreba,prumer,hall_old,hallak,nacti_pro_dojezd,prumerna_sp
    global rychlost_mem,Watts_mem,vzdalenost_mem,write_speed,pocitadlo_prumer,counterr, dojezd, energie, prumerna,celkova_prumerna,Current
    global dist_mem, slide, speed_prumer,speed_counter,once,odo,trip,Voltages,counteer,add,counter_1,prumerna_spotreba,counter_1,counter_11
    global start_reload_test,counter_test,energie,start_timer
    
    
    done = time.time()
    elapsee = done - start_reload
    if elapsee>10:
        nacti_pro_dojezd=1
        start_reload=time.time()
        #print("timer 10")
    else:
        nacti_pro_dojezd=0
    #print(elapsee)

    if hall.rychlost<1 and databaze_true==False or nacti_pro_dojezd==1:
        
        load()
        spotreba_old=spotreba
        spotreba_new=prumerna
        index_old=index
        index_new=index+counter_1
        rozdil_index=counter_1
        #print(prumerna,spotreba_old,index_old,spotreba_new,rozdil_index,index_new)
        if index_new!=0:
            prumerna=((spotreba_old*index_old)+(spotreba_new*rozdil_index))/index_new
            
            
            prumerna_sp=prumerna
        
        if index_new==0:
            prumerna=0

        database.Databaze_odo.zapis_data((odo+hallak),(trip+hallak),prumerna,counter_1+index)
        #print((odo+hallak),(trip+hallak),prumerna,counter_1+index)
        load()
        if index_new==0:
            prumerna=0
            prumerna_sp=0
            
        #print(prumerna_sp)
        prumerna_spotreba=0
        counter_1=0
        hall_old=hall.vzdalenost
        databaze_true=True
        

    elif hall.rychlost>1:
        databaze_true=False
    hallak=hall.vzdalenost-hall_old
     
    button_detection.set_action()
    levy_blinkr,pravy_blinkr=button_detection.blinkr()
    potkavaci,dalkova,ABS,FAIL=button_detection.svetla()
    D=button_detection.transmission_f()
    
    if not GPIO.input(back_button):
        done = time.time()
        timer = done - start_timer      
        if timer>2:
            nuluj()
    else:
        once=False
    
    Motor_temp=0
    num_cell=bmsinfo.num_cell
    SoC=bmsinfo.SoC
    nominal_cap=bmsinfo.nominal_cap
    residual_cap=bmsinfo.residual_cap
    Current=bmsinfo.Current
    
    Current_plus=math.sqrt(Current*Current)
    Temperatures=bmsinfo.Temperatures
    
    Bat_temp=[0]*(len(Temperatures)-1)
    for i in range(len(Temperatures)-1):
        Bat_temp[i]=Temperatures[i]
        Motor_temp=Temperatures[len(Temperatures)-1]
        
    #print(Bat_temp,Motor_temp)   
    Voltages=bmsinfo.Voltages
    Cycle=bmsinfo.Cycle
    full_voltages=bmsinfo.Full_Voltages
    
    if Current<0:
        Watts=Current_plus*full_voltages
        vykon=Watts
    else:
        Watts=0
        vykon=Watts

    spotrebaa(Watts)


 #-----------------HALL limit-------------------------------   
    if hall.rychlost!=rychlost_mem or hall.rychlost==0 and write_speed>0:
        speed_prumer+=hall.rychlost
        speed_counter+=1
        
        if hall.time_dif<0.06:  
            pocet=5
        elif hall.time_dif<0.1:
            pocet=4
        else:
            pocet=2
 
        if speed_counter>=pocet:
            write_speed=(speed_prumer/speed_counter)
            speed_counter=0
            speed_prumer=0
            
    rychlost_mem=hall.rychlost

#------------------------------------------
    
    nominal_cap_kWh=(nominal_cap*full_voltages)/1000  #nominální kapacita v kWh
    residual_cap_kWh=(residual_cap*full_voltages)/1000 #-spotreba_mem #aktuální kapacita v kWh

    try:
        dojezd=residual_cap_kWh/(prumerna_sp/1000)
    except:
        dojezd=0

    
    #print(full_voltages,Current_plus,residual_cap_kWh,prumerna_sp/1000)
    done_graph = time.time()
    elapse_graph = done_graph - start_reload_graph
    if elapse_graph>3 and info_menu==1:
        turn_off=True
        p12.desk(Voltages)
        #Voltages[5]=Voltages[5]+0.0005
        start_reload_graph=time.time()
        #print("deska reload")
    
    if info_menu==1 and turn_off==False:
        p12.desk(Voltages)
        #print("deska off")
    #-----------INFODESKA

    
    x1 = datetime.datetime.now()
    cas=str(x1.strftime("%H:%M:%S"))
    den=str(x1.strftime("%d.%b"))
    
    done_test = time.time()
    elapse_test = done_test - start_reload_test
    try:
        if elapse_test>1:
            database_test.Databaze_testing.data_write(cas,scitanec,prumerna_sp,residual_cap,dojezd,hall.vzdalenost,hall.rpm,hall.rychlost_f,counter_test)
            counter_test+=1
            start_reload_test=time.time()
    except:
        print("")
       
        
    root.after(80, read_every_second)
    
    p1.set_value_baterie(SoC,Current,write_speed)
    
    #p1.set_value_vykon(Current_plus*full_voltages,Current)
    p1.set_value_vykon(Current_plus*full_voltages,Current,Current*full_voltages)
    p1.set_value_budik(write_speed*2,predvidac,hall.rychlost)
    p1.razeni(D)
    p1.sipka_vyberu(aktivace,cislo_menu)
    p2.set_lblink(levy_blinkr)
    p2.set_pblink(pravy_blinkr)
    p2.time(cas)
    p2.den(den)    
    p2.potkavaci(potkavaci)
    p2.dalkova(dalkova)
    p2.ABS(ABS)
    p2.FAIL(FAIL)
    
    p3.bat_temp(max(Bat_temp))
    p4.out_temp(out_temp)
    p5.rounded(dojezd)
    #p5.rounded(22)
    p6.distance(trip+hallak)
    #p6.distance(22)
    p9.aktualni(odo+hallak)
    #p9.aktualni(22)
    p11.motor_temp(Motor_temp)
    try:
        p10.prumerna(prumerna_sp)
        #p10.prumerna(22)
    except:
        p10.prumerna(0)
    
    p7.desk(info_menu,Temperatures,Voltages,
            bmsinfo.BT_address,1,Current,
            0,residual_cap,
            nominal_cap,Cycle,SoC,odo,full_voltages,add)

    
    
   
bg_color="#0e1218"

root.configure(bg=bg_color)
    
view=graphic
p1 = view.budiky(root,size=400,# Velikost
    max_value=100,# maximální hodnota
    min_value=0,# minimální hodnota
                 
    #-------SPEEDOMETER-------------- 
    max_value_speed=100,# maximální hodnota
    min_value_speed=0,# minimální hodnota
    prvni_barevna_zmena_speed=60, #1.Barevná změna SPEEDOMETRU (v procentech % z max_value_speed)
    druha_barevna_zmena_speed=80, #2.Barevná změna SPEEDOMETRU (v procentech % z max_value_speed)
    frekvence_speed=2,#násobek maximální hodnoty (zvýší se počet dílků od max-min a bude plynulejší průběh)
    #-------BATERIE--------------                                            
    max_value_battery=100,
    min_value_battery=0,
    prvni_barevna_zmena_battery=30, #1.Barevná změna BATERIE (v procentech % z max_value_battery)
    druha_barevna_zmena_battery=50, #2.Barevná změna BATERIE (v procentech % z max_value_battery)
    frekvence_battery=1,
                 
    max_value_vykon=12000, # W
    min_value_vykon=0,
    frekvence_vykon=2,
    prvni_barevna_zmena_v=10,
    druha_barevna_zmena_v=10000000000,
    width=width,
    height=height)
   
p1.place(x=0,y=0*width/800)

p2 = view.kontrolky(root,max_value=1,min_value=0,size=400,
    #bg_col='white',
    bg_col="#0e1218", 
    unit = "",
    bg_sel = 2,
    width=width,
    height=height)

p2.place(x=130*width/800,y=0)

p8 = view.pozadi(root,bg_col="#0e1218", width=width,height=height) 
p8.place(x=540*width/800,y=80*width/800)

p3 = view.battery_temperature(root,
    #-------Kontrolka teploty baterie-------------- 
    max_value_bat_temp=100,
    min_value_bat_temp=-10,
    prvni_barevna_zmena_bat=40, #1.Barevná změna kontrolky teploty baterie na danou barvu (na kolikatem stupni se hodnota zmeni)
    druha_barevna_zmena_bat=80, #2.Barevná změna kontrolky teploty baterie na danou barvu (na kolikatem stupni se hodnota zmeni)                    
    bg_col="#0e1218", width=width,height=height)

p4 = view.out_temperature(root,                 
    #-------Kontrolka venkovní teploty--------------                                            
    max_value_out_temp=50,
    min_value_out_temp=-20,                       
    prvni_barevna_zmena_out=0, #1.Barevná změna kontrolky venkovní teploty (v procentech % z max_value_bat_temp)
    druha_barevna_zmena_out=40, #2.Barevná změna kontrolky venkovní teploty (v procentech % z max_value_bat_temp)
    bg_col="#0e1218", width=width,height=height)
   
p5 = view.dojezd(root,max_value=1,min_value=0,bg_col="#0e1218",width=width,height=height)
#bg_col='#0e1218'
        
p6 = view.vzdalenost(root,max_value=1,min_value=0,bg_col="#0e1218",width=width,height=height)
#bg_col='blue'
        
p9 = view.aktualni(root,max_value=1,min_value=0,bg_col="#0e1218",width=width,height=height)
#bg_col='blue'

p10 = view.prumerna(root,max_value=1,min_value=0,bg_col="#0e1218",width=width,height=height)
#bg_col='blue'

p11 = view.motor_temperature(root,                 
    #-------Kontrolka venkovní teploty--------------                                            
    max_value_out_temp=88,
    min_value_out_temp=-20,                       
    prvni_barevna_zmena_out=80, #1.Barevná změna kontrolky venkovní teploty (v procentech % z max_value_bat_temp)
    druha_barevna_zmena_out=40, #2.Barevná změna kontrolky venkovní teploty (v procentech % z max_value_bat_temp)
    bg_col="#0e1218", width=width,height=height)
#bg_col='blue'




#-----------INFODESKA---------------------
p7 = view.info_desk(root,max_value=1,min_value=0,size=400,width=width,height=height)
p12 = view.info_desk_graph(root,max_value=1,min_value=0,size=400,width=width,rozsah_V=bmsinfo.Voltages,height=height)

nabidka=database.Databaze.nacti_z_db()

def oba(i,ypsilon,pouzivalo_se):
    global width

    if i ==0:
        p3.place(x=595*width/800,y=ypsilon*width/800)
    if i ==1:
        p4.place(x=595*width/800,y=ypsilon*width/800)
    if i ==2:
        p5.place(x=595*width/800,y=ypsilon*width/800)
    if i ==3:
        p6.place(x=595*width/800,y=ypsilon*width/800)
    if i ==4:
        p9.place(x=595*width/800,y=ypsilon*width/800)
    if i ==5:
        p10.place(x=595*width/800,y=ypsilon*width/800)
    if i ==6:
        p11.place(x=595*width/800,y=ypsilon*width/800)
        
    if pouzivalo_se ==0:
        p3.place_forget()
    if pouzivalo_se ==1:
        p4.place_forget()
    if pouzivalo_se ==2:
        p5.place_forget()
    if pouzivalo_se ==3:
        p6.place_forget()
    if pouzivalo_se ==4:
        p9.place_forget()
    if pouzivalo_se ==5:
        p10.place_forget()
    if pouzivalo_se ==6:
        p11.place_forget()

for l in range(len(nabidka)):
    ypsilon=13+(88.5*(l+1))
    if l==0:
        ypsilon=ypsilon-1
    if l==1:
        ypsilon=ypsilon
    if l==2:
        ypsilon=ypsilon
    if l==3:
        ypsilon=ypsilon
    for i in range(len(nabidka[l])):
        radek=nabidka[l]
        if radek[i]==1:
            oba(i,ypsilon,10)


   
  
read_every_second()
 
mainloop()
