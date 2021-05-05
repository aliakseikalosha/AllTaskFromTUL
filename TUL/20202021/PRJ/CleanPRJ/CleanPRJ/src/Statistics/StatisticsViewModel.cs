using System.Collections.Generic;
using CleanPRJ.src.Data;
using CleanPRJ.src.UI;
using Microcharts;
using SkiaSharp;

namespace CleanPRJ.Statistics
{
    public class StatisticsViewModel : IViewModel
    {
        public List<ChartEntry> BatteryCharge { get; internal set; } = new List<ChartEntry>();
        public List<ChartEntry> RideDistance { get; internal set; } = new List<ChartEntry>();

        public StatisticsViewModel()
        {
            FillData();
        }

        private void FillData()
        {
            BatteryCharge = DataHelper.MockupBateryData.ChargeLevel.ConverToChartEntry(c => new ChartEntry(c.Data) { Label = $"{c.DateUTC:t}", ValueLabel = $"{c.Data}", Color = SKColor.Parse("#00F000") });
            RideDistance = DataHelper.MockupTravelData.Distance.ConverToChartEntry(c => new ChartEntry(c.Data) { Label = $"{c.DateUTC:d}", ValueLabel = $"{c.Data}", Color = SKColor.Parse(WindowData.Current.ChartColorCode.Random()) });
        }
    }
}