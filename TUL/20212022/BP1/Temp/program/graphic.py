import tkinter as tk
import cmath
import sys
import logging
import math
import time
import tkinter
from tkinter import *
from matplotlib.figure import Figure
import matplotlib.gridspec as gridspec
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg 
from decimal import Decimal
import time
from colour import Color
from PIL import Image,ImageTk
import threading 


number_pamet=0
start_time_speed=0
sekunda_speed=0
class ini(tk.Frame):
    def __init__(self, parent, size=100, **options):
        tk.Frame.__init__(self, parent, padx=0, pady=0, borderwidth=0,highlightthickness=0,
                          **options)
        self.size = size
        
    def to_absolute(self, x, y):
        return x + self.size/2, y + self.size/2

def draw_dial(canv,x0,y0,degree, t,r):
    xr=x0
    yr=y0
    angle = math.radians(degree)
    cos_val = math.cos(angle)
    sin_val = math.sin(angle)
    dy=r*sin_val
    dx=r*cos_val
    dx2=t*sin_val
    dy2=t*cos_val
    mlx=xr+dx
    mly=yr-dy
    mrx=xr-dx
    mry=yr+dy
    px=xr+dx2
    py=yr+dy2
    xy = x0-r,y0-r,x0+1*r,y0+1*r
    xyz = mlx,mly,px,py,mrx,mry
    canv.delete('dial')
   
    canv.create_polygon(xyz, fill="white",tags=('dial'))
    
class predvidac():
    def predvidac(number, int,c,number_mem,start_cyklus,number_cyklus,kolik_bude_mezi,number_pamet,start_time_speed,sekunda_speed):              
    #------------Pro plynulý chod ručičky -(vstupní hodnota number_cyklus= number*2)---                   
            
            sekunda_cyklus=time.time() - start_cyklus
            
    #---------Provádí se sychnronizace každých 1 sekuny------------------------
            if sekunda_cyklus > 0.9:
                sekunda_cyklus=0
                start_cyklus = time.time()
                
            if sekunda_cyklus ==0:
                number_cyklus=int(number)
                
                
            if number_pamet != number_cyklus :
                    start_time_speed = time.time()
                         
                    kolik_bude_mezi=number_cyklus-c
                    number_pamet=number_cyklus
                    number_mem=c

            if c != number_pamet and sekunda_speed<2 and sekunda_speed >0:
                sekunda_speed=time.time() - start_time_speed
            else:
                sekunda_speed=1

            c=(kolik_bude_mezi/20)*(int(sekunda_speed*20))+number_mem
            
            if sekunda_speed>0.9:
                start_time_speed = kolik_bude_mezi
            
            if c<0 or c>300:
                c=number_pamet
                #print(c)
            number_pamet =budiky.runningTotal(number_cyklus)
            return c,number_mem,start_cyklus,number_cyklus,kolik_bude_mezi,number_pamet,start_time_speed,sekunda_speed
        
    def predvidac1(number, int,c,number_mem,start_cyklus,number_cyklus,kolik_bude_mezi,number_pamet,start_time_speed,sekunda_speed):              
    #------------Pro plynulý chod ručičky -(vstupní hodnota number_cyklus= number*2)---                   
            
            sekunda_cyklus=time.time() - start_cyklus
            
    #---------Provádí se sychnronizace každých 1 sekuny------------------------
            if sekunda_cyklus > 0.8:
                sekunda_cyklus=0
                start_cyklus = time.time()
                
            if sekunda_cyklus ==0:
                number_cyklus=int(number)
                
                
            if number_pamet != number_cyklus :
                    start_time_speed = time.time()
                         
                    kolik_bude_mezi=number_cyklus-c
                    number_pamet=number_cyklus
                    number_mem=c

            if c != number_pamet and sekunda_speed<2 and sekunda_speed >0:
                sekunda_speed=time.time() - start_time_speed
            else:
                sekunda_speed=1

            c=(kolik_bude_mezi/12)*(int(sekunda_speed*12))+number_mem
            
            if sekunda_speed>0.8:
                start_time_speed = kolik_bude_mezi
            
            if c<0 or c>300:
                c=number_pamet
                #print(c)
            number_pamet =budiky.runningTotal(number_cyklus)
            return c,number_mem,start_cyklus,number_cyklus,kolik_bude_mezi,number_pamet,start_time_speed,sekunda_speed
        
class budiky(ini):
    def __init__(self, parent,size: (float, int)=100,max_value: (float, int)=100,
                 min_value: (float, int)= 0,max_value_speed=100,min_value_speed=0,prvni_barevna_zmena_speed=70,
                 druha_barevna_zmena_speed=90,frekvence_speed=2,max_value_battery=100,min_value_battery=0,prvni_barevna_zmena_battery=30,
                 druha_barevna_zmena_battery=60,frekvence_battery=2,
                 max_value_vykon=0,min_value_vykon=0,frekvence_vykon=2,prvni_barevna_zmena_v=0,druha_barevna_zmena_v=100,
                 width=800,height=480,
    
                 
                 **options):
        super().__init__(parent, size=size, **options)
        self.width=width
        self.height=height
#----------SPEEDOMETER---------------        
        self.max_value_speed = max_value_speed
        self.min_value_speed = min_value_speed
        self.frekvence_speed=frekvence_speed
         #---Globální pro SPEEDOMETER(BAREVNÉ změny)
        self.active_col_1_speed=0
        self.active_col_2_speed=0
        self.konec_speed=0
        self.konec_1_speed=0
        self.konec1_1_speed=0
        self.konec1_2_speed=0
        self.xx_speed=0
        self.prvni_cyklus_speed=0
        self.count_col_speed=0
        self.barva_blur_speed=0


        self.prvni_barevna_zmena_speed=self.max_value_speed*self.frekvence_speed/100*prvni_barevna_zmena_speed
        self.druha_barevna_zmena_speed=self.max_value_speed*self.frekvence_speed/100*druha_barevna_zmena_speed
        

        
         #---Globální pro SPEEDOMETER(PŘEDVÍDAČ)
        self.c=0
        self.number_mem=0
        self.start_cyklus=0
        self.number_cyklus=0
        self.kolik_bude_mezi=0
        self.number_pamet=0
        self.start_time_speed=0
        self.sekunda_speed=0
        
        
        #---Globální pro Vykon(BAREVNÉ změny)
        self.active_col_1_v=0
        self.active_col_2_v=0
        self.konec_v=0
        self.konec_1_v=0
        self.konec1_1_v=0
        self.konec1_2_v=0
        self.xx_v=0
        self.prvni_cyklus_v=0
        self.count_col_v=0
        self.barva_blur_v=0
        
         #---Globální pro Vykon(PŘEDVÍDAČ)
        self.c_v=0
        self.number_mem_v=0
        self.start_cyklus_v=0
        self.number_cyklus_v=0
        self.kolik_bude_mezi_v=0
        self.number_pamet_v=0
        self.start_time_speed_v=0
        self.sekunda_speed_v=0
        
        
        
        
#----------BATERIE--------------------
        self.max_value_battery = max_value_battery
        self.min_value_battery = min_value_battery
        self.frekvence_battery=frekvence_battery
         #---Globální pro SPEEDOMETER(BAREVNÉ změny)
        self.active_col_1_battery=0
        self.active_col_2_battery=0
        self.konec_battery=0
        self.konec_1_battery=0
        self.konec1_1_battery=0
        self.konec1_2_battery=0
        self.xx_battery=0
        self.prvni_cyklus_battery=0
        self.count_col_battery=0
        self.barva_blur_battery=0
        #---Globální pro Baterie(PŘEDVÍDAČ)
        self.c1=0
        self.number_mem1=0
        self.start_cyklus1=0
        self.number_cyklus1=0
        self.kolik_bude_mezi1=0
        self.number_pamet1=0
        self.start_time_speed1=0
        self.sekunda_speed1=0
        
        self.prvni_barevna_zmena_battery=self.max_value_battery*self.frekvence_battery/100*prvni_barevna_zmena_battery
        self.druha_barevna_zmena_battery=self.max_value_battery*self.frekvence_battery/100*druha_barevna_zmena_battery

