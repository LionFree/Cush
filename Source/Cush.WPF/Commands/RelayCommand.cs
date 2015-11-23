using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;
using Cush.Common.Weak;

namespace Cush.WPF.Commands
{
    /// <summary>
    ///     A command whose sole purpose is to relay its functionality
    ///     to other objects by invoking delegates.
    ///     The default return value for the CanExecute method is 'true'.
    ///     This class does not allow you to accept command parameters in the
    ///     Execute and CanExecute callback methods.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly WeakFunc<bool> _canExecute;
        private readonly WeakAction _execute;
        private readonly string _name;
        private EventHandler _requerySuggestedLocal;

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommand" />.
        /// </summary>
        /// <param name="name">The name of the command.  Used for debugging.</param>
        /// <param name="execute">
        ///     Delegate to execute when Execute is called on the command.  This can be null to just hook up a
        ///     CanExecute delegate.
        /// </param>
        /// <remarks><seealso cref="M:CanExecute" /> will always return true.</remarks>
        public RelayCommand(string name, Action execute)
            : this(name, execute, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RelayCommand" /> class.
        /// </summary>
        /// <param name="name">The name of the command.  Used for debugging.</param>
        /// <param name="execute">
        ///     Delegate to execute when Execute is called on the command.  This can be null to just hook up a
        ///     CanExecute delegate.
        /// </param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="execute" /> argument is null.</exception>
        public RelayCommand(string name, Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            if (name == null)
                throw new ArgumentNullException("name");
            if (name.Length == 0)
                throw new ArgumentException("Command name cannot be an empty string.", "name");

            _name = name;

            _execute = new WeakAction(execute);

            if (canExecute != null)
            {
                _canExecute = new WeakFunc<bool>(canExecute);
            }
        }

        /// <summary>
        ///     Gets the name of the command. Useful for debugging.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        /// <returns>
        ///     true if this command can be executed; otherwise, false.
        /// </returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null
                   || (_canExecute.IsStatic || _canExecute.IsAlive)
                   && _canExecute.Execute();
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    // add event handler to local handler backing field in a thread safe manner
                    EventHandler handler2;
                    var canExecuteChanged = _requerySuggestedLocal;

                    do
                    {
                        handler2 = canExecuteChanged;
                        var handler3 = (EventHandler) Delegate.Combine(handler2, value);
                        canExecuteChanged = Interlocked.CompareExchange(
                            ref _requerySuggestedLocal,
                            handler3,
                            handler2);
                    } while (canExecuteChanged != handler2);

                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (_canExecute != null)
                {
                    // removes an event handler from local backing field in a thread safe manner
                    EventHandler handler2;
                    var canExecuteChanged = _requerySuggestedLocal;

                    do
                    {
                        handler2 = canExecuteChanged;
                        var handler3 = (EventHandler) Delegate.Remove(handler2, value);
                        canExecuteChanged = Interlocked.CompareExchange(
                            ref _requerySuggestedLocal,
                            handler3,
                            handler2);
                    } while (canExecuteChanged != handler2);

                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command. If the command does not require data to be passed, this object can be
        ///     set to <see langword="null" />.
        /// </param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter)
                && _execute != null
                && (_execute.IsStatic || _execute.IsAlive))
            {
                _execute.Execute();
            }
        }

        /// <summary>
        ///     This method can be used to raise the CanExecuteChanged handler.
        ///     This will force WPF to re-query the status of this command directly.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}