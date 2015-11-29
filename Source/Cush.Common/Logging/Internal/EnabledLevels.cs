using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Cush.Common.Logging.Internal
{
    [DataContract, KnownType(typeof(EnabledLevelsImplementation))]
    internal abstract class EnabledLevels : PropertyChangedBase
    {
        public static EnabledLevels All
        {
            get
            {
                return new EnabledLevelsImplementation
                {
                    Trace = true,
                    Debug = true,
                    Info = true,
                    Warn = true,
                    Error = true,
                    Fatal = true
                };
            }
        }

        public static EnabledLevels DebugLevels
        {
            get
            {
                return new EnabledLevelsImplementation
                {
                    Debug = true,
                    Info = true,
                    Warn = true,
                    Error = true,
                    Fatal = true
                };
            }
        }

        public static EnabledLevels InfoLevels
        {
            get { return new EnabledLevelsImplementation { Info = true, Warn = true, Error = true, Fatal = true }; }
        }

        public static EnabledLevels WarnLevels
        {
            get { return new EnabledLevelsImplementation { Warn = true, Error = true, Fatal = true }; }
        }

        public static EnabledLevels ErrorLevels
        {
            get { return new EnabledLevelsImplementation { Error = true, Fatal = true }; }
        }

        public static EnabledLevels FatalLevels
        {
            get { return new EnabledLevelsImplementation { Fatal = true }; }
        }

        public static EnabledLevels None
        {
            get { return new EnabledLevelsImplementation(); }
        }

        [DataMember]
        public bool Trace { get; set; }

        [DataMember]
        public bool Debug { get; set; }

        [DataMember]
        public bool Info { get; set; }

        [DataMember]
        public bool Warn { get; set; }

        [DataMember]
        public bool Error { get; set; }

        [DataMember]
        public bool Fatal { get; set; }

        private ObservableCollection<LogLevel> Collection
        {
            get
            {
                var output = new ObservableCollection<LogLevel>();
                if (Trace) output.Add(LogLevel.Trace);
                if (Debug) output.Add(LogLevel.Debug);
                if (Info) output.Add(LogLevel.Info);
                if (Warn) output.Add(LogLevel.Warn);
                if (Error) output.Add(LogLevel.Error);
                if (Fatal) output.Add(LogLevel.Fatal);
                return output;
            }
        }

        public bool IsEnabled(LogLevel level)
        {
            return (Collection.Contains(level));
        }

        [DataContract]
        private sealed class EnabledLevelsImplementation : EnabledLevels
        {   
        }
    }
}
