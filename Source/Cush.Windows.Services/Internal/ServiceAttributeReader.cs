using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cush.Common.Logging;

namespace Cush.Windows.Services.Internal
{
    public class ServiceAttributeReader
    {
        private readonly ILogger _logger;

        internal ServiceAttributeReader(ILogger logger)
        {
            _logger = logger;
        }

        internal WindowsServiceAttribute GetAttribute(WindowsService service)
        {
            var attribute = service.GetType().GetAttribute<WindowsServiceAttribute>();
            if (attribute != null) return attribute;

            _logger.Error(Strings.EXCEPTION_ServiceMustBeMarkedWithAttribute);
            throw new ArgumentException(Strings.EXCEPTION_ServiceMustBeMarkedWithAttribute);
        }

        internal ServiceMetadata GetMetadata(WindowsService service)
        {
            var attribute = GetAttribute(service);
            return new ServiceMetadata
            {
                Service = service,
                //EventLogName = attribute.EventLogName,
                //EventLogSource = attribute.EventLogSource,
                //LongDescription = attribute.Description,
                Quiet = false,
                ServiceName = attribute.ServiceName,
                //DisplayName = attribute.DisplayName,
                Silent = false
            };
        }
    }
}
