﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using Cush.Common;
using Cush.Common.Messaging;
using Cush.WPF.Messaging;

namespace Cush.WPF.MVVM
{
    /// <summary>
    ///     A base class for the ViewModel classes in the MVVM pattern.
    /// </summary>
    public abstract class ViewModelBase : BindableBase, ICleanup
    {
        private static bool? _isInDesignMode;
        private IMessenger _messengerInstance;

        /// <summary>
        ///     Initializes a new instance of the ViewModelBase class.
        /// </summary>
        // ReSharper disable PublicConstructorInAbstractClass
        // Must be public to allow for serialization.
        public ViewModelBase()
            // ReSharper restore PublicConstructorInAbstractClass
            : this(null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the ViewModelBase class.
        /// </summary>
        /// <param name="messenger">
        ///     An instance of a <see cref="Messenger" />
        ///     used to broadcast messages to other objects. If null, this class
        ///     will attempt to broadcast using the Messenger's default
        ///     instance.
        /// </param>
        // ReSharper disable PublicConstructorInAbstractClass
        public ViewModelBase(IMessenger messenger)
            // ReSharper restore PublicConstructorInAbstractClass
        {
            MessengerInstance = messenger;
        }

        /// <summary>
        ///     Gets a value indicating whether the control is in design mode
        ///     (running under Blend or Visual Studio).
        /// </summary>
        public bool IsInDesignMode
        {
            get { return IsInDesignModeStatic; }
        }

        /// <summary>
        ///     Gets a value indicating whether the control is in design mode
        ///     (running in Blend or Visual Studio).
        /// </summary>
        public static bool IsInDesignModeStatic
        {
            get
            {
                if (!_isInDesignMode.HasValue)
                {
                    var prop = DesignerProperties.IsInDesignModeProperty;
                    _isInDesignMode
                        = (bool) DependencyPropertyDescriptor
                            .FromProperty(prop, typeof (FrameworkElement))
                            .Metadata.DefaultValue;
                }

                return _isInDesignMode.Value;
            }
        }

        /// <summary>
        ///     Gets or sets an instance of a <see cref="IMessenger" /> used to
        ///     broadcast messages to other objects. If null, this class will
        ///     attempt to broadcast using the Messenger's default instance.
        /// </summary>
        protected IMessenger MessengerInstance
        {
            get { return _messengerInstance ?? Messenger.Default; }
            set { _messengerInstance = value; }
        }

        /// <summary>
        ///     Unregisters this instance from the Messenger class.
        ///     <para>
        ///         To cleanup additional resources, override this method, clean
        ///         up and then call base.Cleanup().
        ///     </para>
        /// </summary>
        public virtual void Cleanup()
        {
            MessengerInstance.Unregister(this);
        }

        /// <summary>
        ///     Broadcasts a PropertyChangedMessage using either the instance of
        ///     the Messenger that was passed to this class (if available)
        ///     or the Messenger's default instance.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the property that
        ///     changed.
        /// </typeparam>
        /// <param name="oldValue">
        ///     The value of the property before it
        ///     changed.
        /// </param>
        /// <param name="newValue">
        ///     The value of the property after it
        ///     changed.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property that
        ///     changed.
        /// </param>
        protected virtual void Broadcast<T>(T oldValue, T newValue, string propertyName)
        {
            var message = new PropertyChangedMessage<T>(this, oldValue, newValue, propertyName);
            MessengerInstance.Send(message);
        }

        /// <summary>
        ///     Raises the PropertyChanged event if needed, and broadcasts a
        ///     PropertyChangedMessage using the Messenger instance (or the
        ///     static default instance if no Messenger instance is available).
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the property that
        ///     changed.
        /// </typeparam>
        /// <param name="propertyName">
        ///     The name of the property that
        ///     changed.
        /// </param>
        /// <param name="oldValue">
        ///     The property's value before the change
        ///     occurred.
        /// </param>
        /// <param name="newValue">
        ///     The property's value after the change
        ///     occurred.
        /// </param>
        /// <param name="broadcast">
        ///     If true, a PropertyChangedMessage will
        ///     be broadcasted. If false, only the event will be raised.
        /// </param>
        /// <remarks>
        ///     If the propertyName parameter
        ///     does not correspond to an existing property on the current class, an
        ///     exception is thrown in DEBUG configuration only.
        /// </remarks>
        protected virtual void OnPropertyChanged<T>(
            [CallerMemberName] string propertyName = null,
            T oldValue = default(T),
            T newValue = default(T),
            bool broadcast = false)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("This method cannot be called with an empty string", "propertyName");
            }

            // ReSharper disable ExplicitCallerInfoArgument
            OnPropertyChanged(propertyName);
            // ReSharper restore ExplicitCallerInfoArgument

