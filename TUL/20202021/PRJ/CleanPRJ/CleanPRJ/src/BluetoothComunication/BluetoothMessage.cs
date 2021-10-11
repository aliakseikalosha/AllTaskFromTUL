using CleanPRJ.src.BluetoothComunication;
using System;
using System.Linq;

namespace CleanPRJ
{
    public class BluetoothMessage
    {
        public DateTime Date { get; private set; }
        public string Message { get; private set; }
        public MessageState State { get; private set; }
        public char Type => (char)BMSData[1];//Message.Length > 0 ? (char)BMSData[3] : '\0';
        public string Data
        {
            get
            {
                var data = Message.Split(BluetoothCommand.Separator);
                if (data.Length < 2)
                {
                    return null;
                }
                return data[1];
            }
        }

        public byte[] BMSData { get; set; }

        public BluetoothMessage(DateTime date, string message, MessageState state)
        {
            Date = date;
            Message = message;
            State = state;
        }

        public BluetoothMessage(byte[] command, MessageState state)
        {
            Date = DateTime.Now;
            BMSData = command;
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