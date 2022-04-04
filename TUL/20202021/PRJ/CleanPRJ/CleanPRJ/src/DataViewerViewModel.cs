using System;
using System.Linq;
using CleanPRJ.src.Data;
using CleanPRJ.DataProvider;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using CleanPRJ.src.BluetoothComunication;
using Microcharts;

namespace CleanPRJ
{
    public class DataViewerViewModel : IViewModel
    {
        private const int dataPointsCompact = 50;
        private string dataLoadedFrom = null;
        public ObservableCollection<string> Files { get; private set; } = new ObservableCollection<string>();
        public List<Data> LoadedData { get; private set; } = new List<Data>();
        public string DataAsText
        {
            get
            {
                var text = "";
                foreach (var data in LoadedData)
                {
                    text += $"{data.date} CELL : {data.cell.HumanData.Replace(" ", "\t")} |\t\t BASE INFO :{data?.info.HumanData.Replace(" ", "\t")}".Replace("\n", "\t") + "\n";
                }
                return text;
            }
        }

        public string DataAsTextCompact
        {
            get
            {
                var text = "";
                var step = LoadedData.Count / dataPointsCompact;
                if (step < 1)
                {
                    step = 1;
                }
                for (int i = 0; i < LoadedData.Count; i += step)
                {
                    Data data = LoadedData[i];
                    text += $"{data.date} CELL : {data.cell.HumanData.Replace(" ", "\t")} |\t\t BASE INFO :{data?.info.HumanData.Replace(" ", "\t")}".Replace("\n", "\t") + "\n";
                }
                return text;
            }
        }

        public List<List<ChartEntry>> VoltageChartData
        {
            get
            {
                List<List<ChartEntry>> entries = new List<List<ChartEntry>>();
                var color = SkiaSharp.SKColor.Parse("#00ff22");
                Data data;
                int step = LoadedData.Count < dataPointsCompact ? 1 : LoadedData.Count / dataPointsCompact;
                for (int i = 0; i < LoadedData.Count; i += step)
                {
                    data = LoadedData[i];
                    int baseCount = 4;
                    for (int j = baseCount; j < data.cell.Voltage.Length; j++)
                    {
                        int index = j - baseCount;
                        if (entries.Count <= index)
                        {
                            entries.Add(new List<ChartEntry>());
                        }
                        entries[index].Add(new ChartEntry(data.cell.Voltage[j])
                        {
                            Label = "",
                            Color = color,
                            ValueLabel = data.cell.Voltage[j].ToString(),
                        });
                    }
                }
                return entries;
            }
        }

        public List<List<ChartEntry>> Temperatures
        {
            get
            {
                List<List<ChartEntry>> entries = new List<List<ChartEntry>>();
                var color = SkiaSharp.SKColor.Parse("#ff9900");
                Data data;
                int step = LoadedData.Count < dataPointsCompact ? 1 : LoadedData.Count / dataPointsCompact;
                for (int i = 0; i < LoadedData.Count; i += step)
                {
                    data = LoadedData[i];
                    for (int j = 0; j < data.info.Temperatures.Length; j++)
                    {
                        if (entries.Count <= j)
                        {
                            entries.Add(new List<ChartEntry>());
                        }
                        entries[j].Add(new ChartEntry(data.info.Temperatures[j])
                        {
                            Label = "",
                            Color = color,
                            ValueLabel = data.info.Temperatures[j].ToString(),
                        });
                    }
                }
                return entries;
            }
        }

        public List<ChartEntry> Current
        {
            get
            {
                var entries = new List<ChartEntry>();
                var color = SkiaSharp.SKColor.Parse("#0011BB");
                Data data;
                int step = 1;//LoadedData.Count < dataPointsCompact ? 1 : LoadedData.Count / dataPointsCompact;
                for (int j = 0; j < LoadedData.Count; j += step)
                {
                    data = LoadedData[j];
                    var value = data.info.Current;
                    entries.Add(new ChartEntry(value)
                    {
                        Label = "",
                        Color = color,
                        ValueLabel = value.ToString(),
                    });
                }
                return entries;
            }
        }

        public List<ChartEntry> FullVoltage
        {
            get
            {
                var entries = new List<ChartEntry>();
                var color = SkiaSharp.SKColor.Parse("#0011BB");
                Data data;
                int step = LoadedData.Count < dataPointsCompact ? 1 : LoadedData.Count / dataPointsCompact;
                for (int j = 0; j < LoadedData.Count; j += step)
                {
                    data = LoadedData[j];
                    var value = data.info.FullVoltage;
                    entries.Add(new ChartEntry(value)
                    {
                        Label = "",
                        Color = color,
                        ValueLabel = value.ToString(),
                    });
                }
                return entries;
            }
        }

        public DataViewerViewModel()
        {
            Init();
        }

        public void Init()
        {
            Files.Clear();
            var paths = DependencyService.Get<IAccessFileService>().GetAllDataFiles();

            if (paths == null)
            {
                return;
            }

            foreach (var path in paths)
            {
                Files.Add(path.Split('/').Last());
            }
        }

        public void Load(string path)
        {
            Debug.WriteLine($"Load from {path}");
            if (path == null || dataLoadedFrom == path)
            {
                return;
            }
            var text = DependencyService.Get<IAccessFileService>().ReadFile(path).Split('\n');
            LoadedData = new List<Data>();
            foreach (var line in text)
            {
                var data = GetData(line);
                if (data != null)
                {
                    LoadedData.Add(data);
                }
            }
            dataLoadedFrom = path;
            Debug.WriteLine($"Loaded {LoadedData.Count} data point from {text.Length} lines");
        }

        public Data GetData(string line)
        {
            if (line.Length > 0)
            {
                try
                {
                    var date = DateTime.Parse(line.Split(',')[0]);
                    Debug.WriteLine(date);
                    string pattern = "(DD[0-9,|A-F]+77)";

                    var matchs = Regex.Matches(line, pattern);
                    BaseInfoStateData info = BMSBluetoothCommand.ParseResponce<BaseInfoStateData>(ReadBytesFromCSV(matchs[0].Value));
                    CellsStateData cell = BMSBluetoothCommand.ParseResponce<CellsStateData>(ReadBytesFromCSV(matchs[1].Value));
                    return new Data(date, cell, info);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            return null;
        }

        private byte[] ReadBytesFromCSV(string data, char separator = '|')
        {
            var bytes = new List<byte>();
            foreach (var c in data.Split(separator))
            {
                bytes.Add(byte.Parse(c, System.Globalization.NumberStyles.HexNumber));
            }
            return bytes.ToArray();
        }

        public class Data
        {
            public readonly DateTime date;
            public readonly CellsStateData cell;
            public readonly BaseInfoStateData info;

            public Data(DateTime date, CellsStateData cell, BaseInfoStateData info)
            {
                this.date = date;
                this.cell = cell;
                this.info = info;
            }
        }
    }
}

