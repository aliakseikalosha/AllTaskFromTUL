using System;
using TestBluetooth;
using TestBth.Statistics;
using Xamarin.Forms;

namespace TestBth.MainScreen
{
    //todo add model as T
    public abstract class PageWithBottomMenu : ContentPage
    {
        //todo use enum?
        public Action<Type> OnChagePageCliked;
        protected abstract void InitUI();

        protected StackLayout BottomButtonUI(Type currentScreen)
        {
            var main = new Button { Text = "MainMenu", IsEnabled = currentScreen != typeof(MainScreenPage), };
            var statistic = new Button { Text = "Statistics", IsEnabled = currentScreen != typeof(StatisticsPage) };
            var test = new Button { Text = "TESTBLUETOOTH", IsEnabled = currentScreen != typeof(BluetoothComunicationPage) };
            main.Clicked += (obj, eventData) =>
            {
                OnChagePageCliked.Invoke(typeof(MainScreenPage));
            };
            statistic.Clicked += (obj, eventData) =>
            {
                OnChagePageCliked.Invoke(typeof(StatisticsPage));
            };
            test.Clicked += (obj, eventData) =>
            {
                OnChagePageCliked.Invoke(typeof(BluetoothComunicationPage));
            };
            return new StackLayout
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children = { new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { main, statistic, test } }
                }
            };
        }

        protected Frame SmallBlock(string text)
        {
            return new Frame
            {
                BorderColor = Color.DarkGray,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                CornerRadius = 100,
               // BackgroundColor = Color.LightBlue,
                Padding = 1,
                HasShadow = true,
                //Content = new Frame
                //{
                //   // BackgroundColor = Color.White,
                //    CornerRadius = 16,
                    Content = new Label { Text = text, HorizontalTextAlignment = TextAlignment.Center }
                //}
            };
        }
    }
}
