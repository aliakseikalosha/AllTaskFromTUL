using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CleanPRJ.src.BluetoothComunication;

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
            currentCellsData = ParseResponce<CellsStateData>(message.ByteData);
        }

        public static void GetResponceBaseInfo(BluetoothMessage message)
        {
            currentBaseInfo = ParseResponce<BaseInfoStateData>(message.ByteData);
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


    public static class SabvotonBluetoothCommand
    {
        public static SabvotonData SabvotonData { get; internal set; }
        public static readonly char CommandKey = 'S';

        public static void StartConversation()
        {
            BluetoothManager.I.Send(new List<byte> { 0x01, 0x03, 0x0A, 0xBC, 0x00, 0x1A, 0x06, 0x3D });
        }

        public static void SendGetDataCommand()
        {
            BluetoothManager.I.Send(new List<byte> { 0x01, 0x06, 0x0F, 0xC7, 0x34, 0x21, 0xED, 0xFB });
        }

        public static void GetResponce(BluetoothMessage message)
        {
            SabvotonData = new SabvotonData();
            SabvotonData.FillData(message.ByteData);
            SabvotonData.FillSourceData(message.ByteData);
        }
    }

    public class SabvotonData : StateData
    {
        public override void FillData(byte[] data)
        {
            int i = 3;
            AddData("SysStatus", (Convert(data[i++], data[i++])).ToString());
            AddData("BatteryVoltage", (Convert(data[i++], data[i++]) / 54.6f).ToString());
            AddData("WeakenCurrentCMD", (Convert(data[i++], data[i++])).ToString());
            AddData("WeakenCurrentFBK", (Convert(data[i++], data[i++])).ToString());
            AddData("TorqueCurrentCMD", (Convert(data[i++], data[i++])).ToString());
            AddData("TorqueCurrentFBK", (Convert(data[i++], data[i++])).ToString());
            AddData("ControllerTemp", (Convert(data[i++], data[i++])).ToString());
            AddData("MotorTemp", (Convert(data[i++], data[i++])).ToString());
            AddData("MotorAngle", (Convert(data[i++], data[i++])).ToString());
            AddData("MotorSpeed", (Convert(data[i++], data[i++])).ToString());
            AddData("HallStatus", (Convert(data[i++], data[i++])).ToString());
            AddData("ThrottleVoltage", (Convert(data[i++], data[i++])).ToString());
            AddData("MosfetStatus", (Convert(data[i++], data[i++])).ToString());
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
    public string CSVHeader { get; protected set; } = "BinData,Keyword,";


    public abstract void FillData(byte[] data);

    protected ushort Convert(byte high, byte low)
    {
        return (ushort)((high << 8) | (low));
    }

    protected short ConvertSign(byte high, byte low)
    {
        return (short)((high << 8) | (low));
    }

    public override string ToString()
    {
        return $"{SourceData},DATA,{Data}";
    }

    public void FillSourceData(byte[] data)
    {
        SourceData = "";
        for (int i = 0; i < data.Length; i++)
        {
            SourceData += $"{data[i]:X2}" + (i == data.Length - 1 ? "" : "|");
        }
    }

    protected void AddData(string message, string data, string dataType = null)
    {
        Data += $"{data.Replace(",", ".")},";
        HumanData += $"{message}:\t{data} {dataType ?? ""}\n";
        CSVHeader += $"{message},";
    }
}
