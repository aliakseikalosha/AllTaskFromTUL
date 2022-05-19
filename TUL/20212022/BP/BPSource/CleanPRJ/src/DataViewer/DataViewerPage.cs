using System;
using DataGrabber.MainScreen;
using DataGrabber.DataProvider;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace DataGrabber.src.DataViewer
{
    public class DataViewerPage : ApplicationPage<DataViewerModel>
    {
        private StackLayout scrollStack;
        private Picker filePicker;
        private Button asText;
        private Button asTextCompact;
        private Button asGraph;

        public DataViewerPage(DataViewerModel model) : base(model)
        {
            this.model = model;
            this.BindingContext = model;
        }

        public override void InitUI()
        {
            model.Init();

            filePicker = new Picker() { Title = "File to load" };
            filePicker.SetBinding(Picker.ItemsSourceProperty, nameof(model.Files));
            filePicker.SelectedIndexChanged += FileSelected;
            scrollStack = new StackLayout();
            var scroll = new ScrollView
            {
                Content = scrollStack,
                Orientation = ScrollOrientation.Both,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            Label buttonInfo = new Label
            {
                Text = "Load data as : ",
            };
            asText = CreateButton("Text", ViewAsTextClicked, false);
            asTextCompact = CreateButton("Text Compact", ViewAsTextCompactClicked, false);
            asGraph = CreateButton("Graph", ViewAsGraphClicked, false);

            StackLayout buttons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { asText, asTextCompact, asGraph }
            };

            StackLayout buttonBlock = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { buttonInfo, buttons }
            };
            var windows = new StackLayout
            {
                Children = { filePicker, buttonBlock, scroll }
            };
            Content = new StackLayout { Children = { TopLine("Data Viewer", true), windows } };
        }

        private void FileSelected(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => // On MainThread because it's a change in your UI
            {
                scrollStack.Children.Clear();
                asGraph.IsEnabled = true;
                asText.IsEnabled = true;
                asTextCompact.IsEnabled = true;
            });
        }

        private void ViewAsGraphClicked(object sender, EventArgs e)
        {
            ShowAsGraph();
        }

        private void ViewAsTextCompactClicked(object sender, EventArgs e)
        {
            ShowAsText();
        }

        private void ViewAsTextClicked(object sender, EventArgs e)
        {
            ShowAsText(false);
        }


        private async void ShowAsGraph()
        {
            await LoadData();
            ShowLoadedDataGraph();
        }

        private async void ShowAsText(bool compact = true)
        {
            await LoadData();
            ShowLoadedData(compact);
        }

        private async Task LoadData()
        {
            var path = (string)filePicker.SelectedItem;
            Device.BeginInvokeOnMainThread(() => // On MainThread because it's a change in your UI
            {
                scrollStack.Children.Clear();
                scrollStack.Children.Add(new Label { Text = "LOADING DATA" });
            });
            await Task.Delay(50);
            model.Load(path);
        }

        private void ShowLoadedData(bool compact)
        {
            Device.BeginInvokeOnMainThread(() => // On MainThread because it's a change in your UI
            {
                scrollStack.Children.Clear();
                scrollStack.Children.Add(new Label { Text = compact ? model.DataAsTextCompact : model.DataAsText });
            });
        }

        private void ShowLoadedDataGraph()
        {
            Device.BeginInvokeOnMainThread(() => // On MainThread because it's a change in your UI
            {
                scrollStack.Children.Clear();
                scrollStack.Children.Add(GetPlotView(GetPlotModel(model.VoltageChartData, "Voltage V")));
                scrollStack.Children.Add(GetPlotView(GetPlotModel(model.Temperatures, "Temperatures °C")));
                scrollStack.Children.Add(GetPlotView(GetPlotModel(model.Current, "Current A")));
                scrollStack.Children.Add(GetPlotView(GetPlotModel(model.FullVoltage, "Full Voltage V")));
                scrollStack.Children.Add(GetPlotView(GetPlotModel(model.ResidualCapacity, "Residual Capacity x10 mAh")));
                scrollStack.Children.Add(GetPlotView(GetPlotModel(model.SoC, "State of Charge %")));
                scrollStack.Children.Add(GetPlotView(GetPlotModel(model.Altitude, "Altitude m")));
                //scrollStack.Children.Add(GetPlotView(GetPlotModel(model.NominalCapacity, "Nominal Capacity")));
            });
        }


        protected override void BackCliked()
        {
            Device.BeginInvokeOnMainThread(() => // On MainThread because it's a change in your UI
            {
                scrollStack.Children.Clear();
            });
            OnChangePageCliked?.Invoke(typeof(DataProviderPage));
        }
    }
}

