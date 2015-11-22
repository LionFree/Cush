using System.ComponentModel;
using System.Configuration.Install;
using Cush.Windows.Services;

namespace Cush.TestHarness.WinService.Infrastructure
{
    // Provide the ProjectInstaller class which allows 
    // the service to be installed by the Installutil.exe tool
    [RunInstaller(true)]
    public class ServiceInstaller : Installer
    {
        // creates a blank windows service installer with configuration in MonitorWindowsService
        public ServiceInstaller()
        {
            var installers = WindowsServiceInstaller.WrapService(ServiceProgram.ComposeObjectGraph());
            Installers.Add(installers.ServiceInstaller);
            Installers.Add(installers.ProcessInstaller);
        }
    }
}