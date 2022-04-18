using System;
using System.Linq;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using DataGrabber.src.BluetoothComunication;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace DataGrabber.src.DataViewer
{
    public class DataViewerModel : IModel
    {
        private const int dataPointsCompact = 150;

        private const int dataPointsCompactTextMultiplier = 3;
        private string dataLoadedFrom = null;
        public ObservableCollection<string> Files { get; private set; } = new ObservableCollection<string>();
        public List<BMSData> LoadedBMSData { get; private set; } = new List<BMSData>();
        public List<GeoData> LoadedGeoData { get; private set; } = new List<GeoData>();
        private int dataStepCompact => LoadedBMSData.Count < dataPointsCompact ? 1 : LoadedBMSData.Count / dataPointsCompact;
        public string DataAsText
        {
            get
            {
                var text = "BMS data:\n";
                foreach (var data in LoadedBMSData)
                {
                    text += $"{data.Date} CELL : {data.Cell.HumanData.Replace(" ", "\t")} |\t\t BASE INFO :{data?.Info.HumanData.Replace(" ", "\t")}".Replace("\n", "\t") + "\n";
                }

                text += "GPS data:\n";
                foreach (var data in LoadedGeoData)
                {
                    text += $"{data.Date}, Accuracy: {data.Accuracy}, Altitude: {data.Altitude}, Latitude: {data.Latitude}, Longitude: {data.Longitude}\n";
                }
                return text;
            }
        }

        public string DataAsTextCompact
        {
            get
            {
                var text = "BMS data:\n";
                for (int i = 0; i < LoadedBMSData.Count; i += dataStepCompact * dataPointsCompactTextMultiplier)
                {
                    BMSData data = LoadedBMSData[i];
                    text += $"{data.Date} CELL : {data.Cell.HumanData.Replace(" ", "\t")} |\t\t BASE INFO :{data?.Info.HumanData.Replace(" ", "\t")}".Replace("\n", "\t") + "\n";
                }

                text += "GPS data:\n";
                foreach (var data in LoadedGeoData)
                {
                    text += $"{data.Date}, Accuracy: {data.Accuracy}, Altitude: {data.Altitude}, Latitude: {data.Latitude}, Longitude: {data.Longitude}\n";
                }
                return text;
            }
        }

        public List<LineSeries> VoltageChartData
        {
            get
            {
                List<LineSeries> entries = new List<LineSeries>();
                BMSData data;
                for (int i = 0; i < LoadedBMSData.Count; i += dataStepCompact)
                {
                    data = LoadedBMSData[i];
                    int baseCount = 4;
                    for (int j = baseCount; j < data.Cell.Voltage.Length; j++)
                    {
                        int index = j - baseCount;
                        if (entries.Count <= index)
                        {
                            entries.Add(new LineSeries());
                        }
                        entries[index].Points.Add(new OxyPlot.DataPoint(DateTimeAxis.ToDouble(data.Date), data.Cell.Voltage[j]));
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
                BMSData data;
                for (int i = 0; i < LoadedBMSData.Count; i += dataStepCompact)
                {
                    data = LoadedBMSData[i];
                    for (int j = 0; j < data.Info.Temperatures.Length; j++)
                    {
                        if (entries.Count <= j)
                        {
                            entries.Add(new LineSeries());
                        }
                        entries[j].Points.Add(new OxyPlot.DataPoint(DateTimeAxis.ToDouble(data.Date), data.Info.Temperatures[j]));
                    }
                }
                return entries;
            }
        }

        public LineSeries Current
        {
            get
            {
                return LineSeriesOf(LoadedBMSData, x => x.Info.Current);
            }
        }

        public LineSeries FullVoltage
        {
            get
            {
                return LineSeriesOf(LoadedBMSData, x => x.Info.FullVoltage);
            }
        }

        public LineSeries ResidualCapacity
        {
            get
            {
                return LineSeriesOf(LoadedBMSData, x => x.Info.ResidualCapacity);
            }
        }


        public LineSeries SoC
        {
            get
            {
                return LineSeriesOf(LoadedBMSData, x => x.Info.SoC);
            }
        }

        public LineSeries NominalCapacity
        {
            get
            {
                return LineSeriesOf(LoadedBMSData, x => x.Info.NominalCapacity);
            }
        }

        public LineSeries Altitude
        {
            get
            {
                return LineSeriesOf(LoadedGeoData, x => x.Altitude);
            }
        }

        private LineSeries LineSeriesOf<T>(List<T> dataSet, Func<T, double> map) where T : IData
        {
            var entries = new LineSeries();
            T data;
            for (int j = 0; j < dataSet.Count; j += dataStepCompact)
            {
                data = dataSet[j];
                entries.Points.Add(new OxyPlot.DataPoint(DateTimeAxis.ToDouble(data.Date), map(data)));
            }
            return entries;
        }

        public DataViewerModel()
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
                var fileName = path.Split('/').Last();
                if (paths.Any(c => c != path && c.Contains("_geo") && c.Contains(fileName.Split('.')[0])))
                {
                    Files.Add(fileName);
                }
            }
        }

        public void Load(string path)
        {
            Debug.WriteLine($"Load from {path}");
            if (path == null || dataLoadedFrom == path)
            {
                return;
            }
            LoadBMSData(path);
            LoadGeoData(path);
            dataLoadedFrom = path;
        }

        private void LoadGeoData(string path)
        {
            var text = DependencyService.Get<IAccessFileService>().ReadFile(path.Split('.')[0] + "_geo.csv").Split('\n');
            LoadedGeoData = new List<GeoData>();
            foreach (var line in text)
            {
                var data = GetGeoData(line);
                if (data != null)
                {
                    LoadedGeoData.Add(data);
                }
            }
            Debug.WriteLine($"Loaded {LoadedGeoData.Count} GeoData point from {text.Length} lines");
        }

        private void LoadBMSData(string path)
        {
            var text = DependencyService.Get<IAccessFileService>().ReadFile(path).Split('\n');
            LoadedBMSData = new List<BMSData>();
            foreach (var line in text)
            {
                var data = GetBMSData(line);
                if (data != null)
                {
                    LoadedBMSData.Add(data);
                }
            }
            Debug.WriteLine($"Loaded {LoadedBMSData.Count} BMSData point from {text.Length} lines");
        }

        public GeoData GetGeoData(string line)
        {
            if (line.Length > 0)
            {
                try
                {
                    var data = line.Split(',');

                    return new GeoData(DateTime.Parse(data[0]),
                        double.Parse(data[1], System.Globalization.CultureInfo.InvariantCulture),
                        double.Parse(data[2], System.Globalization.CultureInfo.InvariantCulture),
                        double.Parse(data[3], System.Globalization.CultureInfo.InvariantCulture),
                        double.Parse(data[4], System.Globalization.CultureInfo.InvariantCulture));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            return null;
        }

        public BMSData GetBMSData(string line)
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
                    return new BMSData(date, cell, info);
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

        public interface IData
        {
            DateTime Date { get; }
        }

        public class BMSData : IData
        {
            public DateTime Date { get; protected set; }
            public readonly CellsStateData Cell;
            public readonly BaseInfoStateData Info;

            public BMSData(DateTime date, CellsStateData cell, BaseInfoStateData info)
            {
                this.Date = date;
                this.Cell = cell;
                this.Info = info;
            }

        }
        public class GeoData : IData
        {
            public DateTime Date { get; protected set; }
            public readonly double Accuracy;
            public readonly double Latitude;
            public readonly double Longitude;
            public readonly double Altitude;

            public GeoData(DateTime date, double accuracy, double latitude, double longitude, double altitude)
            {
                this.Date = date;
                Accuracy = accuracy;
                Latitude = latitude;
                Longitude = longitude;
                Altitude = altitude;
            }
        }
    }
}

