using System;
using System.ServiceModel;
using Cush.Common.Logging;

namespace Cush.TestHarness.WinService.Infrastructure
{
    public abstract class Engine
    {
        internal static Engine ComposeObjectGraph()
        {
            var logger = Loggers.Trace;
            

            return new Implementation(ComposeService());
        }

        internal static TestService ComposeService()
        {
            var logger = Loggers.Trace;

            return TestService.GetInstance(logger, new ServiceHost(typeof(TestService)));
        }

        internal abstract void Start(string[] args);
        internal abstract void Shutdown();

        private class Implementation : Engine
        {
            private readonly TestService _service;
            
            internal Implementation(TestService service)
            {
                _service = service;
            }

            internal override void Start(string[] args)
            {
                _service.Startup(args);
            }

            internal override void Shutdown()
            {
                throw new NotImplementedException();
            }
        }
    }
}