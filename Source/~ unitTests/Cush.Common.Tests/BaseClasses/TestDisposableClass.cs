using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Cush.Common.Helpers;

using Cush.Testing;

namespace Cush.Common.Tests
{
    internal class TestDisposableClass : DisposableEquatableBase
    {
        private IDisposable _managed;
        private bool _resourcesChanged;
        private IntPtr _unmanaged;
        private bool _getSetTester;
        private readonly int _code;
        private readonly bool _equal;

        public TestDisposableClass()
            : this(new Component())
        {
        }

        public TestDisposableClass(IDisposable disposable)
            : this(disposable, new IntPtr(GetRandom.Int(256000, 65535)))
        {
        }

        public TestDisposableClass(IDisposable disposable, IntPtr handle)
        {
            _managed = disposable;
            _unmanaged = handle;
        }

        public TestDisposableClass(bool equal, int code)
        {
            _equal = equal;
            _code = code;
        }

        public bool ResourcesChanged
        {
            get { return _resourcesChanged; }
            set
            {
                if (_resourcesChanged == value) return;
                _resourcesChanged = value;
                OnPropertyChanged();
            }
        }

        internal bool RaiseWithNoParameter
        {
            get { return _resourcesChanged; }
            set
            {
                if (_resourcesChanged == value) return;
                _resourcesChanged = value;
                OnPropertyChanged();
            }
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        internal bool RaiseWithParameterName
        {
            get { return _resourcesChanged; }
            set
            {
                if (_resourcesChanged == value) return;
                _resourcesChanged = value;
                OnPropertyChanged("ResourcesChanged");
            }
        }
        
        internal bool RaiseWithExpression
        {
            get { return _resourcesChanged; }
            set
            {
                if (_resourcesChanged == value) return;
                _resourcesChanged = value;
                OnPropertyChanged(() => RaiseWithExpression);
            }
        }

        public bool RaiseWithMultipleParameters
        {
            get { return _resourcesChanged; }
            set
            {
                if (_resourcesChanged == value) return;
                _resourcesChanged = value;
                OnPropertyChanged("ResourcesChanged", "RaiseWithMultipleParameters");
            }
        }

        internal bool RaiseWithMultipleExpressions
        {
            get { return _resourcesChanged; }
            set
            {
                SetProperty(ref _resourcesChanged, value, () => RaiseWithMultipleExpressions,
                    new Expression<Func<object>>[] { () => ResourcesChanged, ()=>UnmanagedResource });
            }
        }

        internal bool SetNotifyingPropertyTester
        {
            get { return _getSetTester; }
            set { SetProperty(ref _getSetTester, value, () => SetNotifyingPropertyTester); }
        }

        internal IDisposable ManagedResource
        {
            get { return _managed; }
            set { SetProperty(ref _managed, value, () => ManagedResource); }
        }

        internal IntPtr UnmanagedResource
        {
            get { return _unmanaged; }
            set
            {
                _resourcesChanged = true;
                SetProperty(ref _unmanaged, value, () => UnmanagedResource,
                    new Expression<Func<object>>[] {() => ResourcesChanged});
                //OnPropertyChanged("ResourcesChanged");
            }
        }

        internal static string[] GetPropertyNames(Expression<Func<object>>[] properties)
        {
            return Expressions.GetPropertyNames(properties);
        }

        protected override void DisposeOfManagedResources()
        {
            ManagedResource.Dispose();
        }

        protected override void DisposeOfUnManagedObjects()
        {
            try
            {
                CloseHandle(UnmanagedResource);
                UnmanagedResource = IntPtr.Zero;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + (ex.InnerException == null ? "" : " || " + ex.InnerException.Message));
            }
        }

        // Use interop to call the method necessary
        // to clean up the unmanaged resource.
        [DllImport("Kernel32")]
        private static extern bool CloseHandle(IntPtr handle);

        public override bool Equals(object obj)
        {
            return _equal;
        }

        public override int GetHashCode()
        {
            return _code;
        }
    }
}