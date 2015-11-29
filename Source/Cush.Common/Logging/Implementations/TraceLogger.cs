using System;
using System.Diagnostics;
using Cush.Common.Logging.Internal;

namespace Cush.Common.Logging
{
    public sealed class TraceLogger : BaseLogger
    {
        public TraceLogger() : base(Log.Trace, string.Empty, string.Empty, null)
        {
        }
        
        public override string Name
        {
            get { return "Cush Trace Logger"; }
        }

        public override bool IsTraceEnabled
        {
            get { return true; }
        }

        public override bool IsDebugEnabled
        {
            get { return true; }
        }

        public override bool IsInfoEnabled
        {
            get { return true; }
        }

        public override bool IsWarnEnabled
        {
            get { return true; }
        }

        public override bool IsErrorEnabled
        {
            get { return true; }
        }

        public override bool IsFatalEnabled
        {
            get { return true; }
        }
    }
}