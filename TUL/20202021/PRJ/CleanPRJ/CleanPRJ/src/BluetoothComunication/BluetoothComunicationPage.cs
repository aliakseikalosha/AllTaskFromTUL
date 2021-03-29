using System;
using System.Collections.Generic;
using System.Linq;
using CleanPRJ.MainScreen;
using Xamarin.Forms;

namespace CleanPRJ
{
    public class BluetoothComunicationPage : PageWithBottomMenu
    {
        private Picker pickerBluetoothDevices = null;
        private Entry entrySleepTime = null;
        private Button connect = null;
        private Button disconnect = null;
        private BluetoothComunicationViewModel viewModel = null;
        private Entry messageText = null;


        private Button send = null;
        private ScrollView messageScroll = null;
        private StackLayout messageStack = null;
        private IBluetoothReader bluetooth = null;
        public BluetoothComunicationPage(BluetoothComunicationViewModel model)
        {
            viewModel = model;
            bluetooth = DependencyService.Get<IBluetoothReader>();
            InitUI();
        }


        protected override void InitUI()
        {
            this.BindingContext = viewModel;

            pickerBluetoothDevices = new Picker() { Title = "Select a bluetooth device" };
            pickerBluetoothDevices.SetBinding(Picker.ItemsSourceProperty, "ListOfDevices");
            pickerBluetoothDevices.SelectedIndexChanged += OnSelectedBluetoothDevice;

            entrySleepTime = new Entry() { Keyboard = Keyboard.Numeric, Placeholder = "Sleep time" };
            entrySleepTime.Text = viewModel.SleepTime;
            entrySleepTime.TextChanged += ChangeSleepTime;

            connect = new Button() { Text = "Connect" };
            connect.Clicked += ConnectToSelected;
            connect.IsEnabled = !viewModel.IsConnectEnabled;

            disconnect = new Button() { Text = "Disconnect" };
            disconnect.Clicked += DiconnectFromDevice;


            messageText = new Entry { Placeholder = "Message To Send" };
            send = new Button { Text = "Send" };
            send.Clicked += SendMessage;

            messageStack = new StackLayout();
            UpdateMessage();

            messageScroll = new ScrollView { Content = messageStack, IsVisible = true };
            int topPadding = Device.RuntimePlatform == Device.iOS ? 20 : 0;
            StackLayout slButtons = new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { disconnect, connect } };
            StackLayout sendStack = new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { messageText, send } };
            StackLayout sl = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = { pickerBluetoothDevices, entrySleepTime, slButtons, sendStack, messageScroll },
                Padding = new Thickness(0, topPadding, 0, 0)
            };
            Content = new StackLayout { Children = { sl, BottomButtonUI(typeof(BluetoothComunicationPage)) } };
        }

        public void UpdateMessage()
        {
            Device.BeginInvokeOnMainThread(() => // On MainThread because it's a change in your UI
            {
                var sorted = bluetooth.All.OrderBy(c => c.Date.Ticks).ToArray();
                messageStack.Children.Clear();
                for (int i = 0; i < sorted.Length; i++)
                {
                    var m = sorted[i];
                    messageStack.Children.Add(new Label
                    {
                        Text = $" {m.State}:{m.Message}",
                        TextDecorations = TextDecorations.None,
                        TextColor = m.State == MessageState.Recived ? Color.Black : Color.DarkGray,
                        BackgroundColor = Color.White,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        VerticalOptions = LayoutOptions.Fill,
                        HorizontalOptions = LayoutOptions.Fill,
                    });
                }
            });
        }

        private void SendMessage(object sender, EventArgs e)
        {
            var bluetooth = DependencyService.Get<IBluetoothReader>();
            bluetooth.Send(new BluetoothMessage(DateTime.Now, messageText.Text + "\n", MessageState.Sended));
        }

        private void ChangeSleepTime(object sender, TextChangedEventArgs e)
        {
            viewModel.SleepTime = entrySleepTime.Text;
        }

        private void DiconnectFromDevice(object sender, EventArgs e)
        {
            viewModel.Disconnect();
            connect.IsEnabled = true;
            disconnect.IsEnabled = false;
        }

        private void ConnectToSelected(object sender, EventArgs e)
        {
            viewModel.Connect();
            connect.IsEnabled = false;
            disconnect.IsEnabled = true;
        }

        private void OnSelectedBluetoothDevice(object sender, EventArgs e)
        {
            viewModel.SelectedBthDevice = (string)((Picker)sender).SelectedItem;
            connect.IsEnabled = viewModel.IsConnectEnabled;
        }

        protected override bool OnBackButtonPressed()
        {
            try
            {
                var bluetooth = DependencyService.Get<IBluetoothReader>();
                ((BluetoothComunicationViewModel)BindingContext).ListOfDevices = bluetooth.PairedDevices();
            }
            catch (Exception e)
            {

            }

            return true;
        }
    }
}

