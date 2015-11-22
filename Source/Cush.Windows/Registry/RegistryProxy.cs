namespace Cush.Windows.Registry
{
    /// <summary>
    ///     Provides <see cref="T:IRegistryKey" />
    ///     objects that represent the root keys in the Windows registry.
    /// </summary>
    public sealed class RegistryProxy: IRegistry
    {
        /// <summary>
        ///     Contains information about the current user preferences.
        ///     This field reads the Windows registry base key HKEY_CURRENT_USER.
        /// </summary>
        public IRegistryKey CurrentUser
        {
            get { return new RegistryKeyProxy(Microsoft.Win32.Registry.CurrentUser); }
        }

        /// <summary>
        ///     Contains the configuration data for the local machine.
        ///     This field reads the Windows registry base key HKEY_LOCAL_MACHINE.
        /// </summary>
        public IRegistryKey LocalMachine
        {
            get { return new RegistryKeyProxy(Microsoft.Win32.Registry.LocalMachine); }
        }
    }
}