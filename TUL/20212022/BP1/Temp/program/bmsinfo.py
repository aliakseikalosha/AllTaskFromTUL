import gatt
from threading import Thread
import time

BT_address="A4:C1:38:A0:59:EB"

#TESLA
#nominal_cap=225 #Ah
#residual_cap=67.50 #Ah
#Current=-33.75
#Full_Voltages=400

num_cell=28
SoC=0
nominal_cap=0 #Ah
residual_cap=0 #Ah
Current=0
Temperatures=[0]*4
Temperatures[1]=0
Temperatures[2]=0
Temperatures[3]=0

Voltages=[0.000]*num_cell
Voltages[6]=0
Voltages[16]=0

Full_Voltages=0
Cycle=0
start=0
set_bite=False


class AnyDevice(gatt.Device):
    
    def __init__(self, mac_address, manager, auto_reconnect=True):
        super().__init__(mac_address=mac_address, manager=manager)
        self.auto_reconnect = auto_reconnect

    def connect(self):
        global start
        print("Connecting...")
        start=1
        super().connect()

    def disconnect_succeeded(self):
        super().disconnect_succeeded()
        print("[%s] Disconnected" % (self.mac_address))
        self.manager.stop()
        
    def connect_succeeded(self):
        global connect
        super().connect_succeeded()
        print("[%s] Připojeno" % (self.mac_address))
        connect=1

    def connect_failed(self, error):
        global start
        start=1
        super().connect_failed(error)
        print("[%s] Connection failed: %s" % (self.mac_address, str(error)))
        

    def disconnect_succeeded(self):
        global connect,start
        super().disconnect_succeeded()
        
        print("[%s] Disconnected" % (self.mac_address))
        connect=0
        start=0
        if self.auto_reconnect:
            self.connect()
 
          

    def services_resolved(self):
        super().services_resolved()
        
        device_information_service = next(
            s for s in self.services
            if s.uuid == '0000ff00-0000-1000-8000-00805f9b34fb')

        self.bms_read_characteristic = next(
            c for c in device_information_service.characteristics
            if c.uuid == '0000ff01-0000-1000-8000-00805f9b34fb')

        self.bms_write_characteristic = next(
            c for c in device_information_service.characteristics
            if c.uuid == '0000ff02-0000-1000-8000-00805f9b34fb')
    

        print("BMS found")
        self.bms_read_characteristic.enable_notifications()
        

    def characteristic_enable_notifications_succeeded(self, characteristic):
        super().characteristic_enable_notifications_succeeded(characteristic)
        self.response=bytearray()
        self.get_voltages=False
        self.bms_write_characteristic.write_value(bytes([0xDD,0xA5,0x03,0x00,0xFF,0xFD,0x77]));
            

    def characteristic_enable_notifications_failed(self, characteristic, error):
        super.characteristic_enable_notifications_failed(characteristic, error)
        print("BMS notification failed:",error)

    def characteristic_value_updated(self, characteristic, value):
        global num_cell,SoC,nominal_cap,residual_cap,Current,Temperatures,Voltages,Cycle,start,Full_Voltages,set_bite
        
        self.response+=value
        
        if (self.response.endswith(b'w')):
            self.response=self.response[4:]
            if (self.get_voltages):
                #print("VOLTS")
                Voltages=[0]*num_cell
                for i in range(num_cell):
                    Voltages[i]=int.from_bytes(self.response[i*2:i*2+2], byteorder = 'big')/1000

                #print("Výkon: ")
                #print(num_cell,residual_cap,nominal_cap,Cycle,SoC)
                    
                self.get_voltages=False
                self.response=bytearray()
                #time.sleep(1)
                self.bms_write_characteristic.write_value(bytes([0xDD,0xA5,0x03,0x00,0xFF,0xFD,0x77]))
                
       
                
            else:
                #print("TEMP")
        
                Full_Voltages=int.from_bytes(self.response[0:2], byteorder = 'big',signed=True)/100.0
                Current=int.from_bytes(self.response[2:4], byteorder = 'big',signed=True)/100.0
                residual_cap=int.from_bytes(self.response[4:6], byteorder = 'big')
                nominal_cap=int.from_bytes(self.response[6:8], byteorder = 'big')
                Cycle=int.from_bytes(self.response[8:10], byteorder = 'big')
                SoC=int.from_bytes(self.response[19:20], byteorder = 'big')
                num_cell=int.from_bytes(self.response[21:22], byteorder = 'big')
                num_temp=int.from_bytes(self.response[22:23], byteorder = 'big')
                Temperatures=[0]*num_temp
                for i in range(num_temp):
                        Temperatures[i]=(int.from_bytes(self.response[23+i*2:i*2+25],'big')-2731)/10
                
                #print(Current)
                #print("proud: "+str(Current)+ "  Teplota_1: "+str(Temperatures[0])+ "  Teplota_2: "+str(Temperatures[1])+ "  Teplota_3 :"+str(Temperatures[2])+ "  Teplota_4 :"+str(Temperatures[3]))

                #self.get_voltages=True
                self.response=bytearray()

                if set_bite==True:
                    time.sleep(0.1)
                    self.bms_write_characteristic.write_value(bytes([0xDD,0xA5,0x04,0x00,0xFF,0xFC,0x77]))
                    self.get_voltages=True
                    #print(self.get_voltages)
                    start=1
                else:

                    self.bms_write_characteristic.write_value(bytes([0xDD,0xA5,0x03,0x00,0xFF,0xFD,0x77]))
                    self.get_voltages=False
                    #print("get_vol")
                    start=1
                    
                    
    def characteristic_write_value_failed(self, characteristic, error):
        print("BMS write failed:",error)

    def get_vol(set_):
        global set_bite
        if set_==1:
            set_bite=True
        else:
            set_bite=False
    
class myClassA(Thread):
    def __init__(self):
        Thread.__init__(self)
        self.daemon = True
        self.start()
    def run(self):
        global BT_address
        manager = gatt.DeviceManager(adapter_name='hci0')
        device = AnyDevice(mac_address=BT_address, manager=manager)
        device.connect()
        manager.run()



