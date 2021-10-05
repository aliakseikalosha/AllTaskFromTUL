using CleanPRJ.src.BluetoothComunication;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CleanPRJ.DataProvider
{
    public class DataProviderViewModel : IViewModel
    {
        public Action OnStartGatheringData;
        public Action OnStopGatheringData;
        public Action OnDataUpdated;
        private readonly string fileName;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> ListOfDevices { get; set; } = new ObservableCollection<string>();

        public string SelectedBthDevice = string.Empty;
        bool isConnected = false;
        int sleepTime = 250;

        public string SleepTime
        {
            get { return sleepTime.ToString(); }
            set
            {
                int.TryParse(value, out sleepTime);
            }
        }

        private bool isSelectedBthDevice => !string.IsNullOrEmpty(SelectedBthDevice);
        public bool IsConnectEnabled => isSelectedBthDevice && !isConnected;
        public bool IsDisconnectEnabled => isSelectedBthDevice && isConnected;
        public bool IsPickerEnabled => !isConnected;

        public CellsStateData CurrentCellInfo { get; internal set; }
        public BaseInfoStateData CurrentBaseInfo { get; internal set; }

        public DataProviderViewModel()
        {
            fileName = $"{DateTime.Now:yyyy_MM_dd_HH_mm_ss}_data.csv";
            Init();
            MessagingCenter.Subscribe<App>(this, "Sleep", (obj) =>
            {
                // When the app "sleep", I close the connection with bluetooth
                //if (isConnected)
                //{
                //    Disconnect();
                //}
            });

            MessagingCenter.Subscribe<App>(this, "Resume", (obj) =>
            {

                // When the app "resume" I try to restart the connection with bluetooth
                //if (isConnected)
                //{
                //    DependencyService.Get<IBluetoothReader>().Start(SelectedBthDevice, sleepTime, true);
                //}
            });

            Refresh();
        }

        public void Init()
        {
        }

        public void Connect()
        {
            // Try to connect to a bth device
            DependencyService.Get<IBluetoothReader>().Start(SelectedBthDevice, sleepTime, true);
            isConnected = true;
        }

        public void Disconnect()
        {
            // Disconnect from bth device
            DependencyService.Get<IBluetoothReader>().Cancel();
            isConnected = false;
        }

        internal void CreateFile()
        {
            var fileAccess = DependencyService.Get<IAccessFileService>();
            fileAccess.CreateFile(fileName);
        }

        internal async void Refresh()
        {
            await BluetoothManager.I.RefreshAsync();
            ListOfDevices = BluetoothManager.I.ListOfDevices;
        }
        private Task task = null;

        private CancellationTokenSource source = new CancellationTokenSource();
        private CancellationToken token;
        internal void StartGatheringData(int waitMS)
        {
            OnStartGatheringData?.Invoke();
            token = source.Token;
            task = DataGathering(waitMS);
        }

        private async Task DataGathering(int waitMS)
        {
            var fileAccess = DependencyService.Get<IAccessFileService>();
            while (!token.IsCancellationRequested)
            {
                BMSBluetoothCommand.SendGetBaseInfo();
                await Task.Delay(500);
                await WaitWhile(() => BMSBluetoothCommand.currentBaseInfo == null, 2);
                if (BMSBluetoothCommand.currentBaseInfo == null)
                {
                    continue;
                }
                CurrentBaseInfo = BMSBluetoothCommand.currentBaseInfo;
                await Task.Delay(2000);
                BMSBluetoothCommand.SendGetCellDataCommand();
                await Task.Delay(500);
                await WaitWhile(() => BMSBluetoothCommand.currentCellsData == null, 2);
                if (BMSBluetoothCommand.currentCellsData == null)
                {
                    continue;
                }
                CurrentCellInfo = BMSBluetoothCommand.currentCellsData;
                fileAccess.WriteNewLineToFile(fileName, $"{DateTime.Now:O},{BMSBluetoothCommand.currentBaseInfo},{BMSBluetoothCommand.currentCellsData}");
                OnDataUpdated?.Invoke();
                await Task.Delay(15000);
            }
        }

        private async Task WaitWhile(Func<bool> condition, float maxWaitTime)
        {
            maxWaitTime *= 1000;
            int waitFrame = 16;
            while (condition())
            {
                await Task.Delay(waitFrame);
                maxWaitTime -= waitFrame;
                if (maxWaitTime < 0)
                {
                    break;
                }
            }
        }

        internal async void StopGatheringData()
        {
            if (!task.IsCanceled)
            {
                source.Cancel();
            }
            await Task.Delay(1000);
            source.Dispose();
            source = new CancellationTokenSource();
            OnStopGatheringData?.Invoke();
        }
    }
}

