using CleanPRJ.src.BluetoothComunication;
using CleanPRJ.src.Location;
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
        private string fileNameBMS;
        private string fileNameSabvoton;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> ListOfDevices { get; set; } = new ObservableCollection<string>();

        public string SelectedBMS = string.Empty;
        public string SelectedSabvoton = string.Empty;
        bool isConnected = false;

        private bool isSelectedBthDevice => !(string.IsNullOrEmpty(SelectedBMS) || string.IsNullOrEmpty(SelectedSabvoton));
        public bool IsConnectEnabled => isSelectedBthDevice && !isConnected;
        public bool IsDisconnectEnabled => isSelectedBthDevice && isConnected;
        public bool IsPickerEnabled => !isConnected;
        public bool IsConnected => isConnected;

        public CellsStateData CurrentCellInfo { get; internal set; }
        public BaseInfoStateData CurrentBaseInfo { get; internal set; }
        public SabvotonData CurrentSabvotonInfo { get; internal set; }

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
            var reader = DependencyService.Get<IBluetoothReader>();
            reader.Start(SelectedBMS, 250, false);
            reader.Start(SelectedSabvoton, 250, false);
            isConnected = true;
        }

        public void Disconnect()
        {
            // Disconnect from bth device
            DependencyService.Get<IBluetoothReader>().Cancel();
            isConnected = false;
        }

        private void CreateFile(string file)
        {
            var fileAccess = DependencyService.Get<IAccessFileService>();
            fileAccess.CreateFile(file);
        }

        internal async void Refresh()
        {
            await BluetoothManager.I.RefreshAsync();
            ListOfDevices = BluetoothManager.I.ListOfDevices;
        }
        private Task taskBMS = null;
        private Task taskSabvoton = null;

        private CancellationTokenSource source = new CancellationTokenSource();
        private CancellationToken token;
        internal void StartGatheringData()
        {
            var time = DateTime.Now;
            fileNameBMS = $"{time:yyyy_MM_dd_HH_mm_ss}_data.csv";
            CreateFile(fileNameBMS);
            fileNameSabvoton = $"{time:yyyy_MM_dd_HH_mm_ss}_data_sab.csv";
            OnStartGatheringData?.Invoke();
            token = source.Token;
            taskBMS = DataGatheringBMS((int)(GrabberSettingsViewModel.WaitInBetween * 1000));
            taskSabvoton = DataGatheringSabvoton((int)(GrabberSettingsViewModel.WaitInBetween * 1000));
            LocationLogger.StartLogingLocation(time);
        }

        private async Task DataGatheringBMS(int waitMS)
        {
            var fileAccess = DependencyService.Get<IAccessFileService>();
            bool needAddHeadder = true;
            while (!token.IsCancellationRequested)
            {
                BMSBluetoothCommand.SendGetBaseInfo();
                await WaitWhile(() => BMSBluetoothCommand.currentBaseInfo == null, GrabberSettingsViewModel.TimeToReadBase);
                if (BMSBluetoothCommand.currentBaseInfo == null)
                {
                    ClearFront(false);
                    continue;
                }
                CurrentBaseInfo = BMSBluetoothCommand.currentBaseInfo;
                await Task.Delay(10);
                BMSBluetoothCommand.SendGetCellDataCommand();
                await WaitWhile(() => BMSBluetoothCommand.currentCellsData == null, GrabberSettingsViewModel.TimeToReadCell);
                if (BMSBluetoothCommand.currentCellsData == null)
                {
                    ClearFront(false);
                    continue;
                }
                CurrentCellInfo = BMSBluetoothCommand.currentCellsData;
                if (needAddHeadder)
                {
                    needAddHeadder = false;
                    fileAccess.WriteNewLineToFile(fileNameBMS, $"DateTime,{BMSBluetoothCommand.currentBaseInfo.CSVHeader}{BMSBluetoothCommand.currentCellsData.CSVHeader}");
                }
                var newLine = $"{DateTime.Now:O},{BMSBluetoothCommand.currentBaseInfo}{BMSBluetoothCommand.currentCellsData}";
                fileAccess.WriteNewLineToFile(fileNameBMS, newLine);
                OnDataUpdated?.Invoke();
                await Task.Delay(waitMS);
            }
        }

        private async Task DataGatheringSabvoton(int waitMS)
        {
            var fileAccess = DependencyService.Get<IAccessFileService>();
            SabvotonBluetoothCommand.StartConversation();
            await Task.Delay(1000);
            ClearFront(true);
            while (!token.IsCancellationRequested)
            {
                SabvotonBluetoothCommand.SendGetDataCommand();
                await WaitWhile(() => SabvotonBluetoothCommand.SabvotonData == null, GrabberSettingsViewModel.TimeToReadBase);
                if (SabvotonBluetoothCommand.SabvotonData == null)
                {
                    ClearFront(true);
                    continue;
                }
                CurrentSabvotonInfo = SabvotonBluetoothCommand.SabvotonData;
                await Task.Delay(1000);
                var newLine = $"{DateTime.Now:O},{SabvotonBluetoothCommand.SabvotonData}";
                fileAccess.WriteNewLineToFile(fileNameSabvoton, newLine);
                Debug.Print($"{newLine}");
                await Task.Delay(waitMS);
            }
        }

        private void ClearFront(bool isSabvoton) => DependencyService.Get<IBluetoothReader>().ClearFront(isSabvoton);

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
            if (!taskBMS.IsCanceled || !taskSabvoton.IsCanceled)
            {
                source.Cancel();
            }
            LocationLogger.StopLogingLocation();
            await Task.Delay(1000);
            source.Dispose();
            source = new CancellationTokenSource();
            OnStopGatheringData?.Invoke();
        }
    }
}

