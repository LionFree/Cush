/*  Command-Line Syntax

         copy.exe /copy "C:\robbletron.txt" "c:\robblecopy.txt"
         ^------^ ^---^ ^-----------------^ ^-----------------^
         utility  option     parameter 1         parameter 2
 
     Utility:    aka, the command.  The application.
     option:     aka, the flag. Alters how the utility operates.    
     parameters: arguments passed into the option delegate
 
     Switch Character - The character that precedes the option. In the above example, the switch character is the forward-slash, "/".
     Generally, this is either -, --, or /
 
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Cush.CommandLine
{
    /// <summary>
    ///     A class that holds metadata and
    ///     an <see cref="Action{T}" />
    ///     for a command-line parameter.
    ///     Used by <see cref="T:Cush.CommandLine.Parser" />.
    /// </summary>
    [DebuggerDisplay("[{ShortName},{Name}] : Delegate={_delegate}")]
    public sealed class CommandLineOption
    {
        private readonly Action<string> _delegate;
        private readonly string _description;
        private readonly string _longName;
        private readonly bool _optional;
        private readonly IList<string> _parameterDisplayNames;
        private readonly string _shortName;

        /// <summary>
        ///     Creates a new OPTIONAL command-line option, using the given details.
        /// </summary>
        /// <param name="longName">The long-form name of this command-line option</param>
        /// <param name="shortName">The short (e.g., two-character) name of the command line option.</param>
        /// <param name="description">The text to display (in the usage page) for the description of this command-line option.</param>
        /// <param name="delegate">
        ///     The <see cref="Action&lt;String&gt;" /> to execute when this option is indicated in the
        ///     command-line.
        /// </param>
        /// <param name="parameterDisplayNames">
        ///     The text to display (in the usage page) for the parameters of this command-line
        ///     option.
        /// </param>
        public CommandLineOption(string longName, string shortName, string description,
            Action<string> @delegate, params string[] parameterDisplayNames)
            : this(true, longName, shortName, description, @delegate, parameterDisplayNames)
        {
        }

        /// <summary>
        ///     Creates a new command-line option, using the given details.
        /// </summary>
        /// <param name="optional">
        ///     The <see cref="Boolean" /> value indicating whether this command-line option is optional or not.
        ///     <see langword="true" /> if optional, otherwise <see langword="false" /> (the option is required).
        /// </param>
        /// <param name="longName">The long-form name of this command-line option</param>
        /// <param name="shortName">The short (e.g., two-character) name of the command line option.</param>
        /// <param name="description">The text to display (in the usage page) for the description of this command-line option.</param>
        /// <param name="delegate">
        ///     The <see cref="Action&lt;String&gt;" /> to execute when this option is indicated in the
        ///     command-line.
        /// </param>
        /// <param name="parameterDisplayNames">
        ///     The text to display (in the usage page) for the parameters of this command-line
        ///     option.
        /// </param>
        public CommandLineOption(bool optional, string longName, string shortName, string description,
            Action<string> @delegate, params string[] parameterDisplayNames)
        {
            _longName = longName;
            _shortName = shortName;
            _delegate = @delegate;
            _description = description;
            _parameterDisplayNames = parameterDisplayNames;
            _optional = optional;
        }

        /// <summary>
        ///     Gets the short (e.g., two-character) name of the command line option.
        /// </summary>
        public string ShortName
        {
            get { return _shortName; }
        }

        /// <summary>
        ///     Gets the text to display (in the usage page) for the
        ///     parameters
        ///     of this command-line option.
        /// </summary>
        public IList<string> ParameterDisplayNames
        {
            get { return _parameterDisplayNames; }
        }

        /// <summary>
        ///     Gets the text to display (in the usage page) for the
        ///     description
        ///     of this command-line option.
        /// </summary>
        public string Description
        {
            get { return _description; }
        }

        /// <summary>
        ///     Gets the <see cref="Boolean" /> value indicating whether this command-line option is optional or not.
        ///     <see langword="true" /> if optional, otherwise <see langword="false" /> (the option is required).
        /// </summary>
        public bool Optional
        {
            get { return _optional; }
        }

        /// <summary>
        ///     Gets the long-form name of this command-line option.
        /// </summary>
        public string Name
        {
            get { return _longName; }
        }

        /// <summary>
        ///     Gets the <see cref="Action&lt;String&gt;" /> to execute when this option is indicated in the command-line.
        /// </summary>
        public Action<string> @Delegate
        {
            get { return _delegate; }
        }
    }
}