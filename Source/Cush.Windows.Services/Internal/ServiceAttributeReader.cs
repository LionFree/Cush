using System;
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

            _logger.Error(Strings.EXCEPTION_ServiceMustBeMarkedWithAttribute, service.GetType().FullName);
            throw new ArgumentException(string.Format(Strings.EXCEPTION_ServiceMustBeMarkedWithAttribute,
                service.GetType().FullName));
        }

        internal ServiceMetadata GetMetadata(WindowsService service)
        {
            var attribute = GetAttribute(service);
            return new ServiceMetadata
            {
                Service = service,
                Quiet = false,
                ServiceName = attribute.ServiceName,
                Silent = false
            };
        }
    }
}