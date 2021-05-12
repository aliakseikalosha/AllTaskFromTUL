using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace CleanPRJ.src.UI
{
    public static class WindowData
    {
        public static Action OnThemeChanged;
        public static readonly ThemeData Light = new ThemeData
        {
            Button = new ThemedObject { Background = Color.FromHex("#DDD") },
            TopLine = new ThemedObject { Background = Color.FromHex("#1876d3"), Text = Color.White },
            ChartColorCode = new string[] { "#ffcb91", "#ffefa1", "#94ebcd", "#6ddccf" },
        };
        public static readonly ThemeData Dark = new ThemeData
        {
            Name = "Dark",
            Background = new ThemedObject { Background = Color.Black, Border = Color.LightGray, Text = Color.White },
            Button = new ThemedObject { Background = Color.FromHex("#111"), Border = Color.LightGray, Text = Color.White },
            Wiget = new ThemedObject { Background = Color.Black, Border = Color.LightGray, Text = Color.White },
            Chart = new ThemedObject { Background = Color.Black, Border = Color.LightGray, Text = Color.White },
            TopLine = new ThemedObject { Background = Color.FromHex("#1876d3"), Border = Color.LightGray, Text = Color.White },
            ChartColorCode = new string[] { "#35477d", "#03506f", "#a3ddcb", "#ffe3de", },
        };
        private static ThemeData[] themes = { Light, Dark };
        private static DisplayInfo displayInfo = DeviceDisplay.MainDisplayInfo;
        public static Vector2 ScreenSize => new Vector2(displayInfo.Width / 4, displayInfo.Height / 4);

        public static ThemeData Current { get; private set; } = Light;
        public static void ChangeTheme()
        {
            Current = Current == Light ? Dark : Light;
            OnThemeChanged?.Invoke();
        }
        public class ThemedObject
        {
            public Color Background = Color.White;
            public Color Text = Color.Black;
            public Color Border = Color.DarkGray;
        }

        public class ThemeData
        {
            public string Name = "Light";
            public ThemedObject Background = new ThemedObject();
            public ThemedObject Button = new ThemedObject();
            public ThemedObject Wiget = new ThemedObject();
            public ThemedObject Chart = new ThemedObject();
            public ThemedObject TopLine = new ThemedObject();
            public string[] ChartColorCode = new string[] { "#2c3e50", "#77d065", "#b455b6", "#3498db" };
        }

        public static bool TrySetTheme(string selectedTheme)
        {
            foreach (var theme in themes)
            {
                if (theme.Name == selectedTheme)
                {
                    Current = theme;
                    return true;
                }
            }
            return false;
        }
    }
}
