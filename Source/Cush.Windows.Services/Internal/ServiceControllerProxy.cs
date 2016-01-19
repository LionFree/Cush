using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable MemberCanBeProtected.Global

namespace Cush.Windows.Services.Internal
{
    internal abstract class ServiceControllerProxy
    {
        public abstract ServiceControllerStatus? Status { get; }
        public abstract bool CanStop { get; }

        public abstract string ServiceName { get; }


        internal static ServiceControllerProxy GetInstance(string serviceName)
        {
            return new Implementation(serviceName);
        }

        public abstract void Start();
        public abstract void Start(string[] args);
        public abstract void Stop();
        public abstract void WaitForStatus(ServiceControllerStatus status, TimeSpan timeout);
        public abstract void WaitForStatus(ServiceControllerStatus status);

        public abstract IEnumerable<ServiceControllerProxy> GetServices();
        public abstract bool IsInstalled(string serviceName);

        private sealed class Implementation : ServiceControllerProxy
        {
            public Implementation(string serviceName)
            {
                ServiceName = serviceName;
            }

            public override string ServiceName { get; }

            public override ServiceControllerStatus? Status
            {
                get
                {
                    if (!IsInstalled(ServiceName)) return null;
                    using (var sc = new ServiceController(ServiceName))
                    {
                        return sc.Status;
                    }
                }
            }


            public override bool CanStop
            {
                get
                {
                    if (!IsInstalled(ServiceName)) return false;
                    using (var sc = new ServiceController(ServiceName))
                    {
                        return sc.CanStop;
                    }
                }
            }

            public override IEnumerable<ServiceControllerProxy> GetServices()
            {
                return ServiceController.GetServices().Select(item => GetInstance(item.ServiceName)).ToArray();
            }

            public override bool IsInstalled(string serviceName)
            {
                return ServiceController.GetServices().Any(s => s.ServiceName == serviceName);
            }

            public override void Start()
            {
                if (!IsInstalled(ServiceName)) throw new ArgumentException("Service is not installed.");
                using (var sc = new ServiceController(ServiceName))
                {
                    sc.Start();
                }
            }

            public override void Start(string[] args)
            {
                if (!IsInstalled(ServiceName)) throw new ArgumentException("Service is not installed.");
                using (var sc = new ServiceController(ServiceName))
                {
                    sc.Start(args);
                }
            }

            public override void Stop()
            {
                if (!IsInstalled(ServiceName)) throw new ArgumentException("Service is not installed.");
                using (var sc = new ServiceController(ServiceName))
                {
                    sc.Stop();
                }
            }

            public override void WaitForStatus(ServiceControllerStatus status, TimeSpan timeout)
            {
                if (!IsInstalled(ServiceName)) throw new ArgumentException("Service is not installed.");
                using (var sc = new ServiceController(ServiceName))
                {
                    sc.WaitForStatus(status, timeout);
                }
            }

            public override void WaitForStatus(ServiceControllerStatus status)
            {
                if (!IsInstalled(ServiceName)) throw new ArgumentException("Service is not installed.");
                WaitForStatus(status, TimeSpan.MaxValue);
            }
        }
    }
}