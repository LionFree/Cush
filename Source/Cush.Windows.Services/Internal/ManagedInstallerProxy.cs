using System.Configuration.Install;

namespace Cush.Windows.Services.Internal
{
    internal abstract class ManagedInstallerProxy
    {
        internal static ManagedInstallerProxy Default
        {
            get { return new ManagedInstallerProxyImplementation(); }
        }

        public abstract void InstallHelper(string[] args);

        private sealed class ManagedInstallerProxyImplementation : ManagedInstallerProxy
        {
            public override void InstallHelper(string[] args)
            {
                ManagedInstallerClass.InstallHelper(args);
            }
        }
    }
}