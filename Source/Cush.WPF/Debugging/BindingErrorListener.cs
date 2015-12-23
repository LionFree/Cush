using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Cush.WPF.Debugging
{
    /// <summary>
    ///     Raises an event each time a WPF Binding error occurs.
    /// </summary>
    public sealed class BindingErrorListener : TraceListener, IDisposable
    {
        readonly DebugTraceListener _debugListener = new DebugTraceListener();
        readonly ObservableTraceListener _traceListener = new ObservableTraceListener();
        private static List<TraceListener> _listeners = new List<TraceListener>();
        private readonly Action<string> _logAction;

        static BindingErrorListener()
        {
            PresentationTraceSources.Refresh();
        }

        public BindingErrorListener()
        {
            PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Error;
            AddListener(_traceListener);
            AddListener(_debugListener);
        }

        private BindingErrorListener(Action<string> action)
        {
            _logAction = action;
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static void Listen(Action<string> action)
        {
            AddListener(new BindingErrorListener(action));
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static void BreakOnError()
        {
            AddListener(new DebugTraceListener());
        }


        public new void Dispose()
        {
            foreach (var item in _listeners)
            {
                RemoveListener(item);
            }
            _listeners = new List<TraceListener>();
            base.Dispose();
        }

        public override void Write(string message)
        {
            // Do nothing.
        }

        public override void WriteLine(string message)
        {
            _logAction(message);
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        internal static void AddListener(TraceListener listener)
        {
            PresentationTraceSources.DataBindingSource.Listeners.Add(listener);
            _listeners.Add(listener);
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        internal static void RemoveListener(TraceListener listener)
        {
            if (!_listeners.Contains(listener)) return;
            PresentationTraceSources.DataBindingSource.Listeners.Remove(listener);
            listener.Dispose();
        }


        /// <summary>
        ///     Event raised each time a WPF binding error occurs
        /// </summary>
        public event Action<string> ErrorCaught
        {
            add { _traceListener.TraceCaught += value; }
            remove { _traceListener.TraceCaught -= value; }
        }
    }
}