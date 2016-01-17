using System;
using Cush.CommandLine.Internal;

namespace Cush.CommandLine
{
    public interface ICommandLineParser
    {
        /// <summary>
        ///     Adds (or replaces) a <see cref="T:CommandLineOption" /> to handle passed switches and parameters.
        /// </summary>
        Parser AddOption(CommandLineOption option);

        /// <summary>
        ///     Adds (or replaces) an optional method to handle passed switches and parameters.
        /// </summary>
        /// <param name="longName">The long name of the option.  May not contain spaces.</param>
        /// <param name="shortName">The short (one or two characters) name of the option.</param>
        /// <param name="delegate">The method (<see cref="Action&lt;String&gt;" /> to execute when this option is used.</param>
        /// <param name="description">The description of what this method does.</param>
        /// <param name="parameterNames">The names of the parameters that this method takes.</param>
        Parser AddOption(string longName, string shortName, string description, Action<string> @delegate,
            params string[] parameterNames);

        /// <summary>
        ///     Adds (or replaces) a method to handle passed switches and parameters.
        /// </summary>
        /// <param name="isOptional">A <see langword="Boolean" /> value indicating whether this option is optional.</param>
        /// <param name="longName">The long name of the option.  May not contain spaces.</param>
        /// <param name="shortName">The short (one or two characters) name of the option.</param>
        /// <param name="delegate">The method (<see cref="Action" /> to execute when this option is used.</param>
        /// <param name="description">The description of what this method does.</param>
        /// <param name="parameterNames">The names of the parameters that this method takes.</param>
        Parser AddOption(bool isOptional, string longName, string shortName, string description,
            Action<string> @delegate, params string[] parameterNames);

        /// <summary>
        ///     Separates the command-line arguments into options and parameters,
        ///     and then executes any associated delegates.
        ///     If no arguments are sent, displays the Usage information.
        /// </summary>
        Parser Parse();

        /// <summary>
        ///     Separates the given arguments into options and parameters,
        ///     and then executes any associated delegates.
        ///     If no arguments are sent, displays the Usage information.
        /// </summary>
        Parser Parse(string[] args);

        /// <summary>
        ///     Separates the given arguments into options and parameters,
        ///     and then executes any associated delegates.
        ///     If no arguments are sent, displays the Usage information.
        /// </summary>
        Parser Parse(string args);

        Parser SetApplicationName(string applicationName);

        Parser SetDescription(string description);
    }
}