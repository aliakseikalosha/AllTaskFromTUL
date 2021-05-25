using CleanPRJ.src.Data;
using System;
using System.Globalization;
using System.Linq;

namespace CleanPRJ.src.BluetoothComunication
{
    public static class BluetoothCommand
    {
        public static char Separator = '|';
        public static char DataSeparator = ';';

        public static readonly char[] CommandType = { 'B', 'D', 'T' };
        public static readonly char[] AllowedNotAlfanumericSymbols = { ';', '.', '|' };
        private static CultureInfo provider = new CultureInfo("en-US");
        public static void SendCall(string number)
        {
            BluetoothManager.I.Send($"T{Separator}{number}".PadRight(14) + Separator);
        }

        public static void EndCall()
        {
            BluetoothManager.I.Send($"T{Separator}end".PadRight(14) + Separator);
        }

        public static void SendGetBatteryData(DateTime from)
        {
            BluetoothManager.I.Send($"B{Separator}d{from:yyyyMMddHHmm}".PadRight(15) + Separator);
        }

        public static void SendGetDistanceData(DateTime from)
        {
            BluetoothManager.I.Send($"D{Separator}d{from:yyyyMMddHHmm}".PadRight(15) + Separator);
        }
        private static DateTime Parse(string time, bool withTime = false)
        {
            int year = int.Parse(time.Substring(4, 4));
            int month = int.Parse(time.Substring(2, 2));
            int day = int.Parse(time.Substring(0, 2));
            if (withTime)
            {
                int hour = int.Parse(time.Substring(8, 2));
                int minute = int.Parse(time.Substring(10, 2));
                return new DateTime(year, month, day, hour, minute, 0);
            }
            return new DateTime(year, month, day, 0, 0, 0);
        }

        public static void BatteryCommand(BluetoothMessage message)
        {
            foreach (var data in message.Data.Split(DataSeparator))
            {
                if (data.Trim().ToCharArray().All(char.IsDigit))
                {
                    var date = Parse(data, true);
                    var percent = int.Parse(data.Substring(12, 3));
                    DataHelper.AddBatteryCharge(date, percent);
                }
            }
            if (message.Data[0] == 'd')
            {
                var date = Parse(message.Data.Substring(1));
                if (date > DataHelper.Battery.ChargeLevel.Last().DateUTC)
                {
                    SendGetBatteryData(date);
                }
            }
        }

        public static void DistanceCommand(BluetoothMessage message)
        {
            foreach (var data in message.Data.Split(DataSeparator))
            {
                if (data.Trim().ToCharArray().All(c => char.IsDigit(c) || c == '.'))
                {
                    var date = Parse(data);
                    var distance = float.Parse(data.Substring(8, data.Length - 8), provider);
                    DataHelper.AddDistanceTraveled(date, distance);
                }
            }
            if (message.Data[0] == 'd')
            {
                var date = Parse(message.Data.Substring(1));
                if (date > DataHelper.Travel.Distance.Last().DateUTC)
                {
                    SendGetDistanceData(date);
                }
            }
        }
    }
}
