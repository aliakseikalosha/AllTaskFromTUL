using System;

using Xamarin.Forms;

namespace TestBluetooth
{
    public class App : Application
    {
        private MainPageViewModel model = null;
        public App()
        {
            model = new MainPageViewModel();
            MainPage = new MainPage(model);
            DependencyService.Get<IBluetoothReader>().OnMessageUpdated += UpdateMessages;
        }

        private void UpdateMessages()
        {
           ((MainPage)MainPage).UpdateMessage();// = new MainPage(model);
            MainPage.ForceLayout();
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

