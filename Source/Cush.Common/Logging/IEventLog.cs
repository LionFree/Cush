using System.Diagnostics;

namespace Cush.Common.Logging
{
    public interface IEventLog
    {
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
        void CreateEventSource(string source, string logName);

        /// <summary>
        ///     Determines whether an event source is registered on the local computer.
        /// </summary>
        /// <param name="source">The name of the event source.</param>
        bool SourceExists(string source);

        /// <summary>
        ///     Writes an information type entry with the given message text to an event log, using the specified registered event
        ///     source.
        /// </summary>
        /// <param name="source">The source by which the application is registered on the local computer.</param>
        /// <param name="message">The string to write to the event log.</param>
        void WriteEntry(string source, string message);

        /// <summary>
        ///     Writes an error, warning, information, success audit, or failure audit entry with the given message text to the
        ///     event log, using the specified registered event source.
        /// </summary>
        /// <param name="source">The source name by which the application is registered on the local computer.</param>
        /// <param name="message">The string to write to the event log.</param>
        /// <param name="type">One of the <see cref="EventLogEntryType" /> values.</param>
        void WriteEntry(string source, string message, EventLogEntryType type);

        /// <summary>
        ///     Writes an entry with the given message text and application-defined event identifier to the event log, using the
        ///     specified registered event source.
        /// </summary>
        /// <param name="source">The source name by which the application is registered on the local computer.</param>
        /// <param name="message">The string to write to the event log.</param>
        /// <param name="type">One of the <see cref="EventLogEntryType" /> values.</param>
        /// <param name="eventId">The application-specific identifier for this event.</param>
        void WriteEntry(string source, string message, EventLogEntryType type, int eventId);

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
        void WriteEntry(string source, string message, EventLogEntryType type, int eventId, short category);
    }
}