using System;
using System.Diagnostics;

namespace HardwareHub.UI.Services
{
    public class HardwareMonitorService
    {
        private readonly PerformanceCounter _cpuCounter;
        private readonly PerformanceCounter _ramCounter;

        public HardwareMonitorService()
        {
            try
            {
                _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                _cpuCounter.NextValue();
            }
            catch { }

            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public float GetCpuUsage()
        {
            try { return _cpuCounter?.NextValue() ?? 0f; }
            catch { return 0f; }
        }

        public float GetAvailableMemoryMB()
        {
            try { return _ramCounter?.NextValue() ?? 0f; }
            catch { return 0f; }
        }
    }
}