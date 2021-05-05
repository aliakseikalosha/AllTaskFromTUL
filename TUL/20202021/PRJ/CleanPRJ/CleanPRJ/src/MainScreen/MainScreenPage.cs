using CleanPRJ.src.UI;
using Microcharts;
using System;
using Xamarin.Forms;

namespace CleanPRJ.MainScreen
{
    public class MainScreenPage : ApplicationPage<MainScreenViewModel>
    {
        public MainScreenPage(MainScreenViewModel model) : base(model) { }
        public override void InitUI()
        {
            var battery = MediumWidget("Battery", GetViewFor(GetChartFor<LineChart>(model.BatteryCharge)), () => OnChangePageCliked?.Invoke(typeof(Statistics.StaticticsBattery)));
            var distace = MediumWidget("Distance", GetViewFor(GetChartFor<BarChart>(model.RideDistance)), () => OnChangePageCliked?.Invoke(typeof(Statistics.StaticticsDistance)));
            StackLayout widgets = new StackLayout
            {
                Children = { battery, distace }
            };

            ScrollView scrollView = new ScrollView
            {
                Content = widgets,
                VerticalScrollBarVisibility = ScrollBarVisibility.Never,
                BackgroundColor = WindowData.Current.Background.Background,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(0, 10),
                Margin = new Thickness(0, 0),
            };
            var topline = TopLine("Main Menu", false, true);
            topline.WidthRequest = 1000;
            topline.HorizontalOptions = LayoutOptions.CenterAndExpand;

            Content = new StackLayout { Children = { topline, scrollView }, BackgroundColor = WindowData.Current.Background.Background };
        }
    }
}