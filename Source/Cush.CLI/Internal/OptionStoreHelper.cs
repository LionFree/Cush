using System.Collections.Generic;
using System.Linq;
using Cush.CommandLine.Exceptions;
using Cush.Common.Exceptions;

namespace Cush.CommandLine.Internal
{
    internal class OptionStoreHelper
    {
        internal void VerifyRequiredOptionsArePresent(
            IList<CommandLineOption> options, IList<OptionPair> switches)
        {
            var missingItems =
                options.Where(del => !del.Optional)
                    .Where(del => !SwitchExists(del, switches))
                    .Select(x => x.Name)
                    .ToList();

            if (missingItems.Count > 0)
                throw new MissingOptionsException(missingItems);
        }

        internal void ValidateNumberOfParameters(IList<CommandLineOption> delegates,
            IList<OptionPair> options)
        {
            ThrowHelper.IfNullThenThrow(() => options);
            var keyValuePairs = options as OptionPair[] ?? options.ToArray();

            foreach (var del in delegates)
            {
                foreach (var option in keyValuePairs)
                {
                    if (!OptionMatchesDelegate(option.Option, del)) continue;

                    var desiredParameterCount = CountParametersInDelegate(del);
                    var actualParameterCount = CountParametersInString(option.Parameters);
                    if (desiredParameterCount != actualParameterCount)
                    {
                        throw new IncorrectParametersException(del.Name, option.Parameters);
                    }
                    break;
                }
            }
        }

        private int CountParametersInDelegate(CommandLineOption @delegate)
        {
            return @delegate.ParameterDisplayNames == null
                ? 0
                : @delegate.ParameterDisplayNames.Count();
        }

        private int CountParametersInString(string parameters)
        {
            return string.IsNullOrEmpty(parameters)
                ? 0
                : parameters.Split(' ').Length;
        }

        private bool SwitchExists(CommandLineOption @delegate, IList<OptionPair> switches)
        {
            return switches.Any(item => item.Option == @delegate.ShortName || item.Option == @delegate.Name);
        }

        internal bool OptionMatchesDelegate(string option, CommandLineOption del)
        {
            return del.Name == option || del.ShortName == option;
        }

        internal int GetIndex(IList<CommandLineOption> options, CommandLineOption @delegate)
        {
            foreach (var item in options.Where(item => item.Name == @delegate.Name))
            {
                return options.IndexOf(item);
            }
            return -1;
        }
        
        internal bool DelegateExists(IList<CommandLineOption> options, CommandLineOption @delegate)
        {
            return options.Any(item => item.Name == @delegate.Name);
        }
    }
}