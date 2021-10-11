import sqlite3

class Databaze_testing():
    def data_read():
        global time,act_consumption,avg_consumption,residual_cap,range_,distance,RPM,speed
        conn = sqlite3.connect("test_data.db")
        c= conn.cursor()
        
        c.execute("SELECT * FROM data")
        datas=c.fetchone()
        
        time=datas[0]
        act_consumption=datas[1]
        avg_consumption=datas[2]
        residual_cap=datas[3]
        range_=datas[4]
        distance=datas[5]
        RPM=datas[6]
        speed=datas[7]
        
        return(time,act_consumption,avg_consumption,residual_cap,range_,distance,RPM,speed)
        conn.commit()
        conn.close()

    def data_write(time,act_consumption,avg_consumption,residual_cap,range_,distance,RPM,speed,index_sec):
 
        conn = sqlite3.connect("test_data.db")
        c= conn.cursor()

        c.execute("""UPDATE data SET time=? WHERE rowid=?""",(time,index_sec+1))
        c.execute("""UPDATE data SET act_consumption=? WHERE rowid=?""",(act_consumption,index_sec+1))
        c.execute("""UPDATE data SET avg_consumption=? WHERE rowid=?""",(avg_consumption,index_sec+1))
        c.execute("""UPDATE data SET residual_cap=? WHERE rowid=?""",(residual_cap,index_sec+1))
        c.execute("""UPDATE data SET range=? WHERE rowid=?""",(range_,index_sec+1))
        c.execute("""UPDATE data SET distance=? WHERE rowid=?""",(distance,index_sec+1))
        c.execute("""UPDATE data SET RPM=? WHERE rowid=?""",(RPM,index_sec+1))
        c.execute("""UPDATE data SET speed=? WHERE rowid=?""",(speed,index_sec+1))

        conn.commit()
        conn.close()
        
