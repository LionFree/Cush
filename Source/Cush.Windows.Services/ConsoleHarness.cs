using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Cush.CommandLine;
using Cush.Common.Logging;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Cush.Windows.Services
{
    /// <summary>
    ///     A mockable <see cref="Console" />.
    /// </summary>
    [DebuggerStepThrough]
    public class ConsoleHarness : ConsoleProxy, IConsoleHarness
    {
        static ConsoleHarness()
        {
            Default = new ConsoleHarness();
        }

        public static ConsoleHarness Default { get; }

        private string EndpointsHeader(WindowsService service)
        {
            return (service.Endpoints.Length == 1)
                ? Strings.DEBUG_EndpointHeaderSingular
                : (service.Endpoints.Length > 1)
                    ? Strings.DEBUG_EndpointHeaderPlural
                    : string.Empty;
        }
        /// <summary>
        ///     Runs a service from the console given a service implementation.
        /// </summary>
        /// <param name="args">The command line arguments to pass to the service.</param>
        /// <param name="service">
        ///     The <see cref="WindowsService" /> implementation to start.
        /// </param>
        public void Run(WindowsService service, string[] args)
        {
            var serviceName = service.ServiceName;
            var buildDate = Assembly.GetAssembly(service.GetType()).RetrieveLinkerTimestamp();

            var isRunning = true;

            // Can't clear the console in a unit test,
            // so this line will throw an exception.
            Clear();
            WriteLine();
            WriteToConsole(ConsoleColor.White, Strings.DEBUG_ServiceNameAndBuildDate, serviceName, buildDate);
            WriteLine();
            WriteToConsole(ConsoleColor.White, EndpointsHeader(service));
            foreach (var endpoint in service.Endpoints)
            {
                WriteToConsole(ConsoleColor.White, Strings.DEBUG_EndpointHeader, endpoint);
            }
            
            // simulate starting the windows service
            service.OnStart(args);

            WriteLine();
            WriteToConsole(ConsoleColor.White, Strings.DEBUG_ServiceStarted);

            // let it run as long as Q is not pressed
            while (isRunning)
            {
                WriteLine();
                WriteToConsole(ConsoleColor.Yellow, Strings.DEBUG_EnterPauseResumeOrQuit);

                isRunning = HandleConsoleInput(service, ReadKey(true));
            }

            // stop and shutdown
            WriteLine();
            WriteToConsole(ConsoleColor.Yellow, Strings.DEBUG_ServiceStopping, serviceName);
            service.OnStop();
            WriteToConsole(ConsoleColor.Yellow, Strings.DEBUG_ServiceStopped, serviceName);
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
            WriteLine($"{value.ToString(CultureInfo.InvariantCulture)}");
        }

        [DebuggerStepThrough]
        public void Write(int value)
        {
            Write($"{value.ToString(CultureInfo.InvariantCulture)}");
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
                    WriteLine();
                    canContinue = false;
                    break;

                case ConsoleKey.P:
                    WriteLine();
                    WriteToConsole(ConsoleColor.Green, Strings.DEBUG_PausingService, service.ServiceName);
                    service.OnPause();
                    break;

                case ConsoleKey.R:
                    WriteLine();
                    WriteToConsole(ConsoleColor.Green,
                        Strings.DEBUG_ResumingService, service.ServiceName);
                    service.OnContinue();
                    break;

                default:
                    WriteToConsole(ConsoleColor.Red, Strings.DEBUG_BadKey);
                    break;
            }

            return canContinue;
        }
    }
}