using System;
using System.ServiceProcess;

namespace Cush.Windows.Services.Internal
{
    internal abstract class ServiceControllerProxy
    {
        public abstract ServiceControllerStatus? Status { get; }
        public abstract bool CanStop { get; }

        public static ServiceControllerProxy GetInstance(string serviceName)
        {
            return new Implementation(serviceName);
        }

        public abstract void Start();
        public abstract void Start(string[] args);
        public abstract void Stop();
        public abstract void WaitForStatus(ServiceControllerStatus status, TimeSpan timeout);
        public abstract void WaitForStatus(ServiceControllerStatus status);

        private sealed class Implementation : ServiceControllerProxy
        {
            private readonly string _serviceName;

            public Implementation(string serviceName)
            {
                _serviceName = serviceName;
            }

            public override ServiceControllerStatus? Status
            {
                get
                {
                    using (var sc = new ServiceController(_serviceName))
                    {
                        return sc?.Status;
                    }
                }
            }

            public override bool CanStop
            {
                get
                {
                    using (var sc = new ServiceController(_serviceName))
                    {
                        return sc.CanStop;
                    }
                }
            }

            public override void Start()
            {
                using (var sc = new ServiceController(_serviceName))
                {
                    sc.Start();
                }
            }

            public override void Start(string[] args)
            {
                using (var sc = new ServiceController(_serviceName))
                {
                    sc.Start(args);
                }
            }

            public override void Stop()
            {
                using (var sc = new ServiceController(_serviceName))
                {
                    sc.Stop();
                }
            }

            public override void WaitForStatus(ServiceControllerStatus status, TimeSpan timeout)
            {
                using (var sc = new ServiceController(_serviceName))
                {
                    sc.WaitForStatus(status, timeout);
                }
            }

            public override void WaitForStatus(ServiceControllerStatus status)
            {
                WaitForStatus(status, TimeSpan.MaxValue);
            }
        }
    }
}