using System;
using System.ComponentModel;
using System.Diagnostics;
using Cush.Common.Exceptions;

namespace Cush.Common.Logging
{
    /// <summary>
    ///     Provides logging interface and utility functions for Cush libraries.
    /// </summary>
    [DebuggerStepThrough]
    public abstract class BaseLogger : ILogger
    {
        private readonly string _eventSourceName;
        public IEventLog EventLog { get; }
        public ILog Log { get; }

        protected BaseLogger(ILog log, string eventSourceName, string eventLogName, IEventLog proxy)
        {
            Log = log;
            _eventSourceName = eventSourceName;
            EventLog = proxy;

            if (string.IsNullOrEmpty(eventSourceName)) return;
            if (!EventLog.SourceExists(eventSourceName))
                EventLog.CreateEventSource(eventSourceName, eventLogName);
        }


        /// <summary>
        ///     Sets the logger configuration.
        /// </summary>
        /// <param name="configuration">
        ///     The <see cref="LogConfiguration" /> to use.
        /// </param>
        public ILogger Configure(LogConfiguration configuration)
        {
            ThrowHelper.IfNullThenThrow(() => configuration);
            Log.Configuration = configuration;
            return this;
        }


        #region Passthrough Overrides
        /// <overloads>Logs a diagnostic message at the <c>TRACE</c> level.</overloads>
        /// <summary>
        ///     Writes a diagnostic message at the <c>TRACE</c> level.
        /// </summary>
        /// <param name="message">The message <see langword="string" /> to log.</param>
        /// <remarks>
        ///     <para>
        ///         This method first checks if this logger is <c>TRACE</c>
        ///         enabled. If this logger is <c>TRACE</c> enabled, then it
        ///         logs the message at the <c>TRACE</c> level.
        ///     </para>
        ///     <para>
        ///         <b>WARNING</b> Note that passing an <see cref="Exception" />
        ///         to this method will print the name of the <see cref="Exception" />
        ///         but no stack trace. To print a stack trace use the
        ///         <see cref="M:Trace(Exception, string)" /> form instead.
        ///     </para>
        /// </remarks>
        /// <seealso cref="IsTraceEnabled" />
        public void Trace([Localizable(false)] string message)
        {
            Trace(false, null, message, null);
        }

        /// <overloads>Logs a diagnostic message at the <c>TRACE</c> level.</overloads>
        /// <summary>
        ///     Writes an <see cref="T:Exception" /> at the <c>TRACE</c> level.
        /// </summary>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        public void Trace(Exception exception)
        {
            Trace(false, exception, null, null);
        }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>TRACE</c> level.
        /// </summary>
        /// <param name="message">The message <see langword="string" /> to log.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Trace(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="IsTraceEnabled" />
        public void Trace(Exception exception, [Localizable(false)] string message)
        {
            Trace(false, exception, message, null);
        }

        /// <overloads>_log a formatted diagnostic message with the <c>TRACE</c> level.</overloads>
        /// <summary>
        ///     Writes a diagnostic message at the <c>TRACE</c> level using the specified parameters.
        /// </summary>
        /// <param name="message">A message <see langword="string" /> containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         The message is formatted using the <c>String.Format</c> method. See
        ///         <see cref="M:String.Format(string, object[])" /> for details of the syntax of the format string and the
        ///         behavior
        ///         of the formatting.
        ///     </para>
        ///     <para>
        ///         This method does not take an <see cref="Exception" /> object to include in the
        ///         log event. To pass an <see cref="Exception" />
        ///         use one of the <see cref="M:Trace(Exception, string)" />
        ///         methods instead.
        ///     </para>
        /// </remarks>
        /// <seealso cref="IsTraceEnabled" />
        public void Trace([Localizable(false)] string message, params object[] args)
        {
            Trace(false, null, message, args);
        }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>TRACE</c> level using the specified
        ///     parameters.
        /// </summary>
        /// <param name="message">A message <see langword="string" /> containing format items.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Trace(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="IsTraceEnabled" />
        public void Trace(Exception exception, string message, params object[] args)
        {
            Trace(false, exception, message, args);
        }

