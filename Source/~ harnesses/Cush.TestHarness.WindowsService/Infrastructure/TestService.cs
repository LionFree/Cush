using System;
using System.ServiceModel;
using Cush.Common.Logging;
using Cush.Windows.Services;

namespace Cush.TestHarness.WinService.Infrastructure
{
    [Serializable]
    [WindowsService("Cush Service Program")]
    internal abstract class TestService : WindowsService
    {
        protected TestService(ILogger logger) : base(logger)
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="TestService" />.
        ///     Used by the service Installer.
        /// </summary>
        internal static TestService GetInstance(ILogger logger, ServiceHost host)
        {
            return new Implementation(logger, host);
        }

        private class Implementation : TestService
        {
            public Implementation(ILogger logger, ServiceHost host):base(logger)
            {
                _logger = logger;
                _serviceHost = host;
            }

            private readonly ServiceHost _serviceHost;
            private readonly ILogger _logger;

            public override void OnStart(string[] args)
            {
                // Open the ServiceHostBase to create listeners and start 
                // listening for messages.
                try
                {
                    Logger.Info("Info_StartingService: {0}", ServiceName);
                    _serviceHost.Open();
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
                //OnStarted();
            }

            public override void OnStop()
            {
                // Do nothing.
                // Shutdown.
            }

            public override void OnCustomCommand(int command)
            {
                throw new NotImplementedException();
            }

            private void HandleException(Exception ex)
            {
                if (ex is AddressAlreadyInUseException)
                {
                    Logger.Error(ex, Strings.Error_CannotOpenHost_AddressInUse,
                        ServiceName);

                    Console.WriteToConsole(ConsoleColor.White,
                        Strings.Error_CannotOpenHost_AddressInUse,
                        ServiceName);
                }
                else if (ex is AddressAccessDeniedException)
                {
                    Logger.Error(ex, Strings.Error_CannotOpenHost_AccessDenied,
                        ServiceName);

                    Console.WriteToConsole(ConsoleColor.White,
                        Strings.Error_CannotOpenHost_AccessDenied,
                        ServiceName);
                }
                else
                {
                    Logger.Error(ex, Strings.Error_CannotOpenHost,
                        ServiceName);

                    Console.WriteToConsole(ConsoleColor.White,
                        Strings.Error_CannotOpenHost,
                        ServiceName);
                    Console.WriteToConsole(ConsoleColor.White, Strings.EXCEPTION, ex.Message);
                }

                if (ex.InnerException == null) return;
                Console.WriteLine(ex.InnerException.Message);
            }
        }
    }
}