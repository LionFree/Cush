using System.ServiceModel;
using System.ServiceModel.Description;

namespace Cush.Windows.Services
{
    internal abstract class WebServiceConfigurator
    {
        internal static WebServiceConfigurator GetInstance()
        {
            return GetInstance(new WideOpenAuthorizationManager());
        }

        internal static WebServiceConfigurator GetInstance(ServiceAuthorizationManager manager)
        {
            return new WebServiceConfiguratorImplementation(manager);
        }

        internal abstract void Configure(ServiceHost host);

        private sealed class WebServiceConfiguratorImplementation : WebServiceConfigurator
        {
            private readonly ServiceAuthorizationManager _manager;

            internal WebServiceConfiguratorImplementation(ServiceAuthorizationManager manager)
            {
                _manager = manager;
            }

            internal override void Configure(ServiceHost host)
            {
                var serviceAuthorizationBehavior = host.Description.Behaviors.Find<ServiceAuthorizationBehavior>();
                serviceAuthorizationBehavior.PrincipalPermissionMode = PrincipalPermissionMode.None;
                serviceAuthorizationBehavior.ServiceAuthorizationManager = _manager;

                var serviceDebugBehavior = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                serviceDebugBehavior.HttpHelpPageEnabled = false;
                serviceDebugBehavior.IncludeExceptionDetailInFaults = true;
            }
        }
    }
}