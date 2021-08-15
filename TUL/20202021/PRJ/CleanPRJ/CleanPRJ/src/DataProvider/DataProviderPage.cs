using CleanPRJ.MainScreen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CleanPRJ.DataProvider
{
    public class DataProviderPage : ApplicationPage<DataProviderViewModel>
    {
        private IBluetoothReader bluetooth;
        private Picker pickerBluetoothDevices;
        private Entry entrySleepTime;
        private Entry messageText;
        private Button send;
        private StackLayout messageStack;
        private ScrollView messageScroll;
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
        }

        public override void InitUI()
        {
            this.BindingContext = model;

            pickerBluetoothDevices = new Picker() { Title = "Select a bluetooth device" };
            pickerBluetoothDevices.SetBinding(Picker.ItemsSourceProperty, "ListOfDevices");
            pickerBluetoothDevices.SelectedIndexChanged += OnSelectedBluetoothDevice;

            entrySleepTime = new Entry() { Keyboard = Keyboard.Numeric, Placeholder = "Sleep time" };
            entrySleepTime.Text = model.SleepTime;
            entrySleepTime.TextChanged += ChangeSleepTime;

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

            var refresh = new Button() { Text = "Refresh" };
            refresh.Clicked += RefreshDeviceList;

            messageText = new Entry { Placeholder = "Message To Send" };
            send = new Button { Text = "Send" };
            send.Clicked += SendMessage;

            messageStack = new StackLayout();
            UpdateMessage();

            messageScroll = new ScrollView { Content = messageStack, IsVisible = true };
            int topPadding = Device.RuntimePlatform == Device.iOS ? 20 : 0;
            StackLayout connectionButtons = new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { disconnect, connect, refresh } };
            StackLayout sessionButtons = new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { start, stop } };
            StackLayout sendStack = new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { messageText, send } };
            StackLayout sl = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = { pickerBluetoothDevices, entrySleepTime, connectionButtons, sessionButtons, sendStack, messageScroll },
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
                var sorted = bluetooth.All.OrderBy(c => c.Date.Ticks).ToArray();
                messageStack.Children.Clear();
                for (int i = 0; i < sorted.Length; i++)
                {
                    var m = sorted[i];
                    messageStack.Children.Add(new Label
                    {
                        Text = $" {m.State}:{m.Message}",
                        TextDecorations = TextDecorations.None,
                        TextColor = m.State == MessageState.Recived ? Color.Black : Color.DarkGray,
                        BackgroundColor = Color.White,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        VerticalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                    });
                }
            });
        }

        private void SendMessage(object sender, EventArgs e)
        {
            var bluetooth = DependencyService.Get<IBluetoothReader>();
            bluetooth.Send(new BluetoothMessage(DateTime.Now, messageText.Text + "\n", MessageState.Sended));
        }

        private void ChangeSleepTime(object sender, TextChangedEventArgs e)
        {
            model.SleepTime = entrySleepTime.Text;
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

    public class DataProviderViewModel : IViewModel
    {
        public Action OnStartGatheringData;
        public Action OnStopGatheringData;
        private readonly string fileName;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> ListOfDevices { get; set; } = new ObservableCollection<string>();

        public string SelectedBthDevice = string.Empty;
        bool isConnected = false;
        int sleepTime = 250;

        public string SleepTime
        {
            get { return sleepTime.ToString(); }
            set
            {
                int.TryParse(value, out sleepTime);
            }
        }

        private bool isSelectedBthDevice => !string.IsNullOrEmpty(SelectedBthDevice);
        public bool IsConnectEnabled => isSelectedBthDevice && !isConnected;
        public bool IsDisconnectEnabled => isSelectedBthDevice && isConnected;
        public bool IsPickerEnabled => !isConnected;


        public DataProviderViewModel()
        {
            fileName = $"{DateTime.Now:yyyy_MM_dd_HH_mm_ss}_data.csv";
            Init();
            MessagingCenter.Subscribe<App>(this, "Sleep", (obj) =>
            {
                // When the app "sleep", I close the connection with bluetooth
                //if (isConnected)
                //{
                //    Disconnect();
                //}
            });

            MessagingCenter.Subscribe<App>(this, "Resume", (obj) =>
            {

                // When the app "resume" I try to restart the connection with bluetooth
                //if (isConnected)
                //{
                //    DependencyService.Get<IBluetoothReader>().Start(SelectedBthDevice, sleepTime, true);
                //}
            });

            Refresh();
        }

        public void Init()
        {
        }

        public void Connect()
        {
            // Try to connect to a bth device
            DependencyService.Get<IBluetoothReader>().Start(SelectedBthDevice, sleepTime, true);
            isConnected = true;
        }

        public void Disconnect()
        {
            // Disconnect from bth device
            DependencyService.Get<IBluetoothReader>().Cancel();
            isConnected = false;
        }

        internal void CreateFile()
        {
            var fileAccess = DependencyService.Get<IAccessFileService>();
            fileAccess.CreateFile(fileName);
        }

        internal void Refresh()
        {
            try
            {
                // At startup, I load all paired devices
                ListOfDevices = DependencyService.Get<IBluetoothReader>().PairedDevices();
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Attention", ex.Message, "Ok");
            }
        }
        private Task task = null;

        private CancellationTokenSource source = new CancellationTokenSource();
        private CancellationToken token;
        internal void StartGatheringData(int waitMS)
        {
            OnStartGatheringData?.Invoke();
            token = source.Token;
            task = DataGathering(waitMS);
        }

        private async Task DataGathering(int waitMS)
        {
            var fileAccess = DependencyService.Get<IAccessFileService>();
            while (!token.IsCancellationRequested)
            {
                fileAccess.WriteNewLineToFile(fileName, $"{DateTime.Now:O}");
                await Task.Delay(500);
            }
        }

        internal async void StopGatheringData()
        {
            if (!task.IsCanceled)
            {
                source.Cancel();
            }
            await Task.Delay(1000);
            source.Dispose();
            source = new CancellationTokenSource();
            OnStopGatheringData?.Invoke();
        }
    }
    public interface IAccessFileService
    {
        void CreateFile(string fileName);

        void WriteNewLineToFile(string fileName, string text);
    }
}

