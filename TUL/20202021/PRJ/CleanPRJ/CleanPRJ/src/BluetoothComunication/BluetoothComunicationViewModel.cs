using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.ComponentModel;

namespace CleanPRJ
{
    public class BluetoothComunicationViewModel : INotifyPropertyChanged, IViewModel
    {
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

        public BluetoothComunicationViewModel()
        {

            MessagingCenter.Subscribe<App>(this, "Sleep", (obj) =>
             {
                 // When the app "sleep", I close the connection with bluetooth
                 if (isConnected)
                 {
                     Disconnect();
                 }
             });

            MessagingCenter.Subscribe<App>(this, "Resume", (obj) =>
             {

                 // When the app "resume" I try to restart the connection with bluetooth
                 if (isConnected)
                 {
                     DependencyService.Get<IBluetoothReader>().Start(SelectedBthDevice, sleepTime, true);
                 }
             });

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
        public void Init() { }

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
    }
}
