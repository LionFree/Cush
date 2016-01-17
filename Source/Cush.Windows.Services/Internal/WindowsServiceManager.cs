using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Globalization;
using System.ServiceProcess;
using System.Text.RegularExpressions;

namespace Cush.Windows.Services.Internal
{
    // Manages the windows service.
    [DebuggerStepThrough]
    public abstract class WindowsServiceManager
    {
        [DebuggerStepThrough]
        private sealed class Implementation : WindowsServiceManager
        {
            private readonly IAssembly _assembly;
            private readonly IConsoleHarness _console;
            private ServiceControllerProxy _controller;
            private readonly ManagedInstallerProxy _installerProxy;
            private bool _quiet;
            private string _serviceName;
            private bool _silent;
            private bool _logToConsole;


            public Implementation(IConsoleHarness console, IAssembly assembly, ManagedInstallerProxy installer)
            {
                _assembly = assembly;
                _console = console;
                _installerProxy = installer;
            }

            public override ServiceControllerStatus? Status
            {
                get
                {
                    try
                    {
                        return _controller.Status;
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }
                    return null;
                }
            }

            public override bool LogToConsole
            {
                get { return _logToConsole; }
                set { _logToConsole = value; }
            }

            internal override void SetController(ServiceControllerProxy controller)
            {
                _controller = controller;
            }

            public override void SetMetadata(ServiceMetadata metadata)
            {
                _quiet = metadata.Quiet;
                _silent = metadata.Silent;
                _serviceName = metadata.ServiceName;
                SetController(ServiceControllerProxy.GetInstance(metadata.ServiceName));
            }

            public override bool Install()
            {
                return InstallService(true);
            }

            public override bool Uninstall()
            {
                var assemblyLocation = _assembly.GetEntryAssembly().Location;

                WriteLine();
                Write(Strings.INFO_UninstallingWindowsService, _serviceName);
                try
                {
                    _installerProxy.InstallHelper(new[]
                    {
                        "/u",
                        "/LogToConsole=" + _logToConsole,
                        assemblyLocation
                    });

                    WriteLine("Done.");
                    return true;
                }
                catch (InstallException ex)
                {
                    return HandleException(ex);
                }
                catch (InvalidOperationException ex)
                {
                    return HandleException(ex);
                }
            }

            public override bool InstallAndStart()
            {
                var success = InstallService(true);
                if (!success) return false;

                success = StartService();
                if (!success) return false;

                return true;
            }

            public override bool StartService(string[] args)
            {
                try
                {
                    if (_controller.Status != ServiceControllerStatus.Stopped)
                    {
                        WriteError();
                        WriteError(Strings.INFO_ServiceAlreadyRunning, _serviceName);
                        return false;
                    }

                    WriteLine();
                    Write(Strings.INFO_StartingBackgroundService, _serviceName);

                    if (args == null) _controller.Start();
                    else _controller.Start(args);

                    _controller.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 0, 30));
                    WriteLine(Strings.INFO_Done);
                    return true;
                }
                catch (Exception ex)
                {
                    return HandleException(ex);
                }
            }

            public override bool StartService()
            {
                return StartService(null);
            }

            public override bool StopService()
            {
                try
                {
                    if (_controller.Status == ServiceControllerStatus.Stopped)
                    {
                        WriteError();
                        WriteError(Strings.INFO_ServiceNotStarted, _serviceName);
                        return false;
                    }

                    WriteLine();
                    Write(Strings.INFO_StoppingService, _serviceName);

                    if (_controller.CanStop)
                    {
                        _controller.Stop();
                        _controller.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 0, 30));
                        WriteLine(Strings.INFO_Done);

                        return true;
                    }

