using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;
using TestBluetooth.iOS;
using ExternalAccessory;
using System.Linq;

[assembly: Xamarin.Forms.Dependency(typeof(Bth))]
namespace TestBluetooth.iOS
{
    public class Bth : IBluetoothReader
    {


        public Bth()
        {
        }

        public void Cancel()
        {
            //throw new NotImplementedException();
        }

        public ObservableCollection<string> PairedDevices()
        {
            //throw new NotImplementedException();

            EAAccessoryManager manager = EAAccessoryManager.SharedAccessoryManager;
            var allaccessorries = manager.ConnectedAccessories;
            foreach (var accessory in allaccessorries)
            {
                //yourlable.Text = "find accessory";
                Console.WriteLine(accessory.ToString());
                Console.WriteLine(accessory.Name);
                var protocol = "com.Yourprotocol.name";

                if (accessory.ProtocolStrings.Where(s => s == protocol).Any())
                {
                    //yourlable.Text = "Accessory  found";
                    //start session
                    var session = new EASession(accessory, protocol);
                    //var outputStream = session.OutputStream;
                    //outputStream.Delegate = new MyOutputStreamDelegate(yourlable);
                    //outputStream.Schedule(NSRunLoop.Current, "kCFRunLoopDefaultMode");
                    //outputStream.Open();
                }
            }

            return new ObservableCollection<string>() { "AAA", "BBB" };
        }

        public void Start(string name, int sleepTime, bool readAsCharArray)
        {
            //throw new NotImplementedException();

        }
    }
}

