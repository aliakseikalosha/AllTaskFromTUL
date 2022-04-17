using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using DataGrabber.src.BluetoothComunication;
using Plugin.BLE.Abstractions.Contracts;
using Android.Bluetooth;
using Java.Util;
using Plugin.BLE.Abstractions.EventArgs;

[assembly: Dependency(typeof(DataGrabber.Android.AndroidBluetoothReader))]
namespace DataGrabber.Android
{
    public class ReaderSabvoton : Reader
    {
        public ReaderSabvoton(IAdapter adapter)
        {
            this.adapter = adapter;
            serviceGuid = new Guid("0f557b00-a37d-e411-bedb-50ed7800a5a5");
            sendGuid = new Guid("0f557b02-a37d-e411-bedb-50ed7800a5a5");
            reciveGuid = new Guid("0f557b01-a37d-e411-bedb-50ed7800a5a5");
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
            Debug.WriteLine(message.Select(c=>$"{c:X}").Aggregate((a,b)=>a+b));
            return message[0] == SabvotonBluetoothCommand.start && message[^1] == SabvotonBluetoothCommand.end;
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
                                if (CompliteMessage(answer) || moveOn)
                                {
                                    if (CompliteMessage(answer))
                                    {
                                        RecivedMessage(answer);
                                    }
                                    answer.Clear();
                                    moveOn = false;
                                    break;
                                }
                            }
                        }
                        recive.ValueUpdated += Read;

                        await recive.StartUpdatesAsync();
                        while (canContinue)
                        {
                            await Task.Delay(50, cancelToken);
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
    }
}
