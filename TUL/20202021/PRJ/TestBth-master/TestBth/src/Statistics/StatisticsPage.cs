using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestBth.MainScreen;
using Xamarin.Forms;

namespace TestBth.Statistics
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
            var grafs = new StackLayout
            {
                Children = { GetPlotFor(model.BatteryCharge, "Battery Charge"), GetPlotFor(model.RideDistance, "Distance per Day") },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            var scroll = new ScrollView { Content = grafs, IsVisible = true };
            int topPadding = Device.RuntimePlatform == Device.iOS ? 20 : 0;
            StackLayout sl = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = { lable, GetPlotFor(model.BatteryCharge, "Battery Charge"), GetPlotFor(model.RideDistance, "Distance per Day") },
                Padding = new Thickness(0, topPadding, 0, 0)
            };
            Content = new StackLayout { Children = { GetPlotFor(model.BatteryCharge, "Battery Charge"), GetPlotFor(model.RideDistance, "Distance per Day"), BottomButtonUI(typeof(StatisticsPage)) } };
        }

        private PlotView GetPlotFor(List<DataPoint> points, string title)
        {
            var model = new PlotModel { Title = title, SelectionColor = OxyColor.FromRgb(0, 0, 0) };
            model.Series.Add(new LineSeries { ItemsSource = points });

            return new PlotView
            {
                Model = model,

                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
        }
    }

    public class StatisticsViewModel
    {
        public List<DataPoint> BatteryCharge { get; internal set; } = new List<DataPoint>();
        public List<DataPoint> RideDistance { get; internal set; } = new List<DataPoint>();

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
                BatteryCharge.Add(new DataPoint(i, charge));
                RideDistance.Add(new DataPoint(i * 7 + rnd.Next(7), rnd.NextDouble() * 100));
            }
        }
    }
}