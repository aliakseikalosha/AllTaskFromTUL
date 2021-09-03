using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanPRJ.src.BluetoothComunication
{
    public static class BMSBluetoothCommand
    {
        public static readonly byte cellDataCode = 0x04;
        public static byte baseInfoCode = 0x03;
        public static byte start = 0xDD;
        public static byte end = 0x77;
        private static readonly byte[] emptyData = new byte[] { };

        public static CellsStateData currentCellsData { get; private set; } = null;
        public static BaseInfoStateData currentBaseInfo { get; private set; } = null;
        

        public static void SendGetCellDataCommand()
        {
            BluetoothManager.I.Send(new List<byte>() { 0xDD, 0xA5, 0x03, 0x00, 0xFF, 0xFD, 0x77 });//DD A5 03 00 FF FD 77
            //SendCommand(true, cellDataCode, 0, emptyData);
            currentCellsData = null; 
        }

        public static void SendGetBaseInfo()
        {

            BluetoothManager.I.Send(new List<byte>() { 0xDD, 0xA5, 0x04, 0x00, 0xFF, 0xFC, 0x77 });
            //SendCommand(true, baseInfoCode, 0, emptyData);
            currentBaseInfo = null;
        }

        public static void SendCommand(bool read, byte command, byte count, byte[] data)
        {
            var checksum = CheckSum(count, data);

            var fullCommand = new List<byte>();
            fullCommand.Add(0xDD);
            fullCommand.Add((byte)(read ? 0xA5 : 0x5A));
            fullCommand.Add(command);
            fullCommand.Add(count);
            fullCommand.AddRange(data);
            fullCommand.Add(checksum[0]);
            fullCommand.Add(checksum[1]);
            fullCommand.Add(0x77);

            BluetoothManager.I.Send(fullCommand);
        }

        private static byte[] CheckSum(byte count, byte[] data)
        {
            ushort checkSum = ushort.MaxValue;
            checkSum -= count;
            foreach (var item in data)
            {
                checkSum -= item;
            }
            return new byte[] { (byte)(checkSum >> 8), (byte)((checkSum) ^ (checkSum >> 8)) };
        }

        public static void GetResponceCellData(BluetoothMessage message)
        {
            currentCellsData = ParseResponce<CellsStateData>(message.BMSData);
        }

        public static void GetResponceBaseInfo(BluetoothMessage message)
        {
            currentBaseInfo = ParseResponce<BaseInfoStateData>(message.BMSData);
        }

        public static T ParseResponce<T>(byte[] recived) where T : IBMSStateData, new()
        {
            if (recived[3] == 0)
            {
                return default;
            }
            var data = recived.Range(4, recived.Length - 4);
            var partState = new T();
            partState.FillData(data);
            return partState;
        }

        public static T[] Range<T>(this T[] data, int from, int to)
        {
            if (to - from > 0 && from >= 0 && to < data.Length)
            {
                var range = new T[to - from];
                for (int i = from; i <= to; i++)
                {
                    range[i - from] = data[i];
                }
                return range;
            }
            return null;
        }
    }
}

public interface IBMSStateData
{
    string Data { get; }
    void FillData(byte[] data);
}

public abstract class StateData : IBMSStateData
{
    public string Data { get; protected set; } = "";

    public abstract void FillData(byte[] data);

    protected ushort Convert(byte high, byte low)
    {
        return (ushort)((high << 8) | (low));
    }

    public override string ToString()
    {
        return Data;
    }
}

public class CellsStateData : StateData
{
    public override void FillData(byte[] data)
    {
        ushort count = Convert(data[0], data[1]);
        Data = Convert(data[2], data[3]).ToString();
        for (int i = 4; i < (count) * 2; i += 2)
        {
            Data += $",{Convert(data[i], data[i + 1])}";
        }
    }
}

public class BaseInfoStateData : StateData
{
    public override void FillData(byte[] data)
    {
        Data = "";
        int count = 8;
        int offset = 0;
        int i = 0;
        for (; i < (count) * 2; i += 2)
        {
            Data += $"{Convert(data[i], data[i + 1])},";
        }
        count = i + 5;
        for (; i < count; i++)
        {
            Data += $"{data[i + offset]},";
        }
        Data += $"{Convert(data[i], data[i + 1])}";
    }
}