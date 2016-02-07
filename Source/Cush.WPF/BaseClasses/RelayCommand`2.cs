using System;
using System.Runtime.CompilerServices;

namespace Cush.WPF
{
    /// <summary>
    ///     A command whose sole purpose is to relay its functionality
    ///     to other objects by invoking delegates.
    ///     The default return value for the CanExecute method is 'true'.
    /// </summary>
    public class RelayCommand<T1, T2> : CommandBase
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommand" />.
        /// </summary>
        /// <param name="action">
        ///     Delegate to execute when Execute is called on the command.  This can be null to just hook up a
        ///     CanExecute delegate.
        /// </param>
        /// <param name="name">The name of the command.  Used for debugging.</param>
        /// <remarks><seealso cref="M:CanExecute" /> will always return true.</remarks>
        public RelayCommand(Action<T1, T2> action, string name = null, [CallerMemberName] string caller = null)
            : base(action, null, name, caller)
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="RelayCommand" />.
        /// </summary>
        /// <param name="action">
        ///     Delegate to execute when Execute is called on the command.  This can be null to just hook up a
        ///     CanExecute delegate.
        /// </param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <param name="name">The name of the command.  Used for debugging.</param>
        public RelayCommand(Action<T1, T2> action, Predicate<object> canExecute, string name = null,
            [CallerMemberName] string caller = null)
            : base(action, canExecute, name, caller)
        {
        }

        public override void Execute(object parameter)
        {
            Execute((T1) parameter, default(T2));
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
        public void Execute(T1 parameter1, T2 parameter2)
        {
            ((Action<T1, T2>) WrappedAction).Invoke(parameter1, parameter2);
        }
    }
}