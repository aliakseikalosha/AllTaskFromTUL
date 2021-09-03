using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CleanPRJ
{
    public interface IBluetoothReader
    {
        Action<BluetoothMessage> OnMessageUpdated { get; set; }
        List<BluetoothMessage> Sended { get; }
        List<BluetoothMessage> Recived { get; }
        List<BluetoothMessage> All { get; }
        void Send(BluetoothMessage message);
        void Start(string name, int sleepTime, bool readAsCharArray);
        void Cancel();
        Task<ObservableCollection<string>> PairedDevices();
    }
}