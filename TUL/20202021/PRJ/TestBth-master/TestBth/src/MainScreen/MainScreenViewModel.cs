using System;

namespace TestBth.MainScreen
{
    public class MainScreenViewModel
    {
        
        public float AvrgSpeed { get; protected set; }
        public float AvrgRideDistance { get; protected set; }
        public float TotalRideDistance { get; protected set; }

        public float CurrentBatteryCharge { get; protected set; }
        public DateTime FullyChargedTime { get; private set; }

        public MainScreenViewModel()
        {
            UpdateData();
        }

        private void UpdateData()
        {
            //test data
            var rnd = new Random();
            AvrgSpeed = (float)rnd.NextDouble() * 95 + 5;
            AvrgRideDistance = (float)rnd.NextDouble() * 18 + 2;
            TotalRideDistance = (float)rnd.NextDouble() * 1000 + AvrgRideDistance;
            CurrentBatteryCharge = ((float)rnd.NextDouble() * 95 + 5)/100;
            FullyChargedTime = DateTime.Today.AddHours(rnd.Next(1, 24));
        }
    }
}
