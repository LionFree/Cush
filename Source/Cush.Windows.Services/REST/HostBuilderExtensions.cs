using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;

namespace Cush.Windows.Services
{
    public static class HostBuilderExtensions
    {
        public static WebServiceHost AuthorizationManager(this WebServiceHost host,
            ServiceAuthorizationManager manager)
        {
            var serviceAuthorizationBehavior = host.Description.Behaviors.Find<ServiceAuthorizationBehavior>();
            serviceAuthorizationBehavior.ServiceAuthorizationManager = manager;
            return host;
        }

        public static WebServiceHost AuthorizationBehavior(this WebServiceHost host,
            ServiceAuthorizationBehavior behavior)
        {
            //var sab = host.Description.Behaviors.Find<ServiceAuthorizationBehavior>();
            //sab.PrincipalPermissionMode = PrincipalPermissionMode.None;
            
            host.Description.Behaviors.Remove<ServiceAuthorizationBehavior>();
            host.Description.Behaviors.Add(behavior);
            return host;
        }

        public static WebServiceHost DebugBehavior(this WebServiceHost host,
            ServiceDebugBehavior behavior)
        {
            host.Description.Behaviors.Remove<ServiceDebugBehavior>();
            host.Description.Behaviors.Add(behavior);
            return host;


            //var serviceDebugBehavior = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            //serviceDebugBehavior.HttpHelpPageBinding = behavior.HttpHelpPageBinding;
            //serviceDebugBehavior.HttpHelpPageEnabled = behavior.HttpHelpPageEnabled;
            //serviceDebugBehavior.HttpHelpPageUrl = behavior.HttpHelpPageUrl;
            //serviceDebugBehavior.HttpsHelpPageBinding = behavior.HttpsHelpPageBinding;
            //serviceDebugBehavior.HttpsHelpPageEnabled = behavior.HttpsHelpPageEnabled;
            //serviceDebugBehavior.HttpsHelpPageUrl = behavior.HttpsHelpPageUrl;
            //return host;
        }

        public static WebServiceHost BasicConfiguration(this WebServiceHost host)
        {
            WebServiceConfigurator.GetInstance(new WideOpenAuthorizationManager()).Configure(host);
            return host;
        }

        public static WebServiceHost RESTfulEndpoint<TInterface>(this WebServiceHost host, string apiBasePath)
        {
            var endpoint = host.AddServiceEndpoint(typeof(TInterface), new WebHttpBinding(), apiBasePath);
            endpoint.Behaviors.Add(new WebHttpBehavior());
            return host;
        }

        public static WebServiceHost NetTcpEndpoint<TInterface>(this WebServiceHost host, string apiBasePath)
        {
            host.AddServiceEndpoint(typeof(TInterface), new NetTcpBinding(), apiBasePath);
            return host;
        }

        public static WebServiceHost PublishMetadata(this WebServiceHost host)
        {
            ServiceMetadataBehavior smb = host.Description.Behaviors.Find<ServiceMetadataBehavior>();

            if (smb == null)
                smb = new ServiceMetadataBehavior();
            
            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
            host.Description.Behaviors.Add(smb);

            // Add MEX endpoint
            host.AddServiceEndpoint(
              ServiceMetadataBehavior.MexContractName,
              MetadataExchangeBindings.CreateMexHttpBinding(),
              "mex"
            );

            return host;
        }
    }


    public static class FluentGrammarExtensions
    {
        /// <summary>
        ///     Fluent language extension (no-op).
        /// </summary>
        public static WebServiceHost With(this WebServiceHost host)
        {
            return host;
        }

        /// <summary>
        ///     Fluent language extension (no-op).
        /// </summary>
        public static WebServiceHost Using(this WebServiceHost host)
        {
            return host;
        }

        /// <summary>
        ///     Fluent language extension (no-op).
        /// </summary>
        public static WebServiceHost And(this WebServiceHost host)
        {
            return host;
        }

        /// <summary>
        ///     Fluent language extension (no-op).
        /// </summary>
        public static WebServiceHost Add(this WebServiceHost host)
        {
            return host;
        }
    }
}