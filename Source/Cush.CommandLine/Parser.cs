using System;
using System.Diagnostics;
using System.Linq;
using Cush.CommandLine.Exceptions;
using Cush.CommandLine.Internal;
using Cush.Common;

namespace Cush.CommandLine
{
    public class Parser : ICommandLineParser
    {
        private readonly IConsole _console;
        private readonly CommandLineOptionStore _delegates;

        private readonly bool _displayUsageIfNoArgumentsPassed;
        private readonly CommandLineExceptionHandler _exceptionHandler;
        private readonly CLITokenizer _tokenizer;
        private readonly CommandLineUsageBuilder _usageBuilder;
        private readonly VersionFinder _versionFinder;
        private string _passedArguments;
        private readonly string _specialNotes;

        /// <summary>
        ///     Creates a new instance of the <see cref="T:Parser"/> class.
        /// </summary>
        /// <param name="appName">The friendly and readable text representation of the application name.</param>
        /// <param name="commandDescription">
        ///     This is a short description of the tool.
        ///     Guidance is 12 words maximum.
        /// </param>
        /// <param name="specialNotes">A <see cref="string"/> containing special notes to be added to the end of the usage screen.</param>
        /// <param name="displayUsageIfNoArgumentsPassed">
        ///     A value that determines whether the usage page will be displayed when no
        ///     arguments are passed.
        /// </param>
        public Parser(string appName, string commandDescription, string specialNotes,
            bool displayUsageIfNoArgumentsPassed)
            : this(appName, commandDescription, specialNotes, displayUsageIfNoArgumentsPassed, new ConsoleProxy())
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="T:Parser"/> class.
        /// </summary>
        /// <param name="appName">The friendly and readable text representation of the application name.</param>
        /// <param name="commandDescription">
        ///     This is a short description of the tool.
        ///     Guidance is 12 words maximum.
        /// </param>
        /// <param name="displayUsageIfNoArgumentsPassed">
        ///     A value that determines whether the usage page will be displayed when no
        ///     arguments are passed.
        /// </param>
        /// <param name="console">The <see cref="T:IConsole" /> on which to display output.</param>
        /// <param name="buildInfo">The <see cref="T:IBuildInfo" /> from which to get version and build date information.</param>
        public Parser(string appName, string commandDescription, bool displayUsageIfNoArgumentsPassed,
            IConsole console, IBuildInfo buildInfo)
            : this(
                displayUsageIfNoArgumentsPassed, string.Empty, CLITokenizer.GetInstance(),
                CommandLineUsageBuilder.GetInstance(commandDescription), CommandLineOptionStore.GetInstance(),
                VersionFinder.GetInstance(appName, buildInfo), CommandLineExceptionHandler.GetInstance(console), console
                )
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="T:Parser"/> class.
        /// </summary>
        /// <param name="appName">The friendly and readable text representation of the application name.</param>
        /// <param name="commandDescription">
        ///     This is a short description of the tool.
        ///     Guidance is 12 words maximum.
        /// </param>
        /// <param name="displayUsageIfNoArgumentsPassed">
        ///     A value that determines whether the usage page will be displayed when no
        ///     arguments are passed.
        /// </param>
        /// <param name="console">The <see cref="T:IConsole" /> on which to display output.</param>
        /// <param name="specialNotes">A <see cref="string"/> containing special notes to be added to the end of the usage screen.</param>
        public Parser(string appName, string commandDescription, string specialNotes, bool displayUsageIfNoArgumentsPassed,
            IConsole console)
            : this(
                displayUsageIfNoArgumentsPassed, specialNotes, CLITokenizer.GetInstance(),
                CommandLineUsageBuilder.GetInstance(commandDescription), CommandLineOptionStore.GetInstance(),
                VersionFinder.GetInstance(appName, null), CommandLineExceptionHandler.GetInstance(console), console
                )
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="T:Parser"/> class.
        /// </summary>
        /// <param name="appName">The friendly and readable text representation of the application name.</param>
        /// <param name="commandDescription">
        ///     This is a short description of the tool.
        ///     Guidance is 12 words maximum.
        /// </param>
        /// <param name="displayUsageIfNoArgumentsPassed">
        ///     A value that determines whether the usage page will be displayed when no
        ///     arguments are passed.
        /// </param>
        /// <param name="console">The <see cref="T:IConsole" /> on which to display output.</param>
        /// <param name="buildInfo">The <see cref="T:IBuildInfo" /> from which to get version and build date information.</param>
        /// <param name="specialNotes">A <see cref="String"/> containing special notes to be added to the end of the usage screen.</param>
        public Parser(string appName, string commandDescription, bool displayUsageIfNoArgumentsPassed,
            IConsole console, IBuildInfo buildInfo, string specialNotes)
            : this(
                displayUsageIfNoArgumentsPassed, specialNotes, CLITokenizer.GetInstance(),
                CommandLineUsageBuilder.GetInstance(commandDescription), CommandLineOptionStore.GetInstance(),
                VersionFinder.GetInstance(appName, buildInfo), CommandLineExceptionHandler.GetInstance(console), console
                )
        {
        }


