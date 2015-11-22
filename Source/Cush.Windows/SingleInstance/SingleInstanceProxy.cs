using Cush.Common.Exceptions;
using System;
using System.Windows;
using System.Windows.Threading;


namespace Cush.Windows.SingleInstance
{
    internal abstract class SingleInstanceProxy : MarshalByRefObject
    {
        public static SingleInstanceProxy GetInstance(SingleInstanceDelegate app)
        {
            return new SingleInstanceProxyImpl(app());
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public abstract void InvokeFirstInstance(string[] args);

        private class SingleInstanceProxyImpl : SingleInstanceProxy
        {
            private readonly ISingleInstanceApplication _application;
            private readonly bool _isWPF;

            internal SingleInstanceProxyImpl(ISingleInstanceApplication app)
            {
                ThrowHelper.IfNullThenThrow(() => app);
                _application = app;
                _isWPF = app is Application;
            }

            private object ActivateFirstInstanceCallback(object args)
            {
                try
                {
                    var app = (ISingleInstanceApplication) Application.Current;
                    if (app != null) app.OnSecondInstanceCreated(args as string[]);
                }
                catch (Exception)
                {
                    // Didn't work.  Do nothing.
                }

                return null;
            }

            public override void InvokeFirstInstance(string[] args)
            {
                if (_isWPF)
                {
                    if (Application.Current == null) return;
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new DispatcherOperationCallback(ActivateFirstInstanceCallback), args);
                }
                else
                {
                    if (_application == null) return;
                    _application.OnSecondInstanceCreated(args);
                }
            }
        }
    }
}