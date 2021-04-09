using System;

namespace CleanPRJ
{
    public class BluetoothMessage
    {
        public DateTime Date { get; private set; }
        public string Message { get; private set; }
        public MessageState State { get; private set; }
        public BluetoothMessage(DateTime date, string message, MessageState state)
        {
            Date = date;
            Message = message;// + "\0";
            State = state;
        }

        public override string ToString()
        {
            return $"message :\n{Message},date:\n{Date}";
        }

    }
    public enum MessageState
    {
        None = 0,
        Recived = 50,
        Sended = 100,
    }
}