#----------VÝKON--------------------        
        self.max_value_vykon = max_value_vykon/10
        self.min_value_vykon = min_value_vykon/10
        self.frekvence_vykon=frekvence_vykon
        self.prvni_barevna_zmena_v=prvni_barevna_zmena_v
        self.druha_barevna_zmena_v=druha_barevna_zmena_v
        
        
        
        
        self.prvni_barevna_zmena_bat_temp=prvni_barevna_zmena_v
        self.druha_barevna_zmena_bat_temp=druha_barevna_zmena_v
        
         #---Globální pro BAT_Temp
        self.active_col_1_bat_temp=0
        self.active_col_2_bat_temp=0
        self.konec_bat_temp=0
        self.konec_1_bat_temp=0
        self.konec1_1_bat_temp=0
        self.konec1_2_bat_temp=0
        self.xx_bat_temp=0
        self.prvni_cyklus_bat_temp=0
        self.count_col_bat_temp=0
        self.barva_blur_bat_temp=0
        
        self.size = size
    #0e1218"

        self.canvas = tk.Canvas(self, width=self.width*(1), height=self.height*(1),highlightthickness=0,bg="#0e1218")
    
        self.canvas.grid(row=0)
        self.y=30
        self.draw_img()
        
    def draw_img(self):
        
        simul=(1,1)
        
        self.r_tick_oval = [None]*11;
        self.r_tick= [None]*11;
        
        self.bg = Image.open('icons/bg.png')
        self.bg_r=self.bg.resize((int(self.width*(545/800)), int(self.height*(356/480)))) #Celkova (vyska/sirka)*(sirka obrazku/celova sirka)
        self.image_bg = ImageTk.PhotoImage(self.bg_r)
        self.image_id_bg = self.canvas.create_image(self.width*(27/80),self.height*(((131+(self.y/2))/240)), image=self.image_bg)
        
        self.foreg = Image.open('icons/foreg.png')
        self.foreg_r=self.foreg.resize((int(self.width*(545/800)), int(self.height*(356/480))))
        self.image_foreg = ImageTk.PhotoImage(self.foreg_r)
        self.image_id_foreg =self.canvas.create_image(simul)
        
        self.panel_bg= Image.open('icons/panelv2_bg.png')
        self.panel_bg_r=self.panel_bg.resize((int(self.width*(175/800)), int(self.height*(211/480))))
        self.image_panel_bg = ImageTk.PhotoImage(self.panel_bg_r)
        self.image_id_panel_bg =self.canvas.create_image(simul)
        
        self.charging= Image.open('icons/charging.png')
        self.charging_r=self.charging.resize((int(self.width*(48/800)), int(self.height*(30/480))))
        self.image_charging = ImageTk.PhotoImage(self.charging_r)
        self.image_id_charging =self.canvas.create_image(simul)
        
        self.procento= Image.open('icons/procento.png')
        self.procento_r=self.procento.resize((int(self.width*(25/800)), int(self.height*(25/480))))
        self.image_procento = ImageTk.PhotoImage(self.procento_r)
        self.image_id_procento =self.canvas.create_image(simul)
        
        self.N = Image.open('icons/N.png')
        self.N_r=self.N.resize((int(self.width*(42/800)), int(self.height*(50/480))))
        self.image_N = ImageTk.PhotoImage(self.N_r)
        self.N_id = self.canvas.create_image(simul)
        
        self.D = Image.open('icons/D.png')
        self.D_r=self.D.resize((int(self.width*(42/800)), int(self.height*(50/480))))
        self.image_D = ImageTk.PhotoImage(self.D_r)
        self.D_id = self.canvas.create_image(simul)
        
        self.sipka = Image.open('icons/sipka.png')
        self.sipka_r=self.sipka.resize((int(self.width*(90/800)), int(self.height*(63/480))))
        self.image_sipka = ImageTk.PhotoImage(self.sipka_r)
        self.sipka_id = self.canvas.create_image(simul)
        
        self.nabidka = Image.open('icons/nabidka.png')
        self.nabidka_r=self.nabidka.resize((int(self.width*(800/800)), int(self.height*(480/480))))
        self.image_nabidka= ImageTk.PhotoImage(self.nabidka_r)
        self.nabidka_id = self.canvas.create_image(simul)
        
        blur_green = Image.open('icons/blurs/blurv1.png')
        blur_green_r=blur_green.resize((int(self.width*(251/800)), int(self.height*(293/480))))
        blur_orange = Image.open('icons/blurs/blurv2.png')
        blur_orange_r=blur_orange.resize((int(self.width*(251/800)), int(self.height*(293/480))))
        blur_red = Image.open('icons/blurs/blurv3.png')
        blur_red_r=blur_red.resize((int(self.width*(251/800)), int(self.height*(293/480))))
        self.blur=[blur_green_r,blur_orange_r,blur_red_r]
        self.image_id_blur = self.canvas.create_image(simul)
        
        self.panel = Image.open('icons/panelv2.png')
        self.panel_r=self.panel.resize((int(self.width*(149/800)), int(self.height*(162/480))))
        self.image_panel = ImageTk.PhotoImage(self.panel_r)
        self.image_id_panel = self.canvas.create_image(simul)
        
        #------Text Speedometer + oval
        self.readout_speedometer_text = self.canvas.create_text(simul)
        self.readout_kmph_text = self.canvas.create_text(simul)
        self.readout_lg_circle_anti = self.canvas.create_text(simul)
        self.readout_lg_circle = self.canvas.create_text(simul)
        self.readout_text_color_oval=self.canvas.create_text(simul)
        self.readout_text_color_oval_a=self.canvas.create_text(simul)
        
         #------Text kwbar + ukazatel
        self.readout_kw_number = self.canvas.create_text(simul)
        self.readout_kw_ukazatel = self.canvas.create_text(simul)
        self.readout_kw_text = self.canvas.create_text(simul)
        
        #------Text Baterie
        self.readout1 = self.canvas.create_text(simul)
        self.readout2 = self.canvas.create_text(simul)
        self.readout3 = self.canvas.create_text(simul)
        self.readout4 = self.canvas.create_text(simul)
        self.readout5 = self.canvas.create_text(simul)
        self.readout6 = self.canvas.create_text(simul)
        self.readout7 = self.canvas.create_text(simul)
        self.readout_bat_ukazatel = self.canvas.create_text(simul)
        self.color="#33c661"
        

    def set_value_budik(self, number, predvidacc, real_num: (float, int)):
        self.canvas.delete(self.readout_speedometer_text,self.readout_lg_circle,self.readout_kmph_text,
                           self.readout_lg_circle_anti,self.readout_text_color_oval,self.readout_text_color_oval_a,
                           self.image_id_blur,self.image_id_panel,self.D_id,self.N_id,self.image_id_panel_bg,self.sipka_id)
        real=number
        number=number/self.frekvence_speed
        self.c,self.number_mem,self.start_cyklus,self.number_cyklus,self.kolik_bude_mezi,self.number_pamet,self.start_time_speed,self.sekunda_speed=predvidac.predvidac1(real_num,int,self.c,self.number_mem,self.start_cyklus,self.number_cyklus,self.kolik_bude_mezi,self.number_pamet,self.start_time_speed,self.sekunda_speed)

        number=self.c*self.frekvence_speed
        pocatekbudikx=0+75
        pocatekbudiky=52
        
        #--- Barevné změny 
        col1 = Color("#33c661")
        col2 = Color("orange")
        col1_a = Color("#1a6532")
        col2_a = Color("#b35900")
        col3 = Color("#ff4d4d")
        col3_a = Color("#cc0000")

        self.count_col_speed,self.xx_speed,self.prvni_cyklus_speed,self.active_col_1_speed,self.konec_speed,self.konec_1_speed,self.active_col_2_speed,self.konec1_1_speed,self.konec1_2_speed,self.barva_blur_speed=barva.prvni_zmena(number,self.active_col_1_speed,self.konec_speed,self.konec_1_speed,self.active_col_2_speed,
                                                                                                                                                    self.xx_speed,self.prvni_cyklus_speed,self.prvni_barevna_zmena_speed,self.druha_barevna_zmena_speed,self.count_col_speed,self.konec1_1_speed,self.konec1_2_speed,self.barva_blur_speed)
        
        colors = list(col1.range_to(col2,int(10)))
        colors2 = list(col1_a.range_to(col2_a,int(10)))
        if self.konec_speed==0 and number > self.prvni_barevna_zmena_speed :
                 colors = list(col3.range_to(col2,int(10)))
                 colors2 = list(col3_a.range_to(col2_a,int(10)))
        self.image_blur = ImageTk.PhotoImage(self.blur[self.barva_blur_speed])
        
        number=number/self.frekvence_speed
#-------------------------------------------------------------------------------
#-----------------------------------DEGREE/number-------------------------------
#-------------------------------------------------------------------------------
        if predvidacc:
            degree_number=number  ##Predvidac
        else:
            degree_number=real/2     ##Real
        degree_number = degree_number if degree_number <= self.max_value_speed else self.max_value_speed
        degree_number = degree_number if degree_number > self.min_value_speed else self.min_value_speed
        

        degree = 30.0 + (degree_number- self.min_value_speed) / (self.max_value_speed - self.min_value_speed) * 300.0
                #self.face=self.face.resize((int(220), int(220)))

        self.image_id_panel_bg = self.canvas.create_image(self.width*(275/800), self.height*((262+self.y)/480), image=self.image_panel_bg)
        self.image_id_blur = self.canvas.create_image(self.width*(275/800),self.height*((262+self.y)/480), image=self.image_blur)
        

