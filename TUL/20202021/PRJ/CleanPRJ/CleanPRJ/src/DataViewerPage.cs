using System;
using CleanPRJ.MainScreen;
using CleanPRJ.DataProvider;
using Xamarin.Forms;

namespace CleanPRJ
{
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
            var scroll = new ScrollView
            {
                Content = scrollStack,
                Orientation = ScrollOrientation.Both,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
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
            Device.BeginInvokeOnMainThread(() => // On MainThread because it's a change in your UI
            {
                scrollStack.Children.Clear();
            });
            OnChangePageCliked?.Invoke(typeof(DataProviderPage));
        }
    }
}

