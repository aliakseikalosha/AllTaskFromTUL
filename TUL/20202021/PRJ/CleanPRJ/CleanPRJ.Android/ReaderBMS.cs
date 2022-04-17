using System;
using Xamarin.Forms;
using System.Collections.Generic;
using DataGrabber.src.BluetoothComunication;
using Plugin.BLE.Abstractions.Contracts;

[assembly: Dependency(typeof(DataGrabber.Android.AndroidBluetoothReader))]
namespace DataGrabber.Android
{
    public class ReaderBMS : Reader
    {
        public ReaderBMS(IAdapter adapter)
        {
            this.adapter = adapter;
            serviceGuid = new Guid("0000ff00-0000-1000-8000-00805f9b34fb");
            sendGuid = new Guid("0000ff02-0000-1000-8000-00805f9b34fb");
            reciveGuid = new Guid("0000ff01-0000-1000-8000-00805f9b34fb");
        }

        protected override List<int> CombineMessage(List<int> message, int next)
        {
            if (message.Count == 0 && next != BMSBluetoothCommand.start)
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
            return message[0] == BMSBluetoothCommand.start && message[^1] == BMSBluetoothCommand.end;
        }
    }
}
