﻿using System;
using System.Globalization;
using System.ServiceProcess;
using Cush.Common.Exceptions;

namespace Cush.Windows.Services
{
    /// <summary>
    ///     Creates a new set of installers based on a service object which implements the <see cref="WindowsService" />
    ///     abstract class.
    /// </summary>
    public abstract class WindowsServiceInstaller
    {
        private static readonly ConsoleHarness Harness;

        static WindowsServiceInstaller()
        {
            Harness = ConsoleHarness.Default;
        }

        /// <summary>
        ///     Installs a class that extends <see cref="T:System.ServiceProcess.ServiceBase" /> to implement a service.
        ///     This class is called by installation utilities, such as InstallUtil.exe, when installing a service application.
        /// </summary>
        public abstract ServiceInstaller ServiceInstaller { get; }

        /// <summary>
        ///     Installs an executable containing classes that extend <see cref="T:System.ServiceProcess.ServiceBase" />.
        ///     This class is called by installation utilities, such as InstallUtil.exe, when installing a service application.
        /// </summary>
        public abstract ServiceProcessInstaller ProcessInstaller { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:ServiceSentry.Common.ServiceFramework.WindowsServiceInstaller" />
        ///     class,
        ///     which creates a new set of installers based on the service object <paramref name="service" />.
        /// </summary>
        /// <typeparam name="T">
        ///     A type, which inherits from <see cref="WindowsService" />, to generate the installers from.
        /// </typeparam>
        public static WindowsServiceInstaller WrapService<T>(T service) where T : WindowsService
        {
            return new ServiceInstallerImplementation<T>(service);
        }
        
        private static void HandleException(Exception ex)
        {
            Harness.WriteLine();
            Harness.WriteToConsole(ConsoleColor.Yellow, ex.InnerException.GetType().FullName);
            Harness.WriteToConsole(ConsoleColor.Yellow, ex.InnerException.Message);
            Harness.WriteToConsole(ConsoleColor.Yellow,
                ex.InnerException.GetHashCode().ToString(CultureInfo.InvariantCulture));
            Harness.WriteToConsole(ConsoleColor.Yellow, ex.StackTrace);
        }

        private static WindowsServiceAttribute GetAttribute(WindowsService service)
        {
            var attribute = service.GetType().GetAttribute<WindowsServiceAttribute>();
            ThrowHelper.IfNullThenThrowArgumentException(() => attribute,
                Strings.EXCEPTION_ServiceMustBeMarkedWithAttribute);
            return attribute;
        }

        private class ServiceInstallerImplementation<T> : WindowsServiceInstaller where T : WindowsService
        {
            private readonly WindowsServiceAttribute _configuration;

            // Creates a windows service installer using the type specified.
            public ServiceInstallerImplementation(T service)
            {
                _configuration = GetAttribute(service);

                try
                {
                    ServiceInstaller = ConfigureServiceInstaller();
                    ProcessInstaller = ConfigureProcessInstaller();
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }

            public override ServiceInstaller ServiceInstaller { get; }

            public override ServiceProcessInstaller ProcessInstaller { get; }


            // Helper method to configure a process installer for this windows service
            private ServiceProcessInstaller ConfigureProcessInstaller()
            {
                var result = new ServiceProcessInstaller();

                // if a username is not provided, will run under local system account
                if (string.IsNullOrEmpty(_configuration.UserName))
                {
                    result.Account = ServiceAccount.LocalSystem;
                    result.Username = null;
                    result.Password = null;
                }
                else
                {
                    result.Account = ServiceAccount.User;
                    result.Username = _configuration.UserName;
                    result.Password = _configuration.Password;
                }
                return result;
            }

            // Helper method to configure a service installer for this windows service
            private ServiceInstaller ConfigureServiceInstaller()
            {
                var result = new ServiceInstaller
                {
                    ServiceName = _configuration.ServiceName,
                    DisplayName = _configuration.DisplayName,
                    Description = _configuration.Description,
                    StartType = _configuration.StartMode
                };
                return result;
            }
        }
    }
}