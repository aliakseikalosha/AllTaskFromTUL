using CleanPRJ.MainScreen;
using CleanPRJ.Statistics;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CleanPRJ.src.UI
{
    public static class WindowData
    {
        public readonly static List<ButtonData> ButtonsData = new List<ButtonData>() {
            new ButtonData{label = "MainMenu", windowType = typeof(MainScreenPage)},
            new ButtonData{label = "Statistics", windowType = typeof(StatisticsPage)},
            new ButtonData{label = "TestBT", windowType = typeof(BluetoothComunicationPage)},
        };

        public static readonly ThemeData Light = new ThemeData(Color.White, Color.Black, Color.FromHex("#DDD"), Color.Black);
        public static readonly ThemeData Dark = new ThemeData(Color.Black, Color.White, Color.FromHex("#111"), Color.White);

        public static ThemeData Current { get; private set; } = Light;
        public static void ChangeTheme()
        {
            Current = Current == Light ? Dark : Light;
        }
    }

    public class ThemeData
    {
        public readonly Color Background;
        public readonly Color BackgroundText;
        public readonly Color BackgroundButton;
        public readonly Color BackgroundButtonText;

        public ThemeData(Color background, Color backgroundText, Color backgroundButton, Color backgroundButtonText)
        {
            Background = background;
            BackgroundText = backgroundText;
            BackgroundButton = backgroundButton;
            BackgroundButtonText = backgroundButtonText;
        }
    }
}
