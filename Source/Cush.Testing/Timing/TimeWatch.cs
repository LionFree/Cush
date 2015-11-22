using System;

namespace Cush.Testing
{
    public class TimeWatch : IStopWatch
    {
        private readonly StopwatchWrapper _stopwatch;

        public TimeWatch() : this(StopwatchWrapper.GetInstance()) { }

        private void VerifyHiResolution()
        {
            if (!_stopwatch.IsHighResolution)
                throw new NotSupportedException(Strings.ERROR_Benchmark_NoHiResCounter);
        }

        public TimeWatch(StopwatchWrapper wrapper)
        {
            _stopwatch = wrapper;
            VerifyHiResolution();
        }
        
        public bool IsRunning
        {
            get { return _stopwatch.IsRunning; }
        }

        public TimeSpan Elapsed
        {
            get { return _stopwatch.Elapsed; }
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }

        public void Reset()
        {
            _stopwatch.Reset();
        }
    }
}