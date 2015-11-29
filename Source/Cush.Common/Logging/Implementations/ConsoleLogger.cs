using Cush.Common.Logging.Internal;

namespace Cush.Common.Logging
{
    public sealed class ConsoleLogger : BaseLogger
    {
        public ConsoleLogger() : base(Log.Console, string.Empty, string.Empty, null)
        {
        }

        public override string Name
        {
            get { return "Cush Console Logger"; }
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