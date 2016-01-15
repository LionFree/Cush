using System;
using Cush.Common.Logging.Internal;

namespace Cush.Common.Logging
{
    public class FileLogger : BaseLogger, ILogger
    {
        public FileLogger() : this(new FileLogImplementation(
            LogConfiguration.Default),
            AssemblyInspector.HasEntryAssembly
                ? AssemblyProxy.EntryAssemblyName
                : AppDomain.CurrentDomain.FriendlyName,
            "Application",
            new EventLogProxy())
        {
        }

        public FileLogger(ILog log, string eventSourceName, string eventLogName, IEventLog eventLog)
            : base(log, eventSourceName, eventLogName, eventLog)
        {
            Name = eventSourceName;
        }

        public override string Name { get; }

        public override bool IsTraceEnabled => Log.IsEnabled(LogLevel.Trace);

        public override bool IsDebugEnabled => Log.IsEnabled(LogLevel.Debug);

        public override bool IsInfoEnabled => Log.IsEnabled(LogLevel.Info);

        public override bool IsWarnEnabled => Log.IsEnabled(LogLevel.Warn);

        public override bool IsErrorEnabled => Log.IsEnabled(LogLevel.Error);

        public override bool IsFatalEnabled => Log.IsEnabled(LogLevel.Fatal);
    }
}