#         draw_dial(self.canvas,275,252,-1*degree,50,8)
        draw_dial(self.canvas,self.width*(275/800),self.height*((252+self.y)/480),-1*degree,self.width*1/6,self.width*1/100 )
        self.image_id_panel = self.canvas.create_image(self.width*(275/800),self.height*((250+self.y)/480), image=self.image_panel)
        
        
        self.readout_kmph_text=self.canvas.create_text(self.width*((275)/800),self.height*((275+self.y)/480), font=("Arial",int(self.width/30),'bold'),fill="green", text='km/h',angle=0) 
        
        self.readout_speedometer_text = self.canvas.create_text(self.width*(275/800),self.height*((222+self.y)/480), font=("Arial",int(self.width/20),'bold'),fill="white", text=str(int(real/2)),angle=0)
        number = number if number <= self.max_value_speed else self.max_value_speed
        number = number if number > self.min_value_speed else self.min_value_speed
        
        
        #--------------------oval u dialu ---------------------
        self.readout_lg_circle_anti = self.canvas.create_arc(self.width*(141/800), self.height*(118+self.y)/480, self.width*(408)/800, self.height*(385+self.y)/480,
                                width=self.width*(1/64),style="arc", start=-120, extent=-degree+30,
                                outline = "#009900")
 
        self.readout_lg_circle = self.canvas.create_arc(self.width*(141/800), self.height*(118+self.y)/480, self.width*(408)/800, self.height*(385+self.y)/480,
                                width=self.width*(3/200),style="arc", start=-120, extent=-degree+30,
                                outline = "light green")

        #--------------------oval co mění barvy + anti aliasing ---------------------
        self.readout_text_color_oval_a =self.canvas.create_oval(self.width*(125)/800, self.height*(102+self.y)/480, self.width*(425)/800, self.height*(402+self.y)/480,
                               width=self.width*(27/1600),
                               outline = colors2[self.count_col_speed])#colors2[self.count_col_speed]
       
        self.readout_text_color_oval = self.canvas.create_oval(self.width*(125)/800, self.height*(102+self.y)/480, self.width*(425)/800, self.height*(402+self.y)/480,
                               width=self.width*(13/800),
                               outline = colors[self.count_col_speed])#colors[self.count_col_speed]
        self.draw_tick()
        
    def razeni(self,zarazeno) :
        self.canvas.delete(self.D_id,self.N_id)
        if zarazeno==1:
            self.D_id = self.canvas.create_image(self.width*(275/800),self.height*((335+self.y)/480), image=self.image_D)
        else:
            self.N_id = self.canvas.create_image(self.width*(275/800),self.height*((335+self.y)/480), image=self.image_N)
    
    def sipka_vyberu(self,aktivace,vyber) :
        self.canvas.delete(self.sipka_id,self.nabidka_id)
        if aktivace==1:
            if vyber==0:
                ypsilon=0
            elif vyber==1 :
                ypsilon=80
            elif vyber==2:
                ypsilon=160
            elif vyber==3:
                ypsilon=240
            else:
                ypsilon=0
                self.canvas.delete(self.sipka_id,self.nabidka_id)

                
            self.nabidka_id = self.canvas.create_image(self.width*(395/800),self.height*((205+self.y)/470), image=self.image_nabidka)
            self.sipka_id = self.canvas.create_image(self.width*(500/800),self.height*((120+ypsilon+self.y)/470), image=self.image_sipka)
        else:
            self.canvas.delete(self.sipka_id,self.nabidka_id)
            
        if vyber==None:
            self.canvas.delete(self.sipka_id)
            
    def runningTotal(number):
        return number
       
    def set_value_vykon(self, number_c,Current,zaporne: (float, int)):
        self.canvas.delete(self.readout_kw_text,self.readout_kw_number,self.readout_kw_ukazatel,self.image_id_foreg)
        number_c=number_c/1000
        col2 = Color("#33c661")
        col1 = Color("#ff4d4d")
        col3 = Color("#ff4d4d")
        
        pocatekvykonx=-15
        pocatekvykony=0
        number=number_c
        #print(Current)
        if Current>0:
            numberre=0
        elif Current<=0:
            numberre=20
            

        self.count_col_bat_temp,self.xx_bat_temp,self.prvni_cyklus_bat_temp,self.active_col_1_bat_temp,self.konec_bat_temp,self.konec_1_bat_temp,self.active_col_2_bat_temp,self.konec1_1_bat_temp,self.konec1_2_bat_temp,self.barva_blur_bat_temp=barva.prvni_zmena(numberre,self.active_col_1_bat_temp,self.konec_bat_temp,self.konec_1_bat_temp,self.active_col_2_bat_temp,
                                                                                                                                                    self.xx_bat_temp,self.prvni_cyklus_bat_temp,self.prvni_barevna_zmena_bat_temp,self.druha_barevna_zmena_bat_temp,self.count_col_bat_temp,self.konec1_1_bat_temp,self.konec1_2_bat_temp,self.barva_blur_bat_temp)
        
        colors = list(col1.range_to(col2,int(10)))
        if self.konec_bat_temp==0 and number > self.prvni_barevna_zmena_bat_temp :
                 colors = list(col3.range_to(col2,int(10)))



        if number<0:
            number= int(number * 10**1)/ 10**1
            text=str("{0:.0f}".format(number*1000))
            text1="W"
            
        else:
            stav= int(number * 10**0)/ 10**0
            text=str("%.1f"%((number)))
            text1="kW"
            

        number = number if number <= self.max_value_vykon/100 else self.max_value_vykon/100
        number = number if number > self.min_value_vykon/100 else self.min_value_vykon/100
        #number_count=number/(15/305)

        self.c_v,self.number_mem_v,self.start_cyklus_v,self.number_cyklus_v,self.kolik_bude_mezi_v,self.number_pamet_v,self.start_time_speed_v,self.sekunda_speed_v=predvidac.predvidac(number*100,int,self.c_v,self.number_mem_v,self.start_cyklus_v,self.number_cyklus_v,self.kolik_bude_mezi_v,self.number_pamet_v,self.start_time_speed_v,self.sekunda_speed_v)




        #print(number, self.c_v)
        minimum=(math.sqrt(self.min_value_vykon*self.min_value_vykon))
        maximum=(math.sqrt(self.max_value_vykon*self.max_value_vykon))
                
        nula= (305/100)*(minimum/((maximum+minimum)/100))
        cislo=(305/100)*(self.c_v/((maximum+minimum)/100))
        
        self.readout_kw_ukazatel = self.canvas.create_rectangle(self.width*(pocatekvykonx+0)/800, self.height*(pocatekvykony+405-nula-cislo+self.y)/480, self.width*(pocatekvykonx+150)/800, self.height*(pocatekvykony+405+self.y)/480, fill=colors[self.count_col_bat_temp],width=0)
        self.image_id_foreg = self.canvas.create_image(self.width*(27/80),self.height*((131+(self.y/2))/240), image=self.image_foreg)       
                
        self.readout_kw_text = self.canvas.create_text(self.width*(400/6)/800,self.height*(400/7+self.y)/480, font=("Arial",int(self.width*(23)/800),'bold'),fill="#33c661", text=text1,angle=0)
        self.readout_kw_number = self.canvas.create_text(self.width*(400/6)/800,self.height*(400/15+self.y)/480, font=("Arial",int(self.width*(400/11)/800),'bold'),fill="white", text=str(text),angle=0)

    def set_value_baterie(self, number, charge,rychlost):

        #--- Barevné změny 
        col3 = Color("#33c661")
        col2 = Color("orange")
        col1 = Color("#ff4d4d")
        self.c1,self.number_mem1,self.start_cyklus1,self.number_cyklus1,self.kolik_bude_mezi1,self.number_pamet1,self.start_time_speed1,self.sekunda_speed1=predvidac.predvidac(number,int,self.c1,self.number_mem1,self.start_cyklus1,self.number_cyklus1,self.kolik_bude_mezi1,self.number_pamet1,self.start_time_speed1,self.sekunda_speed1)
        number=self.c1
        self.count_col_battery,self.xx_battery,self.prvni_cyklus_battery,self.active_col_1_battery,self.konec_battery,self.konec_1_battery,self.active_col_2_battery,self.konec1_1_battery,self.konec1_2_battery,self.barva_blur_battery=barva.prvni_zmena(number,self.active_col_1_battery,self.konec_battery,self.konec_1_battery,self.active_col_2_battery,
                                                                                                                                                    self.xx_battery,self.prvni_cyklus_battery,self.prvni_barevna_zmena_battery,self.druha_barevna_zmena_battery,self.count_col_battery,self.konec1_1_battery,self.konec1_2_battery,self.barva_blur_battery)
        
        colors = list(col1.range_to(col2,int(10)))
        if self.konec_battery==0 and number > self.prvni_barevna_zmena_battery :
                 colors = list(col3.range_to(col2,int(10)))

        pocatekbateriex=-15
        pocatekbateriey=0+self.y
        pocatekx=455
        pocateky=200+self.y
        posuny=35
        
        number=number/self.frekvence_battery
        number = number if number <= self.max_value_battery else self.max_value_battery
        number = number if number > self.min_value_battery else self.min_value_battery
        
        self.canvas.delete(self.readout_bat_ukazatel,self.image_id_charging,self.image_id_procento)
        self.canvas.delete(self.readout1,self.readout2,self.readout3,self.readout4,self.readout5,self.readout6,self.readout7)
        
        number_count=number/(100/305)
        
        self.readout_bat_ukazatel = self.canvas.create_rectangle(self.width*(420)/800, self.height*(405-number_count+self.y)/480, self.width*(542)/800, self.height*(405+self.y)/480, fill=colors[self.count_col_battery],width=0)

        if charge>0 and rychlost==0:#nabijeni
            self.image_id_charging = self.canvas.create_image(self.width*(512)/800,self.height*(262+(self.y))/480, image=self.image_charging)
            xx1=self.width*(pocatekx+53)/800
            yy1=self.height*(pocateky+28)/480
            xx2=self.width*(pocatekx+57)/800
            yy2=self.height*(pocateky+32)/480
            xx3=self.width*(pocatekx+55)/800
            yy3=self.height*(pocateky+30)/480
        else: #vybijeni
            self.image_id_procento = self.canvas.create_image(self.width*(512)/800,self.height*(262+(self.y))/480, image=self.image_procento)
            xx1=self.width*(pocatekx+53)/800
            yy1=self.height*(pocateky+28)/480
            xx2=self.width*(pocatekx+57)/800
            yy2=self.height*(pocateky+32)/480
            xx3=self.width*(pocatekx+55)/800
            yy3=self.height*(pocateky+30)/480
  
        self.readout1 =self.readout = self.canvas.create_text(xx1,yy1, font=("Arial",int(self.width*((25)/800)),'bold'),fill="#0e1218", text=str(int(number)),angle=0)
        self.readout2 =self.readout = self.canvas.create_text(xx1,yy2, font=("Arial",int(self.width*((25)/800)),'bold'),fill="#0e1218", text=str(int(number)),angle=0)
        self.readout3 =self.readout = self.canvas.create_text(xx2,yy1, font=("Arial",int(self.width*((25)/800)),'bold'),fill="#0e1218", text=str(int(number)),angle=0)
        self.readout4 =self.readout = self.canvas.create_text(xx2,yy2, font=("Arial",int(self.width*((25)/800)),'bold'),fill="#0e1218", text=str(int(number)),angle=0)
        self.readout5 =self.readout = self.canvas.create_text(xx1-0.5,yy3, font=("Arial",int(self.width*((25)/800)),'bold'),fill="#0e1218", text=str(int(number)),angle=0)
        self.readout6 =self.readout = self.canvas.create_text(xx2+0.5,yy3, font=("Arial",int(self.width*((25)/800)),'bold'),fill="#0e1218", text=str(int(number)),angle=0)
        self.readout7 =self.readout = self.canvas.create_text(xx3,yy3, font=("Arial",int(self.width*((25)/800)),'bold'),fill="white", text=str(int(number)),angle=0)
        
    def draw_tick(self,divisions=100):
        division=int(divisions/10)
        for delete in range(division+1):
            self.canvas.delete(self.r_tick_oval[delete],self.r_tick[delete])        
        size=self.size*self.width/800
        pocatekbudikx=0+75*self.width/800
        pocatekbudiky=(52+(self.y))*self.width/800
        inner_tick_radius = int((size-size/9) * 0.35)
        outer_tick_radius = int((size-size/9) * 0.45)
        inner_tick_radius2 = int((size-size/9) * 0.48)
        outer_tick_radius2 = int((size-size/9) * 0.50)
        inner_tick_radius3 = int((size-size/9) * 0.35)
        outer_tick_radius3 = int((size-size/9) * 0.40)
        
        for tick in range(divisions+1):
            angle_in_radians = (2.0 * cmath.pi / 3.0)+tick/divisions * (5.0 * cmath.pi / 3.0)
            inner_point = cmath.rect(inner_tick_radius, angle_in_radians)
            outer_point = cmath.rect(outer_tick_radius, angle_in_radians)
            
            if (tick%10) == 0:
                inner_point2 = cmath.rect(inner_tick_radius2-9*(self.width/800+0.2), angle_in_radians) #cmat.rect(zuzeni v uhlu, rotace uhlu)
                outer_point2 = cmath.rect(outer_tick_radius2-9*(self.width/800+0.2), angle_in_radians) #cmat.rect(zuzeni v uhlu, rotace uhlu)
                x= outer_point2.real + size/1.99
                y= outer_point2.imag + size/1.99
                ticker=tick
                if ticker > 0:
                    ticker=int(ticker/10)
                self.r_tick_oval[ticker] =self.canvas.create_oval(pocatekbudikx+x-5*self.width/800,pocatekbudiky+y-5*self.width/800,pocatekbudikx+x+3*self.width/800,pocatekbudiky+y+3*self.width/800,fill="white",)
            
            if (tick%10) == 0:
                inner_point2 = cmath.rect(inner_tick_radius2+7, angle_in_radians)
                outer_point2 = cmath.rect(outer_tick_radius2+7, angle_in_radians)
                x= outer_point2.real + size/2
                y= outer_point2.imag + size/2
                label = str(int(self.min_value_speed + tick * (self.max_value_speed-self.min_value_speed)/100))
                ticker1=tick
                if tick > 0:
                    ticker1=int(ticker1/10)
                self.r_tick[ticker1]=self.canvas.create_text(pocatekbudikx+x,pocatekbudiky+y, font=("Arial",int(size/25)),fill="white", text=str(label))
    
    
