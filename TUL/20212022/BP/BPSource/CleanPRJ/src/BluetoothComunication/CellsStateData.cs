namespace DataGrabber.src.BluetoothComunication
{
    public class CellsStateData : StateData
    {
        public float[] Voltage = null;
        public override void FillData(byte[] data)
        {
            int count = BaseInfoStateData.NumberOfCell + 4;
            if (count < 0)
            {
                return;
            }
            Voltage = new float[count];
            for (int i = 4; i < count; i++)
            {
                Voltage[i] = Convert(data[i * 2], data[i * 2 + 1]) / 1000f;
                AddData($"Voltage {i - 4}", Voltage[i].ToString("F3", System.Globalization.CultureInfo.InvariantCulture), " V");
            }
        }
    }
}
