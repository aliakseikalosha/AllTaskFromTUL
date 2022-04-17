using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using Plugin.BLE.Abstractions.EventArgs;

[assembly: Dependency(typeof(DataGrabber.Droid.AndroidBluetoothReader))]
namespace DataGrabber.Droid
{
    public class AndroidBluetoothReader : IBluetoothReader
    {
        public Action<BluetoothMessage> OnMessageUpdated { get; set; }
        private CancellationTokenSource cancelToken { get; set; } = new CancellationTokenSource();

        public List<BluetoothMessage> Sended => All.Where(c => c.State == MessageState.Sended).ToList();
        public List<BluetoothMessage> Recived => All.Where(c => c.State == MessageState.Recived).ToList();
        public List<BluetoothMessage> All { get; private set; } = new List<BluetoothMessage>();

        private ObservableCollection<string> devices = new ObservableCollection<string>();
        private IPermissions _permissions;
        private IAdapter Adapter;
        private Reader readerSabvoton;
        private Reader readerBMS;

        public AndroidBluetoothReader()
        {
            Adapter = CrossBluetoothLE.Current.Adapter;
            //todo remove code dublication
            readerBMS = new ReaderBMS(Adapter);
            readerBMS.OnRecived += AddMessage;

            readerSabvoton = new ReaderSabvoton(Adapter);
            readerSabvoton.OnRecived += AddMessage;
        }

        public void Start(string name, int sleepTime = 200, bool isSabvoton = false)
        {
            (isSabvoton ? readerSabvoton : readerBMS).Start(name, cancelToken.Token, sleepTime);
        }

        private void AddMessage(BluetoothMessage message)
        {
            Debug.WriteLine($"Recived {message.Message}");
            All.Add(message);
            OnMessageUpdated?.Invoke(message);
        }

        public void ClearFront(bool isSabvoton)
        {
            if (isSabvoton)
            {
                readerSabvoton.ClearFront();
            }
            else
            {
                readerBMS.ClearFront();
            }
        }

        public void Cancel()
        {
            if (cancelToken != null)
            {
                Debug.WriteLine("Send a cancel to task!");
                cancelToken.Cancel();
            }
        }

        public async Task<ObservableCollection<string>> PairedDevices()
        {
            var canScanning = await CanStartScanning();
            var devices = new ObservableCollection<string>();
            if (canScanning)
            {
                devices = await ScanForDevices();
            }

            return devices;
        }

        private async Task<bool> CanStartScanning(bool refresh = false)
        {
            if (_permissions == null)
            {
                _permissions = CrossPermissions.Current;
            }
            if (Device.RuntimePlatform == Device.Android)
            {
                var status = await _permissions.CheckPermissionStatusAsync<LocationPermission>();
                if (status != PermissionStatus.Granted)
                {
                    var permissionResult = await _permissions.RequestPermissionAsync<LocationPermission>();

                    if (permissionResult != PermissionStatus.Granted)
                    {
                        _permissions.OpenAppSettings();
                        return false;
                    }
                }
            }

            return true;
        }

        private async Task<ObservableCollection<string>> ScanForDevices()
        {
            devices.Clear();

            foreach (var connectedDevice in Adapter.ConnectedDevices)
            {
                AddDevice(connectedDevice);
            }

            Adapter.ScanMode = ScanMode.LowLatency;
            Adapter.DeviceDiscovered += AddDevice;
            await Adapter.StartScanningForDevicesAsync();
            Adapter.DeviceDiscovered -= AddDevice;

            return devices;
        }

        private void AddDevice(object sender, DeviceEventArgs e)
        {
            AddDevice(e.Device);
        }

        private void AddDevice(IDevice device)
        {
            Debug.WriteLine($"AddDevice ({ device.Name}:{ device.Id})");
            devices.Add($"{device.Name}:{device.Id}");
        }

        public void Send(BluetoothMessage message, bool isSabvoton)
        {
            if (isSabvoton)
            {
                readerSabvoton.AddMessageToFront(message);
            }
            else
            {
                readerBMS.AddMessageToFront(message);
            }
        }

    }
}
