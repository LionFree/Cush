using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Cush.CommandLine;

namespace Cush.Windows.Services
{
    /// <summary>
    ///     A mockable <see cref="Console" />.
    /// </summary>
    [DebuggerStepThrough]
    public class ConsoleHarness : ConsoleProxy, IConsoleHarness
    {
        /// <summary>
        ///     Runs a service from the console given a service implementation.
        /// </summary>
        /// <param name="args">The command line arguments to pass to the service.</param>
        /// <param name="service">
        ///     The <see cref="WindowsService" /> implementation to start.
        /// </param>
        public void Run(string[] args, WindowsService service)
        {
            var serviceName = service.ServiceName;
            var buildDate = Assembly.GetAssembly(service.GetType()).RetrieveLinkerTimestamp();

            var header = Environment.NewLine +
                         string.Format(CultureInfo.InvariantCulture, "{0} : built {1}", serviceName, buildDate);

            var endpoint = Environment.NewLine + string.Format("Service started.  Endpoint: {0}", service.Endpoint) +
                           Environment.NewLine;

            var isRunning = true;

            // Can't clear the console in a unit test,
            // so this line will throw an exception.
            Clear();
            WriteToConsole(ConsoleColor.White, header);
            WriteToConsole(ConsoleColor.White, endpoint);

            // simulate starting the windows service
            service.OnStart(args);

            // let it run as long as Q is not pressed
            while (isRunning)
            {
                WriteLine();
                WriteToConsole(ConsoleColor.Yellow, "Enter [P]ause, [R]esume, or [Q]uit : ");

                isRunning = HandleConsoleInput(service, ReadKey());
            }

            // stop and shutdown
            service.OnStop();
            service.OnShutdown();
        }

        [DebuggerStepThrough]
        public void WriteToConsole(string format, params object[] formatArguments)
        {
            WriteToConsole(ConsoleColor.Gray, format, formatArguments);
        }

        [DebuggerStepThrough]
        public void WriteToConsole(ConsoleColor foregroundColor, string format, params object[] formatArguments)
        {
            var originalColor = ForegroundColor;
            ForegroundColor = foregroundColor;

            WriteLine(format, formatArguments);
            Trace.WriteLine(string.Format(format, formatArguments));
            Flush();

            ForegroundColor = originalColor;
        }

        [DebuggerStepThrough]
        public void WriteLine(ConsoleColor foregroundColor, string format, params object[] args)
        {
            WriteToConsole(foregroundColor, format, args);
        }

        [DebuggerStepThrough]
        public void WriteLine(int value)
        {
            WriteLine("{0}", value.ToString(CultureInfo.InvariantCulture));
        }

        [DebuggerStepThrough]
        public void Write(int value)
        {
            Write("{0}", value.ToString(CultureInfo.InvariantCulture));
        }

        [DebuggerStepThrough]
        public void Write(ConsoleColor foregroundColor, string format, params object[] args)
        {
            var originalColor = ForegroundColor;
            ForegroundColor = foregroundColor;

            Write(format, args);
            Trace.Write(string.Format(format, args));
            Flush();

            ForegroundColor = originalColor;
        }

        [DebuggerStepThrough]
        public string ReadLine(ConsoleColor foregroundColor, string format, params object[] args)
        {
            Write(foregroundColor, format, args);
            return ReadLine();
        }

        [DebuggerStepThrough]
        public string ReadLine(string format, params object[] args)
        {
            return ReadLine(ConsoleColor.Gray, format, args);
        }

        private bool HandleConsoleInput(WindowsService service, ConsoleKeyInfo key)
        {
            var canContinue = true;

            // Check input

            switch (key.Key)
            {
                case ConsoleKey.Q:
                    canContinue = false;
                    break;

                case ConsoleKey.P:
                    service.OnPause();
                    break;

                case ConsoleKey.R:
                    service.OnContinue();
                    break;

                default:
                    WriteToConsole(ConsoleColor.Red, "Did not understand that input, try again.");
                    break;
            }

            return canContinue;
        }
    }
}