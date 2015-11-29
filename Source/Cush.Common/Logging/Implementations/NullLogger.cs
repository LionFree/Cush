using Cush.Common.Logging.Internal;

namespace Cush.Common.Logging
{
    public sealed class NullLogger : BaseLogger
    {
        public NullLogger() : base(Log.Null, string.Empty, string.Empty, null)
        {
        }

        public override bool IsTraceEnabled
        {
            get { return false; }
        }

        public override bool IsDebugEnabled
        {
            get { return false; }
        }

        public override bool IsInfoEnabled
        {
            get { return false; }
        }

        public override bool IsWarnEnabled
        {
            get { return false; }
        }

        public override bool IsErrorEnabled
        {
            get { return false; }
        }

        public override bool IsFatalEnabled
        {
            get { return false; }
        }

        public override string Name
        {
            get { return string.Empty; }
        }
    }
}