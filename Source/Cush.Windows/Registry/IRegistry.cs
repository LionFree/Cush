namespace Cush.Windows.Registry
{
    /// <summary>
    ///     Provides <see cref="T:ServiceSentry.Extensibility.RegistryKeyWrapper" />
    ///     objects that represent the root keys in the Windows registry.
    /// </summary>
    public interface IRegistry
    {
        /// <summary>
        ///     Contains information about the current user preferences.
        ///     This field reads the Windows registry base key HKEY_CURRENT_USER.
        /// </summary>
        IRegistryKey CurrentUser { get; }

        /// <summary>
        ///     Contains the configuration data for the local machine.
        ///     This field reads the Windows registry base key HKEY_LOCAL_MACHINE.
        /// </summary>
        IRegistryKey LocalMachine { get; }
    }
}