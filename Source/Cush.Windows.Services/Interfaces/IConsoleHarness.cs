using System;
using System.Diagnostics;
using Cush.CommandLine;

namespace Cush.Windows.Services
{
    /// <summary>
    ///     A mockable <see cref="T:System.Console" />.
    /// </summary>
    public interface IConsoleHarness : IConsole
    {
        /// <summary>
        ///     Runs a service from the console given a service implementation.
        /// </summary>
        /// <param name="args">The command line arguments to pass to the service.</param>
        /// <param name="service">
        ///     The <see cref="WindowsService" /> implementation to start.
        /// </param>
        void Run(string[] args, WindowsService service);

        /// <summary>
        ///     Helper method to write a message to the console at the given foreground color.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="formatArguments">An object array that contains zero or more objects to format.</param>
        [DebuggerStepThrough]
        void WriteToConsole(string format, params object[] formatArguments);

        /// <summary>
        ///     Helper method to write a message to the console at the given foreground color.
        /// </summary>
        /// <param name="foregroundColor">
        ///     The <see cref="ConsoleColor" /> in which to write the text.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="formatArguments">An object array that contains zero or more objects to format.</param>
        [DebuggerStepThrough]
        void WriteToConsole(ConsoleColor foregroundColor, string format,
                                            params object[] formatArguments);

        /// <summary>
        ///     Writes the text representation of the specified array of objects,
        ///     followed by the current line terminator, to the standard output stream
        ///     using the specified format information.
        /// </summary>
        /// <param name="foregroundColor">
        ///     The <see cref="ConsoleColor" /> in which to write the text.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">
        ///     An array of objects to write using <paramref name="format" />.
        /// </param>
        [DebuggerStepThrough]
        void WriteLine(ConsoleColor foregroundColor, string format, params object[] args);
        
        /// <summary>
        ///     Writes the text representation of the specified 32-bit signed integer value,
        ///     followed by the current line terminator, to the standard output stream
        ///     using the specified format information.
        /// </summary>
        /// <param name="value">The value to write.</param>
        [DebuggerStepThrough]
        void WriteLine(int value);

        /// <summary>
        ///     Writes the text representation of the specified 32-bit signed integer value
        ///     to the standard output stream using the specified format information.
        /// </summary>
        /// <param name="value">The value to write.</param>
        [DebuggerStepThrough]
        void Write(int value);

        /// <summary>
        ///     Writes the text representation of the specified array of objects
        ///     to the standard output stream using the specified format information.
        /// </summary>
        /// <param name="foregroundColor">
        ///     The <see cref="ConsoleColor" /> in which to write the text.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">
        ///     An array of objects to write using <paramref name="format" />.
        /// </param>
        [DebuggerStepThrough]
        void Write(ConsoleColor foregroundColor, string format, params object[] args);

        /// <summary>
        ///     Writes the text representation of the specified
        ///     array of objects to the standard output stream
        ///     using the specified format information, and then
        ///     reads the next line of characters from the
        ///     standard input stream.
        /// </summary>
        /// <param name="foregroundColor">
        ///     The <see cref="ConsoleColor" /> in which to
        ///     write the text.
        /// </param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">
        ///     An array of objects to write using
        ///     <paramref name="format" />.
        /// </param>
        [DebuggerStepThrough]
        string ReadLine(ConsoleColor foregroundColor, string format, params object[] args);

        /// <summary>
        ///     Writes the text representation of the specified
        ///     array of objects to the standard output stream
        ///     using the specified format information, and then
        ///     reads the next line of characters from the
        ///     standard input stream.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">
        ///     An array of objects to write using
        ///     <paramref name="format" />.
        /// </param>
        [DebuggerStepThrough]
        string ReadLine(string format, params object[] args);
    }
}