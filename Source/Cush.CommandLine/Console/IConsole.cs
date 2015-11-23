using System;

namespace Cush.CommandLine
{
    /// <summary>
    ///     An interface for wrapping the <see cref="T:System.Console" />, which
    ///     represents the standard input, output, and error streams for console applications.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    public interface IConsole
    {
        /// <summary>
        ///     Gets or sets the title to display in the console title bar.
        /// </summary>
        /// <returns>
        ///     The string to be displayed in the title bar of the console. The maximum length of the title string is 24500
        ///     characters.
        /// </returns>
        string Title { get; set; }

        /// <summary>
        ///     Gets or sets the foreground color of the console.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.ConsoleColor" /> that specifies the foreground color of the console; that is, the color of
        ///     each character that is displayed. The default is gray.
        /// </returns>
        ConsoleColor ForegroundColor { get; set; }

        /// <summary>
        ///     Gets or sets the background color of the console.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.ConsoleColor" /> that specifies the background color of the console; that is, the color that
        ///     appears behind each character. The default is black.
        /// </returns>
        ConsoleColor BackgroundColor { get; set; }

        /// <summary>
        ///     Plays the sound of a beep through the console speaker.
        /// </summary>
        /// <exception cref="T:System.Security.HostProtectionException">
        ///     This method was executed on a server, such as SQL Server,
        ///     that does not permit access to a user interface.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        void Beep();

        /// <summary>
        ///     Plays the sound of a beep of a specified frequency and duration through the console speaker.
        /// </summary>
        /// <param name="frequency">The frequency of the beep, ranging from 37 to 32767 hertz.</param>
        /// <param name="duration">The duration of the beep measured in milliseconds.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="frequency" /> is less than 37 or more than 32767
        ///     hertz.-or-<paramref name="duration" /> is less than or equal to zero.
        /// </exception>
        /// <exception cref="T:System.Security.HostProtectionException">
        ///     This method was executed on a server, such as SQL Server,
        ///     that does not permit access to the console.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        void Beep(int frequency, int duration);

        /// <summary>
        ///     Brings the <see cref="T:IConsole" /> window to the front of the z-order,
        ///     even if the Console is a child of another process.
        /// </summary>
        IConsole SetOnTop();

        /// <summary>
        ///     Clears the console buffer and corresponding console window of display information.
        /// </summary>
        void Clear();

        /// <summary>
        ///     Reads the next line of characters from the standard input stream.
        /// </summary>
        string ReadLine();

        /// <summary>
        ///     Obtains the next character or function key pressed by the user. The pressed key is displayed in the console window.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.ConsoleKeyInfo" /> object that describes the <see cref="T:System.ConsoleKey" /> constant and
        ///     Unicode character, if any, that correspond to the pressed console key. The <see cref="T:System.ConsoleKeyInfo" />
        ///     object also describes, in a bitwise combination of <see cref="T:System.ConsoleModifiers" /> values, whether one or
        ///     more SHIFT, ALT, or CTRL modifier keys was pressed simultaneously with the console key.
        /// </returns>
        ConsoleKeyInfo ReadKey();

        /// <summary>
        ///     Obtains the next character or function key pressed by the user.
        ///     The pressed key is optionally displayed in the console.
        /// </summary>
        /// <param name="intercept">
        ///     Determines whether to display the pressed key in the console window.
        ///     true to not display the pressed key, otherwise false.
        /// </param>
        ConsoleKeyInfo ReadKey(bool intercept);

        /// <summary>
        ///     Writes the text representation of the specified array of objects to
        ///     the standard output stream using the speficied format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg">
        ///     An array of objects to write using <paramref name="format" />.
        /// </param>
        void Write(string format, params object[] arg);

        /// <summary>
        ///     Writes the text representation of the specified array of objects,
        ///     followed by the current line terminator,
        ///     to the standard output stream using the speficied format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg">
        ///     An array of objects to write using <paramref name="format" />.
        /// </param>
        void WriteLine(string format, params object[] arg);

        /// <summary>
        ///     Writes a blank line 
        ///     to the standard output stream using the speficied format information.
        /// </summary>
        void WriteLine();


        /// <summary>
        ///     Clears all buffers for the standard output stream and causes any
        ///     buffered data to be written to the standard output stream.
        /// </summary>
        void Flush();
    }
}