using CleanPRJ.src.UI;
using System;
using System.Collections.Generic;

namespace CleanPRJ.Settings
{
    public class SettingsVievModel : IViewModel
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
                    OnClicked = () => App.I.ChangePageTo(typeof(BluetoothComunicationPage)),
                } , new SettingsData
                {
                    Label = "App verion",
                    CurrentState = () => AppSettings.I.Version,
                    OnClicked = () => { },
                } };
        }
    }
}