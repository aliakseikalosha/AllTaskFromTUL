namespace DataGrabber.src.BluetoothComunication
{
    public class SabvotonData : StateData
    {
        public int SysStatus { get; protected set; }
        public float BatteryVoltage { get; protected set; }
        public int WeakenCurrentCMD { get; protected set; }
        public int WeakenCurrentFBK { get; protected set; }
        public int TorqueCurrentCMD { get; protected set; }
        public int TorqueCurrentFBK { get; protected set; }
        public int ControllerTemp { get; protected set; }
        public int MotorTemp { get; protected set; }
        public int MotorAngle { get; protected set; }
        public int MotorSpeed { get; protected set; }
        public int HallStatus { get; protected set; }
        public int ThrottleVoltage { get; protected set; }
        public int MosfetStatus { get; protected set; }
        public override void FillData(byte[] data)
        {
            int i = 3;
            AddData("SysStatus", (Convert(data[i++], data[i++])).ToString());
            AddData("BatteryVoltage", (Convert(data[i++], data[i++]) / 54.6f).ToString());
            AddData("WeakenCurrentCMD", (Convert(data[i++], data[i++])).ToString());
            AddData("WeakenCurrentFBK", (Convert(data[i++], data[i++])).ToString());
            AddData("TorqueCurrentCMD", (Convert(data[i++], data[i++])).ToString());
            AddData("TorqueCurrentFBK", (Convert(data[i++], data[i++])).ToString());
            AddData("ControllerTemp", (Convert(data[i++], data[i++])).ToString());
            AddData("MotorTemp", (Convert(data[i++], data[i++])).ToString());
            AddData("MotorAngle", (Convert(data[i++], data[i++])).ToString());
            AddData("MotorSpeed", (Convert(data[i++], data[i++])).ToString());
            AddData("HallStatus", (Convert(data[i++], data[i++])).ToString());
            AddData("ThrottleVoltage", (Convert(data[i++], data[i++])).ToString());
            AddData("MosfetStatus", (Convert(data[i++], data[i++])).ToString());
        }
    }
}
