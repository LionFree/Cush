using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Cush.Common
{
    /// <summary>
    ///     A simple base class that provides an implementation of the <see cref="T:System.IDisposable" />
    ///     pattern on top of the <see cref="T:System.ComponentModel.INotifyPropertyChanged" /> implementations
    ///     of the <see cref="T:Cush.Common.PropertyChangedBase" /> base class.
    /// </summary>
    [DataContract]
    public abstract class DisposableBase : PropertyChangedBase, IDisposable
    {
        /// <summary>
        ///     Tracks whether <see cref="M:Cush.Common.DisposableBase.Dispose" /> has been called or not.
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        ///     Tracks whether <see cref="M:Cush.Common.DisposableBase.Dispose" /> has been called or not.
        /// </summary>
        public virtual bool IsDisposed
        {
            get { return _isDisposed; }
            protected set
            {
                _isDisposed = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        /// <remarks>
        ///     This method is not virtual by design. Derived classes
        ///     should override
        ///     <see
        ///         cref="M:Cush.Common.DisposableBase.Dispose(System.Boolean)" />
        ///     .
        /// </remarks>
        [DebuggerStepThrough]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     This destructor will run only if the
        ///     <see
        ///         cref="M:Cush.Common.DisposableBase.Dispose" />
        ///     method does not get called. This gives this base class the
        ///     opportunity to finalize.
        ///     <para>
        ///         Important: Do not provide destructors in types derived from
        ///         this class.
        ///     </para>
        /// </summary>
        ~DisposableBase()
        {
            Dispose(false);
        }

        /// <summary>
        ///     <c>Dispose(bool disposing)</c> executes in two distinct scenarios.
        ///     If disposing equals <c>true</c>, the method has been called directly
        ///     or indirectly by a user's code. Managed and unmanaged resources
        ///     can be disposed.
        /// </summary>
        /// <param name="disposing">
        ///     If disposing equals <c>false</c>, the method
        ///     has been called by the runtime from inside the finalizer and you
        ///     should not reference other objects. Only unmanaged resources can
        ///     be disposed.
        /// </param>
        /// <remarks>
        ///     Check the <see cref="P:Cush.Common.DisposableBase.IsDisposed" /> property to determine whether
        ///     the method has already been called.
        /// </remarks>
        protected void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            if (disposing)
            {
                DisposeOfManagedResources();
            }

            DisposeOfUnManagedObjects();

            IsDisposed = true;
        }

        /// <summary>
        ///     Dispose of managed resources.
        /// </summary>
        protected abstract void DisposeOfManagedResources();

        /// <summary>
        ///     Dispose of managed resources.  Call the appropriate methods to clean up unmanaged resources here.
        ///     If Disposing is false, only the following code is executed.
        /// </summary>
        protected abstract void DisposeOfUnManagedObjects();
    }
}