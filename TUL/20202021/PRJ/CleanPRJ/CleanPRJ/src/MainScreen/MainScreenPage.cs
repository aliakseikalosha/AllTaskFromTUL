using DataGrabber.src.UI;
using Microcharts;
using System;
using Xamarin.Forms;

namespace DataGrabber.MainScreen
{
    public class MainScreenPage : ApplicationPage<MainScreenModel>
    {
        public MainScreenPage(MainScreenModel model) : base(model) { }
        public override void InitUI()
        {
            model.Init();
            var battery = MediumWidget("Battery", GetViewFor(GetChartFor<LineChart>(model.BatteryCharge)), () => OnChangePageCliked?.Invoke(typeof(Statistics.StaticticsBattery)));
            var distace = MediumWidget("Distance", GetViewFor(GetChartFor<BarChart>(model.RideDistance)), () => OnChangePageCliked?.Invoke(typeof(Statistics.StaticticsDistance)));
            StackLayout widgets = new StackLayout
            {
                Children = { battery, distace },
                Orientation = StackOrientation.Vertical,
            };
            var topline = TopLine("Main Menu", false, true);
            Content = new StackLayout
            {
                Children = { topline, widgets },
                BackgroundColor = WindowData.Current.Background.Background,
            };
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}