using System.Collections.Generic;
using CleanPRJ.MainScreen;
using Xamarin.Forms;
using Microcharts;
using CleanPRJ.src.UI;

namespace CleanPRJ.Statistics
{
    public abstract class StatisticsPage : ApplicationPage<StatisticsViewModel>
    {
        public StatisticsPage(StatisticsViewModel model) : base(model) { }
        public override void InitUI()
        {
            Content = new StackLayout
            {
                Children = {
                    Top(),
                    Chart(),
                    Desctiption()
                },
                BackgroundColor = WindowData.Current.Background.Background,
            };
        }
        protected abstract StackLayout Top();
        protected abstract StackLayout Chart();
        protected abstract StackLayout Desctiption();
    }

    public class StaticticsBattery : StatisticsPage
    {
        public StaticticsBattery(StatisticsViewModel model) : base(model) { }

        protected override StackLayout Chart()
        {
            var chart = GetViewFor(GetChartFor<LineChart>(model.BatteryCharge), WindowData.ScreenSize.X, WindowData.ScreenSize.X * 3);
            var scroll = new ScrollView { Content = chart, Orientation = ScrollOrientation.Horizontal };
            return new StackLayout { Children = { scroll } };
        }

        protected override StackLayout Desctiption()
        {
            return new StackLayout { Children = { } };
        }

        protected override StackLayout Top()
        {
            return TopLine("Battery");
        }
    }
    public class StaticticsDistance : StatisticsPage
    {
        public StaticticsDistance(StatisticsViewModel model) : base(model) { }

        protected override StackLayout Chart()
        {
            var chart = GetViewFor(GetChartFor<BarChart>(model.RideDistance), WindowData.ScreenSize.X, WindowData.ScreenSize.X * 3);
            var scroll = new ScrollView { Content = chart, Orientation = ScrollOrientation.Horizontal };
            return new StackLayout { Children = { scroll } };
        }

        protected override StackLayout Desctiption()
        {
            return new StackLayout { Children = { } };
        }

        protected override StackLayout Top()
        {
            return TopLine("Distance");
        }
    }
}