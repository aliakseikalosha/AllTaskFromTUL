using DataGrabber.DataProvider;
using DataGrabber.src.UI;
using System;

namespace DataGrabber.Settings
{
    public class SettingsModel : IModel
    {
        public class SettingsData
        {
            public string Label;
            public Func<string> CurrentState;
            public Action OnClicked;
        }

        public SettingsData[] AllSettings;

        public void Init()
        {
            AllSettings = new SettingsData[]{
                new SettingsData
                {
                    Label = "Theme",
                    CurrentState = () => WindowData.Current.Name,
                    OnClicked = AppSettings.I.ToggleTheme,
                }, new SettingsData
                {
                    Label = "Send Calls",
                    CurrentState = () => AppSettings.I.SendCalls ? "On" : "Off",
                    OnClicked = AppSettings.I.ToggleSendCalls,
                }, new SettingsData
                {
                    Label = "Open Debug Menu",
                    CurrentState = () => "",
                    OnClicked = () => App.I.ChangePageTo(typeof(DataProviderPage)),
                } , new SettingsData
                {
                    Label = "App verion",
                    CurrentState = () => AppSettings.I.Version,
                    OnClicked = () => { },
                } };
        }
    }
}