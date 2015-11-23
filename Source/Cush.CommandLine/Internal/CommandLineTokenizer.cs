using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Cush.Common.Exceptions;

namespace Cush.CommandLine.Internal
{
    internal abstract class CLITokenizer
    {
        public static CLITokenizer GetInstance()
        {
            return new TokenizerImplementation();
        }

        internal abstract IList<KeyValuePair<string, string>> SplitArgsIntoOptions(string args);

        internal abstract IList<KeyValuePair<string, string>> StripSwitchCharactersFromOptions(
            IList<KeyValuePair<string, string>> tokens);

        public abstract string[] AddQuotesToParametersWithSpaces(string[] args);

        internal abstract IList<OptionPair> GetOptionPairsFromKeyValuePairs(IList<KeyValuePair<string, string>> tokens);

        public abstract IList<OptionPair> GetOptionsFromCommandLineArgs(string passedArguments);


        private class TokenizerImplementation : CLITokenizer
        {
            internal override IList<KeyValuePair<string, string>> SplitArgsIntoOptions(string args)
            {
                var regex = new Regex(@"(?<switch>-{1,2}\S*|\/\S*)(?:[=:]?|\s+)(?<value>[^-\s].*?)?(?=\s+[-\/]|$)",
                    RegexOptions.Compiled | RegexOptions.CultureInvariant |
                    RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

                var matches = (
                    from match in regex.Matches(args).Cast<Match>()
                    select new KeyValuePair<string, string>(
                        match.Groups["switch"].Value, match.Groups["value"].Value
                        )
                    ).ToList();

                return matches;
            }

            internal override IList<KeyValuePair<string, string>> StripSwitchCharactersFromOptions(
                IList<KeyValuePair<string, string>> tokens)
            {
                return (from item in tokens
                    let newKey = item.Key.Trim(' ', '-', '-', '/')
                    select new KeyValuePair<string, string>(newKey, item.Value)).ToList();
            }

            /// <summary>
            ///     Given an array of string arguments, puts quotes around the arguments that include spaces.
            /// </summary>
            public override string[] AddQuotesToParametersWithSpaces(string[] args)
            {
                for (var i = 0; i < args.Length; i++)
                {
                    if (args[i].Contains(' '))
                        args[i] = string.Format("\"{0}\"", args[i]);
                }
                return args;
            }

            internal override IList<OptionPair> GetOptionPairsFromKeyValuePairs(
                IList<KeyValuePair<string, string>> tokens)
            {
                ThrowHelper.IfNullThenThrow(() => tokens);
                return tokens.Select(item => new OptionPair(item.Key, item.Value)).ToList();
            }

            public override IList<OptionPair> GetOptionsFromCommandLineArgs(string args)
            {
                var options = SplitArgsIntoOptions(args);
                var tokens = StripSwitchCharactersFromOptions(options);
                var optionPairs = GetOptionPairsFromKeyValuePairs(tokens);
                return optionPairs;
            }
        }
    }
}