        /// <overloads>Logs a diagnostic message at the <c>DEBUG</c> level.</overloads>
        /// <summary>
        ///     Writes a diagnostic message at the <c>DEBUG</c> level.
        /// </summary>
        /// <param name="message">The message <see langword="string" /> to log.</param>
        /// <remarks>
        ///     <para>
        ///         This method first checks if this logger is <c>DEBUG</c>
        ///         enabled. If this logger is <c>DEBUG</c> enabled, then it
        ///         logs the message at the <c>DEBUG</c> level.
        ///     </para>
        ///     <para>
        ///         <b>WARNING</b> Note that passing an <see cref="Exception" />
        ///         to this method will print the name of the <see cref="Exception" />
        ///         but no stack trace. To print a stack trace use the
        ///         <see cref="M:Debug(Exception, string)" /> form instead.
        ///     </para>
        /// </remarks>
        /// <seealso cref="IsDebugEnabled" />
        public void Debug([Localizable(false)] string message)
        {
            Debug(false, null, message, null);
        }

        /// <overloads>Logs an <see cref="T:Exception" /> at the <c>DEBUG</c> level.</overloads>
        /// <summary>
        ///     Writes an <see cref="T:Exception" /> at the <c>DEBUG</c> level.
        /// </summary>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        public void Debug(Exception exception)
        {
            Debug(false, exception, null, null);
        }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>DEBUG</c> level.
        /// </summary>
        /// <param name="message">The message <see langword="string" /> to log.</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Debug(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Debug(string)" />
        /// <seealso cref="IsDebugEnabled" />
        public void Debug(Exception exception, [Localizable(false)] string message)
        {
            Debug(false, exception, message, null);
        }

        /// <overloads>_log a formatted diagnostic message with the <c>DEBUG</c> level.</overloads>
        /// <summary>
        ///     Writes a formatted diagnostic message at the <c>DEBUG</c> level using the specified
        ///     parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         The message is formatted using the <c>String.Format</c> method. See
        ///         <see cref="M:String.Format(string, object[])" /> for details of the syntax of the format string and the
        ///         behavior
        ///         of the formatting.
        ///     </para>
        ///     <para>
        ///         This method does not take an <see cref="Exception" /> object to include in the
        ///         log event. To pass an <see cref="Exception" />
        ///         use one of the <see cref="M:Debug(Exception, string)" />
        ///         methods instead.
        ///     </para>
        /// </remarks>
        /// <seealso cref="IsDebugEnabled" />
        public void Debug([Localizable(false)] string message, params object[] args)
        {
            Debug(false, null, message, args);
        }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>DEBUG</c> level using the specified
        ///     parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Debug(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Debug(string)" />
        /// <seealso cref="IsDebugEnabled" />
        public void Debug(Exception exception, string message, params object[] args)
        {
            Debug(false, exception, message, args);
        }

        /// <overloads>Logs a diagnostic message at the <c>INFO</c> level.</overloads>
        /// <summary>
        ///     Writes a diagnostic message at the <c>INFO</c> level.
        /// </summary>
        /// <param name="message">The message <see langword="string" /> to log.</param>
        /// <remarks>
        ///     <para>
        ///         This method first checks if this logger is <c>INFO</c>
        ///         enabled. If this logger is <c>INFO</c> enabled, then it
        ///         logs the message at the <C>INFO</C> level.
        ///     </para>
        ///     <para>
        ///         <b>WARNING</b> Note that passing an <see cref="Exception" />
        ///         to this method will print the name of the <see cref="Exception" />
        ///         but no stack trace. To print a stack trace use the
        ///         <see cref="M:Info(Exception, string)" /> form instead.
        ///     </para>
        /// </remarks>
        /// <seealso cref="IsInfoEnabled" />
        public void Info([Localizable(false)] string message)
        {
            Info(false, null, message, null);
        }

        /// <overloads>Logs an <see cref="T:Exception" /> at the <c>INFO</c> level.</overloads>
        /// <summary>
        ///     Writes an <see cref="T:Exception" /> at the <c>INFO</c> level.
        /// </summary>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        public void Info(Exception exception)
        {
            Info(false, exception, null, null);
        }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>INFO</c> level.
        /// </summary>
        /// <param name="message">The message <see langword="string" /> to log.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Info(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Info(string)" />
        /// <seealso cref="IsInfoEnabled" />
        public void Info(Exception exception, [Localizable(false)] string message)
        {
            Info(false, exception, message, null);
        }