                    WriteError();
                    WriteError(Strings.WARN_CannotStopService);
                    return false;
                }
                catch (Exception ex)
                {
                    return HandleException(ex);
                }
            }

            public override bool ShowStatus()
            {
                try
                {
                    var status = Status;
                    if (status == null) return false;

                    _console.WriteLine();
                    _console.WriteLine(Strings.INFO_ServiceStatus, _serviceName, status);
                    return true;
                }
                catch (Exception ex)
                {
                    return HandleException(ex);
                }
            }

            private bool InstallService(bool firstTry)
            {
                WriteLine();
                Write(Strings.INFO_InstallingWindowsService, _serviceName);

                try
                {
                    var assemblyLocation = _assembly.GetEntryAssembly().Location;
                    _installerProxy.InstallHelper(new[]
                    {
                        "/LogToConsole=" + _logToConsole,
                        assemblyLocation
                    });

                    WriteLine(Strings.INFO_Done);
                    return true;
                }
                catch (InvalidOperationException ex)
                {
                    if (firstTry && IsAlreadyInstalled(ex))
                    {
                        OverWriteIfNecessary();
                        return true;
                    }

                    return HandleException(ex);
                }
            }

            [DebuggerStepThrough]
            private void OverWriteIfNecessary()
            {
                const int sameVersion = 0;
                const int newerVersion = 1;
                var existingVersion = InstalledVersionComparison();

                if (existingVersion == sameVersion)
                {
                    WriteLine(Strings.INFO_Done);
                    return;
                }
                if (existingVersion == newerVersion)
                {
                    if (!WantToOverwriteService())
                    {
                        return;
                    }
                }

                // Uninstall this version.
                Uninstall();

                // Try to install.
                InstallService(false);
            }

            [DebuggerStepThrough]
            private bool WantToOverwriteService()
            {
                // Check if we want to overwrite.
                _console.WriteLine(ConsoleColor.Yellow, Strings.Warn_InstalledVersionIsNewer);
                _console.WriteLine();
                _console.Write(ConsoleColor.Yellow, Strings.Warn_InstallAnyway, Strings.Noun_YesKey, Strings.Noun_NoKey);
                var key = _console.ReadKey();
                _console.WriteLine();

                return key.KeyChar.ToString(CultureInfo.CurrentUICulture) == Strings.Noun_YesKey;
            }

            private int InstalledVersionComparison()
            {
                var installedVersion = new Version();
                var thisVersion = new Version();

                var servicePath = GetExistingServicePath();
                try
                {
                    installedVersion = _assembly.GetVersion(servicePath);
                    thisVersion = _assembly.GetVersion();
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
                return installedVersion.CompareTo(thisVersion);
            }

            [DebuggerStepThrough]
            private string GetExistingServicePath()
            {
                var serviceKeyString = @"SYSTEM\CurrentControlSet\Services\" + _serviceName;

                var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(serviceKeyString);
                if (key == null) throw new Exception(Strings.EXCEPTION_CannotFindServiceInRegistry);

                var path = key.GetValue("ImagePath").ToString();
                key.Close();


                if (path.StartsWith("\""))
                {
                    path = Regex.Match(path, "\"([^\"]+)\"").Groups[1].Value;
                }

                return Environment.ExpandEnvironmentVariables(path);
            }

            [DebuggerStepThrough]
            private static bool IsAlreadyInstalled(Exception ex)
            {
                var inEx = ex.InnerException as Win32Exception;
                return inEx?.ErrorCode == -2147467259;
            }

            [DebuggerStepThrough]
            private void Write(string format, params object[] args)
            {
                if (!(_quiet || _silent))
                {
                    _console.Write(format, args);
                }
            }

            [DebuggerStepThrough]
            private void WriteLine(string format, params object[] args)
            {
                if (!(_quiet || _silent))
                {
                    _console.WriteLine(format, args);
                }
            }

            [DebuggerStepThrough]
            private void WriteLine()
            {
                if (!(_quiet || _silent))
                {
                    _console.WriteLine();
                }
            }

            [DebuggerStepThrough]
            private void WriteError(string format, params object[] args)
            {
                if (!_silent)
                {
                    _console.WriteLine(format, args);
                }
            }

            [DebuggerStepThrough]
            private void WriteError()
            {
                if (!_silent)
                {
                    _console.WriteLine();
                }
            }

            private bool HandleException(Exception ex)
            {
                if (ex is InstallException)
                {
                    if (!_silent)
                    {
                        _console.WriteLine();
                        _console.WriteToConsole(ConsoleColor.Yellow, Strings.WARN_ServiceNotInstalled, _serviceName);
                    }
                    return true;
                }

                if (ex is InvalidOperationException)
                {
                    if (ex.GetHashCode() == 55530882)
                    {
                        if (!_silent)
                        {
                            _console.WriteLine();
                            _console.WriteToConsole(ConsoleColor.Yellow, Strings.WARN_ServiceAlreadyInstalled,
                                _serviceName);
                        }
                        return true;
                    }
                }

                if (ex.InnerException != null)
                {
                    if (ex.InnerException is Win32Exception)
                    {
                        // could be trying to install something that's already installed
                        // OR could be trying to start a service that isn't installed

                        if (!_silent)
                        {
                            _console.WriteLine();
                            //_console.WriteToConsole(ConsoleColor.Yellow, Strings.WARN_ServiceAlreadyInstalled,
                            //    _serviceName);
                            _console.WriteToConsole(ConsoleColor.Yellow, ex.InnerException.Message, _serviceName);
                        }
                        return true;
                    }

                    _console.WriteLine();
                    _console.WriteToConsole(ConsoleColor.Yellow, ex.InnerException.GetType().FullName);
                    _console.WriteToConsole(ConsoleColor.Yellow, ex.InnerException.Message);
                    _console.WriteToConsole(ConsoleColor.Yellow,
                        ex.InnerException.GetHashCode().ToString(CultureInfo.InvariantCulture));
                }


                return false;
            }
        }

        #region Factory Methods

        internal static WindowsServiceManager GetInstance(IConsoleHarness harness)
        {
            return GetInstance(harness,
                AssemblyProxy.Default,
                ManagedInstallerProxy.Default);
        }

        internal static WindowsServiceManager GetInstance(
            IConsoleHarness harness,
            IAssembly assembly,
            ManagedInstallerProxy installerProxy)
        {
            return new Implementation(harness, assembly, installerProxy);
        }

        #endregion

        #region Abstract Members

        public abstract ServiceControllerStatus? Status { get; }

        /// <summary>
        ///     Installs the <see cref="WindowsService" />.
        /// </summary>
        /// <returns>true if successful, otherwise false.</returns>
        public abstract bool Install();

        /// <summary>
        ///     Installs and attempts to start the <see cref="WindowsService" />.
        /// </summary>
        /// <returns>true if successful, otherwise false.</returns>
        public abstract bool InstallAndStart();

        /// <summary>
        ///     Displays the <see cref="ServiceControllerStatus" /> of the <see cref="WindowsService" /> in the
        ///     <see cref="Console" />.
        /// </summary>
        /// <returns>true if successful, otherwise false.</returns>
        public abstract bool ShowStatus();

        /// <summary>
        ///     Attempts to start the <see cref="WindowsService" />, passing no arguments.
        /// </summary>
        /// <returns>true if successful, otherwise false.</returns>
        public abstract bool StartService();

        /// <summary>
        ///     Attempts to start the <see cref="WindowsService" />, passing the designated arguments.
        /// </summary>
        /// <returns>true if successful, otherwise false.</returns>
        public abstract bool StartService(string[] args);

        /// <summary>
        ///     Attempts to stop the <see cref="WindowsService" />.
        /// </summary>
        /// <returns>true if successful, otherwise false.</returns>
        public abstract bool StopService();

        /// <summary>
        ///     Uninstalls the <see cref="WindowsService" />.
        /// </summary>
        /// <returns>true if successful, otherwise false.</returns>
        public abstract bool Uninstall();


        public abstract bool LogToConsole { get; set; }

        public abstract void SetMetadata(ServiceMetadata metadata);
        internal abstract void SetController(ServiceControllerProxy controller);

        #endregion


    }
}