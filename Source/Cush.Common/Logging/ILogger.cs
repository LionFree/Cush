using System;
using System.ComponentModel;

namespace Cush.Common.Logging
{
    /// <summary>
    ///     Provides logging interface and utility functions for Cush libraries.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        ///     Gets the name of the logger.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     The <see cref="T:Log" /> to use.
        /// </summary>
        ILog Log { get; }

        #region Is__Enabled methods

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>TRACE</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>TRACE</c> level, otherwise it returns
        ///     <see langword="false" />.
        /// </returns>
        bool IsTraceEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>DEBUG</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>DEBUG</c> level, otherwise it returns
        ///     <see langword="false" />.
        /// </returns>
        bool IsDebugEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>INFO</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>INFO</c> level, otherwise it returns
        ///     <see langword="false" />.
        /// </returns>
        bool IsInfoEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>WARN</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>WARN</c> level, otherwise it returns
        ///     <see langword="false" />.
        /// </returns>
        bool IsWarnEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>ERROR</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>ERROR</c> level, otherwise it returns
        ///     <see langword="false" />.
        /// </returns>
        bool IsErrorEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>FATAL</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>FATAL</c> level, otherwise it returns
        ///     <see langword="false" />.
        /// </returns>
        bool IsFatalEnabled { get; }

        #endregion

        #region Trace() overloads

        /// <overloads>Logs a diagnostic message at the <c>TRACE</c> level.</overloads>
        /// <summary>
        ///     Writes a diagnostic message at the <c>TRACE</c> level.
        /// </summary>
        /// <param name="message">The message <see langword="string" /> to log.</param>
        ///// <remarks>
        /////     <para>
        /////         This method first checks if this logger is <c>TRACE</c>
        /////         enabled. If this logger is <c>TRACE</c> enabled, then it
        /////         logs the message at the <c>TRACE</c> level.
        /////     </para>
        /////     <para>
        /////         <b>WARNING</b> Note that passing an <see cref="Exception" />
        /////         to this method will print the name of the <see cref="Exception" />
        /////         but no stack trace. To print a stack trace use the
        /////         <see cref="M:Trace(Exception, string)" /> form instead.
        /////     </para>
        ///// </remarks>
        ///// <seealso cref="IsTraceEnabled" />
        void Trace([Localizable(false)] string message);

        /// <overloads>Logs a diagnostic message at the <c>TRACE</c> level.</overloads>
        /// <summary>
        ///     Writes an <see cref="T:Exception" /> at the <c>TRACE</c> level.
        /// </summary>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        void Trace([Localizable(false)] Exception exception);

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
        void Trace(Exception exception, [Localizable(false)] string message);