        /// <overloads>_log a formatted diagnostic message with the <c>INFO</c> level.</overloads>
        /// <summary>
        ///     Writes a diagnostic message at the <c>INFO</c> level using the specified parameters.
        /// </summary>
        /// <param name="message">A message <see langword="string" /> containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         The message is formatted using the <c>String.Format</c> method. See
        ///         <see cref="M:String.Format(string, object[])" /> for details of the syntax of the format string and the
        ///         behavior
        ///         of the formatting.
        ///     </para>
        ///     <para>
        ///         This method does not take an <see cref="Exception" /> object to include in the
        ///         log event. To pass an <see cref="Exception" />
        ///         use one of the <see cref="M:Info(Exception, string)" />
        ///         methods instead.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Info(string)" />
        /// <seealso cref="IsInfoEnabled" />
        public void Info([Localizable(false)] string message, params object[] args)
        {
            Info(false, null, message, args);
        }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>INFO</c> level.
        /// </summary>
        /// <param name="message">A message <see langword="string" /> containing format items.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Info(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Info(string)" />
        /// <seealso cref="IsInfoEnabled" />
        public void Info(Exception exception, string message, params object[] args)
        {
            Info(false, exception, message, args);
        }

        /// <overloads>Logs a diagnostic message at the <c>WARN</c> level.</overloads>
        /// <summary>
        ///     Writes the diagnostic message at the <c>WARN</c> level.
        /// </summary>
        /// <param name="message">_log message.</param>
        /// <remarks>
        ///     <para>
        ///         This method first checks if this logger is <c>WARN</c>
        ///         enabled. If this logger is <c>WARN</c> enabled, then it
        ///         logs the message at the <c>WARN</c> level.
        ///     </para>
        ///     <para>
        ///         <b>WARNING</b> Note that passing an <see cref="Exception" />
        ///         to this method will print the name of the <see cref="Exception" />
        ///         but no stack trace. To print a stack trace use the
        ///         <see cref="M:Warn(Exception, string)" /> form instead.
        ///     </para>
        /// </remarks>
        /// <seealso cref="IsWarnEnabled" />
        public void Warn([Localizable(false)] string message)
        {
            Warn(false, null, message, null);
        }

        /// <overloads>Logs an <see cref="T:Exception" /> at the <c>WARN</c> level.</overloads>
        /// <summary>
        ///     Writes an <see cref="T:Exception" /> at the <c>WARN</c> level.
        /// </summary>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        public void Warn(Exception exception) { Warn(false, exception, null, null); }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>WARN</c> level.
        /// </summary>
        /// <param name="message">The message <see langword="string" /> to log.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Warn(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Warn(string)" />
        /// <seealso cref="IsWarnEnabled" />
        public void Warn(Exception exception, [Localizable(false)] string message) { Warn(false, exception, message, null); }

