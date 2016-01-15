using System;

namespace Cush.Common.Logging
{
    public interface ILog
    {
        LogConfiguration Configuration { get; set; }
        ThreadSafeObservableCollection<string> LogEntries { get; }
        ThreadSafeObservableCollection<LogEntry> LogItems { get; }

        void AddEntry(LogEntry entry);
        void AddEntry(LogLevel level, Exception exception, string message, params object[] args);
        bool IsEnabled(LogLevel level);
    }
}