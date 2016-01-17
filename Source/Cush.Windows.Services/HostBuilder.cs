using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Cush.Common.Logging;

namespace Cush.Windows.Services
{
    public abstract class HostBuilder
    {
        public static HostBuilder GetInstance(ILogger logger, ServiceAuthorizationManager manager)
        {
            return new Implementation(logger, manager);
        }

        public abstract void AddEndpoints(ServiceHost host, WindowsServiceDescription description);

        public abstract void WireUpEvents(ServiceHost host, IHostEventResponder hostEventResponder);

        public abstract void ConfigureAuthorization(ServiceHost host);

        public abstract ServiceHost ManufactureServiceHost(WindowsServiceDescription description);


        private class Implementation : HostBuilder
        {
            private readonly ILogger _logger;
            private readonly ServiceAuthorizationManager _manager;

            internal Implementation(ILogger logger, ServiceAuthorizationManager manager)
            {
                _logger = logger;
                _manager = manager;
            }

            public override void AddEndpoints(ServiceHost host, WindowsServiceDescription description)
            {
                foreach (var item in description.Endpoints)
                {
                    var address = GetEndpointUri(item.Binding, item.Port, item.Path);
                    var endpoint = host.AddServiceEndpoint(item.Contract, item.Binding, address);

                    if (item.Binding is WebHttpBinding)
                        endpoint.Behaviors.Add(new WebHttpBehavior());
                }
            }

            public override void ConfigureAuthorization(ServiceHost host)
            {
                var serviceAuthorizationBehavior = host.Description.Behaviors.Find<ServiceAuthorizationBehavior>();
                serviceAuthorizationBehavior.PrincipalPermissionMode = PrincipalPermissionMode.None;
                serviceAuthorizationBehavior.ServiceAuthorizationManager = _manager;

                var serviceDebugBehavior = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                serviceDebugBehavior.HttpHelpPageEnabled = false;
                serviceDebugBehavior.IncludeExceptionDetailInFaults = true;
            }

            public override ServiceHost ManufactureServiceHost(WindowsServiceDescription description)
            {
                if (description.ServiceType != null)
                {
                    return new ServiceHost(description.ServiceType);
                }

                if (description.ServiceObject != null)
                {
                    return new ServiceHost(description.ServiceObject);
                }

                throw new InvalidOperationException(Strings.EXCEPTION_ServiceDescriptionNeedsTypeOrObject);
            }

            public override void WireUpEvents(ServiceHost host, IHostEventResponder hostEventResponder)
            {
                host.Opening += hostEventResponder.OnOpening;
                host.Opened += hostEventResponder.OnOpened;
                host.Closing += hostEventResponder.OnClosing;
                host.Closed += hostEventResponder.OnClosed;
                host.Faulted += hostEventResponder.OnFaulted;
                host.UnknownMessageReceived += hostEventResponder.OnUnknownMessageReceived;
            }

            private Uri GetEndpointUri(Binding binding, int port, string path)
            {
                var hostName = Dns.GetHostEntry("localhost").HostName;
                var protocol = binding.Scheme;
                var baseAddress = $"{protocol}://{hostName}"
                                  + (port > -1 ? $":{port}/" : "/")
                                  + path;
                return new Uri(baseAddress);
            }
        }
    }
}