using System;
using System.Diagnostics;

namespace Cush.Testing
{
    public class CpuWatch : IStopWatch
    {
        private TimeSpan _endTime;
        private TimeSpan _startTime;

        public TimeSpan Elapsed
        {
            get
            {
                if (!IsRunning) return _endTime - _startTime;

                var lapTime = Process.GetCurrentProcess().TotalProcessorTime;
                return lapTime - _startTime;
            }
        }

        public bool IsRunning { get; private set; }

        public void Start()
        {
            IsRunning = true;
            _startTime = Process.GetCurrentProcess().TotalProcessorTime;
        }

        public void Stop()
        {
            _endTime = Process.GetCurrentProcess().TotalProcessorTime;
            IsRunning = false;
        }

        public void Reset()
        {
            _startTime = TimeSpan.Zero;
            _endTime = TimeSpan.Zero;
        }
    }
}