class kontrolky(ini):
    def __init__(self, parent,
                 max_value: (float, int)=100,
                 min_value: (float, int)= 0,
                 size: (float, int)=100,
                 img_data: str=None,
                 bg_col:str='blue',unit: str=None,bg_sel=1,hodnota=2,width=800,height=480,
                 
                 **options):
        super().__init__(parent, size=size, **options)

        self.max_value = float(max_value)
        self.min_value = float(min_value)
        self.size = size
        self.bg_col = bg_col
        self.bg_sel=bg_sel
        self.hodnota=hodnota
        self.height=height
        self.width=width

        self.canvas = tk.Canvas(self, width=int(self.width*(650)/800), height=int(self.height*(90)/480),bg="#0e1218",highlightthickness=0,)
        
        simul=(1,1)
        self.image_1 = Image.open('icons/blinkry/blink.png')
        self.image_1_r=self.image_1.resize((int(self.width*(66/800)), int(self.height*(64/480))))
        self.image = ImageTk.PhotoImage(self.image_1_r)
     
        self.image1_1 = Image.open('icons/blinkry/blink_off.png')
        self.image1_1_r=self.image1_1.resize((int(self.width*(66/800)), int(self.height*(64/480))))
        self.image1 = ImageTk.PhotoImage(self.image1_1_r)
        
        self.image2_1 = Image.open('icons/kontrolky/potkavaci_off.png')
        self.image2_1_r=self.image2_1.resize((int(self.width*(60/800)), int(self.height*(52/480))))
        self.image2 = ImageTk.PhotoImage(self.image2_1_r)
        
        self.image3_1 = Image.open('icons/kontrolky/potkavaci.png')
        self.image3_1_r=self.image3_1.resize((int(self.width*(60/800)), int(self.height*(52/480))))
        self.image3 = ImageTk.PhotoImage(self.image3_1_r)
        
        self.image4_1 = Image.open('icons/kontrolky/dalkova_off.png')
        self.image4_1_r=self.image4_1.resize((int(self.width*(62/800)), int(self.height*(52/480))))
        self.image4 = ImageTk.PhotoImage(self.image4_1_r)
        
        self.image5_1 = Image.open('icons/kontrolky/dalkova.png')
        self.image5_1_r=self.image5_1.resize((int(self.width*(62/800)), int(self.height*(52/480))))
        self.image5 = ImageTk.PhotoImage(self.image5_1_r)
        
        self.image6_1 = Image.open('icons/kontrolky/ABS_off.png')
        self.image6_1_r=self.image6_1.resize((int(self.width*(75/800)), int(self.height*(58/480))))
        self.image6 = ImageTk.PhotoImage(self.image6_1_r)
        
        self.image7_1 = Image.open('icons/kontrolky/ABS.png')
        self.image7_1_r=self.image7_1.resize((int(self.width*(75/800)), int(self.height*(58/480))))
        self.image7 = ImageTk.PhotoImage(self.image7_1_r)
        
        self.image8_1 = Image.open('icons/kontrolky/FAIL_off.png')
        self.image8_1_r=self.image8_1.resize((int(self.width*(80/800)), int(self.height*(60/480))))
        self.image8 = ImageTk.PhotoImage(self.image8_1_r)
        
        self.image9_1 = Image.open('icons/kontrolky/FAIL.png')
        self.image9_1_r=self.image9_1.resize((int(self.width*(80/800)), int(self.height*(60/480))))
        self.image9 = ImageTk.PhotoImage(self.image9_1_r)

        rotated_image = self.image1_1_r.rotate(180)
        self.image11 = ImageTk.PhotoImage(rotated_image)
       
        rotated_image1 = self.image_1_r.rotate(180)
        self.image12 = ImageTk.PhotoImage(rotated_image1)
        
        
        #Počáteční bar Kontrolky------
        self.pocatecnix=0
        self.pocatecniy=5
        
        #Potkávací světla X/Y---------
        self.potkavaci_x=0
        self.potkavaci_y=0
        
        #Dálková světla X/Y---------
        self.dalkova_x=0
        self.dalkova_y=0
        
        #ABS kontrolka X/Y---------
        self.ABS_x=0
        self.ABS_y=0
        
        #FAIL kontrolka X/Y---------
        self.FAIL_x=0
        self.FAIL_y=0
        
        #Levy blinkr kontrolka X/Y---------
        self.l_blink_x=0
        self.l_blink_y=0
        
        #Pravy blinkr kontrolka X/Y---------
        self.p_blink_x=20
        self.p_blink_y=0
        
        
        self.start_l_blink=time.time()
        self.start_p_blink=time.time()
        
        self.canvas.grid(row=0)

        self.background()
        
        self.image_id = self.canvas.create_image(simul)
        self.image_id1 = self.canvas.create_image(simul)
        self.image_id2 = self.canvas.create_image(simul)
        self.image_id3 = self.canvas.create_image(simul)
        self.image_id4 = self.canvas.create_image(simul)
        self.image_id5 = self.canvas.create_image(simul)
        
        self.readout_levy_blinker = self.canvas.create_text(simul)
        self.readout_pravy_blinker = self.canvas.create_text(simul)
        
        self.readout_den = self.canvas.create_text(simul)
        self.readout_cas = self.canvas.create_text(simul)

        

    
    def background(self):
        
        self.image_id = self.canvas.create_image(self.width*(370+self.potkavaci_x+self.pocatecnix)/800,self.height*(30+self.potkavaci_y+self.pocatecniy)/480, image=self.image2)
        self.image_id1 = self.canvas.create_image(self.width*(450+self.dalkova_x+self.pocatecnix)/800,self.height*(30+self.dalkova_y+self.pocatecniy)/480, image=self.image4)
        self.image_id2 = self.canvas.create_image(self.width*(530+self.ABS_x+self.pocatecnix)/800,self.height*(30+self.ABS_y+self.pocatecniy)/480, image=self.image6)
        self.image_id3 = self.canvas.create_image(self.width*(610+self.FAIL_x+self.pocatecnix)/800,self.height*(30+self.FAIL_y+self.pocatecniy)/480, image=self.image8)
        self.image_id4 = self.canvas.create_image(self.width*(25+self.l_blink_x+self.pocatecnix)/800,self.height*(30+self.l_blink_y+self.pocatecniy)/480, image=self.image1)
        self.image_id5 = self.canvas.create_image(self.width*(250+self.p_blink_x+self.pocatecnix)/800,self.height*(30+self.p_blink_y+self.pocatecniy)/480, image=self.image11)
    
    def set_lblink(self, stav: (float, int)):

        aktualni_stav,self.start_l_blink=blinkr.blink(stav,self.start_l_blink)
            
        self.canvas.delete(self.image_id)
        
        #if aktualni_stav == 1: ## Po přidání bude blikat pomocí funkce
        if stav == 1:
            self.image_id = self.canvas.create_image(self.width*(25+self.pocatecnix+self.l_blink_x)/800,self.height*(30+self.pocatecniy+self.l_blink_y)/480, image=self.image)
        else:
            self.canvas.delete(self.image_id)

    def set_pblink(self, stav: (float, int)):
    
        aktualni_stav,self.start_p_blink=blinkr.blink(stav,self.start_p_blink)
            
        self.canvas.delete(self.image_id5)
        
        #if aktualni_stav == 1:  ## Po přidání bude blikat pomocí funkce
        if stav == 1:
            self.image_id5 = self.canvas.create_image(self.width*(250+self.pocatecnix+self.p_blink_x)/800,self.height*(30+self.pocatecniy+self.p_blink_y)/480, image=self.image12)
        else:
            self.canvas.delete(self.image_id5)
            
    def time(self, time: (float, str)):
        
        self.canvas.delete(self.readout_cas)
        self.readout_cas = self.canvas.create_text(self.width*(400/2.7)/800,self.height*(400/12)/800, font=("Arial",int(self.width*(self.size/16)/800),'bold'),fill="white", text=time,angle=0)
        
    def den(self, den: (float, str)):
        
        self.canvas.delete(self.readout_den)
        self.readout_den = self.canvas.create_text(self.width*(400/2.7)/800,self.height*(400/9)/480, font=("Arial",int(self.width*(400/20)/800),'bold'),fill="white", text=den,angle=0) 
  
    def potkavaci(self, stav: (float, str)):

        if stav == 1:
            self.canvas.delete(self.image_id1)
            self.image_id1 = self.canvas.create_image(self.width*(370+self.potkavaci_x+self.pocatecnix)/800,self.height*(30+self.potkavaci_y+self.pocatecniy)/480, image=self.image3)  
        else:
            self.canvas.delete(self.image_id1)
                         
    def dalkova(self, stav: (float, str)):

        if stav == 1:
            self.canvas.delete(self.image_id2)
            self.image_id2 = self.canvas.create_image(self.width*(450+self.dalkova_x+self.pocatecnix)/800,self.height*(30+self.dalkova_y+self.pocatecniy)/480, image=self.image5)  
        else:
            self.canvas.delete(self.image_id2)
                
    def ABS(self, stav: (float, int)):

        if stav == 1:
            self.canvas.delete(self.image_id3)
            self.image_id3 = self.canvas.create_image(self.width*(530+self.ABS_x+self.pocatecnix)/800,self.height*(30+self.ABS_y+self.pocatecniy)/480, image=self.image7)
        else:
            self.canvas.delete(self.image_id3)
            
    def FAIL(self, stav: (float, int)):

        if stav == 1:
            self.canvas.delete(self.image_id4)
            self.image_id4 = self.canvas.create_image(self.width*(610+self.FAIL_x+self.pocatecnix)/800,self.height*(30+self.FAIL_y+self.pocatecniy)/480, image=self.image9)
        else:
            self.canvas.delete(self.image_id4)

class pozadi(ini):
    def __init__(self, parent,bg_col:str='blue',width=800,height=480,**options):
        super().__init__(parent, **options)

        self.bg_col = bg_col
        self.height=height
        self.width=width
        self.canvas = tk.Canvas(self, width=self.width*(290)/800, height=self.height*(380)/480,bg=bg_col,highlightthickness=0,)
        self.canvas.grid(row=0)

        self.image3_1 = Image.open('icons/shape.png')
        self.image3_1_r=self.image3_1.resize((int(self.width*(273/800)), int(self.height*(359/480))))
        self.image3 = ImageTk.PhotoImage(self.image3_1_r)
        self.background()
        
    def background(self):
        self.image_id3 = self.canvas.create_image(self.width*(120)/800,self.height*(190)/480, image=self.image3)

