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
        private CancellationTokenSource cancelToken { get; set; } = new CancellationTokenSource();

        public List<BluetoothMessage> Sended => All.Where(c => c.State == MessageState.Sended).ToList();
        public List<BluetoothMessage> Recived => All.Where(c => c.State == MessageState.Recived).ToList();
        public List<BluetoothMessage> All { get; private set; } = new List<BluetoothMessage>();

        private ObservableCollection<string> devices = new ObservableCollection<string>();
        private IPermissions _permissions;
        private IAdapter Adapter;
        private ReaderSabvoton readerSabvoton;
        private ReaderBMS readerBMS;

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
            if (isSabvoton)
            {
                readerSabvoton.Start(name, cancelToken.Token, sleepTime);
            }
            else
            {
                readerBMS.Start(name, cancelToken.Token, sleepTime);
            }
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

    public abstract class Reader
    {
        protected IAdapter adapter;
        protected bool moveOn;

        protected CancellationToken cancelToken { get; set; }
        protected bool canContinue => !cancelToken.IsCancellationRequested;

        protected Queue<BluetoothMessage> toSend = new Queue<BluetoothMessage>();

        public event Action<BluetoothMessage> OnRecived;

        protected Guid serviceGuid;
        protected Guid sendGuid;
        protected Guid reciveGuid;

        public void AddMessageToFront(BluetoothMessage m)
        {
            toSend.Enqueue(m);
        }

        public void Start(string name, CancellationToken token, int sleepTime)
        {
            cancelToken = token;
            Task.Run(() => Loop(name, sleepTime));
        }

        private async Task Loop(string name, int sleepTime)
        {
            var prefix = $"{name}{GetType()}";
            IDevice device = null;
            while (canContinue)
            {
                try
                {
                    Thread.Sleep(sleepTime);


                    Debug.WriteLine(prefix + (adapter == null ? "No Bluetooth adapter found." : "Adapter found!!"));
                    Debug.WriteLine($"Try to connect to {name}");

                    device = adapter.DiscoveredDevices.First(c => name.Contains(c.Id.ToString()));

                    if (device == null)
                    {
                        Debug.WriteLine($"{prefix}Named device not found.");
                    }
                    else
                    {
                        await adapter.ConnectToDeviceAsync(device);
                        var services = await device.GetServicesAsync();
                        ICharacteristic send = null;
                        ICharacteristic recive = null;
                        var s = $"{prefix}\n";
                        for (int i = 0; i < services.Count; i++)
                        {
                            s += ($"{services[i].Name}:{services[i].Id}\n");

                            var characteristics = await services[i].GetCharacteristicsAsync();

                            foreach (var characteristic in characteristics)
                            {
                                s += ($"\t{characteristic.Name}:{characteristic.Id} == {sendGuid} : {characteristic.Id == sendGuid}\n");
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
                                    s += ($"\t\t{desc.Name}:{desc.Id}\n");
                                }
                            }
                        }
                        Debug.WriteLine(s);
                        if (send == null || recive == null)
                        {
                            Debug.WriteLine($"{prefix} Dont found send({send}) or recive({recive}) device");
                            break;
                        }
                        toSend = new Queue<BluetoothMessage>();

                        var answer = new List<int>();
                        void Read(object o, CharacteristicUpdatedEventArgs args)
                        {
                            var data = args.Characteristic.Value;
                            for (int i = 0; i < data.Length; i++)
                            {
                                answer = CombineMessage(answer, data[i]);
                                Debug.WriteLine($"{prefix} Message add {data[i]:X}");
                                if (CompliteMessage(answer))
                                {
                                    break;
                                }
                            }
                        }
                        recive.ValueUpdated += Read;

                        await recive.StartUpdatesAsync();
                        while (canContinue)
                        {
                            if (toSend.Count > 0)
                            {
                                var mes = toSend.Dequeue();
                                answer.Clear();
                                await send.WriteAsync(mes.ByteData);
                                Debug.WriteLine($"{prefix}\n Send message{mes.Message}\nStart reading");
                                while (!CompliteMessage(answer) && canContinue && !moveOn)
                                {
                                    await Task.Delay(10);
                                }
                                if (canContinue && !moveOn)
                                {
                                    RecivedMessage(answer);
                                }
                            }
                            await Task.Delay(50);
                            moveOn = false;
                        }
                        recive.ValueUpdated -= Read;
                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{prefix}EXCEPTION: {ex.Message}\n{ex.StackTrace}");
                }

            }
            if (device != null)
            {
                await adapter.DisconnectDeviceAsync(device);
            }

            Debug.WriteLine($"{prefix}Exit the external loop");
        }

        protected abstract List<int> CombineMessage(List<int> message, int next);

        protected abstract bool CompliteMessage(List<int> message);

        private void RecivedMessage(List<int> answer)
        {
            if (answer.Count > 0)
            {
                var message = new BluetoothMessage(answer.Select(c => (byte)c).ToArray(), MessageState.Recived);
                OnRecived?.Invoke(message);
            }
            else
            {
                Debug.WriteLine("recived empty Message");
            }
        }

        public void ClearFront()
        {
            Debug.WriteLine($"{GetType()}ClearFront()");
            toSend.Clear();
            moveOn = true;
        }
    }

    public class ReaderBMS : Reader
    {
        public ReaderBMS(IAdapter adapter)
        {
            this.adapter = adapter;
            serviceGuid = new Guid("0000ff00-0000-1000-8000-00805f9b34fb");
            sendGuid = new Guid("0f557b02-a37d-e411-bedb-50ed7800a5a5");
            reciveGuid = new Guid("0f557b01-a37d-e411-bedb-50ed7800a5a5");
        }

        protected override List<int> CombineMessage(List<int> message, int next)
        {
            if (message.Count == 0 && next != BMSBluetoothCommand.start)
            {
                return message;
            }
            message.Add(next);
            return message;
        }

        protected override bool CompliteMessage(List<int> message)
        {
            if (message.Count < 2)
            {
                return false;
            }
            return message[0] == BMSBluetoothCommand.start && message[^1] == BMSBluetoothCommand.end;
        }
    }

    public class ReaderSabvoton : Reader
    {
        public ReaderSabvoton(IAdapter adapter)
        {
            this.adapter = adapter;
            serviceGuid = new Guid("0f557b00-a37d-e411-bedb-50ed7800a5a5");
            sendGuid =    new Guid("0f557b01-a37d-e411-bedb-50ed7800a5a5");
            reciveGuid =  new Guid("0f557b02-a37d-e411-bedb-50ed7800a5a5");
        }

        protected override List<int> CombineMessage(List<int> message, int next)
        {
            if (message.Count == 0 && next != SabvotonBluetoothCommand.start)
            {
                return message;
            }
            message.Add(next);
            return message;
        }

        protected override bool CompliteMessage(List<int> message)
        {
            if (message.Count < 2)
            {
                return false;
            }
            return message[0] == SabvotonBluetoothCommand.start && message[^1] == SabvotonBluetoothCommand.end;
        }
    }
}
