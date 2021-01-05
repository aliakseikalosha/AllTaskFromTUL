using Android.App;
using Android.Bluetooth;
using Android.Content.PM;
using Android.OS;


namespace TestBluetooth.Droid
{
    [Activity(Label = "TestBth.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        const string Tag = "MainActivity";

        public static BluetoothSocket BthSocket = null;

        const int RequestResolveError = 1000;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();

            LoadApplication(new App());
        }
    }
}