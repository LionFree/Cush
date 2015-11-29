using System;
using System.Linq;
using Cush.CommandLine;
using Cush.Common.Logging;
using Cush.Windows.Services.Internal;

namespace Cush.Windows.Services
{
    /// <summary>
    ///     An interface for defining Windows services.
    /// </summary>
    public abstract class WindowsService : IDisposable
    {
        private readonly IAssembly _assembly;
        private readonly IConsoleHarness _console;
        private readonly bool _interactive;
        private readonly WindowsServiceManager _manager;
        private readonly ServiceMetadata _metadata;
        protected readonly ICommandLineParser CommandLineParser;
        protected readonly WindowsServiceAttribute Configuration;
        protected readonly ServiceHarness Harness;
        protected readonly ILogger Logger;

        protected WindowsService()
            : this(Environment.UserInteractive,
                ServiceWrapper.Default,
                new NullLogger(),
                new ConsoleHarness(),
                AssemblyProxy.Default,
                new ServiceMetadata())
        {
        }

        protected WindowsService(bool interactive, IServiceWrapper wrapper, ILogger logger,
            IConsoleHarness console, IAssembly assembly,
            ServiceMetadata metadata)
            : this(
                interactive, wrapper, logger, new Parser("Test", "Test", true, console, assembly), console, assembly,
                metadata,
                WindowsServiceManager.GetInstance(metadata, console))
        {
        }

        protected WindowsService(bool interactive, IServiceWrapper wrapper, ILogger logger,
            ICommandLineParser parser, IConsoleHarness console, IAssembly assembly,
            ServiceMetadata metadata, WindowsServiceManager manager)
        {
            var attribute = GetType().GetAttribute<WindowsServiceAttribute>();
            if (attribute == null)
            {
                logger.Error(Strings.EXCEPTION_ServiceMustBeMarkedWithAttribute);
                throw new ArgumentException(Strings.EXCEPTION_ServiceMustBeMarkedWithAttribute);
            }

            Logger = logger;
            _interactive = interactive;
            Configuration = attribute;
            _assembly = assembly;
            _console = console;
            _metadata = metadata;
            _manager = manager;

            CommandLineParser = parser
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
        public virtual string BuildDate
        {
            get { return _assembly.BuildDate; }
        }

        /// <summary>
        ///     The endpoint address of the service.
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        ///     The name of the service.
        /// </summary>
        public string ServiceName
        {
            get { return Configuration.ServiceName; }
        }

        /// <summary>
        ///     A harness to which console output is directed.
        /// </summary>
        public IConsoleHarness Console
        {
            get { return _console; }
        }

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

            Console.WriteLine();
            Console.WriteLine("Paused.  Attach to process, then press Enter to continue.");
            Console.WriteLine();
            Console.ReadLine();

            if (args.Length > 0) Console.Run(args, _metadata.Service);
        }

        /// <summary>
        ///     This method is called when the service gets a request to start.
        /// </summary>
        public abstract void OnStart(string[] args);

        /// <summary>
        ///     This method is called when the service gets a request to stop.
        /// </summary>
        public abstract void OnStop();

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
        }

        /// <summary>
        ///     This method is called when a service gets a request to resume
        /// </summary>
        public virtual void OnContinue()
        {
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
                ServiceName = Configuration.ServiceName,
                EventLogName = Configuration.EventLogName,
                EventLogSource = Configuration.EventLogSource,
                LongDescription = Configuration.Description,
                Quiet = _metadata.Quiet,
                Silent = _metadata.Silent
            });

            CommandLineParser.Parse(args);
        }
    }
}