using System;
using CleanPRJ.MainScreen;
using CleanPRJ.DataProvider;
using Xamarin.Forms;
using System.Threading.Tasks;
using Microcharts;
using System.Collections.Generic;
using Microcharts.Forms;
using System.Linq;

namespace CleanPRJ
{
    public class DataViewerPage : ApplicationPage<DataViewerViewModel>
    {
        private StackLayout scrollStack;
        private Picker filePicker;
        private Button asText;
        private Button asTextCompact;
        private Button asGraph;

        public DataViewerPage(DataViewerViewModel model) : base(model)
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
            LineChart chart;
            //Voltages
            var voltageCharts = new StackLayout
            {
                Orientation = StackOrientation.Vertical
            };
            foreach (var data in model.VoltageChartData)
            {
                chart = GetChartFor<LineChart>(data);
                voltageCharts.Children.Add(GetViewFor(chart, 200, data.Count * 6));
            }
            StackLayout voltageLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { new Label() { Text =  "Voltages"}, voltageCharts}
            };
            //temperatures
            var temperatureCharts = new StackLayout
            {
                Orientation = StackOrientation.Vertical
            };
            foreach (var data in model.Temperatures)
            {
                chart = GetChartFor<LineChart>(data);
                temperatureCharts.Children.Add(GetViewFor(chart, 200, data.Count * 6));
            }
            StackLayout temperatureLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { new Label() { Text = "Temperatures" }, temperatureCharts }
            };
            //Current
            chart = GetChartFor<LineChart>(model.Current);
            var currentLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = 
                {
                    new Label() { Text = "Current" },
                    GetViewFor(chart, 200, model.Current.Count * 6)
                }
            };
            //FullVoltage
            chart = GetChartFor<LineChart>(model.FullVoltage);
            var fullVoltageLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    new Label() { Text = "Full Voltage" },
                    GetViewFor(chart, 200, model.FullVoltage.Count * 6)
                }
            };

            Device.BeginInvokeOnMainThread(() => // On MainThread because it's a change in your UI
            {
                scrollStack.Children.Clear();
                scrollStack.Children.Add(voltageLayout);
                scrollStack.Children.Add(temperatureLayout);
                scrollStack.Children.Add(currentLayout);
                scrollStack.Children.Add(fullVoltageLayout);
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

