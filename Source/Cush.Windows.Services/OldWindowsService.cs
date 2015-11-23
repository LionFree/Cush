using System;
using Cush.CLI;
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
        protected readonly WindowsServiceAttribute Configuration;
        protected readonly ServiceHarness Harness;
        protected readonly ILogger Logger;
        protected readonly ICommandLineParser Parser;

        protected WindowsService()
            : this(Environment.UserInteractive,
            ServiceWrapper.Default,
            NullLogger.Default, 
            new Parser("Test"), 
            new ConsoleHarness(), 
            AssemblyProxy.Default)
        {
        }

        protected WindowsService(bool interactive, IServiceWrapper wrapper, ILogger logger, ICommandLineParser parser, IConsoleHarness console, IAssembly assembly)
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
            Parser = parser;
            _console = console;
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

            var description = new ServiceMetadata
            {
                Service = this,
                ServiceName = Configuration.ServiceName,
                EventLogName = Configuration.EventLogName,
                EventLogSource = Configuration.EventLogSource,
                LongDescription = Configuration.Description
            };

            ParseArgs(args, description);
        }

        protected virtual void ShowDefaultHelpOnConsole(ServiceMetadata metadata)
        {
            var name = metadata.ServiceName + "\t(" + BuildDate + ")";

            var description = metadata.ShortDescription;
            var synopsis = metadata.ServiceName +
                           ".exe [OPTIONS]";
            const string dnw =
                "Does not prevent installer logs from appearing on the console if the -l (log to console) option is enabled.";
            const string siOption = "--si, --silent";
            const string siDetails = "Enable silent mode.  Will display nothing (not even errors) on the console.";
            const string qOption = "--q, --quiet";
            const string qDetails = "Enable quiet mode.  Will display only errors on the console.";
            const string uOption = "--u, --uninstall";
            const string uDetails = "Uninstalls the service.";
            const string iOption = "--i, --install";
            const string iDetails = "Installs the service.";
            const string lOption = "--l, --logtoconsole";
            const string lDetails = "Instructs the installer/uninstaller to log the output to the console.";
            const string isOption = "--is, --installandstart";
            const string isDetails = "Installs and then starts the service.";
            const string sOption = "--s, --start";
            const string sDetails = "Starts the service.";
            const string xOption = "--x, --stop";
            const string xDetails = "Stops the service.";
            const string stOption = "--status";
            const string stDetails = "Displays the status of the service.";

            Console.WriteLine(" ");
            Console.WriteLine("NAME");
            Console.WriteLine("\t" + name);
            Console.WriteLine("\t" + description);
            Console.WriteLine(" ");
            Console.WriteLine("USAGE");
            Console.WriteLine("\t" + synopsis);
            Console.WriteLine(" ");
            Console.WriteLine("OPTIONS");
            Console.WriteLine("\t" + lOption);
            Console.WriteLine("\t   " + lDetails);
            Console.WriteLine(" ");
            Console.WriteLine("\t" + uOption);
            Console.WriteLine("\t   " + uDetails);
            Console.WriteLine(" ");
            Console.WriteLine("\t" + iOption);
            Console.WriteLine("\t   " + iDetails);
            Console.WriteLine(" ");
            Console.WriteLine("\t" + isOption);
            Console.WriteLine("\t   " + isDetails);
            Console.WriteLine(" ");
            Console.WriteLine("\t" + sOption);
            Console.WriteLine("\t   " + sDetails);
            Console.WriteLine(" ");
            Console.WriteLine("\t" + xOption);
            Console.WriteLine("\t   " + xDetails);
            Console.WriteLine(" ");
            Console.WriteLine("\t" + stOption);
            Console.WriteLine("\t   " + stDetails);
            Console.WriteLine(" ");
            Console.WriteLine("\t" + siOption);
            Console.WriteLine("\t   " + siDetails);
            Console.WriteLine("\t   " + dnw);
            Console.WriteLine(" ");
            Console.WriteLine("\t" + qOption);
            Console.WriteLine("\t   " + qDetails);
            Console.WriteLine("\t   " + dnw);
        }

        protected virtual void ParseArgs(string[] args, ServiceMetadata metadata)
        {
            if (args == null) return;

            if (args.Length == 0)
            {
                ShowDefaultHelpOnConsole(metadata);
                return;
            }

            var commandLine = Parser.Parse(args);
            if (commandLine.Length == 0) ShowDefaultHelpOnConsole(metadata);

            if (commandLine["q"] != null || commandLine["quiet"] != null)
            {
                metadata.Quiet = true;
            }

            if (commandLine["si"] != null || commandLine["silent"] != null)
            {
                metadata.Silent = true;
            }

            if (commandLine["d"] != null || commandLine["debug"] != null)
            {
                PauseForDebugger();
                if (commandLine.Length == 1) Console.Run(args, metadata.Service);
            }

            var logToConsole = (commandLine["l"] != null || commandLine["logtoconsole"] != null);
            var manager = WindowsServiceManager
                .GetInstance(metadata, Console, logToConsole);

            if (commandLine["i"] != null || commandLine["install"] != null)
            {
                manager.Install();
            }

            if (commandLine["u"] != null || commandLine["uninstall"] != null)
            {
                manager.Uninstall();
            }

            if (commandLine["is"] != null || commandLine["installandstart"] != null)
            {
                manager.InstallAndStart();
            }

            if (commandLine["s"] != null || commandLine["start"] != null)
            {
                manager.StartService();
            }

            if (commandLine["x"] != null || commandLine["stop"] != null)
            {
                manager.StopService();
            }

            if (commandLine["status"] != null)
            {
                manager.ShowStatus();
            }
        }

        protected virtual void PauseForDebugger()
        {
            Console.WriteLine();
            Console.WriteLine("Paused.  Attach to process, then press Enter to continue.");
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}