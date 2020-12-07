using Android.Util;
using Java.IO;
using System;

using Xamarin.Forms;

namespace TestBth
{
    public class MyPage : ContentPage
    {
        private Picker pickerBluetoothDevices = null;
        private Entry entrySleepTime = null;
        private Button connect = null;
        private Button disconnect = null;
        private MyPageViewModel viewModel = null;
        public MyPage()
        {
            viewModel = new MyPageViewModel();
            this.BindingContext = viewModel;

            pickerBluetoothDevices = new Picker() { Title = "Select a bth device" };
            pickerBluetoothDevices.SetBinding(Picker.ItemsSourceProperty, "ListOfDevices");
            pickerBluetoothDevices.SelectedIndexChanged += OnSelectedBluetoothDevice;

            entrySleepTime = new Entry() { Keyboard = Keyboard.Numeric, Placeholder = "Sleep time" };
            entrySleepTime.SetBinding(Entry.TextProperty, "SleepTime");

            connect = new Button() { Text = "Connect" };
            connect.Clicked += ConnectToSelected;
            connect.IsEnabled = false;

            disconnect = new Button() { Text = "Disconnect" };
            disconnect.SetBinding(Button.CommandProperty, "DisconnectCommand");
            disconnect.SetBinding(VisualElement.IsEnabledProperty, "IsDisconnectEnabled");

            StackLayout slButtons = new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { disconnect, connect } };

            ListView lv = new ListView();
            lv.SetBinding(ListView.ItemsSourceProperty, "ListOfBarcodes");
            lv.ItemTemplate = new DataTemplate(typeof(TextCell));
            lv.ItemTemplate.SetBinding(TextCell.TextProperty, ".");

            int topPadding = 0;
            if (Device.RuntimePlatform == Device.iOS)
            {
                topPadding = 20;
            }

            StackLayout sl = new StackLayout { Children = { pickerBluetoothDevices, entrySleepTime, slButtons, lv }, Padding = new Thickness(0, topPadding, 0, 0) };
            Content = sl;
        }

        private void ConnectToSelected(object sender, EventArgs e)
        {
            viewModel.Connect();
            disconnect.IsEnabled = true;
        }

        private void OnSelectedBluetoothDevice(object sender, EventArgs e)
        {
            viewModel.SelectedBthDevice = (string)((Picker)sender).SelectedItem;
            connect.IsEnabled = viewModel.IsConnectEnabled;
        }

        protected override bool OnBackButtonPressed()
        {
            try
            {
                // At startup, I load all paired devices
                var bth = DependencyService.Get<IBth>();
                ((MyPageViewModel)BindingContext).ListOfDevices = bth.PairedDevices();
            }
            catch (Exception e)
            {
                string tag = "DEBUG";
                Log.Warn(tag, $" Try to refresh list of devices and get {e.Message}");
            }

            return true;
        }
    }
}

