using Android.Telephony;
using DataGrabber.src.PhoneCall;

namespace DataGrabber.Droid
{
    public class PhoneCallDetector : PhoneStateListener
    {
        public override void OnCallStateChanged(CallState state, string incomingNumber)
        {
            base.OnCallStateChanged(state, incomingNumber);
            switch (state)
            {
                case CallState.Idle:
                case CallState.Offhook:
                    PhoneCallManager.I.EndCallPhoneNumber();
                    break;
                case CallState.Ringing:
                    PhoneCallManager.I.StartCallPhoneNumber(incomingNumber);
                    break;
                default:
                    break;
            }
        }
    }
}