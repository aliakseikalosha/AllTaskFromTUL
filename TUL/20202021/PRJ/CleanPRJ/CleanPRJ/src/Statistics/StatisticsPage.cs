using DataGrabber.MainScreen;
using Xamarin.Forms;
using DataGrabber.src.UI;

namespace DataGrabber.Statistics
{
    public abstract class StatisticsPage : ApplicationPage<StatisticsModel>
    {
        public StatisticsPage(StatisticsModel model) : base(model) { }
        public override void InitUI()
        {
            model.Init();
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

        protected Label DescriptionLabel(string text)
        {
            return new Label { Text = text, TextColor = WindowData.Current.Background.Text, FontSize = 16 };
        }
    }
}