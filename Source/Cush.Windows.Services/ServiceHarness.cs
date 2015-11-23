using Cush.Common.Exceptions;
using System;
using System.ServiceProcess;


namespace Cush.Windows.Services
{
    /// <summary>
    ///     A mockable interface for <see cref="T:System.ServiceProcess.ServiceBase" />s.
    /// </summary>
    public abstract class ServiceHarness : ServiceBase
    {
        internal static ServiceHarness WrapService(WindowsService service)
        {
            return new ServiceHarnessImplementation(service);
        }

        /// <summary>
        ///     Provides the main entry point for a service executable.
        /// </summary>
        public abstract void Run();

        private class ServiceHarnessImplementation : ServiceHarness
        {
            private readonly WindowsService _service;

            internal ServiceHarnessImplementation(WindowsService service)
            {
                ThrowHelper.IfNullThenThrowArgumentException(() => service,
                    Strings.EXCEPTION_IWindowsServiceCannotBeNull);
                _service = service;

                ConfigureServiceFromAttributes(service);
            }

            private void ConfigureServiceFromAttributes(WindowsService service)
            {
                var attribute = service.GetType().GetAttribute<WindowsServiceAttribute>();

                if (attribute != null)
                {
                    EventLog.Source = string.IsNullOrEmpty(attribute.EventLogSource)
                        ? "WindowsServiceHarness"
                        : attribute.EventLogSource;

                    CanStop = attribute.CanStop;
                    CanPauseAndContinue = attribute.CanPauseAndContinue;
                    CanShutdown = attribute.CanShutdown;

                    // we don't handle: laptop power change event
                    CanHandlePowerEvent = false;

                    // we don't handle: Term Services session event
                    CanHandleSessionChangeEvent = false;

                    // always auto-event-log
                    AutoLog = true;
                }
                else
                {
                    throw new InvalidOperationException(
                        string.Format("WindowsService implementer {0} must have a WindowsServiceAttribute.",
                            service.GetType().FullName));
                }
            }

            public override void Run()
            {
                Run(this);
            }

            #region protected members

            // because all of these available overrides are protected,
            // we can't call them directly from our console harness, 
            // so instead we will just delegate to the WindowsService 
            // interface which is internal.
            protected override void OnStart(string[] args)
            {
                _service.OnStart(args);
            }

            protected override void OnStop()
            {
                _service.OnStop();
            }

            protected override void OnPause()
            {
                _service.OnPause();
            }

            protected override void OnShutdown()
            {
                _service.OnShutdown();
            }

            #endregion
        }
    }
}