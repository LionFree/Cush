using System.ServiceModel.Web;

namespace Cush.Windows.Services
{
    public static class HostBuilder
    {
        private static readonly WebServiceBuilder Builder = WebServiceBuilder.GetInstance();

        /// <summary>
        ///     Creates a <see cref="WebServiceHost" /> from a given type.  Will infer the contract.
        /// </summary>
        /// <typeparam name="T">The service type (which implements the contract).</typeparam>
        /// <param name="port">The port.</param>
        /// <param name="apiBasePath">
        ///     The api base path.  This is the part of the URI that comes after "localhost:9944/" which
        ///     every other path must include.
        /// </param>
        /// <param name="useSsl">A boolean that determines whether the default endpoint will use https/ssl or not.</param>
        public static WebServiceHost CreateRESTfulHost<T>
            (bool useSsl, int port, string apiBasePath)
        {
            return Builder.CreateRESTfulHost<T>(port, apiBasePath, useSsl);
        }

        /// <summary>
        ///     Creates a <see cref="WebServiceHost" /> from a type and a specific contract.
        /// </summary>
        /// <typeparam name="TImplementation">The service type (which implements the contract).</typeparam>
        /// <typeparam name="TInterface">The contract to be implemented.</typeparam>
        /// <param name="port">The port.</param>
        /// <param name="apiBasePath">
        ///     The api base path.  This is the part of the URI that comes after "localhost:9944/" which
        ///     every other path must include.
        /// </param>
        /// <param name="useSsl">A boolean that determines whether the default endpoint will use https/ssl or not.</param>
        public static WebServiceHost CreateRESTfulHost<TImplementation, TInterface>
            (bool useSsl, int port, string apiBasePath)
        {
            return Builder.CreateRESTfulHost<TImplementation, TInterface>(port, apiBasePath, useSsl);
        }
    }
}