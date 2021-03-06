﻿using System;
using System.Runtime.CompilerServices;

namespace Cush.WPF
{
    /// <summary>
    ///     A command whose sole purpose is to relay its functionality
    ///     to other objects by invoking delegates.
    ///     The default return value for the CanExecute method is 'true'.
    /// </summary>
    public class RelayCommand : CommandBase
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
        public RelayCommand(Action action, string name = null, [CallerMemberName] string caller = null)
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
        public RelayCommand(Action action, Predicate<object> canExecute, string name = null,
            [CallerMemberName] string caller = null)
            : base(action, canExecute, name, caller)
        {
        }

        public override void Execute(object parameter)
        {
            Execute();
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        public void Execute()
        {
            ((Action)WrappedAction).Invoke();
        }
    }
}