using CleanPRJ.src.UI;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CleanPRJ.Settings
{
    [Serializable]
    public class AppSettings
    {
        private static string jsonPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "settings.json");
        private static AppSettings inst = null;
        public static AppSettings I { get { if (inst == null) { inst = Load(); } return inst; } }
        [JsonProperty] protected string vesion = "0.1";
        [JsonProperty] protected string selectedTheme = WindowData.Light.Name;
        [JsonProperty] protected bool sendCalls = true;

        public string SelectedTheme => selectedTheme;
        public bool SendCalls => sendCalls;
        public string Version => vesion;

        public void ToggleSendCalls()
        {
            sendCalls = !sendCalls;
            Save();
        }

        public void ToggleTheme()
        {
            WindowData.ChangeTheme();
            selectedTheme = WindowData.Current.Name;
            Save();
        }

        public void Apply()
        {
            if (!WindowData.TrySetTheme(SelectedTheme))
            {
                selectedTheme = WindowData.Current.Name;
            }
        }

        private static void Save()
        {
            var json = JsonConvert.SerializeObject(I);
            File.WriteAllText(jsonPath, json);
            Console.WriteLine(json);
        }

        private static AppSettings Load()
        {
            if (File.Exists(jsonPath))
            {
                var json = File.ReadAllText(jsonPath);
                Console.WriteLine(json);
                return JsonConvert.DeserializeObject<AppSettings>(json);
            }
            return new AppSettings();

        }
    }
}