using System.Windows;
using System.Windows.Input;

namespace Cush.WPF
{
    public class CommandCanExecuteParameter
    {
        public CommandCanExecuteParameter(ICommand command, IInputElement target, object parameter)
        {
            Command = command;
            CommandTarget = target;
            CommandParameter = parameter;
        }

        public ICommand Command { get; private set; }
        public IInputElement CommandTarget { get; private set; }
        public object CommandParameter { get; private set; }

        public object CurrentValue { get; set; }
    }
}
