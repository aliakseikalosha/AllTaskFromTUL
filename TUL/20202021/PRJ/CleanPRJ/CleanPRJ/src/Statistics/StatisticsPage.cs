using System.Collections.Generic;
using CleanPRJ.MainScreen;
using Xamarin.Forms;
using Microcharts;
using Microcharts.Forms;
using CleanPRJ.src.UI;

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
            var lable = new Label { Text = "Statistics", BackgroundColor = WindowData.Current.Background, TextColor = WindowData.Current.BackgroundText };
            Content = new StackLayout
            {
                Children = {
                    lable,
                    GetPlotFor(model.BatteryCharge, "Battery Charge"),
                    GetPlotFor(model.RideDistance, "Distance per Day"),
                    BottomButtonUI(typeof(StatisticsPage))
                },
                BackgroundColor = WindowData.Current.Background,
            };
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