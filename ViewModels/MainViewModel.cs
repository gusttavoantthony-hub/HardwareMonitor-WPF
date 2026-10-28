using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using HardwareHub.UI.Services;

namespace HardwareHub.UI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly HardwareMonitorService _monitorService;
        private CancellationTokenSource? _cts;

        [ObservableProperty]
        private float cpuUsage;

        [ObservableProperty]
        private float availableMemory;

        [ObservableProperty]
        private string statusText = "Monitorando Sistema em Tempo Real...";

        public MainViewModel()
        {
            _monitorService = new HardwareMonitorService();
            StartMonitoring();
        }

        private void StartMonitoring()
        {
            _cts = new CancellationTokenSource();
            Task.Run(async () =>
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    CpuUsage = _monitorService.GetCpuUsage();
                    AvailableMemory = _monitorService.GetAvailableMemoryMB();

                    await Task.Delay(1000, _cts.Token);
                }
            }, _cts.Token);
        }
    }
}