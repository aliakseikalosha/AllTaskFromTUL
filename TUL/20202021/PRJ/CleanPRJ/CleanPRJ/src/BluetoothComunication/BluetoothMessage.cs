using CleanPRJ.src.BluetoothComunication;
using System;

namespace CleanPRJ
{
    public class BluetoothMessage
    {
        public DateTime Date { get; private set; }
        public string Message { get; private set; }
        public MessageState State { get; private set; }
        public char Type => Message.Length > 0 ? Message[0] : '\0';
        public string Data
        {
            get
            {
                var data = Message.Split(BluetoothCommand.Separator);
                if (data.Length < 1)
                {
                    return null;
                }
                return data[1];
            }
        }


        public BluetoothMessage(DateTime date, string message, MessageState state)
        {
            Date = date;
            Message = message;
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