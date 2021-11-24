using CleanPRJ.src.BluetoothComunication;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
        private string fileName;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> ListOfDevices { get; set; } = new ObservableCollection<string>();

        public string SelectedBthDevice = string.Empty;
        bool isConnected = false;

        private bool isSelectedBthDevice => !string.IsNullOrEmpty(SelectedBthDevice);
        public bool IsConnectEnabled => isSelectedBthDevice && !isConnected;
        public bool IsDisconnectEnabled => isSelectedBthDevice && isConnected;
        public bool IsPickerEnabled => !isConnected;
        public bool IsConnected => isConnected;

        public CellsStateData CurrentCellInfo { get; internal set; }
        public BaseInfoStateData CurrentBaseInfo { get; internal set; }

        public DataProviderViewModel()
        {
            Init();
            MessagingCenter.Subscribe<App>(this, "Sleep", (obj) =>
            {
                Debug.WriteLine("App go to sleep");
            });

            MessagingCenter.Subscribe<App>(this, "Resume", (obj) =>
            {
                Debug.WriteLine("App resume from sleep");
            });

            Refresh();
        }

        public void Init()
        {
        }

        public void Connect()
        {
            // Try to connect to a bth device
            DependencyService.Get<IBluetoothReader>().Start(SelectedBthDevice, 250, true);
            isConnected = true;
        }

        public void Disconnect()
        {
            // Disconnect from bth device
            DependencyService.Get<IBluetoothReader>().Cancel();
            isConnected = false;
        }

        private void CreateFile()
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
        internal void StartGatheringData()
        {
            fileName = $"{DateTime.Now:yyyy_MM_dd_HH_mm_ss}_data.csv";
            CreateFile();
            OnStartGatheringData?.Invoke();
            token = source.Token;
            task = DataGathering((int)(GrabberSettingsViewModel.WaitInBetween * 1000));
        }

        private async Task DataGathering(int waitMS)
        {
            var fileAccess = DependencyService.Get<IAccessFileService>();
            bool needAddHeadder = true;
            while (!token.IsCancellationRequested)
            {
                BMSBluetoothCommand.SendGetBaseInfo();
                await WaitWhile(() => BMSBluetoothCommand.currentBaseInfo == null, GrabberSettingsViewModel.TimeToReadBase);
                if (BMSBluetoothCommand.currentBaseInfo == null)
                {
                    ClearFront();
                    continue;
                }
                CurrentBaseInfo = BMSBluetoothCommand.currentBaseInfo;
                await Task.Delay(10);
                BMSBluetoothCommand.SendGetCellDataCommand();
                await WaitWhile(() => BMSBluetoothCommand.currentCellsData == null, GrabberSettingsViewModel.TimeToReadCell);
                if (BMSBluetoothCommand.currentCellsData == null)
                {
                    ClearFront();
                    continue;
                }
                CurrentCellInfo = BMSBluetoothCommand.currentCellsData;
                if (needAddHeadder)
                {
                    needAddHeadder = false;
                    fileAccess.WriteNewLineToFile(fileName, $"DateTime,{BMSBluetoothCommand.currentBaseInfo.CSVHeader}{BMSBluetoothCommand.currentCellsData.CSVHeader}");
                }
                var newLine = $"{DateTime.Now:O},{BMSBluetoothCommand.currentBaseInfo}{BMSBluetoothCommand.currentCellsData}";
                fileAccess.WriteNewLineToFile(fileName, newLine);
                OnDataUpdated?.Invoke();
                await Task.Delay(waitMS);
            }
        }

        private void ClearFront()
        {
            DependencyService.Get<IBluetoothReader>().ClearFront();
        }

        private async Task ResetConnection()
        {
            Disconnect();
            await Task.Delay(500);
            Connect();
            await Task.Delay(2000);
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

