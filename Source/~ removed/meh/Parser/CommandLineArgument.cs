using System;
using System.Collections.Generic;
using System.Linq;

namespace Cush.CLI
{
    internal abstract class CommandLineArgument
    {
        // Valid format:
        // {switchCharacter}{switch}{delimiter}[quote]{parameter}[quote]

        internal static readonly string[] ParameterDelimiters = { " ", "=", ":" };
        internal static readonly string[] QuoteCharacters = { "'", "\"" };
        internal static readonly string[] SwitchDelimiters = { "-", "--", "/" };

        internal abstract string SwitchDelimiter { get; }
        internal abstract string Switch { get; }
        internal abstract bool IsQuoted { get; }
        internal abstract string ParameterDelimiter { get; }
        internal abstract string Parameter { get; }
        internal abstract bool HasParameter { get; }

        internal static CommandLineArgument GetInstance(string argument)
        {
            return new Implementation(argument);
        }


        private sealed class Implementation : CommandLineArgument
        {
            private readonly string _argument;
            private readonly bool _hasParameter;
            private int _firstLetterOfParameter;
            private int _firstQuote = -1;
            private bool _isQuoted;
            private int _lastQuote = -1;
            private string _parameter;
            private string _parameterDelimiter;
            private int _parameterDelimiterLocation = -1;
            private int _parameterLength;
            private string _quoteChar = string.Empty;
            private string _switch;
            private string _switchDelimiter;

            internal Implementation(string argument)
            {
                if (string.IsNullOrEmpty(argument)) throw new ArgumentNullException("argument");
                _argument = argument;
                _hasParameter = ArgumentHasParameter();

                if (_hasParameter)
                {
                    GetQuotes();
                    GetParameter();
                }

                GetSwitch();
            }

            internal override string SwitchDelimiter
            {
                get { return _switchDelimiter; }
            }

            internal override string Switch
            {
                get { return _switch; }
            }

            internal override bool IsQuoted
            {
                get { return _isQuoted; }
            }

            internal override string ParameterDelimiter
            {
                get { return _parameterDelimiter; }
            }

            internal override string Parameter
            {
                get { return _parameter; }
            }

            internal override bool HasParameter
            {
                get { return _hasParameter; }
            }

            private bool ArgumentHasParameter()
            {
                for (var i = 0; i < ParameterDelimiters.Length; i++)
                    if (_argument.Contains(ParameterDelimiters[i]))
                    {
                        _parameterDelimiterLocation = _argument.IndexOf(ParameterDelimiters[i],
                                                                        StringComparison.CurrentCulture);
                        return true;
                    }

                _parameter = string.Empty;
                _parameterDelimiter = string.Empty;
                return false;
            }

            private void GetQuotes()
            {
                // Find quotes
                foreach (var item in QuoteCharacters)
                {
                    if (_argument.Contains(item))
                    {
                        // Find the quote
                        var tempFirst = _argument.IndexOf(item, StringComparison.CurrentCulture);
                        var tempLast = _argument.LastIndexOf(item, StringComparison.CurrentCulture);
                        if (tempFirst != tempLast && tempLast != -1)
                        {
                            _firstQuote = tempFirst;
                            _lastQuote = tempLast;
                            _quoteChar = item;
                            _isQuoted = true;
                        }
                    }
                    if (_isQuoted) break;
                }
            }

            private void GetParameter()
            {
                GetParameterDelimiter();
                _parameter = _argument.Substring(_firstLetterOfParameter, _parameterLength);
            }

            private void GetParameterDelimiter()
            {
                if (_isQuoted)
                {
                    // Got a quote.  Extract the parameter.
                    _parameterDelimiter = _argument.Substring(_firstQuote - 1, 1);
                    _firstLetterOfParameter = _firstQuote + _quoteChar.Length;
                    _parameterLength = _lastQuote - _firstLetterOfParameter;
                }
                else
                {
                    // No quotes.  Find the first letter of the parameter.
                    _parameterDelimiter = _argument.Substring(_parameterDelimiterLocation, 1);
                    _firstLetterOfParameter = _parameterDelimiterLocation + 1;
                    _parameterLength = _argument.Length - _firstLetterOfParameter;
                }
            }

            private void GetSwitch()
            {
                _switchDelimiter = string.Empty;
                string possibleSwitch;
                var quoteWidth = _quoteChar.Length;
                if (_parameterDelimiterLocation == 0)
                {
                    possibleSwitch = _argument;
                }
                else
                {
                    possibleSwitch = _argument.Substring(0,
                                                         _argument.Length - _parameter.Length - 2 * quoteWidth -
                                                         _parameterDelimiter.Length);
                }

                GetSwitchDelimiter(possibleSwitch);

                var start = _switchDelimiter.Length;
                _switch = possibleSwitch.Substring(start);
            }

            private void GetSwitchDelimiter(string possibleSwitch)
            {
                var delimiters = GetDelimiterTypes(SwitchDelimiters);

                // Find the longest delimiters, and work down to the smallest
                for (var i = delimiters.Count; i > 0; i--)
                {
                    var delims = delimiters[i - 1];

                    foreach (var delim in delims)
                    {
                        if (!possibleSwitch.Contains(delim)) continue;

                        _switchDelimiter = delim;
                        return;
                    }
                }

                throw new ArgumentException("Argument contains no switch delimiters.");
            }

            private List<string[]> GetDelimiterTypes(string[] delimiters)
            {
                var output = new List<string[]>();

                // Get the longest length.
                var maxLength = delimiters.OrderByDescending(s => s.Length).First().Length;
                var array = new List<string>[maxLength + 1];

                // Split the delimiter list into arrays by length.
                var lengthList = new int[maxLength + 1];
                for (var i = 0; i < delimiters.Length; i++)
                {
                    lengthList[delimiters[i].Length]++;
                    if (array[delimiters[i].Length] == null)
                    {
                        array[delimiters[i].Length] = new List<string>();
                    }
                    array[delimiters[i].Length].Add(delimiters[i]);
                }

                // Add nontrivial arrays to the output list.
                for (var i = 0; i < lengthList.Length; i++)
                {
                    if (lengthList[i] > 0)
                    {
                        output.Add(array[i].ToArray());
                    }
                }

                return output;
            }
        }
    }
}