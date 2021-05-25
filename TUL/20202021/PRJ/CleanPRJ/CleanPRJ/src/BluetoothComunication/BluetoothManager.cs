using CleanPRJ.src.Tool;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace CleanPRJ.src.BluetoothComunication
{
    public class BluetoothManager : Singleton<BluetoothManager>
    {
        public readonly static string TargetBluetoothDevice = "HC-05";
        private ReadOnlyDictionary<char, Action<BluetoothMessage>> commandMap;
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
            FillCommandMap();
            InitBluetooth();
            ConnectTargetDevice();
        }

        private void FillCommandMap()
        {
            commandMap = new ReadOnlyDictionary<char, Action<BluetoothMessage>>(new Dictionary<char, Action<BluetoothMessage>> {
                { 'B', BluetoothCommand.BatteryCommand },
                { 'D', BluetoothCommand.DistanceCommand },
            });
        }

        private void InitBluetooth()
        {
            bluetooth = DependencyService.Get<IBluetoothReader>();
            bluetooth.OnMessageUpdated += NewMessage;
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
        }

        private void ConnectTargetDevice()
        {
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

        private void NewMessage(BluetoothMessage message)
        {
            if (message.State != MessageState.Recived)
            {
                return;
            }
            var type = message.Type;
            if (commandMap.Keys.Contains(type))
            {
                commandMap[type]?.Invoke(message);
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
