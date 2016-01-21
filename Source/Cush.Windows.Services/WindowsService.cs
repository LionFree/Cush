using System;
using System.Linq;
using System.ServiceModel;
using Cush.CommandLine;
using Cush.Common.Logging;
using Cush.Windows.Services.Internal;
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable VirtualMemberNeverOverriden.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable EventNeverSubscribedTo.Global

namespace Cush.Windows.Services
{
    /// <summary>
    ///     An interface for defining Windows services.
    /// </summary>
    [Serializable]
    public abstract class WindowsService : IDisposable
    {
        private readonly IAssembly _assembly;
        private readonly bool _interactive;
        private readonly WindowsServiceManager _manager;
        private readonly ServiceMetadata _metadata;
        protected readonly ICommandLineParser CommandLineParser;
        protected readonly ServiceHarness Harness;
        protected readonly ILogger Logger;
        protected ServiceHost Host;
        private readonly HostFactory _hostFactory;

        public event EventHandler Started;
        public event EventHandler Stopped;

        protected virtual void OnStarted(EventArgs e = null)
        {
            Started?.Invoke(this, e ?? new EventArgs());
        }

        protected virtual void OnStopped(EventArgs e = null)
        {
            Stopped?.Invoke(this, e ?? new EventArgs());
        }

        protected void BuildHost(WindowsServiceDescription description)
        {
            Host?.Close();
            Host = _hostFactory.GetNewServiceHost(description);
        }

        protected WindowsService(ILogger logger)
            : this(logger, new WideOpenAuthorizationManager())
        {
        }

        protected WindowsService(ILogger logger, ServiceAuthorizationManager authManager)
            : this(Environment.UserInteractive, ServiceWrapper.Default, logger, ConsoleHarness.Default, AssemblyProxy.Default,
                new ServiceAttributeReader(logger), HostFactory.GetInstance(logger, HostBuilder.GetInstance(logger, authManager)))
        {
        }

        private WindowsService(bool interactive, IServiceWrapper wrapper, ILogger logger,
            IConsoleHarness console, IAssembly assembly, ServiceAttributeReader reader, HostFactory factory)
            : this(interactive, wrapper, logger, new Parser(Strings.DEBUG_Cush, string.Empty, true, console, assembly), console, assembly, 
                  reader, WindowsServiceManager.GetInstance(console), factory)
        {
        }

        private WindowsService(bool interactive, IServiceWrapper wrapper, ILogger logger,
            ICommandLineParser parser, IConsoleHarness console, IAssembly assembly, 
            ServiceAttributeReader reader, WindowsServiceManager manager, HostFactory hostFactory)
        {
            Logger = logger;
            _metadata = reader.GetMetadata(this);

            _interactive = interactive;
            _assembly = assembly;
            Console = console;
            _manager = manager;
            _manager.SetMetadata(_metadata);
            _hostFactory = hostFactory;

            CommandLineParser = parser
                .SetApplicationName(reader.GetAttribute(this).DisplayName)
                .SetDescription(reader.GetAttribute(this).Description)
                .AddOption("quiet", "q", "Enable quiet mode. Will display only errors on the console.",
                    noArgs => _metadata.Quiet = true)
                .AddOption("silent", "si", "Enable silent mode. Will display nothing (not even errors) on the console.",
                    noArgs => _metadata.Silent = true)
                .AddOption("logtoconsole", "l", "Instructs the installer/uninstaller to log the output to the console.",
                    noArgs => _manager.LogToConsole = true)
                .AddOption("debug", "d", "Pauses to attach a debugger.", noArgs => EnableDebugMode())
                .AddOption("uninstall", "u", "Uninstalls the service.", noArgs => _manager.Uninstall())
                .AddOption("install", "i", "Installs the service.", noArgs => _manager.Install())
                .AddOption("installandstart", "is", "Installs and then starts the service.",
                    noArgs => _manager.InstallAndStart())
                .AddOption("start", "s", "Starts the service.", noArgs => _manager.StartService())
                .AddOption("stop", "x", "Stops the service.", noArgs => _manager.StopService())
                .AddOption("status", "st", "Displays the status of the service.", noArgs => _manager.ShowStatus());

            Harness = wrapper.WrapService(this);
        }
        
