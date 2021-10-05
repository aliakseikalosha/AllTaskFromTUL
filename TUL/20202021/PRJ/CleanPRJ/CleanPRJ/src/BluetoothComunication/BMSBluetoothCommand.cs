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
            BluetoothManager.I.Send(new List<byte>() { 0xDD, 0xA5, cellDataCode, 0x00, 0xFF, 0xFC, 0x77 });//DD A5 03 00 FF FD 77
            //SendCommand(true, cellDataCode, 0, emptyData);
            currentCellsData = null;
        }

        public static void SendGetBaseInfo()
        {

            BluetoothManager.I.Send(new List<byte>() { 0xDD, 0xA5, baseInfoCode, 0x00, 0xFF, 0xFD, 0x77 });
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
            var data = recived.Range(4, recived.Length);
            var partState = new T();
            partState.FillData(data);
            partState.FillSourceData(recived);
            return partState;
        }

        public static T[] Range<T>(this T[] data, int from, int to)
        {
            if (to - from > 0 && from >= 0 && to <= data.Length)
            {
                var range = new T[to - from];
                for (int i = from; i < to; i++)
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
    void FillSourceData(byte[] data);
}

public abstract class StateData : IBMSStateData
{
    public string Data { get; protected set; } = "";
    public string SourceData { get; protected set; } = "";
    public string HumanData { get; protected set; } = "";


    public abstract void FillData(byte[] data);

    protected ushort Convert(byte high, byte low)
    {
        return (ushort)((high << 8) | (low));
    }

    public override string ToString()
    {
        return $"{SourceData},{Data}";
    }

    public void FillSourceData(byte[] data)
    {
        SourceData = "";
        for (int i = 0; i < data.Length; i++)
        {
            SourceData += data[i] + (i == data.Length - 1 ? "" : ",");
        }
    }

    protected void AddData(string message, string data)
    {
        Data += (string.IsNullOrEmpty(Data) ? "" : ",") + data;
        HumanData += $"{message}:{data}\n";
    }
}

public class CellsStateData : StateData
{
    public float[] Voltage = null;
    public override void FillData(byte[] data)
    {
        int count = BaseInfoStateData.NumberOfCell;
        if (count < 0)
        {
            return;
        }
        Voltage = new float[count];
        for (int i = 4; i < count; i++)
        {
            Voltage[i] = Convert(data[i * 2], data[i * 2 + 1]) / 1000f;
            Data += Voltage[i] + (i == count - 1 ? "" : ",");
            AddData($"{nameof(Voltage)}{i - 4}", Voltage[i].ToString());
        }
    }
}

public class BaseInfoStateData : StateData
{
    public static int NumberOfCell = -1;

    public float FullVoltage { get; protected set; }
    public float Current { get; protected set; }
    public uint ResidualCapacity { get; protected set; }
    public uint NominalCapacity { get; protected set; }
    public int Cycles { get; protected set; }
    public int SoC { get; protected set; }
    public int NumberOfTemperature { get; protected set; }
    public int[] Temperatures { get; protected set; }


    public override void FillData(byte[] data)
    {
        FullVoltage = Convert(data[0], data[1]) / 100f;
        Current = Convert(data[2], data[3]) / 100f;
        ResidualCapacity = Convert(data[4], data[5]);
        NominalCapacity = Convert(data[6], data[7]);
        Cycles = Convert(data[8], data[9]);
        SoC = (int)data[19];
        NumberOfCell = (int)data[21];
        NumberOfTemperature = (int)data[22];
        Temperatures = new int[NumberOfTemperature];
        Data = "";
        AddData(nameof(FullVoltage), FullVoltage.ToString());
        AddData(nameof(Current), Current.ToString());
        AddData(nameof(ResidualCapacity), ResidualCapacity.ToString());
        AddData(nameof(NominalCapacity), NominalCapacity.ToString());
        AddData(nameof(SoC), SoC.ToString());
        AddData(nameof(NumberOfTemperature), NumberOfTemperature.ToString());
        for (int i = 0; i < NumberOfTemperature; i++)
        {
            Temperatures[i] = (Convert(data[23 + i * 2], data[23 + i * 2 + 1]) - 2731) / 10;
            AddData($"{nameof(Temperatures)}{i}", Temperatures[i].ToString());
        }
    }
}