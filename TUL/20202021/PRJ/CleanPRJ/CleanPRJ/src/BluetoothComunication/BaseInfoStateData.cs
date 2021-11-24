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
        Current = ConvertSign(data[2], data[3]) / 100f;
        ResidualCapacity = Convert(data[4], data[5]);
        NominalCapacity = Convert(data[6], data[7]);
        Cycles = Convert(data[8], data[9]);
        SoC = (int)data[19];
        NumberOfCell = (int)data[21] - 4;
        NumberOfTemperature = (int)data[22];
        Temperatures = new int[NumberOfTemperature];
        AddData("FullVoltage", FullVoltage.ToString("F3"));
        AddData("Current", Current.ToString("F3"));
        AddData("ResidualCapacity", ResidualCapacity.ToString());
        AddData("NominalCapacity", NominalCapacity.ToString());
        AddData("Cycles", Cycles.ToString());
        AddData("SoC", SoC.ToString());
        AddData("NumberOfCell", NumberOfCell.ToString());
        AddData("NumberOfTemperature", NumberOfTemperature.ToString());
        for (int i = 0; i < NumberOfTemperature; i++)
        {
            Temperatures[i] = (Convert(data[23 + i * 2], data[23 + i * 2 + 1]) - 2731) / 10;
            AddData($"Temperatures {i}", Temperatures[i].ToString());
        }
    }

}