        /// <summary>
        ///     Gets the date and time of the last time the assembly was compiled.
        /// </summary>
        public virtual string BuildDate => _assembly.BuildDate;

        /// <summary>
        ///     The endpoint addresses of the service.
        /// </summary>
        public string[] Endpoints => Host?.Description?.Endpoints?.Select(item => item.Address.ToString()).ToArray();

        /// <summary>
        ///     The name of the service.
        /// </summary>
        public string ServiceName => _metadata.ServiceName;

        /// <summary>
        ///     A harness to which console output is directed.
        /// </summary>
        public IConsoleHarness Console { get; }

        /// <summary>
        ///     Performs application-defined tasks associated with
        ///     freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
        }

        protected virtual void EnableDebugMode()
        {
            var args = Environment.GetCommandLineArgs().Skip(1).ToArray();

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(Strings.INFO_PausedForDebugger);
            Console.WriteLine(Environment.NewLine);
            Console.ReadLine();

            if (args.Length > 0) Console.Run(_metadata.Service, args);
        }

        /// <summary>
        ///     This method is called when the service gets a request to start.
        /// </summary>
        public virtual void OnStart(string[] args)
        {
            // Open the ServiceHostBase to create listeners and start 
            // listening for messages.
            try
            {
                Logger.Info(Strings.DEBUG_StartingService, ServiceName);
                Host.Open();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            OnStarted(EventArgs.Empty);
        }

        /// <summary>
        ///     This method is called when the service gets a request to stop.
        /// </summary>
        public virtual void OnStop()
        {
            Logger.Info(Strings.INFO_StoppingService, ServiceName);
            Console.WriteLine(Strings.INFO_StoppingService, ServiceName);
            if (Host == null) return;

            try
            {
                Host.Close();
                Host = null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Console.WriteLine(ConsoleColor.Red, Strings.EXCEPTION_HeaderWithMessage, ex.Message);
            }
            OnStopped(EventArgs.Empty);
            Console.WriteLine(Strings.INFO_ServiceStopped);
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
                Console.WriteToConsole(ConsoleColor.White, Strings.EXCEPTION_HeaderWithMessage, ex.Message);
            }

            if (ex.InnerException == null) return;
            Console.WriteLine(ex.InnerException.Message);
        }

        /// <summary>
        ///     This method is called when the service gets a request to execute a custom command.
        /// </summary>
        public abstract void OnCustomCommand(int command);

        /// <summary>
        ///     This method is called when a service gets a request to pause,
        ///     but not stop completely.
        /// </summary>
        public virtual void OnPause()
        {
            Logger.Info(Strings.INFO_ServicePaused, ServiceName);
        }
        
        /// <summary>
        ///     This method is called when a service gets a request to resume
        /// </summary>
        public virtual void OnContinue()
        {
            Logger.Info(Strings.INFO_ServiceResumed, ServiceName);
        }

        /// <summary>
        ///     This method is called when the machine the service is running on
        /// </summary>
        public virtual void OnShutdown()
        {
        }

        public virtual void Startup()
        {
            Startup(null);
        }

        public virtual void Startup(string[] args)
        {
            if (_interactive)
            {
                RunInteractiveSession(args);
            }
            else
            {
                Harness.Run();
            }
        }

        protected virtual void RunInteractiveSession(string[] args)
        {
            if (!_interactive) return;

            _manager.SetMetadata(new ServiceMetadata
            {
                Service = this,
                ServiceName = _metadata.ServiceName,
                Quiet = _metadata.Quiet,
                Silent = _metadata.Silent
            });

            CommandLineParser.Parse(args);
        }
    }
}