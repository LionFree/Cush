using System;
using System.Diagnostics;

namespace Cush.Common.Exceptions
{
    public abstract class BindingErrorListener : TraceListener
    {
        internal abstract Action<string> LogAction { get; }

        [DebuggerStepThrough]
        public static BindingErrorListener GetInstance()
        {
            return GetInstance(null);
        }

        [DebuggerStepThrough]
        public static BindingErrorListener GetInstance(Action<string> logAction)
        {
            return new Listener(logAction);
        }

        /// <summary>
        ///     Adds a trace listener to the trace source listener collection.
        /// </summary>
        /// <param name="logAction">The action to take when a trace event appears.</param>
        public abstract void Listen(Action<string> logAction);

        private sealed class Listener : BindingErrorListener
        {
            private readonly Action<string> _logAction;

            internal Listener(Action<string> logAction)
            {
                _logAction = logAction;
            }

            internal override Action<string> LogAction
            {
                get { return _logAction; }
            }

            [DebuggerStepThrough]
            public override void Listen(Action<string> logAction)
            {
                PresentationTraceSources.DataBindingSource.Listeners
                    .Add(GetInstance(logAction));
            }

            [DebuggerStepThrough]
            public override void Write(string message)
            {
            }

            [DebuggerStepThrough]
            public override void WriteLine(string message)
            {
                LogAction(message);
            }
        }
    }
}