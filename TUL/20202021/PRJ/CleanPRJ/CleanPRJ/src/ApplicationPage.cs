using System;
using System.Collections.Generic;
using DataGrabber.src.UI;
using Microcharts;
using Microcharts.Forms;
using Xamarin.Forms;
using DataGrabber.src.Icons;
using System.Reflection;
using DataGrabber.Settings;
using OxyPlot;
using OxyPlot.Xamarin.Forms;
using OxyPlot.Series;

namespace DataGrabber.MainScreen
{
    public abstract class ApplicationPage<T> : ApplicationPage where T : IModel
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

        protected PlotModel GetPlotModel(List<LineSeries> data, string title)
        {
            var model =  new PlotModel
            {
                Title = title,
            };
            foreach (var serie in data)
            {
                model.Series.Add(serie);
            }
            return model;
        }


        protected PlotModel GetPlotModel(LineSeries data, string title)
        {
            var model = new PlotModel
            {
                Series = { data },
                Title = title,
            };
            return model;
        }

        protected PlotView GetPlotView(PlotModel model)
        {
            return new PlotView
            {
                Model = model,
                WidthRequest = WindowData.ScreenSize.X,
                HeightRequest = WindowData.ScreenSize.X,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White,
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
            var backButton = ClickableImage(Images.BackArrow, BackCliked, 46, iconWidth);
            var settingButton = ClickableImage(Images.Settings, SettingsClicked, 46, iconWidth);
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

        protected Button CreateButton(string text, EventHandler onClick, bool isEnable=true)
        {
            var b = new Button
            {
                Text = text
            };
            b.IsEnabled = isEnable;
            b.Clicked += onClick;
            return b;
        }

        protected virtual void BackCliked()
        {
            OnChangePageCliked?.Invoke(typeof(MainScreenPage));
        }

        protected virtual void SettingsClicked()
        {
            OnChangePageCliked?.Invoke(typeof(SettingsPage));
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
