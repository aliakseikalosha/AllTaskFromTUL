using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DataGrabber
{
    public interface IBluetoothReader
    {
        Action<BluetoothMessage> OnMessageUpdated { get; set; }
        List<BluetoothMessage> Sended { get; }
        List<BluetoothMessage> Recived { get; }
        List<BluetoothMessage> All { get; }
        void Send(BluetoothMessage message, bool isSabvoton);
        void Start(string name, int sleepTime, bool isSabvoton);
        void Cancel();
        void ClearFront(bool isSabvoton);
        Task<ObservableCollection<string>> PairedDevices();
    }
}