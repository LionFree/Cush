using System;
using System.Diagnostics;

namespace Cush.Testing
{
    public abstract class StopwatchWrapper : IStopWatch
    {
        public static StopwatchWrapper GetInstance()
        {
            return new StopwatchImplementation();
        }

        public abstract bool IsHighResolution { get; }
        public abstract bool IsRunning { get; }
        public abstract TimeSpan Elapsed { get; }
        public abstract void Start();
        public abstract void Stop();
        public abstract void Reset();

        private class StopwatchImplementation : StopwatchWrapper
        {
            private readonly Stopwatch _stopwatch = new Stopwatch();

            public override bool IsHighResolution
            {
                get { return Stopwatch.IsHighResolution; }
            }

            public override bool IsRunning
            {
                get { return _stopwatch.IsRunning; }
            }

            public override TimeSpan Elapsed
            {
                get { return _stopwatch.Elapsed; }
            }

            public override void Start()
            {
                _stopwatch.Start();
            }

            public override void Stop()
            {
                _stopwatch.Stop();
            }

            public override void Reset()
            {
                _stopwatch.Reset();
            }
        }
    }
}