            if (broadcast)
            {
                Broadcast(oldValue, newValue, propertyName);
            }
        }

        /// <summary>
        ///     Raises the PropertyChanged event if needed, and broadcasts a
        ///     PropertyChangedMessage using the Messenger instance (or the
        ///     static default instance if no Messenger instance is available).
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the property that
        ///     changed.
        /// </typeparam>
        /// <param name="propertyExpression">
        ///     An expression identifying the property
        ///     that changed.
        /// </param>
        /// <param name="oldValue">
        ///     The property's value before the change
        ///     occurred.
        /// </param>
        /// <param name="newValue">
        ///     The property's value after the change
        ///     occurred.
        /// </param>
        /// <param name="broadcast">
        ///     If true, a PropertyChangedMessage will
        ///     be broadcasted. If false, only the event will be raised.
        /// </param>
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression, T oldValue, T newValue,
            bool broadcast)
        {
            var handler = PropertyChangedHandler;

            if (handler != null
                || broadcast)
            {
                var propertyName = Expressions.GetPropertyName(propertyExpression);

                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }

                if (broadcast)
                {
                    Broadcast(oldValue, newValue, propertyName);
                }
            }
        }

        /// <summary>
        ///     Assigns a new value to the property. Then, raises the
        ///     PropertyChanged event if needed, and broadcasts a
        ///     PropertyChangedMessage using the Messenger instance (or the
        ///     static default instance if no Messenger instance is available).
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the property that
        ///     changed.
        /// </typeparam>
        /// <param name="propertyExpression">
        ///     An expression identifying the property
        ///     that changed.
        /// </param>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">
        ///     The property's value after the change
        ///     occurred.
        /// </param>
        /// <param name="broadcast">
        ///     If true, a PropertyChangedMessage will
        ///     be broadcasted. If false, only the event will be raised.
        /// </param>
        /// <returns>True if the PropertyChanged event was raised, false otherwise.</returns>
        protected bool SetProperty<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue, bool broadcast)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            OnPropertyChanging(propertyExpression);

            var oldValue = field;
            field = newValue;
            OnPropertyChanged(propertyExpression, oldValue, field, broadcast);
            return true;
        }

        /// <summary>
        ///     Assigns a new value to the property. Then, raises the
        ///     PropertyChanged event if needed, and broadcasts a
        ///     PropertyChangedMessage using the Messenger instance (or the
        ///     static default instance if no Messenger instance is available).
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the property that
        ///     changed.
        /// </typeparam>
        /// <param name="propertyName">
        ///     The name of the property that
        ///     changed.
        /// </param>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">
        ///     The property's value after the change
        ///     occurred.
        /// </param>
        /// <param name="broadcast">
        ///     If true, a PropertyChangedMessage will
        ///     be broadcasted. If false, only the event will be raised.
        /// </param>
        /// <returns>True if the PropertyChanged event was raised, false otherwise.</returns>
        protected bool SetProperty<T>(string propertyName, ref T field, T newValue = default(T), bool broadcast = false)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            // ReSharper disable ExplicitCallerInfoArgument
            OnPropertyChanging(propertyName);
            // ReSharper restore ExplicitCallerInfoArgument

            var oldValue = field;
            field = newValue;

            // ReSharper disable ExplicitCallerInfoArgument
            OnPropertyChanged(propertyName, oldValue, field, broadcast);
            // ReSharper restore ExplicitCallerInfoArgument

            return true;
        }


        /// <summary>
        ///     Assigns a new value to the property. Then, raises the
        ///     PropertyChanged event if needed, and broadcasts a
        ///     PropertyChangedMessage using the Messenger instance (or the
        ///     static default instance if no Messenger instance is available).
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the property that
        ///     changed.
        /// </typeparam>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">
        ///     The property's value after the change
        ///     occurred.
        /// </param>
        /// <param name="broadcast">
        ///     If true, a PropertyChangedMessage will
        ///     be broadcasted. If false, only the event will be raised.
        /// </param>
        /// <param name="propertyName">
        ///     (optional) The name of the property that
        ///     changed.
        /// </param>
        /// <returns>True if the PropertyChanged event was raised, false otherwise.</returns>
        protected bool SetProperty<T>(ref T field, T newValue = default(T), bool broadcast = false,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            // ReSharper disable ExplicitCallerInfoArgument
            OnPropertyChanging(propertyName);
            // ReSharper restore ExplicitCallerInfoArgument

            var oldValue = field;
            field = newValue;

            // ReSharper disable ExplicitCallerInfoArgument
            OnPropertyChanged(propertyName, oldValue, field, broadcast);
            // ReSharper restore ExplicitCallerInfoArgument

            return true;
        }
    }
}