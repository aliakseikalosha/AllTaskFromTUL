using Android.Util;
using Java.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace TestBluetooth
{
    public class MainPage : ContentPage
    {
        private Picker pickerBluetoothDevices = null;
        private Entry entrySleepTime = null;
        private Button connect = null;
        private Button disconnect = null;
        private MainPageViewModel viewModel = null;
        ScrollView messagesView = null;
        public MainPage()
        {
            viewModel = new MainPageViewModel();
            InitUI();
        }

        private void InitUI()
        {
            this.BindingContext = viewModel;

            pickerBluetoothDevices = new Picker() { Title = "Select a bluetooth device" };
            pickerBluetoothDevices.SetBinding(Picker.ItemsSourceProperty, "ListOfDevices");
            pickerBluetoothDevices.SelectedIndexChanged += OnSelectedBluetoothDevice;

            entrySleepTime = new Entry() { Keyboard = Keyboard.Numeric, Placeholder = "Sleep time" };
            entrySleepTime.Text = viewModel.SleepTime;
            entrySleepTime.TextChanged += ChangeSleepTime;

            connect = new Button() { Text = "Connect" };
            connect.Clicked += ConnectToSelected;
            connect.IsEnabled = false;

            disconnect = new Button() { Text = "Disconnect" };
            disconnect.Clicked += DiconnectFromDevice;

            StackLayout slButtons = new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { disconnect, connect } };

            int topPadding = Device.RuntimePlatform == Device.iOS ? 20 : 0;
            var underlineLabel = new Label { Text = "This is underlined text.", TextDecorations = TextDecorations.None, TextColor = Color.Green, BackgroundColor = Color.Black };
            

            StackLayout sl = new StackLayout { Children = { pickerBluetoothDevices, entrySleepTime, slButtons }, Padding = new Thickness(0, topPadding, 0, 0) };
            Content = sl;
        }

        private ScrollView MessagesScroll(List<BluetoothMessage> all)
        {
            var sv = new ScrollView();
            for (int i = 0; i < all.OrderBy(c=>c.); i++)
            {

            }
            return sv;
        } 

        private void ChangeSleepTime(object sender, TextChangedEventArgs e)
        {
            viewModel.SleepTime = entrySleepTime.Text;
        }

        private void DiconnectFromDevice(object sender, EventArgs e)
        {
            viewModel.Disconnect();
            connect.IsEnabled = true;
            disconnect.IsEnabled = false;
        }

        private void ConnectToSelected(object sender, EventArgs e)
        {
            viewModel.Connect();
            connect.IsEnabled = false;
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
                var bth = DependencyService.Get<IBluetoothReader>();
                ((MainPageViewModel)BindingContext).ListOfDevices = bth.PairedDevices();
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

