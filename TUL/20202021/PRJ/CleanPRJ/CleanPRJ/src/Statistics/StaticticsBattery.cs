using Xamarin.Forms;
using Microcharts;
using DataGrabber.src.UI;

namespace DataGrabber.Statistics
{
    public class StaticticsBattery : StatisticsPage
    {
        public StaticticsBattery(StatisticsModel model) : base(model) { }

        protected override StackLayout Chart()
        {
            var chart = GetViewFor(GetChartFor<LineChart>(model.BatteryCharge), WindowData.ScreenSize.X, WindowData.ScreenSize.X * 3);
            var scroll = new ScrollView { Content = chart, Orientation = ScrollOrientation.Horizontal };
            return new StackLayout { Children = { scroll } };
        }

        protected override StackLayout Desctiption()
        {
            var currentCharge = DescriptionLabel($"Current Charge {model.CurrentCharge} %");
            var minDistance = DescriptionLabel($"Minimal distance on current charge {model.MinTravelDistance} Km");
            var optimalDistance = DescriptionLabel($"Optimal distance on current charge {model.OptimalTravelDistance} Km");
            return new StackLayout { Children = { currentCharge, minDistance, optimalDistance }, Orientation = StackOrientation.Vertical, Margin = new Thickness(5, 0), Padding = new Thickness(0, 1) };
        }

        protected override StackLayout Top()
        {
            return TopLine("Battery");
        }
    }
}