using System;
using System.Linq;
using CleanPRJ.MainScreen;
using CleanPRJ.Settings;
using CleanPRJ.src.Data;
using CleanPRJ.DataProvider;
using CleanPRJ.Statistics;
using Xamarin.Forms;

namespace CleanPRJ
{
    public interface IViewModel { void Init(); }
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
        public static App I { get; private set; }
        private IScreenData[] screenDatas = new IScreenData[] {
            //new ScreenData<MainScreenViewModel>((m) => new MainScreenPage(m), typeof(MainScreenPage)),
            //new ScreenData<BluetoothComunicationViewModel>((m) => new BluetoothComunicationPage(m), typeof(BluetoothComunicationPage)),
            new ScreenData<DataProviderViewModel>((m) => new DataProviderPage(m), typeof(DataProviderPage)),
            //new ScreenData<StatisticsViewModel>((m) => new StaticticsBattery(m), typeof(StaticticsBattery)),
            //new ScreenData<StatisticsViewModel>((m) => new StaticticsDistance(m), typeof(StaticticsDistance)),
            //new ScreenData<SettingsVievModel>((m) => new SettingsPage(m), typeof(SettingsPage)),
        };
        public App()
        {
            Init();
        }

        private void Init()
        {
            I = this;
            DataHelper.Load();
            AppSettings.I.Apply();
            InitUI();
            DependencyService.Get<IBluetoothReader>().OnMessageUpdated += UpdateMessages;
        }

        private void InitUI()
        {
            foreach (var data in screenDatas)
            {
                SubscribeTo(data.Page);
            }
            ChangePageTo(typeof(DataProviderPage));
        }

        private ApplicationPage GetPage(Type pageType)
        {
            return screenDatas.First(c => c.ScreenType == pageType).Page;
        }

        private void SubscribeTo(ApplicationPage page)
        {
            page.OnChangePageCliked += ChangePageTo;
        }

        public void ChangePageTo(Type pageType)
        {
            var page = GetPage(pageType);
            page.InitUI();
            MainPage = page;
        }

        private void UpdateMessages(BluetoothMessage message)
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

