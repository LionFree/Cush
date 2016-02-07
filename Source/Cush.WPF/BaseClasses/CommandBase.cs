using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Cush.WPF
{
    public abstract class CommandBase : ICommand
    {
        private readonly Predicate<object> _canExecute;

        protected readonly object WrappedAction;
        private EventHandler _canExecuteChanged;

        
        protected CommandBase(object action, Predicate<object> canExecute, string name,
            [CallerMemberName] string caller = null)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            ActionType = action.GetType();
            WrappedAction = action;
            _canExecute = canExecute;
            Caller = caller;
            Name = name;
        }

        protected Type ActionType { get; }
        
        //public void Execute1<T1>(T1 parameter)
        //{
        //    var action = CastTo<>(ActionType, WrappedAction);
        //}

        //private T CastTo<T>(T instanceOfType, object input) where T:class
        //{
        //    return input as T;
        //}

        /// <summary>
        ///     Gets the name of the command. Useful for debugging.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Gets the name of the calling method. Useful for debugging.
        /// </summary>
        public string Caller { get; private set; }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                _canExecuteChanged += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                _canExecuteChanged -= value;
                CommandManager.RequerySuggested -= value;
            }
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
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        ///     This method can be used to raise the CanExecuteChanged handler.
        ///     This will force WPF to re-query the status of this command directly.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (_canExecute != null)
                OnCanExecuteChanged();
        }

        /// <summary>
        ///     This method is used to walk the delegate chain and tell WPF that
        ///     our command execution status has changed.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            var handler = _canExecuteChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }

        public abstract void Execute(object parameter);


    }
}