using System;

namespace Cush.Common.Logging
{
    internal sealed class TraceLogImplementation : Log
    {
        internal TraceLogImplementation(LogConfiguration config) : base(config, ExceptionWriter.Default)
        {
        }

        public override void AddEntry(LogEntry entry)
        {
            LogItems.Add(entry);
            LogEntries.Add(entry.ToString());

            try
            {
                System.Diagnostics.Trace.WriteLine(
                    string.Format(Strings.TraceLogEntryFormat,
                        entry.TimeStamp,
                        entry.Level.ToString().ToUpper(),
                        entry.Message));


                if (entry.HasException)
                {
                    Writer.WriteException(entry.Exception, System.Diagnostics.Trace.Listeners);
                }
            }
            catch (Exception ex)
            {
                AddEntry(LogLevel.Error, ex, ex.Message);
            }
        }
    }
}