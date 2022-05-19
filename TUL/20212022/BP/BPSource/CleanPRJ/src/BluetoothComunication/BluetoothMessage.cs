using System;
using System.Linq;

namespace DataGrabber.src.BluetoothComunication
{
    public class BluetoothMessage
    {
        public readonly static char Separator = '|';
        public DateTime Date { get; private set; }
        public string Message { get; private set; }
        public MessageState State { get; private set; }
        public char Type => (char)ByteData[1];//Message.Length > 0 ? (char)ByteData[3] : '\0';

        public byte[] ByteData { get; set; }

        public BluetoothMessage(DateTime date, string message, MessageState state)
        {
            Date = date;
            Message = message;
            State = state;
        }

        public BluetoothMessage(byte[] command, MessageState state)
        {
            Date = DateTime.Now;
            ByteData = command;
            Message = command.Select(c => c.ToString("X")).Aggregate((a, b) => a +" "+ b);
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