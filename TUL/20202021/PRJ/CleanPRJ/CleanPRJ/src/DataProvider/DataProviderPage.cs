using CleanPRJ.MainScreen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CleanPRJ.DataProvider
{
    public class DataProviderPage : ApplicationPage<DataProviderViewModel>
    {
        private IBluetoothReader bluetooth;
        private Picker pickerBluetoothDevices;
        private StackLayout infoStack;
        private Label CellInfo;
        private Label BaseInfo;
        private Button connect;
        private Button disconnect;
        private Button start;
        private Button stop;

        public DataProviderPage(DataProviderViewModel model) : base(model)
        {
            bluetooth = DependencyService.Get<IBluetoothReader>();
            InitUI();
            model.OnStartGatheringData += () => { start.IsEnabled = false; stop.IsEnabled = true; };
            model.OnStopGatheringData += () => { start.IsEnabled = true; stop.IsEnabled = false; };
            model.OnDataUpdated += UpdateMessage;
        }

        public override void InitUI()
        {
            this.BindingContext = model;

            pickerBluetoothDevices = new Picker() { Title = "Select a bluetooth device" };
            pickerBluetoothDevices.SetBinding(Picker.ItemsSourceProperty, "ListOfDevices");
            pickerBluetoothDevices.SelectedIndexChanged += OnSelectedBluetoothDevice;

            var refresh = new Button() { Text = "Refresh" };
            refresh.Clicked += RefreshDeviceList;
            var changeInfoView = new Button() { Text = "Switch" };

            StackLayout picker = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { pickerBluetoothDevices, refresh, changeInfoView }
            };
            connect = new Button() { Text = "Connect" };
            connect.Clicked += ConnectToSelected;
            connect.IsEnabled = !model.IsConnectEnabled;

            disconnect = new Button() { Text = "Disconnect" };
            disconnect.Clicked += DiconnectFromDevice;

            start = new Button() { Text = "Start" };
            start.Clicked += StartDataGathering;

            stop = new Button() { Text = "Stop" };
            stop.Clicked += StopDataGathering;
            stop.IsEnabled = false;
            CellInfo = new Label();
            BaseInfo = new Label();
            infoStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { CellInfo, BaseInfo }
            };
            var scrollView = new ScrollView
            {
                Content = infoStack
            };

            UpdateMessage();

            int topPadding = Device.RuntimePlatform == Device.iOS ? 20 : 0;
            StackLayout connectionButtons = new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { disconnect, connect, start, stop } };
            StackLayout sl = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = { picker, connectionButtons, scrollView },
                Padding = new Thickness(0, topPadding, 0, 0)
            };
            Content = new StackLayout { Children = { TopLine("Data Grabber", false), sl } };
        }

        private void StopDataGathering(object sender, EventArgs e)
        {
            model.StopGatheringData();
        }

        private void RefreshDeviceList(object sender, EventArgs e)
        {
            model.Refresh();
        }

        private void StartDataGathering(object sender, EventArgs e)
        {
            model.CreateFile();
            model.StartGatheringData(500);
        }

        public void UpdateMessage()
        {
            Device.BeginInvokeOnMainThread(() => // On MainThread because it's a change in your UI
            {
                CellInfo.Text = $"Cell Info \n{model.CurrentCellInfo?.HumanData}";
                BaseInfo.Text = $"Base Stats\n{model.CurrentBaseInfo?.HumanData}";
                Console.WriteLine($"Update DataProviderPage:{DateTime.Now}");
            });
        }

        private void DiconnectFromDevice(object sender, EventArgs e)
        {
            model.Disconnect();
            connect.IsEnabled = true;
            disconnect.IsEnabled = false;
        }

        private void ConnectToSelected(object sender, EventArgs e)
        {
            model.Connect();
            connect.IsEnabled = false;
            disconnect.IsEnabled = true;
        }

        private void OnSelectedBluetoothDevice(object sender, EventArgs e)
        {
            model.SelectedBthDevice = (string)((Picker)sender).SelectedItem;
            connect.IsEnabled = model.IsConnectEnabled;
        }
    }
    public interface IAccessFileService
    {
        void CreateFile(string fileName);

        void WriteNewLineToFile(string fileName, string text);
    }
}

