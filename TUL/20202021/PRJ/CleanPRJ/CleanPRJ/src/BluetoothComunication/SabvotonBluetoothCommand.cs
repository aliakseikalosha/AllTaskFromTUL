namespace DataGrabber.src.BluetoothComunication
{
    public static class SabvotonBluetoothCommand
    {
        public static SabvotonData SabvotonData { get; internal set; }
        public static readonly char CommandKey = 'S';
        public static byte start = 0x01;
        public static byte end = 0xD1;
        private static readonly byte[] startConversation = { 0x01, 0x03, 0x0A, 0xBC, 0x00, 0x1A, 0x06, 0x3D };
        private static readonly byte[] dataRequest = { 0x01, 0x06, 0x0F, 0xC7, 0x34, 0x21, 0xED, 0xFB };

        public static void StartConversation()
        {
            Send(startConversation);
        }

        public static void SendGetDataCommand()
        {
            Send(dataRequest);
        }

        private static void Send(byte[] data)
        {
            BluetoothManager.I.Send(data, true);
        }

        public static void GetResponce(BluetoothMessage message)
        {
            SabvotonData = new SabvotonData();
            SabvotonData.FillData(message.ByteData);
            SabvotonData.FillSourceData(message.ByteData);
        }
    }
}
