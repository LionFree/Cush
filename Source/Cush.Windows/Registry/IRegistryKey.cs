using System;

namespace Cush.Windows.Registry
{
    /// <summary>
    ///     Represents a key-level node in the Windows registry.
    ///     This class is a registry encapsulation.
    /// </summary>
    public interface IRegistryKey : IDisposable
    {
        /// <summary>
        ///     Retrieves the name of the key.
        /// </summary>
        /// <returns>
        ///     The absolute (qualified) name of the key.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        ///     The <see cref="T:Microsoft.Win32.RegistryKey" /> is closed (closed keys cannot be accessed).
        /// </exception>
        string Name { get; }

        /// <summary>
        ///     Creates a new subkey, or opens an existing one.
        /// </summary>
        /// <param name="subkey">Name or path to subkey to create or open.</param>
        /// <returns>
        ///     the subkey, or <b>null</b> if the operation failed.
        /// </returns>
        IRegistryKey CreateSubKey(string subkey);

        /// <summary>
        ///     Sets the specified value.
        /// </summary>
        /// <param name="name">Name of value to store data in</param>
        /// <param name="value">Data to store.</param>
        void SetValue(string name, object value);

        /// <summary>
        ///     Deletes the specified value from this key.
        /// </summary>
        /// <param name="name">Name of the value to delete.</param>
        /// <param name="throwOnMissingValue"></param>
        void DeleteValue(string name, bool throwOnMissingValue);

        /// <summary>
        ///     Retrieves the value associated with the specified name. Returns null if the name/value pair does not exist in the
        ///     registry.
        /// </summary>
        /// <returns>
        ///     The value associated with <paramref name="name" />, or null if <paramref name="name" /> is not found.
        /// </returns>
        /// <param name="name">The name of the value to retrieve. </param>
        object GetValue(string name);

        /// <summary>
        ///     Retrieves an array of strings that contains all the subkey names.
        /// </summary>
        /// <returns>
        ///     An array of strings that contains the names of the subkeys for the current key.
        /// </returns>
        string[] GetSubKeyNames();

        /// <summary>
        ///     Retrieves an array of strings that contains all the value names associated with this key.
        /// </summary>
        /// <returns>
        ///     An array of strings that contains the value names for the current key.
        /// </returns>
        string[] GetValueNames();

        /// <summary>
        ///     Retrieves a subkey as read-only.
        /// </summary>
        /// <returns>
        ///     The subkey requested, or null if the operation failed.
        /// </returns>
        /// <param name="name">The name or path of the subkey to open read-only. </param>
        IRegistryKey ReadSubKey(string name);

        /// <summary>
        ///     Determines whether a value exists within this key.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if the value exists, otherwise <see langword="false" />.
        /// </returns>
        bool EntryExists(string entryName);
    }
}