class battery_temperature(ini):
    def __init__(self, parent,max_value_bat_temp=100,min_value_bat_temp=0,
                 prvni_barevna_zmena_bat=0,druha_barevna_zmena_bat=0,bg_col:str='blue',width=800,height=480,
                 
                 **options):
        super().__init__(parent, **options)

        self.min_value_bat_temp=min_value_bat_temp
        self.bg_col = bg_col
        self.height=height
        self.width=width
        self.canvas = tk.Canvas(self, width=self.width*(200)/800, height=self.height*(73)/480,bg=bg_col,highlightthickness=0,)
    
        self.canvas.grid(row=0)
        
        self.max_value_bat_temp=max_value_bat_temp
        self.minimum_bat_temp=(math.sqrt(self.min_value_bat_temp*self.min_value_bat_temp))
        self.maximum_bat_temp=(math.sqrt(self.max_value_bat_temp*self.max_value_bat_temp))
        self.prvni_barevna_zmena_bat_temp=prvni_barevna_zmena_bat
        self.druha_barevna_zmena_bat_temp=druha_barevna_zmena_bat
        
         #---Globální pro BAT_Temp
        self.active_col_1_bat_temp=0
        self.active_col_2_bat_temp=0
        self.konec_bat_temp=0
        self.konec_1_bat_temp=0
        self.konec1_1_bat_temp=0
        self.konec1_2_bat_temp=0
        self.xx_bat_temp=0
        self.prvni_cyklus_bat_temp=0
        self.count_col_bat_temp=0
        self.barva_blur_bat_temp=0
        

        #---Globální pro BAT_temp(PŘEDVÍDAČ)
        self.c1=0
        self.number_mem1=0
        self.start_cyklus1=0
        self.number_cyklus1=0
        self.kolik_bude_mezi1=0
        self.number_pamet1=0
        self.start_time_speed1=0
        self.sekunda_speed1=0

        #--- Celkové X/Y všech objektů
        self.pocatecnix=-5
        self.pocatecniy=0

        simul=(1,1)
 
        self.image_1 = Image.open('icons/kontrolky/bat_temp.png')
        self.image_1_r=self.image_1.resize((int(self.width*(85/800)), int(self.height*(66/480))))
        self.image = ImageTk.PhotoImage(self.image_1_r)
        
        self.image_id = self.canvas.create_text(simul)
        self.bat_meter = self.canvas.create_text(simul)
        self.readout_bat_temp = self.canvas.create_text(simul)
        self.readout_bat_temp_jed = self.canvas.create_text(simul)
        
    def bat_temp(self, number: (float, int)):
        
        self.canvas.delete(self.image_id,self.readout_bat_temp,self.bat_meter,self.readout_bat_temp_jed)
        self.c1,self.number_mem1,self.start_cyklus1,self.number_cyklus1,self.kolik_bude_mezi1,self.number_pamet1,self.start_time_speed1,self.sekunda_speed1=predvidac.predvidac(number,float,self.c1,self.number_mem1,self.start_cyklus1,self.number_cyklus1,self.kolik_bude_mezi1,self.number_pamet1,self.start_time_speed1,self.sekunda_speed1)
        number=self.c1
        #--- Barevné změny 
        col1 = Color("#33c661")
        col2 = Color("orange")
        col3 = Color("#ff4d4d")
        
        #--- X/Y teploty baterie
        bat_tem_x=-50
        bat_tem_y=-13
        col=0
        
        number = number if number <= self.max_value_bat_temp else self.max_value_bat_temp
        number = number if number > self.min_value_bat_temp else self.min_value_bat_temp
        
        
        nula= (60/100)*(self.minimum_bat_temp/((self.maximum_bat_temp+self.minimum_bat_temp)/100))
        cislo=(60/100)*(number/((self.maximum_bat_temp+self.minimum_bat_temp)/100))

        self.count_col_bat_temp,self.xx_bat_temp,self.prvni_cyklus_bat_temp,self.active_col_1_bat_temp,self.konec_bat_temp,self.konec_1_bat_temp,self.active_col_2_bat_temp,self.konec1_1_bat_temp,self.konec1_2_bat_temp,self.barva_blur_bat_temp=barva.prvni_zmena(number,self.active_col_1_bat_temp,self.konec_bat_temp,self.konec_1_bat_temp,self.active_col_2_bat_temp,
                                                                                                                                                    self.xx_bat_temp,self.prvni_cyklus_bat_temp,self.prvni_barevna_zmena_bat_temp,self.druha_barevna_zmena_bat_temp,self.count_col_bat_temp,self.konec1_1_bat_temp,self.konec1_2_bat_temp,self.barva_blur_bat_temp)
        
        colors = list(col1.range_to(col2,int(10)))
        if self.konec_bat_temp==0 and number > self.prvni_barevna_zmena_bat_temp :
                 colors = list(col3.range_to(col2,int(10)))
                 
        self.bat_meter = self.canvas.create_rectangle(self.width*(80+bat_tem_x+self.pocatecnix)/800,
                                                      self.height*(80-nula-cislo+bat_tem_y+self.pocatecniy)/480,
                                                      self.width*(95+bat_tem_x+self.pocatecnix)/800,
                                                      self.height*(80+bat_tem_y+self.pocatecniy)/480,
                                                      fill=colors[self.count_col_bat_temp],width=0)
        self.image_id = self.canvas.create_image(self.width*(97+bat_tem_x+self.pocatecnix)/800,self.height*(50+bat_tem_y+self.pocatecniy)/480, image=self.image)
        #self.readout_bat_temp = self.canvas.create_text(self.width*(200+bat_tem_x+self.pocatecnix)/800,self.height*(55+bat_tem_y+self.pocatecniy)/480, font=("Arial",int(self.width*(20)/800),'bold'),fill="white", text=str("%.1f"%number) + "°C",angle=0)

        text_x=-39
        text_y=-9
        
        self.readout_bat_temp_jed =self.canvas.create_text(self.width*(175+self.pocatecnix+text_x)/800,self.height*(58+self.pocatecniy+text_y)/480, font=("Arial",int(self.width*(14)/800),'bold'),fill="white", text="°C",angle=0)
        self.readout_bat_temp = self.canvas.create_text(self.width*(175+self.pocatecnix+text_x)/800,self.height*(35+self.pocatecniy+text_y)/480, font=("Arial",int(self.width*(20)/800),'bold'),fill="white", text=str("%.1f"%number),angle=0)
        

class out_temperature(ini):
    def __init__(self, parent,
                 max_value_out_temp=100,min_value_out_temp=0,prvni_barevna_zmena_out=0,druha_barevna_zmena_out=0,
                 bg_col:str='blue',width=800,height=480,**options):
        
        super().__init__(parent, **options)

        self.min_value_out_temp=min_value_out_temp
        self.bg_col = bg_col
        self.height=height
        self.width=width
        self.canvas = tk.Canvas(self, width=self.width*(200)/800, height=self.height*(73)/480,bg=bg_col,highlightthickness=0,)
    
        self.canvas.grid(row=0)

        self.max_value_out_temp=max_value_out_temp
        self.minimum_out_temp=(math.sqrt(self.min_value_out_temp*self.min_value_out_temp))
        self.maximum_out_temp=(math.sqrt(self.max_value_out_temp*self.max_value_out_temp))
        self.prvni_barevna_zmena_out_temp=prvni_barevna_zmena_out
        self.druha_barevna_zmena_out_temp=druha_barevna_zmena_out

        #---Globální pro OUT_Temp
        self.active_col_1_out_temp=0
        self.active_col_2_out_temp=0
        self.konec_out_temp=0
        self.konec_1_out_temp=0
        self.konec1_1_out_temp=0
        self.konec1_2_out_temp=0
        self.xx_out_temp=0
        self.prvni_cyklus_out_temp=0
        self.count_col_out_temp=0
        self.barva_blur_out_temp=0
   
        #---Globální pro Out_temp(PŘEDVÍDAČ)
        self.c=0
        self.number_mem=0
        self.start_cyklus=0
        self.number_cyklus=0
        self.kolik_bude_mezi=0
        self.number_pamet=0
        self.start_time_speed=0
        self.sekunda_speed=0
        
        #--- Celkové X/Y všech objektů
        self.pocatecnix=-5
        self.pocatecniy=0

        simul=(1,1)

        self.image1_1 = Image.open('icons/kontrolky/out_temp.png')
        self.image1_1_r=self.image1_1.resize((int(self.width*(92/800)), int(self.height*(71/480))))
        self.image1 = ImageTk.PhotoImage(self.image1_1_r)
        
        self.image_id1 = self.canvas.create_text(simul)
        self.out_meter = self.canvas.create_text(simul)
        self.readout_out_temp = self.canvas.create_text(simul)
        self.readout_out_temp_jed=self.canvas.create_text(simul)
       
    def out_temp(self,number):
        self.canvas.delete(self.image_id1,self.readout_out_temp,self.out_meter,self.readout_out_temp_jed)
        
        #--- Barevné změny 
        col1 = Color("#3399ff")
        col2 = Color("#33c661")
        col3 = Color("orange")
        
        #--- X/Y venkovní teploty 
        out_tem_x=-46
        out_tem_y=-18
        
        col=0
        self.c,self.number_mem,self.start_cyklus,self.number_cyklus,self.kolik_bude_mezi,self.number_pamet,self.start_time_speed,self.sekunda_speed=predvidac.predvidac(number,float,self.c,self.number_mem,self.start_cyklus,self.number_cyklus,self.kolik_bude_mezi,self.number_pamet,self.start_time_speed,self.sekunda_speed)

        number=self.c
        
        number = number if number <= self.max_value_out_temp else self.max_value_out_temp
        number = number if number > self.min_value_out_temp else self.min_value_out_temp
        
        nula= (60/100)*(self.minimum_out_temp/((self.maximum_out_temp+self.minimum_out_temp)/100))
        cislo=(60/100)*(number/((self.maximum_out_temp+self.minimum_out_temp)/100))
        
        self.count_col_out_temp,self.xx_out_temp,self.prvni_cyklus_out_temp,self.active_col_1_out_temp,self.konec_out_temp,self.konec_1_out_temp,self.active_col_2_out_temp,self.konec1_1_out_temp,self.konec1_2_out_temp,self.barva_blur_out_temp=barva.prvni_zmena(number,self.active_col_1_out_temp,self.konec_out_temp,self.konec_1_out_temp,self.active_col_2_out_temp,
                                                                                                                                                    self.xx_out_temp,self.prvni_cyklus_out_temp,self.prvni_barevna_zmena_out_temp,self.druha_barevna_zmena_out_temp,self.count_col_out_temp,self.konec1_1_out_temp,self.konec1_2_out_temp,self.barva_blur_out_temp)
        
        colors = list(col1.range_to(col2,int(10)))
        if self.konec_out_temp==0 and number > self.prvni_barevna_zmena_out_temp :
                 colors = list(col3.range_to(col2,int(10)))
                 
        self.out_meter = self.canvas.create_rectangle(self.width*(80+out_tem_x+self.pocatecnix)/800, self.height*(88-cislo-nula+out_tem_y+self.pocatecniy)/480, self.width*(95+out_tem_x+self.pocatecnix)/800, self.height*(88+out_tem_y+self.pocatecniy)/480, fill=colors[self.count_col_out_temp],width=0)#colors[count_col]
        self.image_id1 = self.canvas.create_image(self.width*(98+self.pocatecnix+out_tem_x)/800,self.height*(55+out_tem_y+self.pocatecniy)/480, image=self.image1)
        
        text_x=-39
        text_y=-9
        
        self.readout_out_temp_jed =self.canvas.create_text(self.width*(175+self.pocatecnix+text_x)/800,self.height*(58+self.pocatecniy+text_y)/480, font=("Arial",int(self.width*(14)/800),'bold'),fill="white", text="°C",angle=0)
        self.readout_out_temp = self.canvas.create_text(self.width*(175+self.pocatecnix+text_x)/800,self.height*(35+self.pocatecniy+text_y)/480, font=("Arial",int(self.width*(20)/800),'bold'),fill="white", text=str("%.1f"%self.c),angle=0)
        
        #self.readout_out_temp = self.canvas.create_text(self.width*(195+out_tem_x+self.pocatecnix)/800,self.height*(55+out_tem_y+self.pocatecniy)/480, font=("Arial",int(self.width*(20)/800),'bold'),fill="white", text=str("%.1f"%self.c) + "°C",angle=0)
        
