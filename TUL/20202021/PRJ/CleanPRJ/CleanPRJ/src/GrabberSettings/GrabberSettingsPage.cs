using System;
using DataGrabber.MainScreen;
using DataGrabber.DataProvider;
using Xamarin.Forms;

namespace DataGrabber
{
    public class GrabberSettingsPage : ApplicationPage<GrabberSettingsModel>
    {
        private Label labelBase;
        private Label labelCell;
        private Label labelWait;
        private Label labelWaitGPS;

        public GrabberSettingsPage(GrabberSettingsModel model)
        {
            this.model = model;
        }

        public override void InitUI()
        {
            this.BindingContext = model;
            labelBase = new Label
            {
                Text = $"Time to read Base : {GrabberSettingsModel.TimeToReadBase}",
            };
            labelCell = new Label
            {
                Text = $"Time to read Cell : {GrabberSettingsModel.TimeToReadCell}",
            };
            labelWait = new Label
            {
                Text = $"Time to Wait : {GrabberSettingsModel.WaitInBetween}",
            };
            labelWaitGPS = new Label
            {
                Text = $"Time to Wait GPS : {GrabberSettingsModel.WaitInBetweenGPS}",
            };
            var timeBase = GetSlider(TimeToReadBaseUpdated, GrabberSettingsModel.TimeToReadBase01);
            var timeCell = GetSlider(TimeToReadCellUpdated, GrabberSettingsModel.TimeToReadCell01);
            var timeWait = GetSlider(TimeToWaitUpdated, GrabberSettingsModel.WaitInBetween01);
            var timeWaitGPS = GetSlider(TimeToWaitUpdatedGPS, GrabberSettingsModel.WaitInBetweenGPS01);
            var sliders = new StackLayout
            {
                Children = { SliderStack(labelBase, timeBase), SliderStack(labelCell, timeCell), SliderStack(labelWait, timeWait), SliderStack(labelWaitGPS, timeWaitGPS) },
                Orientation = StackOrientation.Vertical,
            };

            Content = new StackLayout { Children = { TopLine("Grabber Settings", true), sliders } };
        }

        protected override void BackCliked()
        {
            OnChangePageCliked?.Invoke(typeof(DataProviderPage));
        }

        private StackLayout SliderStack(Label label, Slider slider)
        {
            return new StackLayout
            {
                Children = { label, slider },
                Orientation = StackOrientation.Vertical,
            };
        }

        private void TimeToWaitUpdated(object sender, ValueChangedEventArgs e)
        {
            var val = e.NewValue;
            model.UpdateWaitInBetween01((float)val);
            labelWait.Text = $"Time to Wait : {GrabberSettingsModel.WaitInBetween}";
        }

        private void TimeToWaitUpdatedGPS(object sender, ValueChangedEventArgs e)
        {
            var val = e.NewValue;
            model.UpdateWaitInBetweenGPS01((float)val);
            labelWaitGPS.Text = $"Time to Wait GPS : {GrabberSettingsModel.WaitInBetweenGPS}";
        }

        private void TimeToReadCellUpdated(object sender, ValueChangedEventArgs e)
        {
            var val = e.NewValue;
            model.UpdateTimeToReadCellResponse01((float)val);
            labelCell.Text = $"Time to read Cell : {GrabberSettingsModel.TimeToReadCell}";
        }

        private void TimeToReadBaseUpdated(object sender, ValueChangedEventArgs e)
        {
            var val = e.NewValue;
            model.UpdateTimeToReadBaseResponse01((float)val);
            labelBase.Text = $"Time to read Base : {GrabberSettingsModel.TimeToReadBase}";
        }

        private Slider GetSlider(EventHandler<ValueChangedEventArgs> onUpdate, float value = 0f)
        {
            var slider = new Slider
            {
                Minimum = 0f,
                Maximum = 1f,
                Value = value,
            };
            slider.ValueChanged += onUpdate;
            return slider;
        }
    }
}

