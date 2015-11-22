using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Cush.Common.Annotations;
using Cush.Common.Messaging;

//#pragma warning disable 1685

namespace Cush.Common
{
    /// <summary>
    ///     Simple base class that provides a solid implementation of the
    ///     <see cref="T:System.ComponentModel.INotifyPropertyChanged" />
    ///     event.
    /// </summary>
    [DataContract, Serializable]
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    public abstract class BindableBase : INotifyPropertyChanged
    {
        protected PropertyChangedEventHandler PropertyChangedHandler;
        protected PropertyChangingEventHandler PropertyChangingHandler;

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        [SuppressMessage("ReSharper", "DelegateSubtraction")]
        public event PropertyChangedEventHandler PropertyChanged
        {
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerStepThrough] add { PropertyChangedHandler += value; }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerStepThrough]
            remove
            {
                PropertyChangedHandler -= value;
            }
        }

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        [SuppressMessage("ReSharper", "DelegateSubtraction")]
        public event PropertyChangingEventHandler PropertyChanging
        {
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerStepThrough] add { PropertyChangingHandler += value; }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerStepThrough]
            remove
            {
                PropertyChangingHandler -= value;
            }
        }


        /// <summary>
        ///     Checks if a property already matches a desired value.  Sets the property and
        ///     notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property that changed.</typeparam>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="value">The property's value after the change.</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support CallerMemberName.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
        [NotifyPropertyChangedInvocator, DebuggerStepThrough]
        protected bool SetProperty<T>(string propertyName, ref T field, T value)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            var oldValue = field;

            // ReSharper disable ExplicitCallerInfoArgument
            OnPropertyChanging(this, new ExtendedPropertyChangingEventArgs<T>(propertyName, oldValue, value));
            // ReSharper restore ExplicitCallerInfoArgument

            field = value;

            // ReSharper disable ExplicitCallerInfoArgument
            OnPropertyChanged(this, new ExtendedPropertyChangedEventArgs<T>(propertyName, oldValue, value));
            // ReSharper restore ExplicitCallerInfoArgument

            return true;
        }

        /// <summary>
        ///     Assigns a new value to the property. Then, raises the
        ///     PropertyChanged event if needed.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the property that changed.
        /// </typeparam>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">
        ///     The property's value after the change
        ///     occurred.
        /// </param>
        /// <param name="propertyName">
        ///     (optional) The name of the property that
        ///     changed.
        /// </param>
        /// <returns>
        ///     True if the PropertyChanged event has been raised,
        ///     false otherwise. The event is not raised if the old
        ///     value is equal to the new value.
        /// </returns>
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            return SetProperty(propertyName, ref field, newValue);
        }


        /// <summary>
        ///     Raises the <see cref="E:Cush.Common.PropertyChangedBase.PropertyChanged" />
        ///     event for a set of properties.
        /// </summary>
        /// <param name="propertyNames">Provides the names of the changed properties.</param>
        [NotifyPropertyChangedInvocator, DebuggerStepThrough]
        public void OnPropertyChanged(params string[] propertyNames)
        {
            ThrowHelper.IfNullThenThrow(() => propertyNames);
            foreach (var propertyName in propertyNames)
                OnPropertyChanged(propertyName);
        }

        /// <summary>
        ///     Raises the PropertyChanged event if needed.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the property that
        ///     changed.
        /// </typeparam>
        /// <param name="propertyExpression">
        ///     An expression identifying the property
        ///     that changed.
        /// </param>
        [NotifyPropertyChangedInvocator, DebuggerStepThrough]
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            ThrowHelper.IfNullThenThrow(() => propertyExpression);
            OnPropertyChanged(Expressions.GetPropertyName(propertyExpression));
        }

        /// <summary>
        ///     If needed, notifies listeners that a property value has changed.
        /// </summary>
        /// <remarks>
        ///     If the propertyName parameter
        ///     does not correspond to an existing property on the current class, an
        ///     exception is thrown in DEBUG configuration only.
        /// </remarks>
        /// <param name="propertyName">
        ///     The name of the property that changed.
        ///     This value is optional and can be provided automatically when invoked from compilers
        ///     that support <see cref="CallerMemberNameAttribute" />.
        /// </param>
        [NotifyPropertyChangedInvocator, DebuggerStepThrough]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            VerifyPropertyName(propertyName);

            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     If needed, notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="sender">The object where the property exists.</param>
        /// <param name="e">The arguments to pass to the event.</param>
        [NotifyPropertyChangedInvocator, DebuggerStepThrough]
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Use a variable to prevent race conditions.
            var handler = PropertyChangedHandler;
            if (handler != null)
                handler(sender, e);
        }

        /// <summary>
        ///     If needed, notifies listeners that a property value is changing.
        /// </summary>
        /// <param name="propertyNames">Provides the names of the changed properties.</param>
        [NotifyPropertyChangedInvocator, DebuggerStepThrough]
        public void OnPropertyChanging(params string[] propertyNames)
        {
            ThrowHelper.IfNullThenThrow(() => propertyNames);
            foreach (var propertyName in propertyNames)
                OnPropertyChanging(propertyName);
        }

        /// <summary>
        ///     Raises the PropertyChanging event if needed.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the property that
        ///     changes.
        /// </typeparam>
        /// <param name="propertyExpression">
        ///     An expression identifying the property
        ///     that changes.
        /// </param>
        [NotifyPropertyChangedInvocator, DebuggerStepThrough]
        protected virtual void OnPropertyChanging<T>(Expression<Func<T>> propertyExpression)
        {
            ThrowHelper.IfNullThenThrow(() => propertyExpression);
            OnPropertyChanging(Expressions.GetPropertyName(propertyExpression));
        }

        /// <summary>
        ///     If needed, notifies listeners that a property value is changing.
        /// </summary>
        /// <remarks>
        ///     If the propertyName parameter
        ///     does not correspond to an existing property on the current class, an
        ///     exception is thrown in DEBUG configuration only.
        /// </remarks>
        /// <param name="propertyName">
        ///     The name of the property that is changing.
        ///     This value is optional and can be provided automatically when invoked from compilers
        ///     that support <see cref="CallerMemberNameAttribute" />.
        /// </param>
        [NotifyPropertyChangedInvocator, DebuggerStepThrough]
        protected virtual void OnPropertyChanging(
            [CallerMemberName] string propertyName = null)
        {
            VerifyPropertyName(propertyName);

            OnPropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }


        /// <summary>
        ///     If needed, notifies listeners that a property value is changing.
        /// </summary>
        /// <param name="sender">The object where the property exists.</param>
        /// <param name="e">The arguments to pass to the event.</param>
        [NotifyPropertyChangedInvocator, DebuggerStepThrough]
        protected void OnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            // Use a variable to prevent race conditions.
            var handler = PropertyChangingHandler;
            if (handler != null)
                handler(sender, e);
        }

        /// <summary>
        ///     Verifies that a property name exists in this ViewModel. This method
        ///     can be called before the property is used, for instance before
        ///     calling RaisePropertyChanged. It avoids errors when a property name
        ///     is changed but some places are missed.
        /// </summary>
        /// <remarks>This method is only active in DEBUG mode.</remarks>
        /// <param name="propertyName">
        ///     The name of the property that will be
        ///     checked.
        /// </param>
        [Conditional("DEBUG"), DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            var myType = GetType();

            if (string.IsNullOrEmpty(propertyName) || myType.GetProperty(propertyName) != null) return;

            var descriptor = this as ICustomTypeDescriptor;

            if (descriptor != null)
            {
                if (descriptor.GetProperties()
                    .Cast<PropertyDescriptor>()
                    .Any(property => property.Name == propertyName))
                {
                    return;
                }
            }

            throw new ArgumentException("Property not found", propertyName);
        }

        /// <summary>
        ///     Determines whether a given delegate is subscribed to the PropertyChanged event.
        /// </summary>
        /// <param name="del">The delegate to check for.</param>
        public bool IsSubscribedToNotification(Delegate del)
        {
            return (null != PropertyChangedHandler) && PropertyChangedHandler.GetInvocationList().Contains(del);
        }

        /// <summary>
        ///     Determines whether a given delegate is subscribed to the PropertyChanged event.
        /// </summary>
        /// <param name="del">The delegate to check for.</param>
        public bool IsSubscribedToPreNotification(Delegate del)
        {
            return (null != PropertyChangingHandler) && PropertyChangingHandler.GetInvocationList().Contains(del);
        }
    }
}