class dojezd(ini):
    def __init__(self, parent,max_value: (float, int)=100,min_value: (float, int)= 0,bg_col:str='blue',width=800,height=480,**options):
        super().__init__(parent, **options)

        self.max_value = float(max_value)
        self.min_value = float(min_value)

        self.bg_col = bg_col
        self.height=height
        self.width=width
        self.canvas = tk.Canvas(self, width=self.width*(200)/800, height=self.height*(73)/480,bg=bg_col,highlightthickness=0,)
        self.canvas.grid(row=0)
        
        #--- Celkové X/Y všech objektů
        self.pocatecnix=-5-50
        self.pocatecniy=-10-16

        simul=(1,1)

        self.image2_1 = Image.open('icons/kontrolky/round.png')
        self.image2_1_r=self.image2_1.resize((int(self.width*(60/800)), int(self.height*(60/480))))
        self.image2 = ImageTk.PhotoImage(self.image2_1_r)
        
        self.image_id2 = self.canvas.create_text(simul)
        self.readout_round =self.canvas.create_text(simul)
        self.readout_round_jed=self.canvas.create_text(simul)
       
    def rounded(self,number):
        self.canvas.delete(self.image_id2,self.readout_round,self.readout_round_jed)
        
        #--- X/Y round
        round_x=0
        round_y=8
        
        self.image_id2 = self.canvas.create_image(self.width*(88+self.pocatecnix+round_x)/800,self.height*(55+self.pocatecniy+round_y)/480, image=self.image2)
        #self.readout_round = self.canvas.create_text(self.width*(195+round_x+self.pocatecnix)/800,self.height*(55+round_y+self.pocatecniy)/480, font=("Arial",int(self.width*(20)/800),'bold'),fill="white", text=str(int(number)) + " km",angle=0)
        
        text_x=10
        text_y=17
        
        self.readout_round_jed =self.canvas.create_text(self.width*(175+self.pocatecnix+text_x)/800,self.height*(58+self.pocatecniy+text_y)/480, font=("Arial",int(self.width*(14)/800),'bold'),fill="white", text="km",angle=0)
        self.readout_round = self.canvas.create_text(self.width*(175+self.pocatecnix+text_x)/800,self.height*(35+self.pocatecniy+text_y)/480, font=("Arial",int(self.width*(20)/800),'bold'),fill="white", text=str(int(number)),angle=0)
        
        
        
class vzdalenost(ini):
    def __init__(self, parent,max_value: (float, int)=100,min_value: (float, int)= 0,bg_col:str='blue',width=800,height=480,**options):
        super().__init__(parent, **options)

        self.max_value = float(max_value)
        self.min_value = float(min_value)

        self.bg_col = bg_col
        self.height=height
        self.width=width
        self.canvas = tk.Canvas(self, width=self.width*(200)/800, height=self.height*(73)/480,bg=bg_col,highlightthickness=0,)
        self.canvas.grid(row=0)
        
        #--- Celkové X/Y všech objektů
        self.pocatecnix=-45
        self.pocatecniy=-7

        simul=(1,1)
        self.readout_distance_jed=self.canvas.create_text(simul)
        self.readout_distance = self.canvas.create_text(simul)
        self.readout_Trip = self.canvas.create_text(simul)
        
    def distance(self, stav: (float, int)):
        self.canvas.delete(self.readout_distance,self.readout_Trip,self.readout_distance_jed)
        
        text_x=0
        text_y=-2
        
        if stav<10:
            stav= int(stav * 10**1)/ 10**1
            text=str("{0:.1f}".format(stav))
            
        else:
            stav= int(stav * 10**0)/ 10**0
            text=str("%.0f"%(stav))
        self.readout_Trip =self.canvas.create_text(self.width*(125-90)/800,self.height*(37)/480, font=("Arial",int(self.width*(400/17)/800),'bold'),fill="#33c661", text='Trip',angle=0)
        

        
        self.readout_distance_jed =self.canvas.create_text(self.width*(175+self.pocatecnix+text_x)/800,self.height*(58+self.pocatecniy+text_y)/480, font=("Arial",int(self.width*(14)/800),'bold'),fill="white", text="km",angle=0)
        self.readout_distance = self.canvas.create_text(self.width*(175+self.pocatecnix+text_x)/800,self.height*(35+self.pocatecniy+text_y)/480, font=("Arial",int(self.width*(20)/800),'bold'),fill="white", text=text,angle=0)
        
class aktualni(ini):
    def __init__(self, parent,max_value: (float, int)=100,min_value: (float, int)= 0,bg_col:str='blue',width=800,height=480,**options):
        super().__init__(parent, **options)

        self.max_value = float(max_value)
        self.min_value = float(min_value)

        self.bg_col = bg_col
        self.height=height
        self.width=width
        self.canvas = tk.Canvas(self, width=self.width*(200)/800, height=self.height*(73)/480,bg=bg_col,highlightthickness=0,)
        self.canvas.grid(row=0)
        
        #--- Celkové X/Y všech objektů
        self.pocatecnix=-45
        self.pocatecniy=-7

        simul=(1,1)
        self.readout_odo_jed=simul=(1,1)
        self.readout_odo = self.canvas.create_text(simul)
        self.readout_odo_text = self.canvas.create_text(simul)

    def aktualni(self, stav: (float, int)):
        self.canvas.delete(self.readout_odo,self.readout_odo_text,self.readout_odo_jed)
        
        if stav<10:
            stav= int(stav * 10**1)/ 10**1
            text=str("{0:.1f}".format(stav))
            
        else:
            stav= int(stav * 10**0)/ 10**0
            text=str("%.0f"%(stav))
            
        text_x=0
        text_y=-2
        
        self.readout_odo_jed =self.canvas.create_text(self.width*(175+self.pocatecnix+text_x)/800,self.height*(58+self.pocatecniy+text_y)/480, font=("Arial",int(self.width*(14)/800),'bold'),fill="white", text="km",angle=0)
        self.readout_odo = self.canvas.create_text(self.width*(175+self.pocatecnix+text_x)/800,self.height*(35+self.pocatecniy+text_y)/480, font=("Arial",int(self.width*(20)/800),'bold'),fill="white", text=text,angle=0)
        
        self.readout_odo_text =self.canvas.create_text(self.width*(125-90)/800,self.height*(37)/480, font=("Arial",int(self.width*(400/17)/800),'bold'),fill="#33c661", text='Odo',angle=0)
        
        #self.readout_odo = self.canvas.create_text(self.width*(175+self.pocatecnix)/800,self.height*(55+self.pocatecniy)/480, font=("Arial",int(self.width*(20)/800),'bold'),fill="white", text=text + " km",angle=0)

class prumerna(ini):
    def __init__(self, parent,max_value: (float, int)=100,min_value: (float, int)= 0,bg_col:str='blue',width=800,height=480,**options):
        super().__init__(parent, **options)

        self.max_value = float(max_value)
        self.min_value = float(min_value)
    
        self.bg_col = bg_col
        self.height=height
        self.width=width
        self.canvas = tk.Canvas(self, width=self.width*(200)/800, height=self.height*(73)/480,bg=bg_col,highlightthickness=0,)
        self.canvas.grid(row=0)
        
        #--- Celkové X/Y všech objektů
        self.pocatecnix=-45
        self.pocatecniy=-5
        

        simul=(1,1)
        
        
        self.image1_1 = Image.open('icons/kontrolky/avg_spo.png')
        self.image1_1_r=self.image1_1.resize((int(self.width*(44/800)), int(self.height*(44/480))))
        self.image1 = ImageTk.PhotoImage(self.image1_1_r)
        self.image_id1 =simul
        self.readout_prum = self.canvas.create_text(simul)
        self.readout_prum_text = self.canvas.create_text(simul)

    def prumerna(self, stav: (float, int)):
        self.canvas.delete(self.readout_prum,self.readout_prum_text,self.image_id1)
        
        text_x=0
        text_y=-4
        
        stav= int(stav * 10**0)/ 10**0
        text=str("%.0f"%(stav))
        self.readout_prum_text =self.canvas.create_text(self.width*(175+self.pocatecnix+text_x)/800,self.height*(58+self.pocatecniy+text_y)/480, font=("Arial",int(self.width*(14)/800),'bold'),fill="white", text=" Wh/km",angle=0)
        self.readout_prum = self.canvas.create_text(self.width*(175+self.pocatecnix+text_x)/800,self.height*(35+self.pocatecniy+text_y)/480, font=("Arial",int(self.width*(20)/800),'bold'),fill="white", text=text,angle=0)
        self.image_id1 = self.canvas.create_image(self.width*(70+self.pocatecnix)/800,self.height*(42+self.pocatecniy)/480, image=self.image1)
        
