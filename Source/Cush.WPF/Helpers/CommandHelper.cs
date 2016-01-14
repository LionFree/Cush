using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cush.WPF
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class CommandHelper
    {
        public static bool CanExecute(Control source, CommandCanExecuteParameter parameter)
        {
            if (parameter?.Command == null)
                return false;

            var target = parameter.CommandTarget;

            if (target == null && Keyboard.FocusedElement == null)
                target = source;

            return CanExecute(parameter.Command, parameter.CommandParameter, target);
        }

        public static bool CanExecute(ICommand command, object parameter, IInputElement target)
        {
            if (command == null)
                return false;

            var routedCommand = command as RoutedCommand;
            return routedCommand?.CanExecute(parameter, target) ?? command.CanExecute(parameter);
        }

        public static void Execute(ICommand command, object parameter, IInputElement target)
        {
            if (command == null)
                return;

            var rcmd = command as RoutedCommand;
            if (rcmd == null)
            {
                command.Execute(parameter);
            }
            else
            {
                rcmd.Execute(parameter, target);
            }
        }

        public static void SetCurrentValue(CanExecuteRoutedEventArgs e, object value)
        {
            var parameter = e.Parameter as CommandCanExecuteParameter;
            if (parameter != null)
            {
                parameter.CurrentValue = value;
            }
        }
    }
}
