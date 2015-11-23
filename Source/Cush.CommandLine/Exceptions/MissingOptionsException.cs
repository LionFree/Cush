using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Cush.Common.Exceptions;

namespace Cush.CommandLine.Exceptions
{
    public class MissingOptionsException : Exception
    {
        private readonly IEnumerable<string> _missing;

        public MissingOptionsException()
        {
        }

        public MissingOptionsException(string message) : base(message)
        {
        }

        public MissingOptionsException(IList<string> missingOptions) : base(DefineMessage(missingOptions))
        {
            _missing = missingOptions;
        }


        public MissingOptionsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingOptionsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IEnumerable<string> MissingOptions
        {
            get { return _missing; }
        }

        private static string DefineMessage(IEnumerable<string> missingOptions)
        {
            ThrowHelper.IfNullThenThrow(() => missingOptions);
            //if (missingOptions == null) throw new ArgumentNullException("missingOptions");

            var commandLineOptions = missingOptions as string[] ?? missingOptions.ToArray();
            return "The following required command-line options were not provided: " +
                   string.Join(", ", commandLineOptions.Select(x => x));
        }
    }
}