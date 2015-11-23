using System;

namespace Cush.Testing
{
    public interface IStopWatch
    {
        bool IsRunning { get; }
        TimeSpan Elapsed { get; }
        void Start();
        void Stop();
        void Reset();
    }
}