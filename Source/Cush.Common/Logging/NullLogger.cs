using System;

namespace Cush.Common.Logging
{
    public abstract class NullLogger : ILogger
    {
        private static readonly NullLogger PrivateDefault;

        static NullLogger()
        {
            PrivateDefault = new NullLoggerImplementation();
        }

        public static ILogger Default => PrivateDefault;

        private class NullLoggerImplementation : NullLogger
        {
            public override bool IsTraceEnabled => false;
            public override bool IsDebugEnabled => false;
            public override bool IsInfoEnabled => false;
            public override bool IsWarnEnabled => false;
            public override bool IsErrorEnabled => false;
            public override bool IsFatalEnabled => false;
            public override string Name => string.Empty;

            public override void Trace(string message)
            {
                // Do nothing.
            }

            public override void Trace(Exception exception)
            {
                // Do nothing.
            }

            public override void Trace(Exception exception, string message)
            {
                // Do nothing.
            }

            public override void Trace(string message, params object[] args)
            {
                // Do nothing.
            }

            public override void Info(string message)
            {
                // Do nothing.
            }

            public override void Info(Exception exception)
            {
                // Do nothing.
            }

            public override void Info(Exception exception, string message)
            {
                // Do nothing.
            }

            public override void Info(string message, params object[] args)
            {
                // Do nothing.
            }

            public override void Info(Exception exception, string message, params object[] args)
            {
                // Do nothing.
            }

            public override void Warn(string message)
            {
                // Do nothing.
            }

            public override void Warn(Exception exception)
            {
                // Do nothing.
            }

            public override void Warn(Exception exception, string message)
            {
                // Do nothing.
            }

            public override void Warn(string message, params object[] args)
            {
                // Do nothing.
            }

            public override void Warn(Exception exception, string message, params object[] args)
            {
                // Do nothing.
            }

            public override void Error(string message)
            {
                // Do nothing.
            }

            public override void Error(Exception exception)
            {
                // Do nothing.
            }

            public override void Error(Exception exception, string message)
            {
                // Do nothing.
            }

            public override void Error(string message, params object[] args)
            {
                // Do nothing.
            }

            public override void Error(Exception exception, string message, params object[] args)
            {
                // Do nothing.
            }

            public override void Fatal(string message)
            {
                // Do nothing.
            }

            public override void Fatal(Exception exception)
            {
                // Do nothing.
            }

            public override void Fatal(Exception exception, string message)
            {
                // Do nothing.
            }

            public override void Fatal(string message, params object[] args)
            {
                // Do nothing.
            }

            public override void Fatal(Exception exception, string message, params object[] args)
            {
                // Do nothing.
            }

            public override void Trace(Exception exception, string message, params object[] args)
            {
                // Do nothing.
            }

            public override void Debug(string message)
            {
                // Do nothing.
            }

            public override void Debug(Exception exception)
            {
                // Do nothing.
            }

            public override void Debug(Exception exception, string message)
            {
                // Do nothing.
            }

            public override void Debug(string message, params object[] args)
            {
                // Do nothing.
            }

            public override void Debug(Exception exception, string message, params object[] args)
            {
                // Do nothing.
            }
        }

        #region Abstract Members

        public abstract string Name { get; }
        public abstract bool IsTraceEnabled { get; }
        public abstract bool IsDebugEnabled { get; }
        public abstract bool IsInfoEnabled { get; }
        public abstract bool IsWarnEnabled { get; }
        public abstract bool IsErrorEnabled { get; }
        public abstract bool IsFatalEnabled { get; }
        public abstract void Trace(string message);
        public abstract void Trace(Exception exception);
        public abstract void Trace(Exception exception, string message);
        public abstract void Trace(string message, params object[] args);
        public abstract void Trace(Exception exception, string message, params object[] args);
        public abstract void Debug(string message);
        public abstract void Debug(Exception exception);
        public abstract void Debug(Exception exception, string message);
        public abstract void Debug(string message, params object[] args);
        public abstract void Debug(Exception exception, string message, params object[] args);
        public abstract void Info(string message);
        public abstract void Info(Exception exception);
        public abstract void Info(Exception exception, string message);
        public abstract void Info(string message, params object[] args);
        public abstract void Info(Exception exception, string message, params object[] args);
        public abstract void Warn(string message);
        public abstract void Warn(Exception exception);
        public abstract void Warn(Exception exception, string message);
        public abstract void Warn(string message, params object[] args);
        public abstract void Warn(Exception exception, string message, params object[] args);
        public abstract void Error(string message);
        public abstract void Error(Exception exception);
        public abstract void Error(Exception exception, string message);
        public abstract void Error(string message, params object[] args);
        public abstract void Error(Exception exception, string message, params object[] args);
        public abstract void Fatal(string message);
        public abstract void Fatal(Exception exception);
        public abstract void Fatal(Exception exception, string message);
        public abstract void Fatal(string message, params object[] args);
        public abstract void Fatal(Exception exception, string message, params object[] args);

        #endregion
    }
}