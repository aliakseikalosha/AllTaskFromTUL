using System;
using System.Collections.Generic;
using CleanPRJ.src.UI;
using Microcharts;
using Microcharts.Forms;
using Xamarin.Forms;
using CleanPRJ.src.Icons;
using System.Reflection;
using CleanPRJ.Settings;

namespace CleanPRJ.MainScreen
{
    public abstract class ApplicationPage<T> : ApplicationPage where T : IViewModel
    {
        protected T model;
        public ApplicationPage(T model) : base()
        {
            this.model = model;
        }

        protected ApplicationPage() { }
    }

    public abstract class ApplicationPage : ContentPage
    {
        public Action<Type> OnChangePageCliked;

        public ApplicationPage()
        {
            WindowData.OnThemeChanged += InitUI;
        }
        protected override bool OnBackButtonPressed()
        {
            OnChangePageCliked?.Invoke(typeof(MainScreenPage));
            return true;
        }

        public abstract void InitUI();
        public ContentView MediumWidget(string lable, View content, Action onTapped = null)
        {
            content.HeightRequest = WindowData.ScreenSize.X * 0.9 / 2;

            var frame = new Frame
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = WindowData.Current.Wiget.Background,

                Padding = 1,

                Content = new StackLayout()
                {
                    BackgroundColor = WindowData.Current.Wiget.Background,
                    WidthRequest = WindowData.ScreenSize.X,
                    Children = {
                            content,
                            new Label {
                                Text = lable,
                                HorizontalTextAlignment = TextAlignment.Center,
                                TextColor = WindowData.Current.Wiget.Text
                            }
                    }
                }
            };
            var guestRecognizer = new TapGestureRecognizer();
            guestRecognizer.Tapped += (s, e) => onTapped?.Invoke();
            frame.GestureRecognizers.Add(guestRecognizer);
            return frame;
        }
        protected ChartView GetViewFor(Chart chart, double height = -1.0, double width = -1.0)
        {
            return new ChartView
            {
                WidthRequest = width,
                HeightRequest = height,
                BackgroundColor = WindowData.Current.Wiget.Background,
                Chart = chart,
            };
        }
        protected T GetChartFor<T>(List<ChartEntry> points, TimeSpan animationTime) where T : PointChart, new()
        {
            return new T
            {
                Entries = points,
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal,
                AnimationDuration = animationTime,
                LabelColor = SkiaSharp.SKColor.Parse(WindowData.Current.Chart.Text.ToHex()),
                BackgroundColor = SkiaSharp.SKColor.Parse(WindowData.Current.Chart.Background.ToHex()),
            };
        }
        protected T GetChartFor<T>(List<ChartEntry> points) where T : PointChart, new()
        {
            return GetChartFor<T>(points, new TimeSpan(0, 0, 2));
        }
        protected StackLayout TopLine(string labelText, bool addBack = true, bool addSettings = false)
        {
            var iconWidth = WidthRequest = WindowData.ScreenSize.X * 0.1;
            var label = new Label
            {
                Text = labelText,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = WindowData.Current.TopLine.Text,
                FontSize = 24,
                WidthRequest = WindowData.ScreenSize.X,
            };
            var backButton = ClickableImage(Images.BackArrow, () => OnChangePageCliked?.Invoke(typeof(MainScreenPage)), 46, iconWidth);
            var settingButton = ClickableImage(Images.Settings, () => OnChangePageCliked?.Invoke(typeof(SettingsPage)), 46, iconWidth);
            var fakeButton = ClickableImage(Images.Empty, () => { });
            var sl = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = WindowData.Current.TopLine.Background,
                HeightRequest = 46,
                WidthRequest = iconWidth
            };
            sl.Children.Add(addBack ? backButton : fakeButton);
            sl.Children.Add(label);
            sl.Children.Add(addSettings ? settingButton : fakeButton);
            return sl;
        }

        protected Image ClickableImage(string pathToImage, Action action, double height = -1.0, double width = -1.0)
        {
            var guestRecognizer = new TapGestureRecognizer();
            guestRecognizer.Tapped += (s, e) => action?.Invoke();
            var image = new Image
            {
                WidthRequest = width,
                HeightRequest = height,
                Source = ImageSource.FromResource(pathToImage, typeof(Images).GetTypeInfo().Assembly),
            };
            image.GestureRecognizers.Add(guestRecognizer);
            return image;
        }

    }
}
