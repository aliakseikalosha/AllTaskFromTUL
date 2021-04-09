using CleanPRJ.src.Tool;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CleanPRJ.src.BluetoothComunication
{
    public class BluetoothManager : Singleton<BluetoothManager>
    {
        public readonly static string TargetBluetoothDevice = "HC-05";
        public ObservableCollection<string> ListOfDevices { get; set; } = new ObservableCollection<string>();

        public string SelectedDevice = string.Empty;
        private bool isConnected = false;
        private int sleepTime = 250;

        private IBluetoothReader bluetooth = null;
        private bool isSelectedBthDevice => !string.IsNullOrEmpty(SelectedDevice);
        public bool IsConnectEnabled => isSelectedBthDevice && !isConnected;
        public bool IsDisconnectEnabled => isSelectedBthDevice && isConnected;
        public bool IsPickerEnabled => !isConnected;

        public BluetoothManager()
        {
            bluetooth = DependencyService.Get<IBluetoothReader>();
            MessagingCenter.Subscribe<App>(this, "Sleep", (obj) =>
            {
                if (isConnected)
                {
                //    Disconnect();
                }
            });

            MessagingCenter.Subscribe<App>(this, "Resume", (obj) =>
            {
                if (isConnected)
                {
                //    bluetooth.Start(SelectedDevice, sleepTime, true);
                }
            });

            try
            {
                ListOfDevices = bluetooth.PairedDevices();
                if (ListOfDevices.Any(c => c.Contains(TargetBluetoothDevice)))
                {
                    SelectedDevice = ListOfDevices.First(c => c.Contains(TargetBluetoothDevice));
                    Connect();
                }

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Attention", ex.Message, "Ok");
            }

        }

        public void Connect()
        {
            bluetooth.Start(SelectedDevice, sleepTime, true);
            isConnected = true;
        }

        public void Disconnect()
        {
            bluetooth.Cancel();
            isConnected = false;
        }

        public void Send(string command)
        {
            bluetooth.Send(new BluetoothMessage(DateTime.Now, command, MessageState.Sended));
            Console.WriteLine(command);
        }
    }
}