        /// <overloads>Log a formatted diagnostic message with the <c>TRACE</c> level.</overloads>
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
        void Trace([Localizable(false)] string message, params object[] args);

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
        void Trace(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>TRACE</c> level using the specified
        ///     parameters.
        /// </summary>
        /// <param name="message">A message <see langword="string" /> containing format items.</param>
        /// <param name="logEvent">Determines whether or not to add an entry to the <see cref="System.Diagnostics.EventLog" />.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Trace(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="IsTraceEnabled" />
        void Trace(bool logEvent, Exception exception, [Localizable(false)] string message, params object[] args);

        #endregion

        #region Debug() overloads

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
        void Debug([Localizable(false)] string message);

        /// <overloads>Logs an <see cref="T:Exception" /> at the <c>DEBUG</c> level.</overloads>
        /// <summary>
        ///     Writes an <see cref="T:Exception" /> at the <c>DEBUG</c> level.
        /// </summary>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        void Debug([Localizable(false)] Exception exception);


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
        void Debug(Exception exception, [Localizable(false)] string message);

        /// <overloads>Log a formatted diagnostic message with the <c>DEBUG</c> level.</overloads>
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
        void Debug([Localizable(false)] string message, params object[] args);

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
        void Debug(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>DEBUG</c> level using the specified
        ///     parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="logEvent">Determines whether or not to add an entry to the <see cref="System.Diagnostics.EventLog" />.</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Debug(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Debug(string)" />
        /// <seealso cref="IsDebugEnabled" />
        void Debug(bool logEvent, Exception exception, [Localizable(false)] string message, params object[] args);

        #endregion

        #region Info() overloads

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
        void Info([Localizable(false)] string message);


        /// <overloads>Logs an <see cref="T:Exception" /> at the <c>INFO</c> level.</overloads>
        /// <summary>
        ///     Writes an <see cref="T:Exception" /> at the <c>INFO</c> level.
        /// </summary>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        void Info([Localizable(false)] Exception exception);


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
        void Info(Exception exception, [Localizable(false)] string message);

        /// <overloads>Log a formatted diagnostic message with the <c>INFO</c> level.</overloads>
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
        void Info([Localizable(false)] string message, params object[] args);

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
        void Info(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>INFO</c> level.
        /// </summary>
        /// <param name="message">A message <see langword="string" /> containing format items.</param>
        /// <param name="logEvent">Determines whether or not to add an entry to the <see cref="System.Diagnostics.EventLog" />.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Info(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Info(string)" />
        /// <seealso cref="IsInfoEnabled" />
        void Info(bool logEvent, Exception exception, [Localizable(false)] string message, params object[] args);

        #endregion

        #region Warn() overloads

        /// <overloads>Logs a diagnostic message at the <c>WARN</c> level.</overloads>
        /// <summary>
        ///     Writes the diagnostic message at the <c>WARN</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
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
        void Warn([Localizable(false)] string message);

        /// <overloads>Logs an <see cref="T:Exception" /> at the <c>WARN</c> level.</overloads>
        /// <summary>
        ///     Writes an <see cref="T:Exception" /> at the <c>WARN</c> level.
        /// </summary>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        void Warn([Localizable(false)] Exception exception);

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
        void Warn(Exception exception, [Localizable(false)] string message);

        /// <overloads>Log a formatted diagnostic message with the <c>ERROR</c> level.</overloads>
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
        void Warn([Localizable(false)] string message, params object[] args);

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
        void Warn(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>WARN</c> level.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="logEvent">Determines whether or not to add an entry to the <see cref="System.Diagnostics.EventLog" />.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Warn(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Warn(string)" />
        /// <seealso cref="IsWarnEnabled" />
        void Warn(bool logEvent, Exception exception, [Localizable(false)] string message, params object[] args);

        #endregion

        #region Error() overloads

        /// <overloads>Logs a diagnostic message at the <c>ERROR</c> level.</overloads>
        /// <summary>
        ///     Writes a diagnostic message at the <c>ERROR</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
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
        void Error([Localizable(false)] string message);

        /// <overloads>Logs an <see cref="T:Exception" /> at the <c>ERROR</c> level.</overloads>
        /// <summary>
        ///     Writes an <see cref="T:Exception" /> at the <c>ERROR</c> level.
        /// </summary>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        void Error([Localizable(false)] Exception exception);


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
        void Error(Exception exception, [Localizable(false)] string message);

        /// <overloads>Log a formatted diagnostic message with the <c>ERROR</c> level.</overloads>
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
        void Error([Localizable(false)] string message, params object[] args);

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
        void Error(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>ERROR</c> level.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="logEvent">Determines whether or not to add an entry to the <see cref="System.Diagnostics.EventLog" />.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Error(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Error(string)" />
        /// <seealso cref="IsErrorEnabled" />
        void Error(bool logEvent, Exception exception, [Localizable(false)] string message, params object[] args);

        #endregion

        #region Fatal() overloads

        /// <overloads>Logs a diagnostic message at the <c>FATAL</c> level.</overloads>
        /// <summary>
        ///     Writes a diagnostic message at the <c>FATAL</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
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
        void Fatal([Localizable(false)] string message);

        /// <overloads>Logs an <see cref="T:Exception" /> at the <c>FATAL</c> level.</overloads>
        /// <summary>
        ///     Writes an <see cref="T:Exception" /> at the <c>FATAL</c> level.
        /// </summary>
        /// <param name="exception">An <see cref="T:Exception" /> to be logged.</param>
        void Fatal([Localizable(false)] Exception exception);

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
        void Fatal(Exception exception, [Localizable(false)] string message);

        /// <overloads>Log a formatted diagnostic message with the <c>FATAL</c> level.</overloads>
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
        void Fatal([Localizable(false)] string message, params object[] args);

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
        void Fatal(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        ///     Writes a diagnostic message and <see cref="T:Exception" /> at the <c>FATAL</c> level using the specified
        ///     parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="logEvent">Determines whether or not to add an entry to the <see cref="System.Diagnostics.EventLog" />.</param>
        /// <param name="exception">An <see cref="T:Exception" /> to log.</param>
        /// <param name="args">Arguments to format.</param>
        /// <remarks>
        ///     <para>
        ///         See the <see cref="M:Fatal(string)" /> form for more detailed information.
        ///     </para>
        /// </remarks>
        /// <seealso cref="M:Fatal(string)" />
        /// <seealso cref="IsFatalEnabled" />
        void Fatal(bool logEvent, Exception exception, [Localizable(false)] string message, params object[] args);

        #endregion

        /// <summary>
        ///     Sets the logger configuration.
        /// </summary>
        /// <param name="configuration">
        ///     The <see cref="LogConfiguration" /> to use.
        /// </param>
        ILogger Configure(LogConfiguration configuration);
    }
}