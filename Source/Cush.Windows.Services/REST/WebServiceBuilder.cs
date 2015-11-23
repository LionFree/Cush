using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;

namespace Cush.Windows.Services
{
    internal abstract class WebServiceBuilder
    {
        internal static WebServiceBuilder GetInstance()
        {
            return new WebServiceBuilderImplementation();
        }

        public abstract WebServiceHost CreateRESTfulHost<T>(int port, string apiBasePath, bool useSsl);

        public abstract WebServiceHost CreateRESTfulHost<TImplementation, TInterface>(int port, string apiBasePath,
            bool useSsl);

        private class WebServiceBuilderImplementation : WebServiceBuilder
        {
            private readonly WebServiceConfigurator _configurator = WebServiceConfigurator.GetInstance();

            public override WebServiceHost CreateRESTfulHost<T>(int port, string apiBasePath,
                bool useSsl)
            {
                return CreateRESTfulHost<T>(GetInterface<T>(), port, apiBasePath, useSsl);
            }
            
            public override WebServiceHost CreateRESTfulHost<TImplementation, TInterface>(int port,
                string apiBasePath,
                bool useSsl)
            {
                return CreateRESTfulHost<TImplementation>(GetInterface<TInterface>(), port, apiBasePath, useSsl);
            }

            private WebServiceHost CreateRESTfulHost<TEndpointImplementation>(Type endpointInterface, int port,
                string apiBasePath,
                bool useSsl)
            {
                var uriString = GetUriString(port, useSsl);

                var host = new WebServiceHost(typeof(TEndpointImplementation), new Uri(uriString));
                
                var endpoint = host.AddServiceEndpoint(endpointInterface, new WebHttpBinding(), apiBasePath);
                endpoint.Behaviors.Add(new WebHttpBehavior());

                _configurator.Configure(host);

                return host;
            }

            private Type GetInterface<T>()
            {
                var interfaces = typeof(T).GetInterfaces();
                var endpointInterface = interfaces.Length > 0 ? interfaces[0] : typeof(T);

                // What if T implements multiple interfaces or contracts?

                return endpointInterface;
            }

            private string GetUriString(int port, bool useSsl)
            {
                var protocol = useSsl ? "https" : "http";

                var uriString = protocol + "://localhost";
                return (port == -1) ? uriString : uriString + ":" + port;
            }
        }
    }
}