using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;

[assembly: Dependency(typeof(DataGrabber.Droid.AndroidBluetoothReader))]
namespace DataGrabber.Droid
{
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

        protected virtual async Task Loop(string name, int sleepTime)
        {
            var prefix = $"{GetType()}";
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

        protected void RecivedMessage(List<int> answer)
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
}