        /// <overloads>_log a formatted diagnostic message with the <c>ERROR</c> level.</overloads>
        /// <summary>
        ///     Writes a diagnostic message at the <c>WARN</c> level using the specified parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         The message is formatted using the <c>String.Format</c> method. See
        ///         <see cref="M:String.Format(string, object[])" /> for details of the syntax of the format string and the
        ///         behavior
        ///         of the formatting.
        ///     </para>
        ///     <para>
        ///         This method does not take an <see cref="Exception" /> object to include in the
        ///         log event. To pass an <see cref="Exception" />
        ///         use one of the <see cref="M:Warn(Exception, string)" />
        ///         methods instead.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Warn(string)" />
        /// <seealso cref="IsWarnEnabled" />
        public void Warn([Localizable(false)] string message, params object[] args) { Warn(false, null, message, args); }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>WARN</c> level.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Warn(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Warn(string)" />
        /// <seealso cref="IsWarnEnabled" />
        public void Warn(Exception exception, string message, params object[] args)
        {
            Warn(false, exception, message, args);
        }

        /// <overloads>Logs a diagnostic message at the <c>ERROR</c> level.</overloads>
        /// <summary>
        ///     Writes a diagnostic message at the <c>ERROR</c> level.
        /// </summary>
        /// <param name="message">_log message.</param>
        /// <remarks>
        ///     <para>
        ///         This method first checks if this logger is <c>ERROR</c>
        ///         enabled. If this logger is <c>ERROR</c> enabled, then it
        ///         logs the message at the <c>ERROR</c> level.
        ///     </para>
        ///     <para>
        ///         <b>WARNING</b> Note that passing an <see cref="Exception" />
        ///         to this method will print the name of the <see cref="Exception" />
        ///         but no stack trace. To print a stack trace use the
        ///         <see cref="M:Error(Exception, string)" /> form instead.
        ///     </para>
        /// </remarks>
        /// <seealso cref="IsErrorEnabled" />
        public void Error([Localizable(false)] string message) { Error(false, null, message,null);}

        /// <overloads>Logs an <see cref="T:Exception" /> at the <c>ERROR</c> level.</overloads>
        /// <summary>
        ///     Writes an <see cref="T:Exception" /> at the <c>ERROR</c> level.
        /// </summary>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        public void Error(Exception exception) { Error(false, exception, null, null); }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>ERROR</c> level.
        /// </summary>
        /// <param name="message">The message <see langword="string" /> to log.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Error(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Error(string)" />
        /// <seealso cref="IsErrorEnabled" />
        public void Error(Exception exception, [Localizable(false)] string message) { Error(false, exception, message, null); }

        /// <overloads>_log a formatted diagnostic message with the <c>ERROR</c> level.</overloads>
        /// <summary>
        ///     Writes a diagnostic message at the <c>ERROR</c> level using the specified parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         The message is formatted using the <c>String.Format</c> method. See
        ///         <see cref="M:String.Format(string, object[])" /> for details of the syntax
        ///         of the format string and the behaviorof the formatting.
        ///     </para>
        ///     <para>
        ///         This method does not take an <see cref="Exception" /> object to include in the
        ///         log event. To pass an <see cref="Exception" />
        ///         use one of the <see cref="M:Error(Exception, string)" />
        ///         methods instead.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Error(string)" />
        /// <seealso cref="IsErrorEnabled" />
        public void Error([Localizable(false)] string message, params object[] args) { Error(false, null, message, args); }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>ERROR</c> level.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Error(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Error(string)" />
        /// <seealso cref="IsErrorEnabled" />
        public void Error(Exception exception, string message, params object[] args)
        {
            Error(false, exception, message, args);
        }

        /// <overloads>Logs a diagnostic message at the <c>FATAL</c> level.</overloads>
        /// <summary>
        ///     Writes a diagnostic message at the <c>FATAL</c> level.
        /// </summary>
        /// <param name="message">_log message.</param>
        /// <remarks>
        ///     <para>
        ///         This method first checks if this logger is <c>FATAL</c>
        ///         enabled. If this logger is <c>FATAL</c> enabled, then it
        ///         logs the message at the <c>FATAL</c> level.
        ///     </para>
        ///     <para>
        ///         <b>WARNING</b> Note that passing an <see cref="Exception" />
        ///         to this method will print the name of the <see cref="Exception" />
        ///         but no stack trace. To print a stack trace use the
        ///         <see cref="M:Fatal(Exception, string)" /> form instead.
        ///     </para>
        /// </remarks>
        /// <seealso cref="IsFatalEnabled" />
        public void Fatal([Localizable(false)] string message) { Fatal(false, null, message, null); }

        /// <overloads>Logs an <see cref="T:Exception" /> at the <c>FATAL</c> level.</overloads>
        /// <summary>
        ///     Writes an <see cref="T:Exception" /> at the <c>FATAL</c> level.
        /// </summary>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        public void Fatal(Exception exception) { Fatal(false, exception, null, null); }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>FATAL</c> level.
        /// </summary>
        /// <param name="message">The message <see langword="string" /> to log.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to log.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Fatal(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Fatal(string)" />
        /// <seealso cref="IsFatalEnabled" />
        public void Fatal(Exception exception, [Localizable(false)] string message) { Fatal(false, exception, message, null); }

        /// <overloads>_log a formatted diagnostic message with the <c>FATAL</c> level.</overloads>
        /// <summary>
        ///     Writes a diagnostic message at the <c>FATAL</c> level using the specified parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         The message is formatted using the <c>String.Format</c> method. See
        ///         <see cref="M:String.Format(string, object[])" /> for details of the syntax of the format string and the
        ///         behavior
        ///         of the formatting.
        ///     </para>
        ///     <para>
        ///         This method does not take an <see cref="Exception" /> object to include in the
        ///         log event. To pass an <see cref="Exception" />
        ///         use one of the <see cref="M:Fatal(Exception, string)" />
        ///         methods instead.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Fatal(string)" />
        /// <seealso cref="IsFatalEnabled" />
        public void Fatal([Localizable(false)] string message, params object[] args) { Fatal(false, null, message, args); }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>FATAL</c> level using the specified
        ///     parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to log.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Fatal(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Fatal(string)" />
        /// <seealso cref="IsFatalEnabled" />
        public void Fatal(Exception exception, string message, params object[] args)
        {
            Fatal(false, exception, message, args);
        }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>TRACE</c> level using the specified
        ///     parameters.
        /// </summary>
        /// <param name="message">A message <see langword="string" /> containing format items.</param>
        /// <param name="logEvent">Determines whether or not to add an entry to the <see cref="System.Diagnostics.EventLog"/>.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Trace(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="IsTraceEnabled" />
        public void Trace(bool logEvent, Exception exception, string message, params object[] args)
        {
            WriteEntry(logEvent, LogLevel.Trace, EventLogEntryType.Information, exception, message, args);
        }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>DEBUG</c> level using the specified
        ///     parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="logEvent">Determines whether or not to add an entry to the <see cref="System.Diagnostics.EventLog"/>.</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Debug(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Debug(string)" />
        /// <seealso cref="IsDebugEnabled" />
        public void Debug(bool logEvent, Exception exception, string message, params object[] args)
        {
            WriteEntry(logEvent, LogLevel.Debug, EventLogEntryType.Information, exception, message, args);
        }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>INFO</c> level.
        /// </summary>
        /// <param name="message">A message <see langword="string" /> containing format items.</param>
        /// <param name="logEvent">Determines whether or not to add an entry to the <see cref="System.Diagnostics.EventLog"/>.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Info(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Info(string)" />
        /// <seealso cref="IsInfoEnabled" />
        public void Info(bool logEvent, Exception exception, string message, params object[] args)
        {
            WriteEntry(logEvent, LogLevel.Info, EventLogEntryType.Information, exception, message, args);
        }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>WARN</c> level.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="logEvent">Determines whether or not to add an entry to the <see cref="System.Diagnostics.EventLog"/>.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Warn(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Warn(string)" />
        /// <seealso cref="IsWarnEnabled" />
        public void Warn(bool logEvent, Exception exception, string message, params object[] args)
        {
            WriteEntry(logEvent, LogLevel.Warn,
                exception == null ? EventLogEntryType.Warning : EventLogEntryType.Error, exception, message, args);
        }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>ERROR</c> level.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="logEvent">Determines whether or not to add an entry to the <see cref="System.Diagnostics.EventLog"/>.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Error(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Error(string)" />
        /// <seealso cref="IsErrorEnabled" />
        public void Error(bool logEvent, Exception exception, string message, params object[] args)
        {
            WriteEntry(logEvent, LogLevel.Error, EventLogEntryType.Error, exception, message, args);
        }

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>FATAL</c> level using the specified
        ///     parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="logEvent">Determines whether or not to add an entry to the <see cref="System.Diagnostics.EventLog"/>.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to log.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Fatal(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Fatal(string)" />
        /// <seealso cref="IsFatalEnabled" />
        public void Fatal(bool logEvent, Exception exception, string message, params object[] args)
        {
            WriteEntry(logEvent, LogLevel.Fatal, EventLogEntryType.Error, exception, message, args);
        }

        

        #endregion

        #region Abstract Methods

        /// <summary>
        ///     Gets the name of the logger.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>TRACE</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>TRACE</c> level, otherwise it returns
        ///     <see langword="false" />.
        /// </returns>
        public abstract bool IsTraceEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>DEBUG</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>DEBUG</c> level, otherwise it returns
        ///     <see langword="false" />.
        /// </returns>
        public abstract bool IsDebugEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>INFO</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>INFO</c> level, otherwise it returns
        ///     <see langword="false" />.
        /// </returns>
        public abstract bool IsInfoEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>WARN</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>WARN</c> level, otherwise it returns
        ///     <see langword="false" />.
        /// </returns>
        public abstract bool IsWarnEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>ERROR</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>ERROR</c> level, otherwise it returns
        ///     <see langword="false" />.
        /// </returns>
        public abstract bool IsErrorEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>FATAL</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>FATAL</c> level, otherwise it returns
        ///     <see langword="false" />.
        /// </returns>
        public abstract bool IsFatalEnabled { get; }
        
        private void WriteEntry(bool logEvent, LogLevel level, EventLogEntryType entryType, Exception exception, string message,
           params object[] args)
        {
            Log.AddEntry(level, exception, message, args);

            if (logEvent)
                EventLog.WriteEntry(_eventSourceName, string.Format(message, args), entryType);
        }
        #endregion


    }
}