class motor_temperature(ini):
    def __init__(self, parent,
                 max_value_out_temp=100,min_value_out_temp=0,prvni_barevna_zmena_out=0,druha_barevna_zmena_out=0,
                 bg_col:str='blue',width=800,height=480,**options):
        
        super().__init__(parent, **options)

        self.min_value_out_temp=min_value_out_temp
        self.bg_col = bg_col
        self.height=height
        self.width=width
        self.canvas = tk.Canvas(self, width=self.width*(200)/800, height=self.height*(73)/480,bg=bg_col,highlightthickness=0,)
    
        self.canvas.grid(row=0)

        self.max_value_out_temp=max_value_out_temp
        self.minimum_out_temp=(math.sqrt(self.min_value_out_temp*self.min_value_out_temp))
        self.maximum_out_temp=(math.sqrt(self.max_value_out_temp*self.max_value_out_temp))
        self.prvni_barevna_zmena_out_temp=prvni_barevna_zmena_out
        self.druha_barevna_zmena_out_temp=druha_barevna_zmena_out

        #---Globální pro motor_Temp
        self.active_col_1_out_temp=0
        self.active_col_2_out_temp=0
        self.konec_out_temp=0
        self.konec_1_out_temp=0
        self.konec1_1_out_temp=0
        self.konec1_2_out_temp=0
        self.xx_out_temp=0
        self.prvni_cyklus_out_temp=0
        self.count_col_out_temp=0
        self.barva_blur_out_temp=0
   
        #---Globální pro motor_temp(PŘEDVÍDAČ)
        self.c=0
        self.number_mem=0
        self.start_cyklus=0
        self.number_cyklus=0
        self.kolik_bude_mezi=0
        self.number_pamet=0
        self.start_time_speed=0
        self.sekunda_speed=0
        
        #--- Celkové X/Y všech objektů
        self.pocatecnix=-5
        self.pocatecniy=0

        simul=(1,1)

        self.image1_1 = Image.open('icons/kontrolky/motor_temp.png')
        self.image1_1_r=self.image1_1.resize((int(self.width*(70/800)), int(self.height*(70/480))))
        self.image1 = ImageTk.PhotoImage(self.image1_1_r)
        
        self.image_id1 = self.canvas.create_text(simul)
        self.out_meter = self.canvas.create_text(simul)
        self.readout_out_temp = self.canvas.create_text(simul)
        self.readout_out_temp_text= self.canvas.create_text(simul)
       
    def motor_temp(self,number):
        self.canvas.delete(self.image_id1,self.readout_out_temp,self.out_meter,self.readout_out_temp_text)
        
        #--- Barevné změny 
        col1 = Color("#3399ff")
        col2 = Color("#33c661")
        col3 = Color("orange")
        
        #--- X/Y venkovní teploty 
        out_tem_x=-46
        out_tem_y=-22
        
        text_x=-38
        text_y=-9
        
        col=0
        self.c,self.number_mem,self.start_cyklus,self.number_cyklus,self.kolik_bude_mezi,self.number_pamet,self.start_time_speed,self.sekunda_speed=predvidac.predvidac(number,float,self.c,self.number_mem,self.start_cyklus,self.number_cyklus,self.kolik_bude_mezi,self.number_pamet,self.start_time_speed,self.sekunda_speed)

        number=self.c
        
        number = number if number <= self.max_value_out_temp else self.max_value_out_temp
        number = number if number > self.min_value_out_temp else self.min_value_out_temp
        
        nula= (60/100)*(self.minimum_out_temp/((self.maximum_out_temp+self.minimum_out_temp)/100))
        cislo=(60/100)*(number/((self.maximum_out_temp+self.minimum_out_temp)/100))
        
        self.count_col_out_temp,self.xx_out_temp,self.prvni_cyklus_out_temp,self.active_col_1_out_temp,self.konec_out_temp,self.konec_1_out_temp,self.active_col_2_out_temp,self.konec1_1_out_temp,self.konec1_2_out_temp,self.barva_blur_out_temp=barva.prvni_zmena(number,self.active_col_1_out_temp,self.konec_out_temp,self.konec_1_out_temp,self.active_col_2_out_temp,
                                                                                                                                                    self.xx_out_temp,self.prvni_cyklus_out_temp,self.prvni_barevna_zmena_out_temp,self.druha_barevna_zmena_out_temp,self.count_col_out_temp,self.konec1_1_out_temp,self.konec1_2_out_temp,self.barva_blur_out_temp)
        
        colors = list(col1.range_to(col2,int(10)))
        if self.konec_out_temp==0 and number > self.prvni_barevna_zmena_out_temp :
                 colors = list(col3.range_to(col2,int(10)))
                 
        self.out_meter = self.canvas.create_rectangle(self.width*(80+out_tem_x+self.pocatecnix)/800, self.height*(88-cislo-nula+out_tem_y+self.pocatecniy)/480, self.width*(95+out_tem_x+self.pocatecnix)/800, self.height*(88+out_tem_y+self.pocatecniy)/480, fill=colors[self.count_col_out_temp],width=0)#colors[count_col]
        self.image_id1 = self.canvas.create_image(self.width*(87+self.pocatecnix+out_tem_x)/800,self.height*(58+out_tem_y+self.pocatecniy)/480, image=self.image1)
        #self.readout_out_temp = self.canvas.create_text(self.width*(195+out_tem_x+self.pocatecnix)/800,self.height*(55+out_tem_y+self.pocatecniy)/480, font=("Arial",int(self.width*(20)/800),'bold'),fill="white", text=str("%.1f"%self.c) + "°C",angle=0)
        self.readout_out_temp_text = self.canvas.create_text(self.width*(175+self.pocatecnix+text_x)/800,self.height*(58+self.pocatecniy+text_y)/480, font=("Arial",int(self.width*(14)/800),'bold'),fill="white", text="°C",angle=0)
        self.readout_out_temp = self.canvas.create_text(self.width*(175+self.pocatecnix+text_x)/800,self.height*(35+self.pocatecniy+text_y)/480, font=("Arial",int(self.width*(20)/800),'bold'),fill="white", text=str("%.1f"%self.c),angle=0)
        


class info_desk_graph(ini):
    def __init__(self, parent,
                 max_value: (float, int)=100,
                 min_value: (float, int)= 0,
                 size: (float, int)=100,
                 height =0,
                 width = 0,
                 rozsah_V=0,

                 **options):
        super().__init__(parent, size=size, **options)

        self.max_value = float(max_value)
        self.min_value = float(min_value)
        self.size = size
    
        self.height=height
        self.width=width
        
        fig = Figure(figsize=(3,1.3)) 
         
        self.ax = fig.add_subplot(1,1,1)
    
        
     
        self.graph = FigureCanvasTkAgg(fig, master=self)
        self.graph.get_tk_widget().pack(ipadx=10, ipady=40)
       
        print("jednou")
        self.ax.set_xticks(range(len(rozsah_V)))
        self.ax.tick_params(axis='y', which='major', labelsize=7)
        self.labels=[item.get_text() for item in self.ax.get_xticklabels()]
        if len(rozsah_V)>20:
            self.rotation_x=90
        else:
            self.rotation_x=0
        
        for i in range(len(rozsah_V)):
            self.labels[i]=str(i+1)
        self.lejbl=self.labels 
        self.ax.set_xticklabels(self.ax.get_xticks(),rotation=self.rotation_x, fontsize=6)
         
        self.ax.set_xticklabels(self.labels)
        self.ax.bar(range(len((rozsah_V))), rozsah_V, color='orange')
  
        
    def desk(self,Voltages):
        self.ax.cla()
        self.ax.set_title('Velikost napětí na článcích')
        #self.ax.set_title(self.ax.set_title,rotation=0, fontsize=12)
        self.ax.set_xticks(range(len(Voltages)))
        #self.ax.set_ylabel('Interest Rate')
        self.ax.set_xticklabels(self.ax.get_xticks(),rotation=self.rotation_x, fontsize=6)
         
        self.ax.set_xticklabels(self.lejbl)
        self.ax.bar(range(len((Voltages))), Voltages, color='orange')
        
        self.ax.grid(axis='y')
        #self.ax.bar.clear()
        dpts=(Voltages)
        #print("grafuju")
        self.ax.bar(range(len((Voltages))), dpts, color='orange')
        self.graph.draw()    
             
  
 



