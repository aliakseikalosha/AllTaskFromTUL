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
using OxyPlot.Series;
using OxyPlot.Axes;

namespace CleanPRJ
{
    public class DataViewerViewModel : IViewModel
    {
        private const int dataPointsCompact = 150;
        private string dataLoadedFrom = null;
        public ObservableCollection<string> Files { get; private set; } = new ObservableCollection<string>();
        public List<Data> LoadedData { get; private set; } = new List<Data>();
        private int dataStepCompact => LoadedData.Count < dataPointsCompact ? 1 : LoadedData.Count / dataPointsCompact;
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
                for (int i = 0; i < LoadedData.Count; i += dataStepCompact)
                {
                    Data data = LoadedData[i];
                    text += $"{data.date} CELL : {data.cell.HumanData.Replace(" ", "\t")} |\t\t BASE INFO :{data?.info.HumanData.Replace(" ", "\t")}".Replace("\n", "\t") + "\n";
                }
                return text;
            }
        }

        public List<LineSeries> VoltageChartData
        {
            get
            {
                List<LineSeries> entries = new List<LineSeries>();
                Data data;
                for (int i = 0; i < LoadedData.Count; i += dataStepCompact)
                {
                    data = LoadedData[i];
                    int baseCount = 4;
                    for (int j = baseCount; j < data.cell.Voltage.Length; j++)
                    {
                        int index = j - baseCount;
                        if (entries.Count <= index)
                        {
                            entries.Add(new LineSeries());
                        }
                        entries[index].Points.Add(new OxyPlot.DataPoint(DateTimeAxis.ToDouble(data.date), data.cell.Voltage[j]));
                    }
                }
                return entries;
            }
        }

        public List<LineSeries> Temperatures
        {
            get
            {
                List<LineSeries> entries = new List<LineSeries>();
                Data data;
                for (int i = 0; i < LoadedData.Count; i += dataStepCompact)
                {
                    data = LoadedData[i];
                    for (int j = 0; j < data.info.Temperatures.Length; j++)
                    {
                        if (entries.Count <= j)
                        {
                            entries.Add(new LineSeries());
                        }
                        entries[j].Points.Add(new OxyPlot.DataPoint(DateTimeAxis.ToDouble(data.date), data.info.Temperatures[j]));
                    }
                }
                return entries;
            }
        }

        public LineSeries Current
        {
            get
            {
                return LineSeriesOf(x => x.info.Current);
            }
        }

        public LineSeries FullVoltage
        {
            get
            {
                return LineSeriesOf(x => x.info.FullVoltage);
            }
        }

        public LineSeries ResidualCapacity
        {
            get
            {
                return LineSeriesOf(x=>x.info.ResidualCapacity);
            }
        }


        public LineSeries NominalCapacity
        {
            get
            {
                return LineSeriesOf(x => x.info.NominalCapacity);
            }
        }

        private LineSeries LineSeriesOf(Func<Data,double> map)
        {
            var entries = new LineSeries();
            Data data;
            for (int j = 0; j < LoadedData.Count; j += dataStepCompact)
            {
                data = LoadedData[j];
                entries.Points.Add(new OxyPlot.DataPoint(DateTimeAxis.ToDouble(data.date), map(data)));
            }
            return entries;
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

