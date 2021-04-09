using CleanPRJ.src.BluetoothComunication;
using CleanPRJ.src.Tool;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanPRJ.src.PhoneCall
{
    public class PhoneCallManager : Singleton<PhoneCallManager>
    {
        public void StartCallPhoneNumber(string number)
        {
            BluetoothManager.I.Send($"T:{(number.Length > 0 ? number.Substring(1) : "999999999999")}\0");
        }

        public void EndCallPhoneNumber()
        {
            BluetoothManager.I.Send($"T:end          \0");
        }
        public void MissCallPhoneNumber()
        {
            BluetoothManager.I.Send($"T:miss         \0");
        }
    }
}
