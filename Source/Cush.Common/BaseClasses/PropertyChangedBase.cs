using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Cush.Common.Attributes;
using Cush.Common.Helpers;
using Cush.Common.Annotations;

using ThrowHelper = Cush.Common.Exceptions.ThrowHelper;

#pragma warning disable 1685

namespace Cush.Common
{
    /// <summary>
    ///     Simple base class that provides a solid implementation
    ///     of the <see cref="T:System.ComponentModel.INotifyPropertyChanged" /> event.
    /// </summary>
    [DataContract, __DynamicallyInvokable, Serializable]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        private PropertyChangedEventHandler _propertyChanged;

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        [SuppressMessage("ReSharper", "DelegateSubtraction"), __DynamicallyInvokable]
        public virtual event PropertyChangedEventHandler PropertyChanged
        {
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerStepThrough, __DynamicallyInvokable, TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] add { _propertyChanged += value; }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerStepThrough, __DynamicallyInvokable, TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] remove { _propertyChanged -= value; }
        }

        /// <summary>
        ///     Raises the <see cref="E:Cush.Common.PropertyChangedBase.PropertyChanged" />
        ///     event for a set of properties.
        /// </summary>
        /// <param name="propertyNames">Provides the names of the changed properties.</param>
        [NotifyPropertyChangedInvocator, DebuggerStepThrough, __DynamicallyInvokable]
        public void OnPropertyChanged(params string[] propertyNames)
        {
            ThrowHelper.IfNullThenThrow(() => propertyNames);
            foreach (var propertyName in propertyNames)
                OnPropertyChanged(propertyName);
        }

        /// <summary>
        ///     Allows triggering the <see cref="E:Cush.Common.PropertyChangedBase.PropertyChanged" />
        ///     event using a lambda expression, thus avoiding strings. Keep in
        ///     mind that using this method comes with a performance penalty, so
        ///     don't use it for frequently updated properties that cause a lot
        ///     of events to be fired.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="propertyExpression">
        ///     Expression pointing to a given property.
        /// </param>
        [NotifyPropertyChangedInvocator, DebuggerStepThrough, __DynamicallyInvokable]
        public virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            ThrowHelper.IfNullThenThrow(() => propertyExpression);
            OnPropertyChanged(Expressions.GetPropertyName(propertyExpression));
        }

        /// <summary>
        ///     Raises the <see cref="E:Cush.Common.PropertyChangedBase.PropertyChanged" />
        ///     event for a given property.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        [NotifyPropertyChangedInvocator, DebuggerStepThrough, __DynamicallyInvokable]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Raises the <see cref="E:Cush.Common.PropertyChangedBase.PropertyChanged" />
        ///     event for a given property.
        /// </summary>
        /// <param name="sender">The object where the property exists.</param>
        /// <param name="e">The arguments to pass to the event.</param>
        [NotifyPropertyChangedInvocator, DebuggerStepThrough, __DynamicallyInvokable]
        public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Use a variable to prevent race conditions.
            var handler = _propertyChanged;
            if (handler != null)
                handler(sender, e);
        }

        /// <summary>
        ///     Sets a property and invokes the property changed event handler, passing the old and new values as
        ///     <see cref="T:ExtendedPropertyChangedEventArgs" />.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="expression">
        ///     An expression that points to the property, e.g., if the property is named <c>IsEnabled</c>,
        ///     the expression might be <c>()=>IsEnabled</c>.
        /// </param>
        /// <param name="field">The backing field for the property.</param>
        /// <param name="value">The value to set.</param>
        [NotifyPropertyChangedInvocator, DebuggerStepThrough, __DynamicallyInvokable]
        protected void SetNotifyingProperty<T>(ref T field, T value, Expression<Func<T>> expression)
        {
            if ((null != field) && field.Equals(value)) return;
            var oldValue = field;
            field = value;
            OnPropertyChanged(this,
                new ExtendedPropertyChangedEventArgs<T>(Expressions.GetPropertyName(expression), oldValue, value));
        }

        /// <summary>
        ///     Sets a property and invokes the property changed event handler on that property and also on
        ///     an array of additional properties.
        /// </summary>
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
        [NotifyPropertyChangedInvocator, DebuggerStepThrough, __DynamicallyInvokable]
        protected void SetNotifyingProperty<T>(ref T field, T value, Expression<Func<T>> property,
            Expression<Func<object>>[] additionalProperties)
        {
            SetNotifyingProperty(ref field, value, property);

            var propertyNames = Expressions.GetPropertyNames(additionalProperties);
            foreach (var propertyName in propertyNames)
                OnPropertyChanged(propertyName);
        }

        /// <summary>
        ///     Determines whether a given delegate is subscribed to the PropertyChanged event.
        /// </summary>
        /// <param name="del">The delegate to check for.</param>
        [__DynamicallyInvokable]
        public bool IsSubscribedToNotification(Delegate del)
        {
            return (null != _propertyChanged) && _propertyChanged.GetInvocationList().Contains(del);
        }
    }
}