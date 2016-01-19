using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Cush.Common.Logging
{
    [DataContract, KnownType(typeof (LogConfiguration))]
    public class LogConfiguration : PropertyChangedBase
    {
        #region Constructors

        /// <summary>
        ///     Creates an instance of the <see cref="LogConfiguration" />
        ///     class, populated with the default values.
        /// </summary>
        public static LogConfiguration Default => new LogConfiguration();

        /// <summary>
        ///     Creates an instance of the <see cref="LogConfiguration" />
        ///     class, populated with the null values.
        /// </summary>
        public static LogConfiguration Null => Create(EnabledLevels.None, FileNameFormatter.Default,
            string.Empty, string.Empty);

        /// <summary>
        ///     Initializes a new instance of the <see cref="LogConfiguration" /> class,
        ///     using the specified folder and file name formatting.
        /// </summary>
        /// <param name="levels"></param>
        /// <param name="formatter"></param>
        /// <param name="logFileFolder">
        ///     The path of the directory in which the log will
        ///     reside, without any trailing backslash.
        /// </param>
        /// <param name="fileNameFormat">
        ///     A string that indicates the
        ///     formatting of the filename.
        /// </param>
        /// <param name="args"></param>
        /// <returns>
        ///     The <see cref="T:ServiceSentry.Extensibility.Logging.LogConfiguration" />.
        /// </returns>
        public static LogConfiguration Create(EnabledLevels levels,
            FileNameFormatter formatter,
            string logFileFolder,
            string fileNameFormat,
            params FileNameFormattingOption[] args)
        {
            return new LogConfiguration(levels, formatter, logFileFolder, fileNameFormat, args);
        }

        /// <summary>
        ///     Creates an instance of the <see cref="LogConfiguration" />
        ///     class, populated with the default values.
        /// </summary>
        public LogConfiguration() : this(EnabledLevels.All,
            FileNameFormatter.Default,
            AppDomain.CurrentDomain.BaseDirectory,
            "{0}.{1}.log",
            FileNameFormattingOption.AssemblyName,
            FileNameFormattingOption.SortableDate)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LogConfiguration" /> class,
        ///     using the specified folder and file name formatting.
        /// </summary>
        /// <param name="levels"></param>
        /// <param name="formatter"></param>
        /// <param name="logFileFolder">
        ///     The path of the directory in which the log will
        ///     reside, without any trailing backslash.
        /// </param>
        /// <param name="fileNameFormat">
        ///     A string that indicates the
        ///     formatting of the filename.
        /// </param>
        /// <param name="args"></param>
        /// <returns>
        ///     The <see cref="T:ServiceSentry.Extensibility.Logging.LogConfiguration" />.
        /// </returns>
        internal LogConfiguration(EnabledLevels levels,
            FileNameFormatter formatter,
            string logFileFolder,
            string fileNameFormat,
            params FileNameFormattingOption[] args)
        {
            _levels = levels;
            _formatter = formatter;
            _logFileFolder = logFileFolder;
            _fileNameFormat = fileNameFormat;
            FormattingOptions = args;
        }

        #endregion


        private FileNameFormattingOption[] _args;
        private string _fileNameFormat;
        private FileNameFormatter _formatter;
        private List<string> _formattingStrings;
        private EnabledLevels _levels;
        private string _logFileFolder;

        
        /// <summary>
        ///     The folder in which the log file will reside.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string LogFolder
        {
            get { return _logFileFolder; }
            set
            {
                if (!PathExtensions.ValidFolder(value)) throw new ArgumentException("Folder name is not valid.");
                SetProperty(ref _logFileFolder, value);
                OnPropertyChanged(nameof(FullPath));
            }
        }


        /// <summary>
        ///     The formatting string of the log file name.
        /// </summary>
        [DataMember(IsRequired = false)]
        internal string FileNameFormat
        {
            get { return _fileNameFormat; }
            set
            {
                SetProperty(ref _fileNameFormat, value);
                OnPropertyChanged(nameof(FullPath));
            }
        }

        /// <summary>
        ///     The formatting options to apply.
        /// </summary>
        public FileNameFormattingOption[] FormattingOptions
        {
            get { return _args; }
            set
            {
                if (_args == value) return;
                _args = value;

                if (value == null || value.Length == 0)
                {
                    _formattingStrings = new List<string>();
                }
                else
                {
                    var output =
                        value.Select(item => item.Name).Where(converted => !string.IsNullOrEmpty(converted)).ToList();
                    _formattingStrings = output;
                }

                OnPropertyChanged();
                OnPropertyChanged(nameof(FormattingOptionStrings));
            }
        }

        [DataMember(IsRequired = false)]
        public List<string> FormattingOptionStrings
        {
            get { return _formattingStrings; }
            set
            {
                if (_formattingStrings == value) return;
                _formattingStrings = value;

                if (value == null || value.Count == 0)
                {
                    _args = new FileNameFormattingOption[] {};
                    return;
                }

                var output = new List<FileNameFormattingOption>();
                foreach (var item in value)
                {
                    var converted = FileNameFormattingOption.ByName(item);
                    if (converted == null) continue;
                    output.Add(converted);
                }
                _args = output.ToArray();

                OnPropertyChanged(nameof(FormattingOptions));
                OnPropertyChanged(nameof(FullPath));
                OnPropertyChanged();
            }
        }

        [DataMember(IsRequired = false)]
        public EnabledLevels Levels
        {
            get { return _levels; }
            set
            {
                SetProperty(ref _levels, value);
                OnPropertyChanged(nameof(IsDebugEnabled));
                OnPropertyChanged(nameof(IsTraceEnabled));
                OnPropertyChanged(nameof(IsInfoEnabled));
                OnPropertyChanged(nameof(IsWarnEnabled));
                OnPropertyChanged(nameof(IsErrorEnabled));
                OnPropertyChanged(nameof(IsFatalEnabled));
            }
        }

        /// <summary>
        ///     The full, formatted path of the log file.
        /// </summary>
        public string FullPath => Environment.ExpandEnvironmentVariables(
            $"{LogFolder}\\{_formatter.Format(_fileNameFormat, _args)}"
            );

        [OnDeserializing]
        private void BeforeDeserialization(StreamingContext context)
        {
            _formatter = FileNameFormatter.Default;
        }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>Trace</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>Trace</c> level, otherwise it returns
        ///     <see
        ///         langword="false" />
        ///     .
        /// </returns>
        internal bool IsTraceEnabled => IsEnabled(LogLevel.Trace);

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>Debug</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>Debug</c> level, otherwise it returns
        ///     <see
        ///         langword="false" />
        ///     .
        /// </returns>
        internal bool IsDebugEnabled => IsEnabled(LogLevel.Debug);


        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>Info</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>Info</c> level, otherwise it returns
        ///     <see
        ///         langword="false" />
        ///     .
        /// </returns>
        internal bool IsInfoEnabled => IsEnabled(LogLevel.Info);

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>Warn</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>Warn</c> level, otherwise it returns
        ///     <see
        ///         langword="false" />
        ///     .
        /// </returns>
        internal bool IsWarnEnabled => IsEnabled(LogLevel.Warn);

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>Error</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>Error</c> level, otherwise it returns
        ///     <see
        ///         langword="false" />
        ///     .
        /// </returns>
        internal bool IsErrorEnabled => IsEnabled(LogLevel.Error);

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>Fatal</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>Fatal</c> level, otherwise it returns
        ///     <see
        ///         langword="false" />
        ///     .
        /// </returns>
        internal bool IsFatalEnabled => IsEnabled(LogLevel.Fatal);

        /// <summary>
        ///     Determines whether the specified level is enabled.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns>
        ///     A value of <c>true</c> if the specified level is enabled; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEnabled(LogLevel level)
        {
            return _levels.IsEnabled(level);
        }
    }
}