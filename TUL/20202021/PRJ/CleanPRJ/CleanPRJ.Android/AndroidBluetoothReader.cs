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

        public AndroidBluetoothReader() { }

        public void Start(string name, int sleepTime = 200, bool readAsCharArray = false)
        {
            Task.Run(() => Loop(name, sleepTime));
        }

        private async Task Loop(string name, int sleepTime)
        {
            BluetoothDevice device = null;
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            BluetoothSocket socket = null;

            cancelToken = new CancellationTokenSource();
            while (canContinue)
            {
                try
                {
                    Thread.Sleep(sleepTime);

                    adapter = BluetoothAdapter.DefaultAdapter;

                    Debug.WriteLine(adapter == null ? "No Bluetooth adapter found." : "Adapter found!!");
                    Debug.WriteLine(adapter.IsEnabled ? "Adapter enabled!" : "Bluetooth adapter is not enabled.");
                    Debug.WriteLine("Try to connect to " + name);

                    foreach (var bd in adapter.BondedDevices)
                    {
                        Debug.WriteLine("Paired devices found: " + bd.Name.ToUpper());
                        if (bd.Name.ToUpper().IndexOf(name.ToUpper()) >= 0)
                        {
                            Debug.WriteLine("Found " + bd.Name + ". Try to connect with it!");
                            device = bd;
                            break;
                        }
                    }

                    if (device == null)
                    {
                        Debug.WriteLine("Named device not found.");
                    }
                    else
                    {
                        UUID uuid = UUID.FromString("00001101-0000-1000-8000-00805f9b34fb");

                        socket = device.CreateRfcommSocketToServiceRecord(uuid);

                        if (socket != null)
                        {
                            await socket.ConnectAsync();

                            if (socket.IsConnected)
                            {
                                Debug.WriteLine("Connected!");
                                var mReader = new InputStreamReader(socket.InputStream);
                                var buffer = new BufferedReader(mReader);

                                while (canContinue)
                                {
                                    if (buffer.Ready())
                                    {
                                        char[] chr = new char[100];
                                        string messageText = string.Empty;
                                        await buffer.ReadAsync(chr);
                                        foreach (char c in chr)
                                        {
                                            messageText = CombineMessage(messageText, c);
                                        }

                                        if (CompliteMessage(messageText))
                                        {
                                            Debug.WriteLine("Letto: " + messageText);
                                            AddMessage(new BluetoothMessage(DateTime.Now, messageText, MessageState.Recived));
                                        }
                                    }
                                    else
                                    {
                                        if (toSend.Count > 0)
                                        {
                                            var message = toSend.Peek();
                                            var dataToSend = message.Message.ToCharArray().Select(c => (byte)c).ToArray();
                                            if (socket.OutputStream.CanWrite)
                                            {
                                                await socket.OutputStream.WriteAsync(dataToSend, 0, dataToSend.Length);
                                                AddMessage(toSend.Dequeue());
                                                Thread.Sleep(sleepTime);
                                                Debug.WriteLine($"Send total of {dataToSend.Length} bytes in message {message} ");
                                            }
                                        }
                                    }
                                    if (!socket.IsConnected)
                                    {
                                        throw new Exception("BthSocket.IsConnected = false, Throw exception");
                                    }
                                }
                                Debug.WriteLine("Exit the inner loop");
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine("EXCEPTION: " + ex.Message);
                }

                finally
                {
                    if (socket != null)
                    {
                        socket.Close();
                    }

                    device = null;
                    adapter = null;
                }
            }

            Debug.WriteLine("Exit the external loop");
        }

        private string CombineMessage(string message, char next)
        {
            if (BluetoothCommand.CommandType.Contains(next))
            {
                return next.ToString();
            }
            if (char.IsLetterOrDigit(next) || next == BluetoothCommand.Separator || BluetoothCommand.AllowedNotAlfanumericSymbols.Contains(next))
            {
                return message + next;
            }
            return message;
        }

        private bool CompliteMessage(string message)
        {
            return message.Length > 3 && message[1] == BluetoothCommand.Separator && message[^1] == BluetoothCommand.Separator;
        }

        private void AddMessage(BluetoothMessage message)
        {
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

        public ObservableCollection<string> PairedDevices()
        {
            var adapter = BluetoothAdapter.DefaultAdapter;
            var devices = new ObservableCollection<string>();

            foreach (var bd in adapter.BondedDevices)
            {
                devices.Add(bd.Name);
            }

            return devices;
        }

        public void Send(BluetoothMessage message)
        {
            toSend.Enqueue(message);
        }

    }
}
