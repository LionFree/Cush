using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Cush.CommandLine.Internal
{
    internal abstract class CommandLineUsageBuilder
    {
        public static CommandLineUsageBuilder GetInstance(string applicationDescription)
        {
            return new CliUsageImplementation(applicationDescription, new[] { "-", "--", "/" });
        }

        public abstract string GetUsage(IEnumerable<CommandLineOption> options);

        private sealed class CliUsageImplementation : CommandLineUsageBuilder
        {
            private string _applicationDescription;
            private readonly IList<string> _switchCharacters;

            public CliUsageImplementation(string applicationDescription, IList<string> switchCharacters)
            {
                _applicationDescription = applicationDescription;
                _switchCharacters = switchCharacters;
            }

            [DebuggerStepThrough]
            private void WriteLine(StringBuilder sb, string s)
            {
                sb.Append(Environment.NewLine);
                sb.Append(s);
                
            }

            [DebuggerStepThrough]
            private void WriteApplicationHeader(StringBuilder sb, string appName)
            {
                if (!string.IsNullOrEmpty(_applicationDescription))
                {
                    WriteLine(sb, appName + " - " + _applicationDescription);
                }
                else
                {
                    WriteLine(sb, appName);
                }
            }

            [DebuggerStepThrough]
            private void WriteUsageString(StringBuilder sb, string appName, IEnumerable<CommandLineOption> options)
            {
                //
                // write out the usage string
                //
                WriteLine(sb, GetUsageString(appName, options));
            }

            [DebuggerStepThrough]
            private void WriteOptionsList(StringBuilder sb, IEnumerable<CommandLineOption> options)
            {
                //
                // write out the commands
                //
                WriteLine(sb, "Available commands:");
                WriteLine(sb, "-------------------");

                var commandLineOptions = options as CommandLineOption[] ?? options.ToArray();
                var exprLength = FigureOutLongestCommandExpression(commandLineOptions);

                foreach (var command in commandLineOptions)
                {
                    WriteLine(sb, GetOptionLine(exprLength, command));
                }
            }

            /// <summary>
            ///     Given a list of CommandLineOptions, will identify the proper screen location for the command instructions.
            /// </summary>
            [DebuggerStepThrough]
            private int FigureOutLongestCommandExpression(IEnumerable<CommandLineOption> options)
            {
                //
                // figure out the longest command expression
                //
                var exprLength =
                    options.Select(c => GetOptionString(c.ShortName).Length + GetOptionString(c.Name).Length)
                        .Max();
                return exprLength;
            }

            /// <summary>
            ///     Appends the switch character to the option text.
            /// </summary>
            [DebuggerStepThrough]
            private string GetOptionString(string optionText)
            {
                return _switchCharacters[0] + optionText;
            }

            /// <summary>
            ///     Given the padding length and command, concatenates them into the desired option text.
            /// </summary>
            [DebuggerStepThrough]
            private string GetOptionLine(int exprLength, CommandLineOption command)
            {
                return string.Format("{0}, {1}", GetOptionString(command.ShortName), GetOptionString(command.Name))
                    .PadRight(exprLength + 5, ' ') + command.Description;
            }

            //[DebuggerStepThrough]
            private string GetUsageString(string appName, IEnumerable<CommandLineOption> options)
            {
                var commandLineOptions = options as CommandLineOption[] ?? options.ToArray();

                var sb = new StringBuilder();

                sb.Append("Usage: ");
                sb.Append(appName.ToUpper());
                sb.Append(' ');

                var required = commandLineOptions.Where(a => !a.Optional).ToList();
                if (required.Count > 0)
                {
                    AppendArgumentsToUsage(sb, required);
                }

                var optional = commandLineOptions.Where(a => a.Optional).ToList();
                if (optional.Count > 0)
                {
                    sb.Append("[");
                    AppendArgumentsToUsage(sb, optional);
                    sb.Append("]");
                }

                return sb.ToString();

                //var required = this.options.Where(a => 
                //                                    CommandArgumentFlags.FlagEnabled(a.Flags, CommandArgumentFlags.Required) &&
                //                                    CommandArgumentFlags.FlagDisabled(a.Flags, CommandArgumentFlags.HideInUsage)).ToList();

            }

            private void AppendArgumentsToUsage(StringBuilder sb, List<CommandLineOption> options)
            {
                foreach (var opt in options)
                {
                    sb.Append(GetOptionString(opt.Name));

                    if (opt.ParameterDisplayNames != null)
                    {
                        foreach (var item in opt.ParameterDisplayNames)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                sb.Append(" <");
                                sb.Append(item);
                                sb.Append(">");
                            }
                            else
                            {
                                sb.Append(" <arg>");
                            }
                        }
                    }

                    sb.Append(' ');
                }

                sb.Remove(sb.Length - 1, 1);
            }

            public override string GetUsage(IEnumerable<CommandLineOption> options)
            {
                var commandLineOptions = options as CommandLineOption[] ?? options.ToArray();

                var text = new StringBuilder();
                var appName = AppDomain.CurrentDomain.FriendlyName.ToLower();

                WriteApplicationHeader(text, appName);

                WriteLine(text, string.Empty);
                WriteUsageString(text, appName, commandLineOptions);

                WriteLine(text, string.Empty);
                WriteOptionsList(text, commandLineOptions);
                
                return text.ToString();
            }

            public override void UpdateDescription(string description)
            {
                _applicationDescription = description;
            }
        }

        public abstract void UpdateDescription(string description);
    }
}
