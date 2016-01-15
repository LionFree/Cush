using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Cush.Common.Logging
{
    [DataContract, KnownType(typeof(EnabledLevelsImplementation))]
    public abstract class EnabledLevels : PropertyChangedBase
    {
        public static EnabledLevels All => new EnabledLevelsImplementation
        {
            Trace = true,
            Debug = true,
            Info = true,
            Warn = true,
            Error = true,
            Fatal = true
        };

        public static EnabledLevels DebugLevels => new EnabledLevelsImplementation
        {
            Debug = true,
            Info = true,
            Warn = true,
            Error = true,
            Fatal = true
        };

        public static EnabledLevels InfoLevels => new EnabledLevelsImplementation { Info = true, Warn = true, Error = true, Fatal = true };

        public static EnabledLevels WarnLevels => new EnabledLevelsImplementation { Warn = true, Error = true, Fatal = true };

        public static EnabledLevels ErrorLevels => new EnabledLevelsImplementation { Error = true, Fatal = true };

        public static EnabledLevels FatalLevels => new EnabledLevelsImplementation { Fatal = true };

        public static EnabledLevels None => new EnabledLevelsImplementation();

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
