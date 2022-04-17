using DataGrabber.Settings;
using DataGrabber.src.BluetoothComunication;
using DataGrabber.src.Tool;

namespace DataGrabber.src.PhoneCall
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
