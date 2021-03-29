using System.Collections.Generic;
using CleanPRJ.MainScreen;
using Xamarin.Forms;
using Microcharts;
using Microcharts.Forms;

namespace CleanPRJ.Statistics
{
    public class StatisticsPage : PageWithBottomMenu
    {
        private StatisticsViewModel model;
        public StatisticsPage(StatisticsViewModel model)
        {
            this.model = model;
            InitUI();
        }

        protected override void InitUI()
        {
            var lable = new Label { Text = "Statistics" };
            Content = new StackLayout { Children = { lable, GetPlotFor(model.BatteryCharge, "Battery Charge"), GetPlotFor(model.RideDistance, "Distance per Day"), BottomButtonUI(typeof(StatisticsPage)) } };
        }

        private ChartView GetPlotFor(List<ChartEntry> points, string title)
        {
            foreach (var point in points)
            {
                point.ValueLabel = title;
            }
            var chart = new LineChart { Entries = points };
            return new ChartView
            {
                Chart = chart,
            };
        }
    }
}