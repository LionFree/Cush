namespace Cush.Windows.Services
{
    public sealed class ServiceMetadata
    {
        /// <summary>
        ///     The name of the service.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        ///     The service itself.
        /// </summary>
        public WindowsService Service { get; set; }

        /// <summary>
        ///     Whether to restrict console output to errors ONLY.
        /// </summary>
        public bool Quiet { get; set; }

        /// <summary>
        ///     Whether to display nothing (not even errors) on the console.
        /// </summary>
        public bool Silent { get; set; }
    }
}