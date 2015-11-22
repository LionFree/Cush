using System;
using System.Linq;
using Cush.Common;
using Microsoft.Win32;

namespace Cush.Windows.Registry
{
    /// <summary>
    ///     Represents a key-level node in the Windows registry.
    ///     This class is a registry encapsulation.
    /// </summary>
    public sealed class RegistryKeyProxy : DisposableEquatableBase, IRegistryKey
    {
        private RegistryKey _key;

        public RegistryKeyProxy(RegistryKey key)
        {
            _key = key;
        }

        /// <summary>
        ///     Retrieves the name of the key.
        /// </summary>
        /// <returns>
        ///     The absolute (qualified) name of the key.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        ///     The <see cref="T:Microsoft.Win32.RegistryKey" /> is closed (closed keys cannot be accessed).
        /// </exception>
        public string Name
        {
            get
            {
                if (IsDisposed) throw new ObjectDisposedException("_key");
                return _key.Name;
            }
        }

        /// <summary>
        ///     Retrieves an array of strings that contains all the subkey names.
        /// </summary>
        /// <returns>
        ///     An array of strings that contains the names of the subkeys for the current key.
        /// </returns>
        /// <exception cref="T:System.Security.SecurityException">
        ///     The user does not have the permissions required to read from the
        ///     key.
        /// </exception>
        /// <exception cref="T:System.ObjectDisposedException">
        ///     The <see cref="T:Microsoft.Win32.RegistryKey" /> being manipulated is closed (closed keys cannot be accessed).
        /// </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
        /// <exception cref="T:System.IO.IOException">A system error occurred, for example the current key has been deleted.</exception>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public string[] GetSubKeyNames()
        {
            if (IsDisposed) throw new ObjectDisposedException("_key");
            return _key.GetSubKeyNames();
        }

        /// <summary>
        ///     Creates a new subkey, or opens an existing one.
        /// </summary>
        /// <param name="subkey">Name or path to subkey to create or open.</param>
        /// <returns>
        ///     the subkey, or <b>null</b> if the operation failed.
        /// </returns>
        public RegistryKeyProxy CreateSubKey(string subkey)
        {
            if (IsDisposed) throw new ObjectDisposedException("_key");
            return new RegistryKeyProxy(_key.CreateSubKey(subkey));
        }

        IRegistryKey IRegistryKey.CreateSubKey(string subkey)
        {
            return CreateSubKey(subkey);
        }

        /// <summary>
        ///     Sets the specified value.
        /// </summary>
        /// <param name="name">Name of value to store data in</param>
        /// <param name="value">Data to store.</param>
        public void SetValue(string name, object value)
        {
            if (IsDisposed) throw new ObjectDisposedException("_key");
            _key.SetValue(name, value);
        }

        /// <summary>
        ///     Deletes the specified value from this key.
        /// </summary>
        /// <param name="name">Name of the value to delete.</param>
        /// <param name="throwOnMissingValue"></param>
        public void DeleteValue(string name, bool throwOnMissingValue)
        {
            if (IsDisposed) throw new ObjectDisposedException("_key");
            _key.DeleteValue(name, throwOnMissingValue);
        }

        /// <summary>
        ///     Retrieves the value associated with the specified name. Returns null if the name/value pair does not exist in the
        ///     registry.
        /// </summary>
        /// <returns>
        ///     The value associated with <paramref name="name" />, or null if <paramref name="name" /> is not found.
        /// </returns>
        /// <param name="name">The name of the value to retrieve. </param>
        /// <exception cref="T:System.Security.SecurityException">
        ///     The user does not have the permissions required to read from the
        ///     registry key.
        /// </exception>
        /// <exception cref="T:System.ObjectDisposedException">
        ///     The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value is closed (closed keys cannot be
        ///     accessed).
        /// </exception>
        /// <exception cref="T:System.IO.IOException">
        ///     The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value has been marked for deletion.
        /// </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Read="\" />
        /// </PermissionSet>
        public object GetValue(string name)
        {
            if (IsDisposed) throw new ObjectDisposedException("_key");
            return _key.GetValue(name);
        }

        /// <summary>
        ///     Retrieves an array of strings that contains all the value names associated with this key.
        /// </summary>
        /// <returns>
        ///     An array of strings that contains the value names for the current key.
        /// </returns>
        /// <exception cref="T:System.Security.SecurityException">
        ///     The user does not have the permissions required to read from the
        ///     registry key.
        /// </exception>
        /// <exception cref="T:System.ObjectDisposedException">
        ///     The <see cref="T:Microsoft.Win32.RegistryKey" />  being manipulated is closed (closed keys cannot be accessed).
        /// </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
        /// <exception cref="T:System.IO.IOException">A system error occurred; for example, the current key has been deleted.</exception>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Unrestricted="true" />
        /// </PermissionSet>
        public string[] GetValueNames()
        {
            if (IsDisposed) throw new ObjectDisposedException("_key");
            return _key.GetValueNames();
        }

        /// <summary>
        ///     Retrieves a subkey as read-only.
        /// </summary>
        /// <returns>
        ///     The subkey requested, or null if the operation failed.
        /// </returns>
        /// <param name="name">The name or path of the subkey to open read-only. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="name" /> is null
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="name" /> is longer than the maximum length allowed (255 characters).
        /// </exception>
        /// <exception cref="T:System.ObjectDisposedException">
        ///     The <see cref="T:Microsoft.Win32.RegistryKey" /> is closed (closed keys cannot be accessed).
        /// </exception>
        /// <exception cref="T:System.Security.SecurityException">
        ///     The user does not have the permissions required to read the
        ///     registry key.
        /// </exception>
        /// <PermissionSet>
        ///     <IPermission
        ///         class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
        ///         version="1" Read="\" />
        /// </PermissionSet>
        public RegistryKeyProxy ReadSubKey(string name)
        {
            if (IsDisposed) throw new ObjectDisposedException("_key");
            return new RegistryKeyProxy(_key.OpenSubKey(name, false));
        }
        
        IRegistryKey IRegistryKey.ReadSubKey(string name)
        {
            return ReadSubKey(name);
        }

        /// <summary>
        ///     Determines whether a value exists within this key.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the value exists, otherwise <c>false</c>.
        /// </returns>
        public bool EntryExists(string entryName)
        {
            if (IsDisposed) throw new ObjectDisposedException("_key");
            return GetValueNames().Any(str => string.Equals(str, entryName, StringComparison.CurrentCultureIgnoreCase));
        }

        protected override void DisposeOfManagedResources()
        {
            _key.Close();
        }

        protected override void DisposeOfUnManagedObjects()
        {
            _key = null;
        }

        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            if (ReferenceEquals(this, obj)) return true;

            var p = (RegistryKeyProxy) obj;

            var sameName = (Name == p.Name);

            var same = (sameName);
            return same;
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap.
            {
                // pick two prime numbers
                const int seed = 5;
                var hash = 3;

                // be sure to check for nullity, etc.
                hash *= seed + (Name != null ? Name.GetHashCode() : 0);

                return hash;
            }
        }
    }
}