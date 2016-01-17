using System.ServiceModel;
using Cush.Common.Logging;

namespace Cush.Windows.Services
{
    public abstract class HostFactory
    {
        public static HostFactory GetFactory(ILogger logger, ServiceAuthorizationManager manager)
        {
            return GetInstance(logger, HostBuilder.GetInstance(logger, manager));
        }

        public static HostFactory GetInstance(ILogger logger, HostBuilder builder)
        {
            return new Implementation(logger, builder);
        }

        public abstract ServiceHost GetNewServiceHost(WindowsServiceDescription description);

        private class Implementation : HostFactory
        {
            private readonly ILogger _logger;
            private readonly HostBuilder _hostBuilder;

            public Implementation(ILogger logger, HostBuilder hostBuilder)
            {
                _logger = logger;
                _hostBuilder = hostBuilder;
            }

            public override ServiceHost GetNewServiceHost(WindowsServiceDescription description)
            {
                var host = _hostBuilder.ManufactureServiceHost(description);

                _hostBuilder.AddEndpoints(host, description);
                _hostBuilder.ConfigureAuthorization(host);
                _hostBuilder.WireUpEvents(host, description.HostEventResponder);

                return host;
            }
        }
    }
}