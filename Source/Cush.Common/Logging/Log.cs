using System;
using System.Diagnostics.CodeAnalysis;
using Cush.Common.Exceptions;

namespace Cush.Common.Logging
{
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    public abstract class Log : PropertyChangedBase, ILog
    {
        private LogConfiguration _configuration;
        protected readonly ExceptionWriter Writer;
        
        protected Log(LogConfiguration config, ExceptionWriter writer)
        {
            ThrowHelper.IfNullThenThrow(()=>config);
            LogItems = new ThreadSafeObservableCollection<LogEntry>();
            LogEntries = new ThreadSafeObservableCollection<string>();
            _configuration = config;
            Writer = writer;
        }
        
        public ThreadSafeObservableCollection<LogEntry> LogItems { get; }

        public ThreadSafeObservableCollection<string> LogEntries { get; }

        public LogConfiguration Configuration
        {
            get { return _configuration; }
            set
            {
                if (_configuration == value) return;
                _configuration = value;
                OnPropertyChanged();
            }
        }

        public void AddEntry(LogLevel level, Exception exception, string message,
            params object[] args)
        {
            if (Configuration.Levels == null)
                Configuration.Levels = EnabledLevels.All;

            if (!Configuration.Levels.IsEnabled(level)) return;

            if (string.IsNullOrEmpty(message) && exception == null)
                throw new ArgumentException("Message and exception cannot both be null.");
            
            AddEntry(LogEntry.Create(level, ValidMessage(message, args), exception));
        }

        private static string ValidMessage(string message, params object[] args)
        {
            if (message == null) return string.Empty;
            return args == null ? message : string.Format(message, args);
        }

        public bool IsEnabled(LogLevel level)
        {
            return Configuration.IsEnabled(level);
        }

        public static Log Console => new ConsoleLogImplementation(LogConfiguration.Default);

        public static Log Trace => new TraceLogImplementation(LogConfiguration.Default);

        public static Log Null => new NullLogImplementation(LogConfiguration.Null);

        public static Log FileLog => new FileLogImplementation(LogConfiguration.Default);


        public abstract void AddEntry(LogEntry entry);
    }
}