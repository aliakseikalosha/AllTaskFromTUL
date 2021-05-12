using CleanPRJ.Settings;
using CleanPRJ.src.BluetoothComunication;
using CleanPRJ.src.Tool;

namespace CleanPRJ.src.PhoneCall
{
    public class PhoneCallManager : Singleton<PhoneCallManager>
    {
        public void StartCallPhoneNumber(string number)
        {
            if (AppSettings.I.SendCalls)
            {
                BluetoothCommand.SendCall(number);
            }
        }

        public void EndCallPhoneNumber()
        {
            if (AppSettings.I.SendCalls)
            {
                BluetoothCommand.EndCall();
            }
        }
    }
}
