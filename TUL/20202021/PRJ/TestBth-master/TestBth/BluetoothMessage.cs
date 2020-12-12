using System;

namespace TestBluetooth
{
    public class BluetoothMessage
    {
        public DateTime Date { get; private set; }
        public string Message { get; private set; }

        public BluetoothMessage(DateTime date, string message)
        {
            Date = date;
            Message = message;
        }

        public override string ToString()
        {
            return $"message :\n{Message},date:\n{Date}";
        }
    }
}