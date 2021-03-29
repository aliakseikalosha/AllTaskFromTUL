using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CleanPRJ
{
    public interface IBluetoothReader
    {
        Action OnMessageUpdated { get; set; }
        List<BluetoothMessage> Sended { get; }
        List<BluetoothMessage> Recived { get; }
        List<BluetoothMessage> All { get; }
        void Send(BluetoothMessage message);
        void Start(string name, int sleepTime, bool readAsCharArray);
        void Cancel();
        ObservableCollection<string> PairedDevices();
    }
}