using System;
using System.Diagnostics.CodeAnalysis;
using Cush.Common.Exceptions;

namespace Cush.Common.Logging.Internal
{
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    public abstract class Log : PropertyChangedBase
    {
        private readonly ThreadSafeObservableCollection<LogEntry> _logItems;
        private readonly ThreadSafeObservableCollection<string> _logEntries;
        private LogConfiguration _configuration;
        protected readonly ExceptionWriter Writer;
        
        protected Log(LogConfiguration config, ExceptionWriter writer)
        {
            ThrowHelper.IfNullThenThrow(()=>config);
            _logItems = new ThreadSafeObservableCollection<LogEntry>();
            _logEntries = new ThreadSafeObservableCollection<string>();
            _configuration = config;
            Writer = writer;
        }
        
        public ThreadSafeObservableCollection<LogEntry> LogItems
        {
            get { return _logItems; }
        }

        public ThreadSafeObservableCollection<string> LogEntries
        {
            get { return _logEntries; }
        }

        internal LogConfiguration Configuration
        {
            get { return _configuration; }
            set
            {
                if (_configuration == value) return;
                _configuration = value;
                OnPropertyChanged();
            }
        }

        internal void AddEntry(LogLevel level, Exception exception, string message,
            params object[] args)
        {
            if (Configuration.Levels == null)
                Configuration.Levels = EnabledLevels.All;
            if (!Configuration.Levels.IsEnabled(level)) return;
            if (string.IsNullOrEmpty(message) && exception == null) throw new ArgumentException("Message and exception cannot both be null.");
            

            AddEntry(LogEntry.Create(level, ValidMessage(message, args), exception));
        }

        private string ValidMessage(string message, params object[] args)
        {
            if (message == null) return string.Empty;
            return args == null ? message : string.Format(message, args);
        }

        internal bool IsEnabled(LogLevel level)
        {
            return Configuration.IsEnabled(level);
        }

        public static Log Console
        {
            get { return new ConsoleLogImplementation(LogConfiguration.Default); }
        }

        public static Log Trace
        {
            get { return new TraceLogImplementation(LogConfiguration.Default); }
        }

        public static Log Null
        {
            get { return new NullLogImplementation(LogConfiguration.Null); }
        }

        public static Log FileLog
        {
            get { return new FileLogImplementation(LogConfiguration.Default); }
        }


        public abstract void AddEntry(LogEntry entry);
    }
}