using Android.Bluetooth;
using Java.IO;
using Java.Util;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using CleanPRJ.src.BluetoothComunication;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using Plugin.BLE.Abstractions.EventArgs;

[assembly: Dependency(typeof(CleanPRJ.Droid.AndroidBluetoothReader))]
namespace CleanPRJ.Droid
{

    public class AndroidBluetoothReader : IBluetoothReader
    {
        public Action<BluetoothMessage> OnMessageUpdated { get; set; }
        private CancellationTokenSource cancelToken { get; set; }

        private bool canContinue => !cancelToken.IsCancellationRequested;

        public List<BluetoothMessage> Sended => All.Where(c => c.State == MessageState.Sended).ToList();

        public List<BluetoothMessage> Recived => All.Where(c => c.State == MessageState.Recived).ToList();

        public List<BluetoothMessage> All { get; private set; } = new List<BluetoothMessage>();

        private Queue<BluetoothMessage> toSend = new Queue<BluetoothMessage>();

        private IPermissions _permissions;

        private IAdapter Adapter;

        public AndroidBluetoothReader()
        {
            Adapter = CrossBluetoothLE.Current.Adapter;
        }

        public void Start(string name, int sleepTime = 200, bool readAsCharArray = false)
        {
            Task.Run(() => Loop(name, sleepTime));
        }

        private async Task Loop(string name, int sleepTime)
        {
            cancelToken = new CancellationTokenSource();
            IDevice device = null;
            while (canContinue)
            {
                try
                {
                    Thread.Sleep(sleepTime);


                    Debug.WriteLine(Adapter == null ? "No Bluetooth adapter found." : "Adapter found!!");
                    Debug.WriteLine("Try to connect to " + name);

                    device = Adapter.DiscoveredDevices.First(c => name.Contains(c.Id.ToString()));

                    if (device == null)
                    {
                        Debug.WriteLine("Named device not found.");
                    }
                    else
                    {
                        var serviceGuid = new Guid("0000ff00-0000-1000-8000-00805f9b34fb");
                        var sendGuid = new Guid("0000ff02-0000-1000-8000-00805f9b34fb");
                        var reciveGuid = new Guid("0000ff01-0000-1000-8000-00805f9b34fb");


                        await Adapter.ConnectToDeviceAsync(device);
                        var services = await device.GetServicesAsync();
                        ICharacteristic send = null;
                        ICharacteristic recive = null;

                        for (int i = 0; i < services.Count; i++)
                        {
                            Debug.WriteLine($"{services[i].Name}:{services[i].Id}");

                            var characteristics = await services[i].GetCharacteristicsAsync();

                            foreach (var characteristic in characteristics)
                            {
                                Debug.WriteLine($"\t{characteristic.Name}:{characteristic.Id}");
                                if (characteristic.Id == sendGuid && send == null)
                                {
                                    send = characteristic;
                                }
                                if (characteristic.Id == reciveGuid && recive == null)
                                {
                                    recive = characteristic;
                                }
                                var descriptors = await characteristic.GetDescriptorsAsync();
                                foreach (var desc in descriptors)
                                {
                                    Debug.WriteLine($"\t\t{desc.Name}:{desc.Id}");
                                }
                            }
                        }
                        if(send == null || recive == null)
                        {
                            Debug.WriteLine($"Dont found send({send}) or recive({recive}) device");
                            break;
                        }

                        var answer = new List<int>();
                        recive.ValueUpdated += (o, args) =>
                        {
                            var data = args.Characteristic.Value;
                            for (int i = 0; i < data.Length; i++)
                            {
                                answer = CombineMessage(answer, data[i]);
                                Debug.WriteLine($"Message add {data[i]:X}");
                                if (CompliteMessage(answer))
                                {
                                    break;
                                }
                            }
                        };

                        await recive.StartUpdatesAsync();
                        while (canContinue)
                        {
                            if (toSend.Count > 0)
                            {
                                var mes = toSend.Dequeue();
                                answer.Clear();
                                await send.WriteAsync(mes.BMSData);
                                Debug.WriteLine($"Send message{mes.Message}");
                                Debug.WriteLine("Start reading");
                                while(!CompliteMessage(answer))
                                {
                                    await Task.Delay(20);
                                }
                                AddMessage(answer);
                            }
                            await Task.Delay(500);
                        }

                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"EXCEPTION: {ex.Message}\n{ex.StackTrace}");
                }

            }
            if (device != null)
            {
                await Adapter.DisconnectDeviceAsync(device);
            }

            Debug.WriteLine("Exit the external loop");
        }

        private List<int> CombineMessage(List<int> message, int next)
        {
            if(message.Count == 0 && next != BMSBluetoothCommand.start)
            {
                return message;
            }
            message.Add(next);
            return message;
        }

        private bool CompliteMessage(List<int> message)
        {
            if (message.Count < 2)
            {
                return false;
            }
            return message[0] == BMSBluetoothCommand.start && message[^1] == BMSBluetoothCommand.end;//message.Length > 3 && message[1] == BluetoothCommand.Separator && message[^1] == BluetoothCommand.Separator;
        }

        private void AddMessage(List<int> answer)
        {
            var message = new BluetoothMessage(answer.Select(c=>(byte)c).ToArray(), MessageState.Recived);
            Debug.WriteLine($"Recived {message.Message}");
            All.Add(message);
            OnMessageUpdated?.Invoke(message);
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
            if (Xamarin.Forms.Device.RuntimePlatform == Device.Android)
            {
                var status = await _permissions.CheckPermissionStatusAsync<Plugin.Permissions.LocationPermission>();
                if (status != PermissionStatus.Granted)
                {
                    var permissionResult = await _permissions.RequestPermissionAsync<Plugin.Permissions.LocationPermission>();

                    if (permissionResult != PermissionStatus.Granted)
                    {
                        _permissions.OpenAppSettings();
                        return false;
                    }
                }
            }

            return true;
        }

        private ObservableCollection<string> devices = new ObservableCollection<string>();

        private async Task<ObservableCollection<string>> ScanForDevices()
        {
            devices.Clear();

            foreach (var connectedDevice in Adapter.ConnectedDevices)
            {
                AddDevice(connectedDevice);
            }

            Adapter.ScanMode = Plugin.BLE.Abstractions.Contracts.ScanMode.LowLatency;
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

        public void Send(BluetoothMessage message)
        {
            toSend.Enqueue(message);
        }

    }
}
