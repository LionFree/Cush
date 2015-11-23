using System.Collections.Generic;
using System.Linq;
using Cush.CommandLine.Exceptions;
using Cush.Common;
using Cush.Common.Exceptions;

namespace Cush.CommandLine.Internal
{
    internal abstract class CommandLineOptionStore : DisposableBase
    {
        internal abstract IList<CommandLineOption> CommandLineOptions { get; }
        internal abstract int Count { get; }

        public static CommandLineOptionStore GetInstance()
        {
            return GetInstance(new List<CommandLineOption>(), new OptionStoreHelper());
        }

        internal static CommandLineOptionStore GetInstance(List<CommandLineOption> options, OptionStoreHelper helper)
        {
            return new Implementation(options, helper);
        }


        internal abstract void AddOption(CommandLineOption newOption);
        internal abstract void AddOptions(params CommandLineOption[] options);

        internal abstract void InvokeDelegates(IList<OptionPair> switches);

        private class Implementation : CommandLineOptionStore
        {
            private readonly List<CommandLineOption> _commandLineOptions;
            private readonly OptionStoreHelper _helper;

            internal Implementation(List<CommandLineOption> options, OptionStoreHelper helper)
            {
                _commandLineOptions = options;
                _helper = helper;
            }

            internal override IList<CommandLineOption> CommandLineOptions
            {
                get { return _commandLineOptions; }
            }

            internal override int Count
            {
                get { return _commandLineOptions.Count; }
            }

            internal override void InvokeDelegates(IList<OptionPair> switches)
            {
                ThrowHelper.IfNullThenThrow(() => switches);

                _helper.VerifyRequiredOptionsArePresent(_commandLineOptions, switches);
                _helper.ValidateNumberOfParameters(_commandLineOptions, switches);

                // Iterate through switches
                foreach (var item in switches)
                {
                    var used = false;
                    var item1 = item;

                    // Find the delegate whose name (or short name) matches the current switch
                    foreach (var del in CommandLineOptions
                        .Where(del => _helper.OptionMatchesDelegate(item1.Option, del)))
                    {
                        // Invoke the delegate, using the given parameter
                        del.Delegate.Invoke(item.Parameters);
                        used = true;
                    }
                    if (!used)
                        throw new DelegateNotFoundException(item.Option);
                }
            }

            internal override void AddOption(CommandLineOption newOption)
            {
                if (!_helper.DelegateExists(_commandLineOptions, newOption))
                {
                    _commandLineOptions.Add(newOption);
                }
                else
                {
                    _commandLineOptions[_helper.GetIndex(_commandLineOptions, newOption)] = newOption;
                }
            }

            internal override void AddOptions(params CommandLineOption[] options)
            {
                foreach (var item in options)
                {
                    AddOption(item);
                }
            }

            protected override void DisposeOfManagedResources()
            {
                if (_commandLineOptions != null)
                {
                    _commandLineOptions.Clear();
                }
            }

            protected override void DisposeOfUnManagedObjects()
            {
                // Do nothing.
            }
        }
    }
}