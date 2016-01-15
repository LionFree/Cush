using System;
using System.Diagnostics;

namespace Cush.Common.Logging
{
    public class EventLogProxy : IEventLog
    {
        public bool SourceExists(string source)
        {
            return EventLog.SourceExists(source);
        }

        public void CreateEventSource(string source, string logName)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentException("Must specify value for source.");

            EventLog.CreateEventSource(source, logName);
        }

        public void WriteEntry(string source, string message)
        {
            WriteEntry(source, message, EventLogEntryType.Information, 0, 0);
        }

        public void WriteEntry(string source, string message, EventLogEntryType type)
        {
            WriteEntry(source, message, type, 0, 0);
        }

        public void WriteEntry(string source, string message, EventLogEntryType type, int eventId)
        {
            WriteEntry(source, message, type, eventId, 0);
        }

        public void WriteEntry(string source, string message,
            EventLogEntryType type, int eventId,
            short category)
        {
            if (string.IsNullOrEmpty(source)) return;
            EventLog.WriteEntry(source, message, type, eventId, category);
        }
    }
}