using System;
using System.ServiceModel;
using Cush.Windows.Services;

namespace Cush.TestHarness.WinService.Infrastructure
{
    [Serializable]
    [WindowsService("Cush Service Program")]
    internal abstract class ServiceProgram : WindowsService
    {
        internal static ServiceProgram ComposeObjectGraph()
        {
            return new ProgramImpl();
        }

        private class ProgramImpl : ServiceProgram
        {


            private ServiceHost _serviceHost;

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
                throw new NotImplementedException();
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