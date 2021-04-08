using System;
using CleanPRJ.src;
using CleanPRJ.src.UI;
using Xamarin.Forms;

namespace CleanPRJ.MainScreen
{
    //todo add model as T
    public abstract class PageWithBottomMenu : ContentPage
    {
        //todo use enum?
        public Action<Type> OnChagePageCliked;

        protected abstract void InitUI();

        protected StackLayout BottomButtonUI(Type currentScreen)
        {
            var buttonStackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { } };
            foreach (var data in WindowData.ButtonsData)
            {
                if (data.windowType != currentScreen)
                {
                    buttonStackLayout.Children.Add(AddButton(data, currentScreen));
                }
            }
            return new StackLayout
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children = {
                    buttonStackLayout
                }
            };
        }

        private Button AddButton(ButtonData data, Type currentScreen)
        {
            var button = new Button
            {
                Text = data.label,
                IsEnabled = currentScreen != data.windowType,
                BackgroundColor = WindowData.Current.BackgroundButton,
                TextColor = WindowData.Current.BackgroundButtonText,
            };
            button.Clicked += (obj, eventData) =>
            {
                OnChagePageCliked.Invoke(data.windowType);
            };
            return button;
        }

        protected Frame SmallBlock(string text)
        {
            return new Frame
            {
                BorderColor = Color.DarkGray,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                CornerRadius = 100,
                BackgroundColor = Color.LightBlue,
                Padding = 1,
                HasShadow = true,
                Content = new Label
                {
                    Text = text,
                    HorizontalTextAlignment = TextAlignment.Center,
                    TextColor = WindowData.Current.BackgroundText
                }
            };
        }
    }
}
