using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Cush.Common.Annotations;
using Cush.Common.Exceptions;
using Cush.Common.Helpers;

//#pragma warning disable 1685

namespace Cush.Common
{
    /// <summary>
    ///     Implementation of <see cref="INotifyPropertyChanged" /> to simplify models.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "DelegateSubtraction")]
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
    [DataContract, Serializable, DebuggerStepThrough]
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        ///     Multicast event for property change notifications.
        /// </summary>
        private PropertyChangedEventHandler _propertyChanged;

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged
        {
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerStepThrough] add { _propertyChanged += value; }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerStepThrough] remove { _propertyChanged -= value; }
        }

        /// <summary>
        ///     Allows triggering the <see cref="E:PropertyChanged" />
        ///     event using a lambda expression, thus avoiding strings. Keep in
        ///     mind that using this method comes with a performance penalty, so
        ///     don't use it for frequently updated properties that cause a lot
        ///     of events to be fired.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="propertyExpression">
        ///     Expression pointing to a given property.
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            ThrowHelper.IfNullThenThrow(() => propertyExpression);
            OnPropertyChanged(Expressions.GetPropertyName(propertyExpression));
        }

        /// <summary>
        ///     Raises the <see cref="E:PropertyChanged" />
        ///     event for a set of properties.
        /// </summary>
        /// <param name="propertyNames">
        ///     Provides the names of the changed properties.
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged(params string[] propertyNames)
        {
            ThrowHelper.IfNullThenThrow(() => propertyNames);
            foreach (var propertyName in propertyNames)
                OnPropertyChanged(propertyName);
        }

        /// <summary>
        ///     Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers
        ///     that support <see cref="CallerMemberNameAttribute" />.
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Raises the <see cref="E:PropertyChanged" />
        ///     event for a given property.
        /// </summary>
        /// <param name="sender">The object where the property exists.</param>
        /// <param name="e">The arguments to pass to the event.</param>
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Use a variable to prevent race conditions.
            var handler = _propertyChanged;
            handler?.Invoke(sender, e);
        }

        /// <summary>
        ///     Checks if a property already matches a desired value.
        ///     Sets the property and notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support CallerMemberName.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
        [NotifyPropertyChangedInvocator]
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;
            var oldValue = storage;
            storage = value;
            OnPropertyChanged(this,
                new ExtendedPropertyChangedEventArgs<T>(propertyName, oldValue, value));
            return true;
        }

        /// <summary>
        ///     Sets a property and invokes the
        ///     property changed event handler, passing the old and new values as
        ///     <see cref="T:ExtendedPropertyChangedEventArgs" />.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="expression">
        ///     An expression that points to the property, e.g.,
        ///     if the property is named <c>IsEnabled</c>,
        ///     the expression might be <c>()=>IsEnabled</c>.
        /// </param>
        /// <param name="field">The backing field for the property.</param>
        /// <param name="value">The value to set.</param>
        [NotifyPropertyChangedInvocator]
        protected void SetProperty<T>(ref T field, T value, Expression<Func<T>> expression)
        {
            SetProperty(ref field, value, Expressions.GetPropertyName(expression));
        }


        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">
        ///     An expression that points to the property whose value is changing,
        ///     e.g., if the property is named <c>IsEnabled</c>,
        ///     the expression might be <c>()=>IsEnabled</c>.
        /// </param>
        /// <param name="field">The backing field for the property.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="additionalProperties">
        ///     An array of expressions that should also raise the PropertyChanged event.
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected void SetProperty<T>(ref T field, T value, Expression<Func<T>> property,
            Expression<Func<object>>[] additionalProperties)
        {
            SetProperty(ref field, value, property);
            var propertyNames = Expressions.GetPropertyNames(additionalProperties);
            foreach (var propertyName in propertyNames)
            {
                OnPropertyChanged(propertyName);
            }
        }

        /// <summary>
        ///     Determines whether a given delegate is subscribed to the PropertyChanged event.
        /// </summary>
        /// <param name="delegate">The delegate to check for.</param>
        public bool IsSubscribedToNotification(Delegate @delegate)
        {
            return (null != _propertyChanged) && 
                _propertyChanged.GetInvocationList().Contains(@delegate);
        }


        /// <summary>
        ///     Raises the given event.
        /// </summary>
        protected void RaiseEvent(EventHandler handler, EventArgs args = null)
        {
            handler?.Invoke(this, args);
        }

        /// <summary>
        ///     Raises the given event.
        /// </summary>
        protected void RaiseEvent<TArgs>(EventHandler<TArgs> handler, TArgs args = null) where TArgs : EventArgs
        {
            handler?.Invoke(this, args);
        }
    }
}