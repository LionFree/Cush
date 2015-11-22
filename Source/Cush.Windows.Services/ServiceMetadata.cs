namespace Cush.Windows.Services
{
    public sealed class ServiceMetadata
    {
        public string EventLogName { get; set; }
        public string EventLogSource { get; set; }
        public WindowsService Service { get; set; }
        public string LongDescription { get; set; }
        public bool Quiet { get; set; }
        public string ServiceName { get; set; }
        public string ShortDescription { get; set; }
        public bool Silent { get; set; }
    }
}