using System;
using System.Runtime.Serialization;

namespace Cush.CommandLine.Exceptions
{
    public class DelegateNotFoundException : Exception
    {
        public DelegateNotFoundException()
        {
        }

        public DelegateNotFoundException(string delegateName)
            : base(string.Format("Delegate with name '{0}' not found.", delegateName))
        {
        }

        public DelegateNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DelegateNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DelegateNotFoundException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException)
        { }
    }
}
