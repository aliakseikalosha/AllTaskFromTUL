using CleanPRJ.src.Data;
using CleanPRJ.src.UI;
using Microcharts;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;

namespace CleanPRJ.MainScreen
{
    public class MainScreenViewModel : IViewModel
    {
        public List<ChartEntry> BatteryCharge { get; internal set; } = new List<ChartEntry>();
        public List<ChartEntry> RideDistance { get; internal set; } = new List<ChartEntry>();

        public MainScreenViewModel()
        {
            Init();
        }

        public void Init()
        {
            BatteryCharge = DataHelper.Battery.ChargeLevel.Last(5).ConverToChartEntry(c => new ChartEntry(c.Data) { Label = $"{c.DateUTC:t}", ValueLabel = $"{c.Data}", Color = SKColor.Parse("#00F000") });
            RideDistance = DataHelper.Travel.Distance.Last(5).ConverToChartEntry(c => new ChartEntry(c.Data) { Label = $"{c.DateUTC:dd:M}", ValueLabel = $"{c.Data}"}, WindowData.Current.ChartColorCode);
        }
    }
}
