using System;
using System.Diagnostics;
using System.Runtime;
using System.Security;
using System.Security.Permissions;
using Cush.CLI;

namespace Cush.CommandLine
{
    /// <summary>
    /// Represents the standard input, output, and error streams for console applications. 
    /// This class can be inherited.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    public class ConsoleProxy : IConsole
    {
        private readonly ConsoleInterOp _interOp;

        public ConsoleProxy() : this(ConsoleInterOp.GetInstance())
        {
        }

        internal ConsoleProxy(ConsoleInterOp interOp)
        {
            _interOp = interOp;
        }

        /// <summary>
        ///     Plays the sound of a beep through the console speaker.
        /// </summary>
        /// <exception cref="T:System.Security.HostProtectionException">
        ///     This method was executed on a server, such as SQL Server,
        ///     that does not permit access to a user interface.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        [DebuggerStepThrough]
        public void Beep()
        {
            Console.Beep();
        }

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
        [SecuritySafeCritical]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        [DebuggerStepThrough]
        public void Beep(int frequency, int duration)
        {
            Console.Beep(frequency, duration);
        }

        /// <summary>
        ///     Brings the console window to the front of the z-buffer,
        ///     even if the console belongs to another application.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">There is no console.</exception>
        [SecuritySafeCritical]
        [DebuggerStepThrough]
        public IConsole SetOnTop()
        {
            if (!_interOp.IsConsoleApp)
                throw new InvalidOperationException(
                    "Cannot bring Console window to the foreground: not operating in a Console application.");

            _interOp.ShowFirstConsole();

            return this;
        }

        /// <summary>
        ///     Gets or sets the title to display in the console title bar.
        /// </summary>
        /// <returns>
        ///     The string to be displayed in the title bar of the console. The maximum length of the title string is 24500
        ///     characters.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">
        ///     In a get operation, the retrieved title is longer than 24500
        ///     characters.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     In a set operation, the specified title is longer than 24500
        ///     characters.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">In a set operation, the specified title is null. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Window="SafeTopLevelWindows" />
        /// </PermissionSet>
        public string Title
        {
            [SecuritySafeCritical] get { return Console.Title; }
            [SecuritySafeCritical] set { Console.Title = value; }
        }

        /// <summary>
        ///     Gets or sets the foreground color of the console.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.ConsoleColor" /> that specifies the foreground color of the console; that is, the color of
        ///     each character that is displayed. The default is gray.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The color specified in a set operation is not a valid member of
        ///     <see cref="T:System.ConsoleColor" />.
        /// </exception>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Window="SafeTopLevelWindows" />
        /// </PermissionSet>
        public ConsoleColor ForegroundColor
        {
            [SecuritySafeCritical] get { return Console.ForegroundColor; }
            [SecuritySafeCritical] set { Console.ForegroundColor = value; }
        }

        /// <summary>
        ///     Gets or sets the background color of the console.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.ConsoleColor" /> that specifies the background color of the console; that is, the color that
        ///     appears behind each character. The default is black.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The color specified in a set operation is not a valid member of
        ///     <see cref="T:System.ConsoleColor" />.
        /// </exception>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Window="SafeTopLevelWindows" />
        /// </PermissionSet>
        public ConsoleColor BackgroundColor
        {
            [SecuritySafeCritical] get { return Console.BackgroundColor; }
            [SecuritySafeCritical] set { Console.BackgroundColor = value; }
        }

        /// <summary>
        ///     Clears the console buffer and corresponding console window of display information.
        /// </summary>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
        /// <filterpriority>1</filterpriority>
        [SecuritySafeCritical]
        [DebuggerStepThrough]
        public void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        ///     Reads the next line of characters from the standard input stream.
        /// </summary>
        /// <returns>
        ///     The next line of characters from the input stream, or null if no more lines are available.
        /// </returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <exception cref="T:System.OutOfMemoryException">
        ///     There is insufficient memory to allocate a buffer for the returned
        ///     string.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     The number of characters in the next line of characters is
        ///     greater than <see cref="F:System.Int32.MaxValue" />.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        [DebuggerStepThrough]
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Obtains the next character or function key pressed by the user. The pressed key is displayed in the console window.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.ConsoleKeyInfo"/> object that describes the <see cref="T:System.ConsoleKey"/> constant and Unicode character, if any, that correspond to the pressed console key. The <see cref="T:System.ConsoleKeyInfo"/> object also describes, in a bitwise combination of <see cref="T:System.ConsoleModifiers"/> values, whether one or more SHIFT, ALT, or CTRL modifier keys was pressed simultaneously with the console key.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Console.In"/> property is redirected from some stream other than the console.</exception><filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        [DebuggerStepThrough]
        public ConsoleKeyInfo ReadKey()
        {
            return ReadKey(false);
        }

        /// <summary>
        ///     Obtains the next character or function key pressed by the user. The pressed key is displayed in the console window.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.ConsoleKeyInfo" /> object that describes the <see cref="T:System.ConsoleKey" /> constant and
        ///     Unicode character, if any, that correspond to the pressed console key. The <see cref="T:System.ConsoleKeyInfo" />
        ///     object also describes, in a bitwise combination of <see cref="T:System.ConsoleModifiers" /> values, whether one or
        ///     more Shift, Alt, or Ctrl modifier keys was pressed simultaneously with the console key.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The <see cref="P:System.Console.In" /> property is redirected from
        ///     some stream other than the console.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        [DebuggerStepThrough]
        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        /// <summary>
        ///     Writes the text representation of the specified array of objects to the standard output stream using the specified
        ///     format information.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg">An array of objects to write using <paramref name="format" />. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="format" /> or <paramref name="arg" /> is null. </exception>
        /// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        [DebuggerStepThrough]
        public void Write(string format, params object[] arg)
        {
            Console.Write(format, arg);
        }

        /// <summary>
        ///     Writes the text representation of the specified array of objects, followed by the current line terminator, to the
        ///     standard output stream using the specified format information.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="arg">An array of objects to write using <paramref name="format" />. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="format" /> or <paramref name="arg" /> is null. </exception>
        /// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        [DebuggerStepThrough]
        public void WriteLine(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
        }


        /// <summary>
        ///     Writes a blank line to the
        ///     standard output stream using the specified format information.
        /// </summary>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        [DebuggerStepThrough]
        public void WriteLine()
        {
            Console.WriteLine();
        }

        /// <summary>
        ///     Clears all buffers for this Console and causes any buffered data to be
        ///     written to the Console.
        /// </summary>
        [DebuggerStepThrough]
        public void Flush()
        {
            Console.Out.Flush();
        }
    }
}