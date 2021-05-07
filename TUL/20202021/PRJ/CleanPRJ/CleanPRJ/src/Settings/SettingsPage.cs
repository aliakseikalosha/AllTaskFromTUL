using CleanPRJ.MainScreen;
using CleanPRJ.src.UI;
using System;
using Xamarin.Forms;

namespace CleanPRJ.Settings
{
    public class SettingsPage : ApplicationPage<SettingsVievModel>
    {
        public SettingsPage(SettingsVievModel model) : base(model) { }
        public override void InitUI()
        {
            model.Init();
            BackgroundColor = WindowData.Current.Background.Background;
            Content = new StackLayout
            {
                Children = { TopLine("Settings", true, false), AllSettings() }
            };
        }
        protected ScrollView AllSettings()
        {
            var stack = new StackLayout { Orientation = StackOrientation.Vertical };
            foreach (var data in model.AllSettings)
            {
                stack.Children.Add(SettingsView(data));
            }
            var scroll = new ScrollView
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                BackgroundColor = WindowData.Current.Background.Background,
                Content = stack,
            };
            return scroll;
        }
        protected Frame SettingsView(SettingsVievModel.SettingsData data)
        {
            var label = new Label()
            {
                Text = data.Label + ":\t" + data.CurrentState(),
                BackgroundColor = WindowData.Current.Background.Background,
                TextColor = WindowData.Current.Background.Text,
                WidthRequest = WindowData.ScreenSize.X,
                HeightRequest = 46,
            };
            var guestRecognizer = new TapGestureRecognizer();
            guestRecognizer.Tapped += (s, e) =>
            {
                label.Text = data.Label + ":\t" + data.CurrentState();
                data.OnClicked?.Invoke();
            };
            label.GestureRecognizers.Add(guestRecognizer);
            return new Frame
            {
                Content = label,
                BorderColor = WindowData.Current.Wiget.Border,
                BackgroundColor = WindowData.Current.Wiget.Background,
            };
        }
    }
}