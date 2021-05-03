using CleanPRJ.MainScreen;
using System;
using Xamarin.Forms;

namespace CleanPRJ.Statistics
{
    public class Settings : ApplicationPage<SettingsVievModel>
    {
        public Settings(SettingsVievModel model) : base(model) { }

        protected override void InitUI()
        {
            Content = new StackLayout
            {
                Children = { TopLine("Settings", true, false) }
            };
        }
    }
}