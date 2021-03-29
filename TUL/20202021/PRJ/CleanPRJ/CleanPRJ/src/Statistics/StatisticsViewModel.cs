using System;
using System.Collections.Generic;
using Microcharts;
using SkiaSharp;

namespace CleanPRJ.Statistics
{
    public class StatisticsViewModel
    {
        public List<ChartEntry> BatteryCharge { get; internal set; } = new List<ChartEntry>();
        public List<ChartEntry> RideDistance { get; internal set; } = new List<ChartEntry>();

        public StatisticsViewModel()
        {
            FillData();
        }

        private void FillData()
        {
            Random rnd = new Random();
            var n = rnd.Next(10) + 10;
            var charge = rnd.Next(40, 101);
            for (int i = 0; i < n; i++)
            {
                charge = Math.Max(0, Math.Min(100, charge + rnd.Next(-4, +1)));
                BatteryCharge.Add(new ChartEntry(charge)
                {
                    Label = "UWP",
                    ValueLabel = "112",
                    Color = SKColor.Parse("#2c3e50")
                });
                RideDistance.Add(new ChartEntry((float)rnd.NextDouble() * 100)
                {
                    Label = "UWP",
                    ValueLabel = "112",
                    Color = SKColor.Parse("#ccdd50")
                });
            }
        }
    }
}