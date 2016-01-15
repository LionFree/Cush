using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Cush.Common.Logging
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "IntroduceOptionalParameters.Global")]
    public abstract class LogEntry : PropertyChangedBase
    {
        ///// <summary>
        /////     Gets the date of the first log event created.
        ///// </summary>
        //internal static readonly DateTime ZeroDate = DateTime.UtcNow;

        private static int _globalSequenceId;

        /// <summary>
        ///     Creates the log event.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The message.</param>
        /// <returns>
        ///     Instance of <see cref="LogEntry" />.
        /// </returns>
        public static LogEntry Create(LogLevel logLevel, [Localizable(false)] string message)
        {
            return Create(logLevel, message, null);
        }

        /// <summary>
        ///     Creates the log event.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>
        ///     Instance of <see cref="LogEntry" />.
        /// </returns>
        public static LogEntry Create(LogLevel logLevel, [Localizable(false)] string message, Exception exception)
        {
            return new LogEntryImplementation(logLevel, message, exception);
        }

        #region Abstract Members
        
        /// <summary>
        ///     Gets the unique identifier of log event which is automatically generated
        ///     and monotonically increasing.
        /// </summary>
        protected abstract int SequenceId { get; }

        #endregion

        #region Non-Abstract

        /// <summary>
        ///     Gets or sets the exception information.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        ///     Gets or sets the level of the logging event.
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        ///     Gets or sets the timestamp of the logging event.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        ///     Gets the number index of the stack frame that represents the user
        ///     code (not the NLog code).
        /// </summary>
        internal int UserStackFrameNumber { get; set; }

        /// <summary>
        ///     Gets or sets the log message including any parameter placeholders.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Gets the entire stack trace.
        /// </summary>
        public StackTrace StackTrace { get; set; }

        /// <summary>
        ///     Gets or sets the exception information.
        /// </summary>
        public bool HasException
        {
            get { return Exception != null; }
        }

        /// <summary>
        ///     Gets the stack frame of the method that did the logging.
        /// </summary>
        public StackFrame UserStackFrame
        {
            get { return (StackTrace != null) ? StackTrace.GetFrame(UserStackFrameNumber) : null; }
        }

        /// <summary>
        ///     Gets a value indicating whether stack trace has been set for this event.
        /// </summary>
        public bool HasStackTrace
        {
            get { return StackTrace != null; }
        }

        /// <summary>
        ///     Returns a string representation of this log event.
        /// </summary>
        /// <returns>String representation of the log event.</returns>
        public override string ToString()
        {
            return string.Format(Strings.LogEntryToString, Level, Message, SequenceId);
        }

        /// <summary>
        ///     Sets the stack trace for the event info.
        /// </summary>
        /// <param name="stackTrace">The stack trace.</param>
        /// <param name="userStackFrame">Index of the first user stack frame within the stack trace.</param>
        internal void SetStackTrace(StackTrace stackTrace, int userStackFrame)
        {
            StackTrace = stackTrace;
            UserStackFrameNumber = userStackFrame;
        }
        #endregion

        private sealed class LogEntryImplementation : LogEntry
        {
            private readonly int _sequenceId;

            internal LogEntryImplementation(LogLevel level, [Localizable(false)] string message, Exception exception)
            {
                TimeStamp = DateTime.Now;
                Level = level;
                Message = message;
                Exception = exception;
                _sequenceId = Interlocked.Increment(ref _globalSequenceId);
            }

            protected override int SequenceId
            {
                get { return _sequenceId; }
            }
        }
    }
}
