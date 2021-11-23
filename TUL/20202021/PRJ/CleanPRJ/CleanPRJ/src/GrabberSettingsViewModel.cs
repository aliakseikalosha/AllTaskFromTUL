using System;
using System.Linq;
using CleanPRJ.DataProvider;
using Xamarin.Forms;

namespace CleanPRJ
{
    public class GrabberSettingsViewModel : IViewModel
    {
        private static readonly float minTimeRead = 0.1f;
        private static readonly float maxTimeRead = 10f;

        private static readonly float minWaitInBetween = 0.01f;
        private static readonly float maxWaitInBetween = 5f;
        private static readonly string settingsFileName = "GrabberSettings.txt";
        private static readonly char dataSeparator = '|';
        private static readonly char valueSeparator = ':';

        public static float TimeToReadBase { get; private set; } = 0.2f;
        public static float TimeToReadBase01 => (TimeToReadBase - minTimeRead) / (maxTimeRead - minTimeRead);
        public static float TimeToReadCell { get; private set; } = 0.2f;
        public static float TimeToReadCell01 => (TimeToReadCell - minTimeRead) / (maxTimeRead - minTimeRead);
        public static float WaitInBetween { get; private set; } = 0.25f;
        public static float WaitInBetween01 => (WaitInBetween - minWaitInBetween) / (maxWaitInBetween - minWaitInBetween);

        private IAccessFileService fileAccess;

        public GrabberSettingsViewModel()
        {
            Init();
        }

        public void Init()
        {
            fileAccess = DependencyService.Get<IAccessFileService>();
            LoadData();
        }

        private void LoadData()
        {
            var text = fileAccess.ReadFile(settingsFileName);
            if (text == null)
            {
                Save();
                return;
            }
            foreach (var item in text.Split(dataSeparator))
            {
                if (!item.Contains(valueSeparator))
                {
                    continue;
                }
                var data = item.Split(valueSeparator);
                var name = data[0];
                var value = float.Parse(data[1]);
                if (name == nameof(TimeToReadBase))
                {
                    TimeToReadBase = value;
                }
                if (name == nameof(TimeToReadCell))
                {
                    TimeToReadCell = value;
                }
                if (name == nameof(WaitInBetween))
                {
                    WaitInBetween = value;
                }
            }
        }

        private void Save()
        {
            var data = "";
            void add(string name, float value)
            {
                data += $"{dataSeparator}{name}{valueSeparator}{value}";
            }
            add(nameof(TimeToReadBase), TimeToReadBase);
            add(nameof(TimeToReadCell), TimeToReadCell);
            add(nameof(WaitInBetween), WaitInBetween);
            fileAccess.WriteNewLineToFile(settingsFileName, data);
        }

        public void UpdateTimeToReadBaseResponse01(float val)
        {
            TimeToReadBase = val * (maxTimeRead - minTimeRead) + minTimeRead; ;
            Save();
        }

        public void UpdateTimeToReadCellResponse01(float val)
        {
            TimeToReadCell = val * (maxTimeRead - minTimeRead) + minTimeRead;
            Save();
        }

        public void UpdateWaitInBetween01(float val)
        {
            WaitInBetween = val * (maxWaitInBetween - minWaitInBetween) + minWaitInBetween;
            Save();
        }
    }
}

