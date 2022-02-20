using CleanPRJ.src.Tool;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CleanPRJ.src.BluetoothComunication
{
    public class BluetoothManager : Singleton<BluetoothManager>
    {
        public readonly static string TargetBluetoothDevice = "xiaoxiang BMS";
        private ReadOnlyDictionary<char, Action<BluetoothMessage>> commandMap;
        public ObservableCollection<string> ListOfDevices { get; set; } = new ObservableCollection<string>();

        public string SelectedDevice = string.Empty;
        private bool isConnected = false;
        private int sleepTime = 250;

        private IBluetoothReader bluetooth = null;
        private IBluetoothLE ble;
        private IAdapter adapter;

        private bool isSelectedBthDevice => !string.IsNullOrEmpty(SelectedDevice);
        public bool IsConnectEnabled => isSelectedBthDevice && !isConnected;
        public bool IsDisconnectEnabled => isSelectedBthDevice && isConnected;
        public bool IsPickerEnabled => !isConnected;

        public BluetoothManager()
        {
            FillCommandMap();
            InitBluetoothAsync();
            ConnectTargetDevice();
        }

        private void FillCommandMap()
        {
            commandMap = new ReadOnlyDictionary<char, Action<BluetoothMessage>>(new Dictionary<char, Action<BluetoothMessage>> {
                //{ 'B', BluetoothCommand.BatteryCommand },
                //{ 'D', BluetoothCommand.DistanceCommand },
                { (char)BMSBluetoothCommand.cellDataCode, BMSBluetoothCommand.GetResponceCellData},
                { (char)BMSBluetoothCommand.baseInfoCode, BMSBluetoothCommand.GetResponceBaseInfo},
                { SabvotonBluetoothCommand.CommandKey, SabvotonBluetoothCommand.GetResponce }
            });
        }

        private void InitBluetoothAsync()
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

        private async Task ConnectTargetDevice()
        {
            try
            {
                ListOfDevices = await bluetooth.PairedDevices();
                if (ListOfDevices.Any(c => c.Contains(TargetBluetoothDevice)))
                {
                    SelectedDevice = ListOfDevices.First(c => c.Contains(TargetBluetoothDevice));
                   // Connect();
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
        public void Refresh()
        {
            var task = bluetooth.PairedDevices();
            task.RunSynchronously();
            ListOfDevices = task.Result;
        }

        public async Task RefreshAsync()
        {
            ListOfDevices = await bluetooth.PairedDevices();
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
            Send(new BluetoothMessage(DateTime.Now, command, MessageState.Sended));
        }


        internal void Send(List<byte> fullCommand)
        {
            Send(fullCommand.ToArray());
        }

        private void Send(byte[] command)
        {
            Send(new BluetoothMessage(command, MessageState.Sended));
        }


        private void Send(BluetoothMessage mesage)
        {
            bluetooth.Send(mesage);
            Console.WriteLine(mesage.Message);
        }
    }
}
