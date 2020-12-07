using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using PropertyChanged;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TestBth
{
    public class MyPageViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<string> ListOfDevices { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> ListOfBarcodes { get; set; } = new ObservableCollection<string>();

        public string SelectedBthDevice = string.Empty;
        bool _isConnected  = false;
        int _sleepTime = 250;

        public string SleepTime
        {
            get { return _sleepTime.ToString(); }
            set
            {
                int.TryParse(value, out _sleepTime);
            }
        }

        private bool _isSelectedBthDevice
        {
            get
            {
                if (string.IsNullOrEmpty(SelectedBthDevice))
                {
                    return false;
                }

                return true;
            }
        }

        public bool IsConnectEnabled
        {
            get
            {
                if (_isSelectedBthDevice == false)
                {
                    return false;
                }

                return !_isConnected;
            }
        }

        public bool IsDisconnectEnabled
        {
            get
            {
                if (_isSelectedBthDevice == false)
                {
                    return false;
                }

                return _isConnected;
            }
        }

        public bool IsPickerEnabled
        {
            get
            {
                return !_isConnected;
            }
        }

        public MyPageViewModel()
        {

            MessagingCenter.Subscribe<App>(this, "Sleep", (obj) =>
             {
                // When the app "sleep", I close the connection with bluetooth
                if (_isConnected)
                 {
                     DependencyService.Get<IBth>().Cancel();
                 }
             });

            MessagingCenter.Subscribe<App>(this, "Resume", (obj) =>
             {

                 // When the app "resume" I try to restart the connection with bluetooth
                 if (_isConnected)
                 {
                     DependencyService.Get<IBth>().Start(SelectedBthDevice, _sleepTime, true);
                 }
             });

            try
            {
                // At startup, I load all paired devices
                ListOfDevices = DependencyService.Get<IBth>().PairedDevices();
            }
            catch (Exception ex)
            {
                //Application.Current.MainPage.DisplayAlert("Attention", ex.Message, "Ok");
            }
        }

        public void Connect()
        {
            // Try to connect to a bth device
            DependencyService.Get<IBth>().Start(SelectedBthDevice, _sleepTime, true);
            _isConnected = true;

            // Receive data from bth device
            MessagingCenter.Subscribe<App, string>(this, "Barcode", (sender, arg) =>
            {

                // Add the barcode to a list (first position)
                ListOfBarcodes.Insert(0, arg);
            });
        }

        public void Disconnect()
        {
            // Disconnect from bth device
            DependencyService.Get<IBth>().Cancel();
            MessagingCenter.Unsubscribe<App, string>(this, "Barcode");
            _isConnected = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
