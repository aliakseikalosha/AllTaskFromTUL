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

namespace CleanPRJ
{
    public class DataViewerViewModel : IViewModel
    {
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
            if (path == null)
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

