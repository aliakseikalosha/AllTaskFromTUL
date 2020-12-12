using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestBluetooth
{
    public interface IBluetoothReader
    {
        List<BluetoothMessage> Sended { get; }
        List<BluetoothMessage> Recived { get; }
        void Send(BluetoothMessage message);
        void Start(string name, int sleepTime, bool readAsCharArray);
        void Cancel();
        ObservableCollection<string> PairedDevices();
    }
}