class info_desk(ini):
    def __init__(self, parent,
                 max_value: (float, int)=100,
                 min_value: (float, int)= 0,
                 size: (float, int)=100,
                 height =0,
                 width = 0,

                 **options):
        super().__init__(parent, size=size, **options)

        self.max_value = float(max_value)
        self.min_value = float(min_value)
        self.size = size
    
        self.height=height
        self.width=width
        
        
        
        
 
        
        self.canvas = tk.Canvas(self, width=self.width*(800)/800, height=self.height*(480)/480,bg="#9b9b9b",highlightthickness=0,)
        self.canvas.grid()
        
        simul=(1,1)
        self.readout_distance = self.canvas.create_text(simul)
        self.readout_bat_temp = self.canvas.create_text(simul)
        self.readout_out_temp = self.canvas.create_text(simul)
        
        self.image_1 = Image.open('icons/info_deska.png')
        self.image_1_r=self.image_1.resize((int(self.width*(800/800)), int(self.height*(480/480))))
        self.image = ImageTk.PhotoImage(self.image_1_r)
        self.image_id1=self.canvas.create_text(simul)
        

        self.image_id2=self.canvas.create_text(simul)
        
        self.image2_1 = Image.open('icons/temp_desk.png')
        self.image2_1_r=self.image2_1.resize((int(self.width*(336/800)), int(self.height*(128/480))))
        self.image2 = ImageTk.PhotoImage(self.image2_1_r)
        self.image_id3=self.canvas.create_text(simul)
        
        self.readout_out_temp=[0]*4
        self.readout_temp_ukazatel=self.canvas.create_text(simul)
        self.readout_out_volts=[0]*100
        self.readout_out_full_volts=self.canvas.create_text(simul)
        self.readout_out_current=self.canvas.create_text(simul)
        self.readout_out_vykon=self.canvas.create_text(simul)
        self.readout_out_cap=[0]*3
        self.readout_out_version=self.canvas.create_text(simul)
        self.readout_out_con=self.canvas.create_text(simul)
        self.readout_out_add=self.canvas.create_text(simul)
        self.readout_out_Cycle=[0]*3
        self.pie_chart=self.canvas.create_text(simul)
        self.pie_chart1=self.canvas.create_text(simul)
        self.pie_chart_a=self.canvas.create_text(simul)
        self.pie_chart1_a=self.canvas.create_text(simul)
        self.readout_out_proc=self.canvas.create_text(simul)
        self.readout_clanky=self.canvas.create_text(simul)
        self.readout_spotreba=self.canvas.create_text(simul)
        self.readout_strany=self.canvas.create_text(simul)
        
        self.animace=False
        self.mezi=[0]*50
        self.readout_out_max_volts =self.canvas.create_text(simul)
        self.readout_out_min_volts =self.canvas.create_text(simul)
        
    

    
    def desk(self,turn_on,TEMP,VOLTS,address,connect,Current,Version,residual_cap,nominal_cap,Cycle,SoC,odo,full_voltages,add):
        self.canvas.delete(self.image_id1,self.image_id2,self.pie_chart1_a,self.pie_chart_a,self.pie_chart1,self.pie_chart,
                           self.readout_out_add,self.readout_temp_ukazatel,self.image_id3,
                           self.readout_out_full_volts,self.readout_out_current,self.readout_out_vykon,self.readout_out_version,
                           self.readout_out_con,self.readout_spotreba,self.readout_clanky,self.readout_out_proc,self.readout_out_max_volts,
                           self.readout_out_min_volts,self.readout_strany,self.readout_out_full_volts)
        
        
        Current=math.sqrt(Current*Current)
        
        for i in range(len(VOLTS)):
            self.canvas.delete(self.readout_out_volts[i])
            if i<len(TEMP):
                self.canvas.delete(self.readout_out_temp[i])
            if i<3:
                self.canvas.delete(self.readout_out_cap[i])
                

        if turn_on==1:
            max_value=50
            min_value=-10
            for i in range(len(TEMP)):

                number= TEMP[i]
                number = number if number <= max_value else max_value
                number = number if number > min_value else min_value
                
                minimum=(math.sqrt(min_value*min_value))
                maximum=(math.sqrt(max_value*max_value))
                
                nula= (70/100)*(minimum/((maximum+minimum)/100))
                cislo=(70/100)*(number/((maximum+minimum)/100))
                self.readout_temp_ukazatel = self.canvas.create_rectangle(self.width*(125)/800+(i*62), self.height*(400-cislo-nula)/480+(i*0.1), self.width*(160)/800+(i*62), self.height*(400)/480, fill="orange",width=0)
                      
                    
            

            self.image_id1 = self.canvas.create_image(self.width*(400)/800,self.height*(240)/480, image=self.image)
            #self.image_id2 = self.canvas.create_image(self.width*(580)/800,self.height*(47)/480, image=self.image1)

            i=0
           
            if connect==1:
                text_connect="Připojeno"  
            elif connect==0:
                text_connect="Odpojeno"
            
            #self.readout_out_add = self.canvas.create_text(self.width*(680)/800,self.height*(40)/480+(1*25), font=("Arial",int(self.width*(12)/800),'bold'),fill="black", text=str(address) ,angle=0)
            #self.image_id3 = self.canvas.create_image(self.width*(212)/800,self.height*(357)/480, image=self.image2)
            for i in range(len(TEMP)):
                self.readout_out_temp[i] = self.canvas.create_text(self.width*(120+25)/800+(i*62),self.height*(420+25)/480, font=("Arial",int(self.width*(12)/800),'bold'),fill="black", text=str("%.1f"%TEMP[i]) +"°C" + "\n",angle=0)
            #lenn=(len(VOLTS))
            max_side= math.ceil(len(VOLTS)/8)
            
            
            prebytek=(len(VOLTS))
            while prebytek>8:
                prebytek=prebytek-8
            prebytek=8-prebytek
            
            slide=0+(8*add)
    
            if max_side==add+1:
                slider=slide+8-prebytek
                
            else:
                slider=slide+8

            for i in range(slide,slider):
                if(i<slide+(8*(add+1))):
                    posunx=10
                    posuny=0+((add)*(-152))
          
                if(i<9):
                    mezera="Cell {0:0=1} : ".format(i+1)
                else:
                    mezera="Cell {0:0=1}: ".format(i+1)
                
                if(len(VOLTS)!=0):
                    text_volts= mezera + str("%.3f"%VOLTS[i]) +" V" + "\n"
                else:
                    text_volts= ""
                max_val=max(VOLTS)
                min_val=min(VOLTS)
                volt_max_show=("{:.3f}".format(max_val))
                volt_min_show=("{:.3f}".format(min_val))
                text_max="Cell {0:0=1} : ".format(VOLTS.index(max_val)+1)+str(volt_max_show)+" V"
                text_min="Cell {0:0=1} : ".format(VOLTS.index(min_val)+1)+str(volt_min_show)+" V"
                
                
                self.readout_out_volts[i] = self.canvas.create_text(self.width*(100+posunx)/800,self.height*(100+posuny)/480+(i*19), font=("Arial",int(self.width*(11.5)/800),'bold'),fill="black", text=text_volts ,angle=0)
            Full_VOLTS=full_voltages
            self.readout_out_full_volts = self.canvas.create_text(self.width*(660)/800,self.height*(160+69)/480, font=("Arial",int(self.width*(13)/800),'bold'),fill="black", text= str("%.3f"%Full_VOLTS) +" V",angle=0)
            self.readout_out_max_volts = self.canvas.create_text(self.width*(660-5)/800,self.height*(105)/480, font=("Arial",int(self.width*(13)/800),'bold'),fill="black", text= text_max ,angle=0)
            self.readout_out_min_volts = self.canvas.create_text(self.width*(660-5)/800,self.height*(158)/480, font=("Arial",int(self.width*(13)/800),'bold'),fill="black", text= text_min,angle=0)
            
                   
            self.readout_out_current = self.canvas.create_text(self.width*(607)/800,self.height*(204+71)/480, font=("Arial",int(self.width*(13)/800),'bold'),fill="black", text= str("%.2f"%Current) +" A",angle=0)
            self.readout_out_vykon = self.canvas.create_text(self.width*(710-10)/800,self.height*(204+71)/480, font=("Arial",int(self.width*(13)/800),'bold'),fill="black", text= str("%.0f"%((Current*Full_VOLTS))) +" W",angle=0)
            
            MAX_CYCLE=600
            for i in range(3):
                if i ==0:
                    data=str("%.2f"%(residual_cap/100))+"Ah"
                    data1=str(int(Cycle))
                if i ==1:
                    data="z"
                    data1=data
                if i ==2:
                    data=str("%.2f"%(nominal_cap/100))+"Ah"
                    data1=str(int(MAX_CYCLE))
                self.readout_out_cap[i] = self.canvas.create_text(self.width*(695)/800,self.height*(335+(i*17)+25)/480, font=("Arial",int(self.width*(13)/800),'bold'),fill="black", text= data,angle=0)
                self.readout_out_Cycle[i]= self.canvas.create_text(self.width*(422)/800+150,self.height*(355+(i*18)+25)/480, font=("Arial",int(self.width*(13)/800),'bold'),fill="black", text=str(data1),angle=0)
            
            #self.readout_out_version = self.canvas.create_text(self.width*(135)/800,self.height*(65)/480, font=("Arial",int(self.width*(15)/800),'bold'),fill="black", text= str(Version)+" bit/s",angle=0)
            #self.readout_out_con= self.canvas.create_text(self.width*(685)/800,self.height*(40)/480, font=("Arial",int(self.width*(self.size/24)/800),'bold'),fill="black", text=text_connect,angle=0)
            self.readout_out_proc= self.canvas.create_text(self.width*(550)/800+150,self.height*(405+25)/480, font=("Arial",int(self.width*(self.size/24)/800),'bold'),fill="black", text=str(int(SoC))+" %",angle=0)
            self.readout_clanky= self.canvas.create_text(self.width*(-10)/800+150,self.height*(279)/480, font=("Arial",int(self.width*(13)/800),'bold'),fill="black", text=str(len(VOLTS)),angle=0)
            self.readout_strany= self.canvas.create_text(self.width*(-43)/800+150,self.height*(245)/480, font=("Arial",int(self.width*(12)/800),'bold'),fill="black", text="strana "+str(add+1)+" z "+str(max_side),angle=0)
            
            #self.readout_spotreba=self.canvas.create_text(self.width*(507)/800+150,self.height*(251)/480, font=("Arial",int(self.width*(13)/800),'bold'),fill="black", text=str("%.1f"%(odo))+" km",angle=0)
            
            x=100
            y=15
            zmensit=-205
            cislo=(360/MAX_CYCLE)*(Cycle)
            if Cycle!=0:
                extended=360-cislo-16
                start=cislo+8
            else:
                extended=359-cislo
                start=cislo
            
            self.pie_chart_a = self.canvas.create_arc(self.width*((141+x-zmensit)/800), self.height*(118+y-zmensit+25)/480, self.width*(408+x)/800, self.height*(385+y+25)/480,
                                width=self.width*((0.016925+0.007)),style="arc", start=start, extent=extended,
                                outline = "#71da92")
            self.pie_chart1_a = self.canvas.create_arc(self.width*((141+x-zmensit)/800), self.height*(118+y-zmensit+25)/480, self.width*(408+x)/800, self.height*(385+y+25)/480,
                                width=self.width*((0.016925+0.007)),style="arc", start=0, extent=cislo,
                                outline = "#ff8080") 
            self.pie_chart = self.canvas.create_arc(self.width*((141+x-zmensit)/800), self.height*(118+y-zmensit+25)/480, self.width*(408+x)/800, self.height*(385+y+25)/480,
                                width=self.width*(0.015625+0.007),style="arc", start=start, extent=extended,
                                outline = "#33c661")
            self.pie_chart1 = self.canvas.create_arc(self.width*((141+x-zmensit)/800), self.height*(118+y-zmensit+25)/480, self.width*(408+x)/800, self.height*(385+y+25)/480,
                                width=self.width*(0.015625+0.007),style="arc", start=0, extent=cislo,
                                outline ="#ff4d4d")   
 

class barva():
    def prvni_zmena(number,active_col_1,konec,konec_1,active_col_2,xx,prvni_cyklus,prvni_zmena_barvy,druha_zmena_barvy,count_col,konec1_1,konec1_2,barva_blur):
        
        
        if number>prvni_zmena_barvy and active_col_1==0 or konec ==1:
            sekunda=time.time()
            konec=1
            barva_blur=1
            if xx==0:
                prvni_cyklus=sekunda*20
                    
                xx=xx+1

            count_col=int(sekunda*20-prvni_cyklus)
            if count_col>=9:
                count_col=9
                xx=0
                active_col_1=1
                konec=0
                active_col_2=0
                barva_blur=1
            
        if number<prvni_zmena_barvy and active_col_1==1 or konec_1==1:
            sekunda=time.time()
            konec_1=1      
            if xx==0:
                prvni_cyklus=sekunda*20
                    
                xx=xx+1

            count_col=9-int(sekunda*20-prvni_cyklus)
            if count_col<=1:
                count_col=0
                xx=0
                active_col_1=0
                konec_1=0
                barva_blur=0
                 
        if number>druha_zmena_barvy and active_col_2==0 or konec1_1==1:
            sekunda=time.time()
            konec1_1=1      
            if xx==0:
                prvni_cyklus=sekunda*20
                    
                xx=xx+1

            count_col=9-int(sekunda*20-prvni_cyklus)
            
            if count_col<=1:
                count_col=0
                xx=0
                active_col_2=1
                barva_blur=2
                konec1_1=0
                
        if number<druha_zmena_barvy and active_col_2==1 and number>prvni_zmena_barvy or konec1_2==1:
            sekunda=time.time()
            konec1_2==1      
            if xx==0:
                prvni_cyklus=sekunda*20
                    
                xx=xx+1

            count_col=int(sekunda*20-prvni_cyklus)
            #print(str(count_col)+"="+str(sekunda*20)+"-"+str(prvni_cyklus))
            if count_col>=9:
                count_col=9
                xx=0
                active_col_2=0
                konec1_2==0
                barva_blur=1
        #print(barva_blur)   
        return count_col,xx,prvni_cyklus,active_col_1,konec,konec_1,active_col_2,konec1_1,konec1_2,barva_blur
                    
class blinkr():
    def blink(stav,start):
        if stav == 1:

            sekunda=time.time() - start

            if sekunda > 2:
                start = time.time()
            
            if sekunda < 1:
                aktualni_stav=1
                
            if sekunda > 1:
                aktualni_stav=0
               
        else:
            start = time.time()
            aktualni_stav=0
               
        return (aktualni_stav,start)
    


        
       

        
        
        

        
       
                

        
        