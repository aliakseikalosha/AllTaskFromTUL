using System;
using System.Linq;
using CleanPRJ.MainScreen;
using CleanPRJ.Settings;
using CleanPRJ.src.Data;
using CleanPRJ.DataProvider;
using CleanPRJ.Statistics;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CleanPRJ
{
    public interface IViewModel { void Init(); }
    public interface IScreenData
    {
        Type ScreenType { get; }
        ApplicationPage Page { get; }
    }
    public class ScreenData<T1> : IScreenData where T1 : IViewModel, new()
    {
        public Type ScreenType { get; }

        public ApplicationPage Page { get; }

        public T1 Model;
        public ScreenData(Func<T1, ApplicationPage> construct, Type screenType)
        {
            ScreenType = screenType;
            Model = new T1();
            Page = construct(Model);
        }
    }
    public class App : Application
    {
        public static App I { get; private set; }
        private IScreenData[] screenDatas = new IScreenData[] {
            new ScreenData<DataProviderViewModel>((m) => new DataProviderPage(m), typeof(DataProviderPage)),
            new ScreenData<GrabberSettingsViewModel>((m)=> new GrabberSettingsPage(m), typeof(GrabberSettingsPage)),
            new ScreenData<DataViewerViewModel>((m)=> new DataViewerPage(m), typeof(DataViewerPage)),
        };
        public App()
        {
            Init();
        }

        private void Init()
        {
            I = this;
            DataHelper.Load();
            AppSettings.I.Apply();
            InitUI();
            DependencyService.Get<IBluetoothReader>().OnMessageUpdated += UpdateMessages;
        }

        private void InitUI()
        {
            foreach (var data in screenDatas)
            {
                SubscribeTo(data.Page);
            }
            ChangePageTo(typeof(DataProviderPage));
        }

        private ApplicationPage GetPage(Type pageType)
        {
            return screenDatas.FirstOrDefault(c => c.ScreenType == pageType)?.Page;
        }

        private void SubscribeTo(ApplicationPage page)
        {
            page.OnChangePageCliked += ChangePageTo;
        }

        public void ChangePageTo(Type pageType)
        {
            var page = GetPage(pageType);
            if (page == null)
            {
                return;
            }
            page.InitUI();
            MainPage = page;
        }

        private void UpdateMessages(BluetoothMessage message)
        {
            if (MainPage is BluetoothComunicationPage)
            {
                ((BluetoothComunicationPage)MainPage).UpdateMessage();
                MainPage.ForceLayout();
            }
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // MessagingCenter.Send<App>(this, "Sleep");
        }

        protected override void OnResume()
        {
            // MessagingCenter.Send<App>(this, "Resume"); 
        }
    }

    public class DataViewerPage : ApplicationPage<DataViewerViewModel>
    {
        private StackLayout scrollStack;

        public DataViewerPage(DataViewerViewModel model) : base(model)
        {
            this.model = model;
            this.BindingContext = model;
        }

        public override void InitUI()
        {
            model.Init();

            var filePicker = new Picker() { Title = "File to load" };
            filePicker.SetBinding(Picker.ItemsSourceProperty, nameof(model.Files));
            filePicker.SelectedIndexChanged += FileSelected;
            scrollStack = new StackLayout();
            var scroll = new ScrollView { Content = scrollStack};
            var windows = new StackLayout
            {
                Children = { filePicker, scroll }
            };
            Content = new StackLayout { Children = { TopLine("Data Viewer", true), windows } };
        }

        private void FileSelected(object sender, EventArgs e)
        {
            var path = (string)((Picker)sender).SelectedItem;
            model.Load(path);
            ShowLoadedData();
        }

        private void ShowLoadedData()
        {
            Device.BeginInvokeOnMainThread(() => // On MainThread because it's a change in your UI
            {
                scrollStack.Children.Clear();
                scrollStack.Children.Add(new Label { Text = model.DataAsText });
            });
        }

        protected override void BackCliked()
        {
            OnChangePageCliked?.Invoke(typeof(DataProviderPage));
        }
    }

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
                    text += $"{data.date},{data.cell.HumanData},{data?.info.HumanData}\n";
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
            if(paths == null)
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
            if(path == null)
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
                    var data = line.Split(',');
                    var date = DateTime.Parse(data[0]);
                    Debug.WriteLine(date);
                    string pattern = "(DD[0-9,A-F]+77)";

                    var matchs = Regex.Matches(line, pattern);
                    CellsStateData cell = new CellsStateData();
                    try
                    {
                        cell.FillData(ReadBytesFromCSV(matchs[1].Value));
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                    BaseInfoStateData info = new BaseInfoStateData();
                    try
                    {
                        info.FillData(ReadBytesFromCSV(matchs[0].Value));
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                    return new Data(date, cell, info);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            return null;
        }

        private byte[] ReadBytesFromCSV(string data, char separator = ',')
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

