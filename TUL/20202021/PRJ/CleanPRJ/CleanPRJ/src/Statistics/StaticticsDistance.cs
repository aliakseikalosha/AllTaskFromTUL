using Xamarin.Forms;
using Microcharts;
using DataGrabber.src.UI;

namespace DataGrabber.Statistics
{
    public class StaticticsDistance : StatisticsPage
    {
        public StaticticsDistance(StatisticsModel model) : base(model) { }

        protected override StackLayout Chart()
        {
            var chart = GetViewFor(GetChartFor<BarChart>(model.RideDistance), WindowData.ScreenSize.X, WindowData.ScreenSize.X * 3);
            var scroll = new ScrollView { Content = chart, Orientation = ScrollOrientation.Horizontal };
            return new StackLayout { Children = { scroll } };
        }

        protected override StackLayout Desctiption()
        {
            var avgDistance = DescriptionLabel($" Avarage travel distance per {model.AvgTravelDistance:F1} Km");
            var maxDistance = DescriptionLabel($" Max travel distance per {model.MaxTravelDistance:F1} Km");
            return new StackLayout { Children = { avgDistance, maxDistance, }, Orientation = StackOrientation.Vertical, Margin = new Thickness(5, 0), Padding = new Thickness(0, 1) };
        }

        protected override StackLayout Top()
        {
            return TopLine("Distance");
        }
    }
}