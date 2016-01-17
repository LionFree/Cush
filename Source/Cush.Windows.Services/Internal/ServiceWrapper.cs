namespace Cush.Windows.Services.Internal
{
    public abstract class ServiceWrapper : IServiceWrapper
    {
        public static ServiceWrapper Default
        {
            get { return new WrapperImplementation(); }
        }

        public abstract ServiceHarness WrapService(WindowsService service);

        private class WrapperImplementation : ServiceWrapper
        {
            public override ServiceHarness WrapService(WindowsService service)
            {
                return ServiceHarness.WrapService(service);
            }
        }
    }
}