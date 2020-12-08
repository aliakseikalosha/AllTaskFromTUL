using Android.Bluetooth;
using Android.Runtime;
using Java.IO;
using Java.Util;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestBth.Droid;
using Xamarin.Forms;
using System.Diagnostics;

[assembly: Dependency(typeof(Bth))]
namespace TestBth.Droid
{

    public class Bth : IBth
    {

        private CancellationTokenSource _ct { get; set; }

        private bool canContinue => !_ct.IsCancellationRequested;

        const int RequestResolveError = 1000;

        public Bth()
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
            _ct = new CancellationTokenSource();
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
                                        string barcode = string.Empty;
                                        if (readAsCharArray)
                                        {

                                            await buffer.ReadAsync(chr);
                                            foreach (char c in chr)
                                            {

                                                if (c == '\0')
                                                {
                                                    break;
                                                }

                                                barcode += c;
                                            }

                                        }
                                        else
                                        {
                                            barcode = await buffer.ReadLineAsync();
                                        }

                                        if (barcode.Length > 0)
                                        {
                                            Debug.WriteLine("Letto: " + barcode);
                                            MessagingCenter.Send<App, string>((App)Application.Current, "Barcode", barcode);
                                        }
                                        else
                                        {
                                            Debug.WriteLine("No data");
                                        }
                                    }
                                    else
                                    {
                                        Debug.WriteLine("No data to read");
                                        using (var ost = BthSocket.OutputStream)
                                        {
                                            var _ost = (ost as OutputStreamInvoker).BaseOutputStream;
                                            var message = $"Hello form Android application!\n{DateTime.Now}\n".ToCharArray().Select(c=>(byte)c).ToArray();
                                            _ost.Write(message, 0, message.Length);
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
            if (_ct != null)
            {
                Debug.WriteLine("Send a cancel to task!");
                _ct.Cancel();
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


        #endregion
    }
}

