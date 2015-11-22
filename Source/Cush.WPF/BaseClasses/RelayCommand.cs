using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Cush.WPF
{
    /// <summary>
    ///     A command whose sole purpose is to relay its functionality
    ///     to other objects by invoking delegates.
    ///     The default return value for the CanExecute method is 'true'.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object, object> _execute;
        private readonly string _name;
        private EventHandler _canExecuteChanged;

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommand" />.
        /// </summary>
        /// <param name="name">The name of the command.  Used for debugging.</param>
        /// <param name="execute">
        ///     Delegate to execute when Execute is called on the command.  This can be null to just hook up a
        ///     CanExecute delegate.
        /// </param>
        /// <remarks><seealso cref="M:CanExecute" /> will always return true.</remarks>
        public RelayCommand(string name, Action<object> execute)
            : this(name, execute, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommand" />.
        /// </summary>
        /// <param name="name">The name of the command.  Used for debugging.</param>
        /// <param name="execute">
        ///     Delegate to execute when Execute is called on the command.  This can be null to just hook up a
        ///     CanExecute delegate.
        /// </param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(string name, Action<object> execute, Predicate<object> canExecute)
            : this(name, execute == null ? (Action<object, object>)null : (o, p) => execute(o), canExecute)
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommand" />.
        /// </summary>
        /// <param name="name">The name of the command.  Used for debugging.</param>
        /// <param name="execute">
        ///     Delegate to execute when Execute is called on the command.  This can be null to just hook up a
        ///     CanExecute delegate.
        /// </param>
        /// <remarks><seealso cref="M:CanExecute" /> will always return true.</remarks>
        public RelayCommand(string name, Action<object, object> execute)
            : this(name, execute, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommand" />.
        /// </summary>
        /// <param name="name">The name of the command.  Used for debugging.</param>
        /// <param name="execute">
        ///     Delegate to execute when Execute is called on the command.  This can be null to just hook up a
        ///     CanExecute delegate.
        /// </param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(string name, Action<object, object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            if (name == null)
                throw new ArgumentNullException("name");
            if (name.Length == 0)
                throw new ArgumentException("Command name cannot be an empty string.", "name");
            
            _name = name;
            _execute = execute;
            _canExecute = canExecute;
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
            return _canExecute == null || _canExecute(parameter);
        }

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
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command. If the command does not require data to be passed, this object can be
        ///     set to <see langword="null" />.
        /// </param>
        public void Execute(object parameter)
        {
            Execute(parameter, "single parameter execution method");
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
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter1">
        ///     Data used by the command. If the command does not require data to be passed, this object can be
        ///     set to <see langword="null" />.
        /// </param>
        /// <param name="parameter2">
        ///     Data used by the command. If the command does not require data to be passed, this object can be
        ///     set to <see langword="null" />.
        /// </param>
        public void Execute(object parameter1, object parameter2)
        {
            _execute(parameter1, parameter2);
        }
    }
}