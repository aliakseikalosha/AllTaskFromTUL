import sqlite3

class Databaze_odo():
    def nacti_trip():
        global trip,odo,spotreba,index
        conn = sqlite3.connect("nabidka.db")
        c= conn.cursor()
        
        c.execute("SELECT * FROM odo_trip")
        datas=c.fetchone()
        
        odo=datas[0]
        trip=datas[1]
        spotreba=datas[2]
        index=datas[3]
        
        return(odo,trip,spotreba,index)
        conn.commit()
        conn.close()

    def zapis_data(odo,trip,spotreba,index):
 
        conn = sqlite3.connect("nabidka.db")
        c= conn.cursor()

        c.execute("""UPDATE odo_trip SET trip=? WHERE rowid=?""",(trip,1))
        c.execute("""UPDATE odo_trip SET odometr=? WHERE rowid=?""",(odo,1))
        c.execute("""UPDATE odo_trip SET prumerna_spotreba=? WHERE rowid=?""",(spotreba,1))
        c.execute("""UPDATE odo_trip SET prumerna_index=? WHERE rowid=?""",(index,1))
        
        conn.commit()
        conn.close()
        
class Databaze():

    def nacti_z_db():
        conn = sqlite3.connect("nabidka.db")
        c= conn.cursor()
        
        c.execute("SELECT * FROM parametry")

        radek0=c.fetchone()
        radek1=c.fetchone()
        radek2=c.fetchone()
        radek3=c.fetchone()

        nabidka0=[0]*7
        nabidka1=[0]*7
        nabidka2=[0]*7
        nabidka3=[0]*7

        for item in range(7):
                nabidka0[item]=radek0[item]
        for item in range(7):
                nabidka1[item]=radek1[item]
        for item in range(7):
                nabidka2[item]=radek2[item]
        for item in range(7):
                nabidka3[item]=radek3[item]
                
        nabidka=[nabidka0,nabidka1,nabidka2,nabidka3]      
        return(nabidka)
        conn.commit()
        conn.close()
        
    def zakoduj_zapis(nabidka):
        
        radek=[0]*len(nabidka)
        bat_tempe=[0]*len(nabidka)
        out_tempe=[0]*len(nabidka)
        rounded=[0]*len(nabidka)
        trip=[0]*len(nabidka)
        aktualni=[0]*len(nabidka)
        prumerna=[0]*len(nabidka)
        motor_temp=[0]*len(nabidka)

        for l in range(len(nabidka)):
            radek=nabidka[l]
            bat_tempe[l]=radek[0]
            out_tempe[l]=radek[1]
            rounded[l]=radek[2]
            trip[l]=radek[3]
            aktualni[l]=radek[4]
            prumerna[l]=radek[5]
            motor_temp[l]=radek[6]

        Databaze.zapis_do_db(bat_tempe,out_tempe,rounded,trip,aktualni,prumerna,motor_temp)
        
    def zapis_do_db(bat_tempe,out_tempe,rounded,trip,aktualni,prumerna,motor_temp):  
        conn = sqlite3.connect("nabidka.db")
        c= conn.cursor()
        #print(bat_tempe,out_tempe,rounded,trip,aktualni,prumerna)
        
        for i in range(4):
            c.execute("""UPDATE parametry SET bat_temp=? WHERE rowid=?""",(bat_tempe[i],i+1))
            c.execute("""UPDATE parametry SET out_temp=? WHERE rowid=?""",(out_tempe[i],i+1))
            c.execute("""UPDATE parametry SET dojezd=? WHERE rowid=?""",(rounded[i],i+1))
            c.execute("""UPDATE parametry SET trip=? WHERE rowid=?""",(trip[i],i+1))
            c.execute("""UPDATE parametry SET aktualni=? WHERE rowid=?""",(aktualni[i],i+1))
            c.execute("""UPDATE parametry SET prumerna=? WHERE rowid=?""",(prumerna[i],i+1))
            c.execute("""UPDATE parametry SET prumerna_speed=? WHERE rowid=?""",(motor_temp[i],i+1))
            
            
        conn.commit()
        conn.close()        
        