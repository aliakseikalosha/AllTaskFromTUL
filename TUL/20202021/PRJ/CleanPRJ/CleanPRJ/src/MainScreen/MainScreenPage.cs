using CleanPRJ.src.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CleanPRJ.MainScreen
{
    public class MainScreenPage : PageWithBottomMenu
    {
        private MainScreenViewModel model;

        public MainScreenPage(MainScreenViewModel model)
        {
            this.model = model;
            InitUI();
        }

        protected override void InitUI()
        {
            var avrgSpeed = SmallBlock($"Average Speed:\n{model.AvrgSpeed:.00} km/h");
            var avrgRideDistance = SmallBlock($"Average Ride:\n{model.AvrgSpeed:.00}km");
            var totalRideDistance = SmallBlock($"Total Ride:\n{model.TotalRideDistance:.0}km");

            var currentCharge = SmallBlock($"Battery charge : {Math.Round(model.CurrentBatteryCharge * 100)}%");
            var chargeEstimate = SmallBlock(model.FullyChargedTime > DateTime.Now ? $"Battery will be charged in :\n {(model.FullyChargedTime - DateTime.Now)}" : "Battery is fully charged.");


            int topPadding = Device.RuntimePlatform == Device.iOS ? 20 : 0;
            StackLayout avrgData = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(10, 10),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children = { avrgSpeed, avrgRideDistance, totalRideDistance }
            };
            StackLayout chargeData = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(10, 10),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children = { currentCharge, chargeEstimate }
            };
            StackLayout sl = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(10, 10),
                Children = { avrgData, chargeData },
                Padding = new Thickness(0, topPadding, 0, 0)
            };
            Content = new StackLayout { Children = { sl, BottomButtonUI(typeof(MainScreenPage)) }, BackgroundColor = WindowData.Current.Background };
        }
    }
}
