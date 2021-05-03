using System;
using System.Collections.Generic;
using CleanPRJ.src;
using CleanPRJ.src.UI;
using Microcharts;
using Microcharts.Forms;
using Xamarin.Forms;
using Xamarin.Essentials;
using CleanPRJ.src.Icons;
using System.Reflection;
using CleanPRJ.Statistics;

namespace CleanPRJ.MainScreen
{
    public abstract class ApplicationPage<T> : ApplicationPage where T : IViewModel
    {
        protected T model;
        public ApplicationPage(T model) : base()
        {
            this.model = model;
            InitUI();
        }

        protected ApplicationPage() { }
    }

    public abstract class ApplicationPage : ContentPage
    {
        public Action<Type> OnChangePageCliked;

        protected abstract void InitUI();

        private Button AddButton(ButtonData data, Type currentScreen)
        {
            var button = new Button
            {
                Text = data.label,
                IsEnabled = currentScreen != data.windowType,
                BackgroundColor = WindowData.Current.Button.Background,
                TextColor = WindowData.Current.Button.Text,
            };
            button.Clicked += (obj, eventData) =>
            {
                OnChangePageCliked.Invoke(data.windowType);
            };
            return button;
        }

        public Frame MediumWidget(string lable, View content, Action onTapped = null)
        {
            content.WidthRequest = WindowData.ScreenSize.X * 0.9;
            content.HeightRequest = WindowData.ScreenSize.X * 0.9 / 2;
            content.VerticalOptions = LayoutOptions.Center;
            var frame = new Frame
            {
                BorderColor = WindowData.Current.Wiget.Border,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                CornerRadius = 20,
                BackgroundColor = WindowData.Current.Wiget.Background,

                Padding = 1,
                HasShadow = true,
                Content = new StackLayout()
                {
                    BackgroundColor = WindowData.Current.Wiget.Background,
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
                Chart = chart,
            };
        }

        protected T GetChartFor<T>(List<ChartEntry> points) where T : PointChart, new()
        {
            return new T { Entries = points, LabelOrientation = Orientation.Horizontal, ValueLabelOrientation = Orientation.Horizontal };
        }
        protected StackLayout TopLine(string labelText, bool addBack = true, bool addSettings = false)
        {
            var iconWidth = WidthRequest = WindowData.ScreenSize.X * 0.1 ;
            var label = new Label
            {
                Text = labelText,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = WindowData.Current.Button.Text,
                FontSize = 24,
                WidthRequest = WindowData.ScreenSize.X * 0.8,
            };
            var backButton = ClickableImage(Images.BackArrow, () => OnChangePageCliked?.Invoke(typeof(MainScreenPage)), 46, iconWidth);
            var settingButton = ClickableImage(Images.Settings, () => OnChangePageCliked?.Invoke(typeof(Settings)), 46, iconWidth);
            var fakeButton = ClickableImage(Images.Empty, () => { });
            var sl = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = WindowData.Current.TopLine.Background,
                HeightRequest = 46,
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
