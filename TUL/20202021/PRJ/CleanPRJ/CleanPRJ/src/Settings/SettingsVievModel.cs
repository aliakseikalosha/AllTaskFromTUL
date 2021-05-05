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

        public SettingsData[] AllSettings = {
            new SettingsData {
                Label =  "Theme",
                CurrentState =  () => WindowData.Current.Name,
                OnClicked = WindowData.ChangeTheme
            }, new SettingsData{
                Label =  "Send Calls",
                CurrentState =  () => AppSettings.I.SendCalls?"On":"Off",
                OnClicked = ()=> AppSettings.I.SendCalls = !AppSettings.I.SendCalls,
            }, new SettingsData{
                Label =  "Open Debug Menu",
                CurrentState =  () => "",
                OnClicked = ()=> App.I.ChangePageTo(typeof(BluetoothComunicationPage)),
            } , new SettingsData{
                Label =  "App verion",
                CurrentState =  () => AppSettings.I.Version,
                OnClicked = ()=>{ },
            } };
    }

    public class AppSettings
    {
        private static AppSettings inst = null;
        public static AppSettings I { get { if (inst == null) { inst = new AppSettings(); } return inst; } }

        public string SelectedTheme = WindowData.Light.Name;
        public bool SendCalls = true;
        public string Version => "0.1";

        private static void Save()
        {

        } 

        private static void Load()
        {

        }
    }
}