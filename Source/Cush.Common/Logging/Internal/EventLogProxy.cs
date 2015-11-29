using System;
using System.Diagnostics;

namespace Cush.Common.Logging.Internal
{
    public abstract class EventLogProxy
    {
        public static EventLogProxy GetInstance()
        {
            return new EventLogImplementation();
        }

        private sealed class EventLogImplementation : EventLogProxy
        {
            public override bool SourceExists(string source)
            {
                return EventLog.SourceExists(source);
            }

            public override void CreateEventSource(string source, string logName)
            {
                if (string.IsNullOrEmpty(source))
                    throw new ArgumentException("Must specify value for source.");

                EventLog.CreateEventSource(source, logName);
            }

            public override void WriteEntry(string source, string message)
            {
                WriteEntry(source, message, EventLogEntryType.Information, 0, 0);
            }

            public override void WriteEntry(string source, string message, EventLogEntryType type)
            {
                WriteEntry(source, message, type, 0, 0);
            }

            public override void WriteEntry(string source, string message, EventLogEntryType type, int eventId)
            {
                WriteEntry(source, message, type, eventId, 0);
            }

            public override void WriteEntry(string source, string message,
                EventLogEntryType type, int eventId,
                short category)
            {
                if (string.IsNullOrEmpty(source)) return;
                EventLog.WriteEntry(source, message, type, eventId, category);
            }
        }

        #region Abstract Methods

        /// <summary>
        ///     Determines whether an event source is registered on the local computer.
        /// </summary>
        /// <param name="source">The name of the event source.</param>
        public abstract bool SourceExists(string source);

        /// <summary>
        ///     Establishes the specified source name as a valid event source for writing entries
        ///     to a log on the local computer.
        ///     This method can also create a new custom log on the local computer.
        /// </summary>
        /// <param name="source">The source name by which the application is registered on the local computer.</param>
        /// <param name="logName">
        ///     The name of the log the source's entries are written to.
        ///     Possible values include Application, System, or a custom event log.
        /// </param>
        public abstract void CreateEventSource(string source, string logName);

        /// <summary>
        ///     Writes an information type entry with the given message text to an event log, using the specified registered event
        ///     source.
        /// </summary>
        /// <param name="source">The source by which the application is registered on the local computer.</param>
        /// <param name="message">The string to write to the event log.</param>
        public abstract void WriteEntry(string source, string message);

        /// <summary>
        ///     Writes an error, warning, information, success audit, or failure audit entry with the given message text to the
        ///     event log, using the specified registered event source.
        /// </summary>
        /// <param name="source">The source name by which the application is registered on the local computer.</param>
        /// <param name="message">The string to write to the event log.</param>
        /// <param name="type">One of the <see cref="EventLogEntryType" /> values.</param>
        public abstract void WriteEntry(string source, string message, EventLogEntryType type);

        /// <summary>
        ///     Writes an entry with the given message text and application-defined event identifier to the event log, using the
        ///     specified registered event source.
        /// </summary>
        /// <param name="source">The source name by which the application is registered on the local computer.</param>
        /// <param name="message">The string to write to the event log.</param>
        /// <param name="type">One of the <see cref="EventLogEntryType" /> values.</param>
        /// <param name="eventId">The application-specific identifier for this event.</param>
        public abstract void WriteEntry(string source, string message, EventLogEntryType type, int eventId);

        /// <summary>
        ///     Writes an entry with the given message text, application-defined event identifier, and application-defined category
        ///     to the event log, using the specified registered event source.  The category can be used by the Event Viewer to
        ///     filter events in the log.
        /// </summary>
        /// <param name="source">The source name by which the application is registered on the local computer.</param>
        /// <param name="message">The string to write to the event log.</param>
        /// <param name="type">One of the <see cref="EventLogEntryType" /> values.</param>
        /// <param name="eventId">The application-specific identifier for this event.</param>
        /// <param name="category">The application-specific subcategory associated with the message.</param>
        public abstract void WriteEntry(string source, string message, EventLogEntryType type, int eventId,
            short category);

        #endregion
    }
}