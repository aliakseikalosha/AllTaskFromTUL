using System;
using System.Linq;
using CleanPRJ.MainScreen;
using CleanPRJ.Statistics;
using Xamarin.Forms;

namespace CleanPRJ
{
    public interface IViewModel { }
    public interface IScreenData
    {
        Type ScreenType { get; }
        ApplicationPage Page { get; }
    }
    public class ScreenData<T1> : IScreenData where T1 : IViewModel, new()
    {
        public Type ScreenType { get; }

        public ApplicationPage Page { get; }

        public T1 Model;
        public ScreenData(Func<T1, ApplicationPage> construct, Type screenType)
        {
            ScreenType = screenType;
            Model = new T1();
            Page = construct(Model);
        }
    }
    public class App : Application
    {
        private IScreenData[] screenDatas = new IScreenData[] {
            new ScreenData<MainScreenViewModel>((m) => new MainScreenPage(m), typeof(MainScreenPage)),
            new ScreenData<BluetoothComunicationViewModel>((m) => new BluetoothComunicationPage(m), typeof(BluetoothComunicationPage)),
            new ScreenData<StatisticsViewModel>((m) => new StaticticsBattery(m), typeof(StaticticsBattery)),
            new ScreenData<StatisticsViewModel>((m) => new StaticticsDistance(m), typeof(StaticticsDistance)),
            new ScreenData<SettingsVievModel>((m) => new Settings(m), typeof(Settings)),
        };
        public App()
        {
            InitUI();
            DependencyService.Get<IBluetoothReader>().OnMessageUpdated += UpdateMessages;
        }

        private void InitUI()
        {
            foreach (var data in screenDatas)
            {
                SubscribeTo(data.Page);
            }
            ChangePageTo(typeof(MainScreenPage));
        }

        private ApplicationPage GetPage(Type pageType)
        {
            return screenDatas.First(c => c.ScreenType == pageType).Page;
        }

        private void SubscribeTo(ApplicationPage page)
        {
            page.OnChangePageCliked += ChangePageTo;
        }

        private void ChangePageTo(Type pageType)
        {
            MainPage = GetPage(pageType);
        }

        private void UpdateMessages()
        {
            if (MainPage is BluetoothComunicationPage)
            {
                ((BluetoothComunicationPage)MainPage).UpdateMessage();
                MainPage.ForceLayout();
            }
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // MessagingCenter.Send<App>(this, "Sleep");
        }

        protected override void OnResume()
        {
            // MessagingCenter.Send<App>(this, "Resume"); 
        }
    }
}

