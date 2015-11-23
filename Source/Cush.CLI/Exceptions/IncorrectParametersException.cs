using System;
using System.Runtime.Serialization;

namespace Cush.CommandLine.Exceptions
{
    class IncorrectParametersException : Exception
    {
        private readonly string _option;
        private readonly string _parameters;

        public string Option { get { return _option; } }
        public string Parameters { get { return _parameters; } }

        public IncorrectParametersException()
        {
        }

        public IncorrectParametersException(string option, string parameters)
            : base(string.Format("Incorrect number of parameters passed. option: {0}, parameters: {1}", option, parameters))
        {
            _option = option;
            _parameters = parameters;
        }

        public IncorrectParametersException(string message) : base(message)
        {
        }

        public IncorrectParametersException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IncorrectParametersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
