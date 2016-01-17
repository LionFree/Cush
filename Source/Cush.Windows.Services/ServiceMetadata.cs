namespace Cush.Windows.Services
{
    public sealed class ServiceMetadata
    {
        //private WindowsServiceAttribute Attribute { get; set; }
        public string ServiceName { get; set; }

        public WindowsService Service { get; set; }
        
        public bool Quiet { get; set; }
        
        public bool Silent { get; set; }
    }
}