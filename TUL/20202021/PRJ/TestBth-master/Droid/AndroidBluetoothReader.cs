using Android.Bluetooth;
using Android.Runtime;
using Java.IO;
using Java.Util;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestBluetooth.Droid;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.Generic;

[assembly: Dependency(typeof(AndroidBluetoothReader))]
namespace TestBluetooth.Droid
{

    public class AndroidBluetoothReader : IBluetoothReader
    {

        private CancellationTokenSource cancelToken { get; set; }

        private bool canContinue => !cancelToken.IsCancellationRequested;

        public List<BluetoothMessage> Sended { get; private set; } = new List<BluetoothMessage>();

        public List<BluetoothMessage> Recived { get; private set; } = new List<BluetoothMessage>();

        private Queue<BluetoothMessage> toSend = new Queue<BluetoothMessage>();;

        const int RequestResolveError = 1000;

        public AndroidBluetoothReader()
        {
        }

        #region IBth implementation

        /// <summary>
        /// Start the "reading" loop 
        /// </summary>
        /// <param name="name">Name of the paired bluetooth device (also a part of the name)</param>
        public void Start(string name, int sleepTime = 200, bool readAsCharArray = false)
        {
            Task.Run(() => Loop(name, sleepTime, readAsCharArray));
        }

        private async Task Loop(string name, int sleepTime, bool readAsCharArray)
        {
            BluetoothDevice device = null;
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            BluetoothSocket BthSocket = null;

            //Thread.Sleep(1000);
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
                        if ((int)Android.OS.Build.VERSION.SdkInt >= 10) // Gingerbread 2.3.3 2.3.4
                        {
                            BthSocket = device.CreateInsecureRfcommSocketToServiceRecord(uuid);
                        }
                        else
                        {
                            BthSocket = device.CreateRfcommSocketToServiceRecord(uuid);
                        }

                        if (BthSocket != null)
                        {
                            await BthSocket.ConnectAsync();

                            if (BthSocket.IsConnected)
                            {
                                Debug.WriteLine("Connected!");
                                var mReader = new InputStreamReader(BthSocket.InputStream);
                                var buffer = new BufferedReader(mReader);

                                while (canContinue)
                                {
                                    if (buffer.Ready())
                                    {
                                        char[] chr = new char[100];
                                        string messageText = string.Empty;
                                        if (readAsCharArray)
                                        {

                                            await buffer.ReadAsync(chr);
                                            foreach (char c in chr)
                                            {

                                                if (c == '\0')
                                                {
                                                    break;
                                                }

                                                messageText += c;
                                            }

                                        }
                                        else
                                        {
                                            messageText = await buffer.ReadLineAsync();
                                        }

                                        if (messageText.Length > 0)
                                        {
                                            Debug.WriteLine("Letto: " + messageText);
                                            Recived.Add(new BluetoothMessage(DateTime.Now, messageText));
                                        }
                                        else
                                        {
                                            Debug.WriteLine("No data");
                                        }
                                    }
                                    else
                                    {
                                        Debug.WriteLine("No data to read");
                                        if (toSend.Count > 0)
                                        {
                                            var message = toSend.Peek();
                                            using (var output = BthSocket.OutputStream)
                                            {
                                                var baseOutput = (output as OutputStreamInvoker).BaseOutputStream;
                                                var dataToSend = message.Message.ToCharArray().Select(c => (byte)c).ToArray();
                                                baseOutput.Write(dataToSend, 0, dataToSend.Length);
                                                Sended.Add(toSend.Dequeue());
                                                Debug.WriteLine($"Send {message}");
                                            }
                                        }
                                        else
                                        {
                                            Debug.WriteLine("No data to send");
                                        }
                                    }

                                    // A little stop to the uneverending thread...
                                    Thread.Sleep(sleepTime);
                                    if (!BthSocket.IsConnected)
                                    {
                                        throw new Exception("BthSocket.IsConnected = false, Throw exception");
                                    }
                                }

                                Debug.WriteLine("Exit the inner loop");

                            }
                        }
                        else
                        {
                            Debug.WriteLine("BthSocket = null");
                        }
                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine("EXCEPTION: " + ex.Message);
                }

                finally
                {
                    if (BthSocket != null)
                    {
                        BthSocket.Close();
                    }

                    device = null;
                    adapter = null;
                }
            }

            Debug.WriteLine("Exit the external loop");
        }

        /// <summary>
        /// Cancel the Reading loop
        /// </summary>
        /// <returns><c>true</c> if this instance cancel ; otherwise, <c>false</c>.</returns>
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
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            ObservableCollection<string> devices = new ObservableCollection<string>();

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


        #endregion
    }
}
