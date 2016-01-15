namespace Cush.Common.Logging
{
    internal sealed class NullLogImplementation : Log
    {
        internal NullLogImplementation(LogConfiguration config) : base(config, ExceptionWriter.Default)
        {
        }

        public override void AddEntry(LogEntry entry)
        {
            // Do nothing.
        }
    }
}