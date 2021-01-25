using System;
using TestBth.MainScreen;
using TestBth.Statistics;
using Xamarin.Forms;

namespace TestBluetooth
{
    public class App : Application
    {
        //add Better Storage Type  for screens
        private BluetoothComunicationPage bluetoothComunication = null;
        private BluetoothComunicationViewModel model = null;
        private MainScreenPage mainScreen = null;
        private MainScreenViewModel mainScreenModel = null;
        private StatisticsPage statistics = null;
        private StatisticsViewModel statisticsModel = null;
        public App()
        {
            InitUI();
            DependencyService.Get<IBluetoothReader>().OnMessageUpdated += UpdateMessages;
        }

        private void InitUI()
        {
            mainScreenModel = new MainScreenViewModel();
            mainScreen = new MainScreenPage(mainScreenModel);
            model = new BluetoothComunicationViewModel();
            bluetoothComunication = new BluetoothComunicationPage(model);
            statisticsModel = new StatisticsViewModel();
            statistics = new StatisticsPage(statisticsModel);
            SubscribeTo(statistics);
            SubscribeTo(mainScreen);
            SubscribeTo(bluetoothComunication);
            MainPage = mainScreen;
        }

        private void SubscribeTo(PageWithBottomMenu page)
        {
            page.OnChagePageCliked += OnPageChanged;
        }

        private void OnPageChanged(Type pageType)
        {
            //todo conver to switch when we will use Enum 
            if (pageType == typeof(MainScreenPage))
            {
                MainPage = mainScreen;
            }
            else if (pageType == typeof(StatisticsPage))
            {
                MainPage = statistics;
            }
            else if (pageType == typeof(BluetoothComunicationPage))
            {
                MainPage = bluetoothComunication;
            }
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
            MessagingCenter.Send<App>(this, "Sleep"); // When app sleep, send a message so I can "Cancel" the connection
        }

        protected override void OnResume()
        {
            MessagingCenter.Send<App>(this, "Resume"); // When app resume, send a message so I can "Resume" the connection
        }
    }
}

