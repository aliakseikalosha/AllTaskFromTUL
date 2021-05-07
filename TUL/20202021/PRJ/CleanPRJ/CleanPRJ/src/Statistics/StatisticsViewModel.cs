using System.Collections.Generic;
using System.Linq;
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
        public float CurrentCharge => BatteryCharge[BatteryCharge.Count - 1].Value;
        public float MinTravelDistance => Motorcycle.ConvertBatteryPercetToWatt(BatteryCharge[BatteryCharge.Count - 1].Value) / Motorcycle.maxWattPerKm;
        public float OptimalTravelDistance => Motorcycle.ConvertBatteryPercetToWatt(BatteryCharge[BatteryCharge.Count - 1].Value) / Motorcycle.optimalWattPerKm;

        public float MaxTravelDistance => RideDistance.Max(c => c.Value);
        public float AvgTravelDistance => RideDistance.Average(c => c.Value);

        public StatisticsViewModel()
        {
            Init();
        }

        public void Init()
        {
            BatteryCharge = DataHelper.MockupBateryData.ChargeLevel.ConverToChartEntry(c => new ChartEntry(c.Data) { Label = $"{c.DateUTC:t}", ValueLabel = $"{c.Data}", Color = SKColor.Parse("#00F000") });
            RideDistance = DataHelper.MockupTravelData.Distance.ConverToChartEntry(c => new ChartEntry(c.Data) { Label = $"{c.DateUTC:dd:M}", ValueLabel = $"{c.Data}" }, WindowData.Current.ChartColorCode);
        }
    }

    public class Motorcycle
    {
        public static readonly float TotalBatteryWatt = 300_000;
        public static readonly float maxWattPerKm = 5000;
        public static readonly float optimalWattPerKm = 2000;
        /// <summary>
        /// Convert battery percent to Watt
        /// </summary>
        /// <param name="percent">0..100 perent value</param>
        /// <returns></returns>
        public static float ConvertBatteryPercetToWatt(float percent)
        {
            return percent / 100 * TotalBatteryWatt;
        }
    }
}