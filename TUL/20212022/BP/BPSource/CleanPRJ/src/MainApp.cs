using System;
using System.Linq;
using DataGrabber.MainScreen;
using DataGrabber.Settings;
using DataGrabber.DataProvider;
using Xamarin.Forms;
using DataGrabber.src.BluetoothComunication;
using DataGrabber.src.DataViewer;

namespace DataGrabber
{
    public interface IModel { void Init(); }
    public interface IScreenData
    {
        Type ScreenType { get; }
        ApplicationPage Page { get; }
    }
    public class ScreenData<T1> : IScreenData where T1 : IModel, new()
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
            new ScreenData<DataProviderModel>((m) => new DataProviderPage(m), typeof(DataProviderPage)),
            new ScreenData<GrabberSettingsModel>((m)=> new GrabberSettingsPage(m), typeof(GrabberSettingsPage)),
            new ScreenData<DataViewerModel>((m)=> new DataViewerPage(m), typeof(DataViewerPage)),
        };
        public App()
        {
            Init();
        }

        private void Init()
        {
            I = this;
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
            return screenDatas.FirstOrDefault(c => c.ScreenType == pageType)?.Page;
        }

        private void SubscribeTo(ApplicationPage page)
        {
            page.OnChangePageCliked += ChangePageTo;
        }

        public void ChangePageTo(Type pageType)
        {
            var page = GetPage(pageType);
            if (page == null)
            {
                return;
            }
            page.InitUI();
            MainPage = page;
        }

        private void UpdateMessages(BluetoothMessage message)
        {
            if (MainPage is DataProviderPage)
            {
                ((DataProviderPage)MainPage).UpdateMessage();
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

