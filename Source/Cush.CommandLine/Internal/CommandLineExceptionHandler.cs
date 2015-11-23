using System;
using Cush.CommandLine.Exceptions;

namespace Cush.CommandLine.Internal
{
    internal abstract class CommandLineExceptionHandler
    {
        public static CommandLineExceptionHandler GetInstance(IConsole console)
        {
            return new Implementation(console);
        }

        /// <summary>
        ///     Displays an error and exits.
        ///     Use this when a user passes an invalid command line option.
        /// </summary>
        public abstract void DelegateNotFound(Exception ex, string passedArguments);

        /// <summary>
        ///     Displays an error and exits.
        ///     Use this when a user fails to pass a required command line option.
        /// </summary>
        public abstract void MissingOptions(MissingOptionsException ex, string passedArguments);

        /// <summary>
        ///     Displays an error and exits.
        ///     Use this when a user passes the incorrect number of parameters for an option.
        /// </summary>
        public abstract void IncorrectParameters(IncorrectParametersException ex, string passedArguments);

        /// <summary>
        ///     Displays an error and exits.
        /// </summary>
        internal abstract void DisplayException(Exception ex, string passedArguments);

        private class Implementation : CommandLineExceptionHandler
        {
            private readonly IConsole _console;

            internal Implementation(IConsole console)
            {
                _console = console;
            }

            public override void DelegateNotFound(Exception ex, string passedArguments)
            {
                DisplayException(ex, passedArguments);
            }

            public override void MissingOptions(MissingOptionsException ex, string passedArguments)
            {
                DisplayException(ex, passedArguments);
            }

            public override void IncorrectParameters(IncorrectParametersException ex, string passedArguments)
            {
                DisplayException(ex, passedArguments);
            }

            internal override void DisplayException(Exception ex, string passedArguments)
            {
                _console.WriteLine("");
                _console.WriteLine(ex.Message);
                var passedArgs = string.IsNullOrEmpty(passedArguments) ? "<none>" : passedArguments;
                _console.WriteLine("Passed arguments: " + passedArgs);
            }
        }
    }
}