        internal Parser(bool displayUsageIfNoArgumentsPassed, string specialNotes,
            CLITokenizer tokenizer, CommandLineUsageBuilder cliUsage, CommandLineOptionStore delegates,
            VersionFinder versionFinder, CommandLineExceptionHandler handler, IConsole console)
        {
            _displayUsageIfNoArgumentsPassed = displayUsageIfNoArgumentsPassed;
            _specialNotes = specialNotes;
            _tokenizer = tokenizer;
            _usageBuilder = cliUsage;
            _versionFinder = versionFinder;
            _exceptionHandler = handler;
            _console = console;
            _delegates = delegates;
            _delegates.AddOptions(
                new CommandLineOption("help", "?", "Shows the help screen.", val => DisplayUsage()),
                new CommandLineOption("version", "v", "Displays the version.", val => DisplayVersion())
                );
        }

        /// <summary>
        ///     Adds (or replaces) a <see cref="CommandLineOption" /> to handle passed switches and parameters.
        /// </summary>
        public Parser AddOption(CommandLineOption option)
        {
            _delegates.AddOption(option);
            return this;
        }


        public Parser SetApplicationName(string applicationName)
        {
            _versionFinder.UpdateApplicationName(applicationName);
            return this;
        }

        public Parser SetDescription(string description)
        {
            _usageBuilder.UpdateDescription(description);
            return this;
        }


        /// <summary>
        ///     Adds (or replaces) a method to handle passed switches and parameters.
        /// </summary>
        /// <param name="isOptional">A <see langword="Boolean" /> value indicating whether this option is optional.</param>
        /// <param name="longName">The long name of the option.  May not contain spaces.</param>
        /// <param name="shortName">The short (one or two characters) name of the option.</param>
        /// <param name="delegate">The method (<see cref="Action" /> to execute when this option is used.</param>
        /// <param name="description">The description of what this method does.</param>
        /// <param name="parameterNames">The names of the parameters that this method takes.</param>
        public Parser AddOption(bool isOptional, string longName, string shortName, string description,
            Action<string> @delegate, params string[] parameterNames)
        {
            return
                AddOption(new CommandLineOption(isOptional, longName, shortName, description, @delegate,
                    parameterNames));
        }

        /// <summary>
        ///     Adds (or replaces) an optional method to handle passed switches and parameters.
        /// </summary>
        /// <param name="longName">The long name of the option.  May not contain spaces.</param>
        /// <param name="shortName">The short (one or two characters) name of the option.</param>
        /// <param name="delegate">The method (<see cref="Action&lt;String&gt;" /> to execute when this option is used.</param>
        /// <param name="description">The description of what this method does.</param>
        /// <param name="parameterNames">The names of the parameters that this method takes.</param>
        public Parser AddOption(string longName, string shortName, string description,
            Action<string> @delegate, params string[] parameterNames)
        {
            return AddOption(true, longName, shortName, description, @delegate, parameterNames);
        }

        /// <summary>
        ///     Separates the command-line arguments into options and parameters,
        ///     and then executes any associated delegates.
        ///     If no arguments are sent, displays the Usage information.
        /// </summary>
        public Parser Parse()
        {
            return Parse(Environment.GetCommandLineArgs().Skip(1).ToArray());
        }

        /// <summary>
        ///     Separates the given arguments into options and parameters,
        ///     and then executes any associated delegates.
        ///     If no arguments are sent, displays the Usage information.
        /// </summary>
        public Parser Parse(string[] args)
        {
            // strips "s
            var newArgs = _tokenizer.AddQuotesToParametersWithSpaces(args);
            return Parse(string.Join(" ", newArgs));
        }

        /// <summary>
        ///     Separates the given arguments into options and parameters,
        ///     and then executes any associated delegates.
        ///     If no arguments are sent, displays the Usage information.
        /// </summary>
        public Parser Parse(string args)
        {
            _passedArguments = args;

            var tokens = _tokenizer.GetOptionsFromCommandLineArgs(_passedArguments);

            try
            {
                _delegates.InvokeDelegates(tokens);
            }
            catch (Exception ex)
            {
                HandleKnownExceptions(ex);
                throw;
            }

            if (string.IsNullOrEmpty(args))
            {
                DisplayUsage();
            }

            return this;
        }

        private void HandleKnownExceptions(Exception ex)
        {
            if (ex is DelegateNotFoundException ||
                ex is MissingOptionsException ||
                ex is IncorrectParametersException)
            {
                _exceptionHandler.DisplayException(ex, _passedArguments);
                DisplayUsage();
                Environment.Exit(4);
            }
        }


        // Displays the version information. Used with the "version" or "v" switch.
        private void DisplayVersion()
        {
            var versionString = _versionFinder.GetVersion();
            _console.WriteLine(versionString);

            if (Debugger.IsAttached)
                WaitForKeyPress();

            Environment.Exit(4);
        }

        // Displays the usage information. Used with the "help" or "?" switches, or when there are no arguments given.
        private void DisplayUsage()
        {
            if (!_displayUsageIfNoArgumentsPassed) return;
            var usageString = _usageBuilder.GetUsage(_delegates.CommandLineOptions);
            _console.WriteLine(usageString);

            if (!string.IsNullOrEmpty(_specialNotes))
            {
                _console.WriteLine();
                _console.WriteLine(_specialNotes);
            }

            if (Debugger.IsAttached)
                WaitForKeyPress();

            Environment.Exit(4);
        }
        
        private void WaitForKeyPress()
        {
            _console.WriteLine();
            _console.WriteLine("Press any key to exit...");
            _console.ReadKey();
        }
    }
}