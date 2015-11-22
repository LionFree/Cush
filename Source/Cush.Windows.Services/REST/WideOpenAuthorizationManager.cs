using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Cush.Windows.Services
{
    internal class WideOpenAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            var prop = new HttpResponseMessageProperty();
            prop.Headers.Add("Access-Control-Allow-Origin", "*");
            prop.Headers.Add("Access-Control-Allow-Methods", "POST, GET, DELETE, PUT, OPTIONS");
            prop.Headers.Add("Access-Control-Allow-Headers", "Content-Type, accept");

            operationContext.OutgoingMessageProperties.Add(HttpResponseMessageProperty.Name, prop);
            return true;
        }
    }
}