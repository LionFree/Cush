using System;

namespace Cush.Common.Logging
{
    internal sealed class ConsoleLogImplementation : Log
    {
        internal ConsoleLogImplementation(LogConfiguration config) : base(config, ExceptionWriter.Default)
        {
        }

        public override void AddEntry(LogEntry entry)
        {
            LogItems.Add(entry);
            LogEntries.Add(entry.ToString());

            try
            {
                System.Console.WriteLine(Strings.FileLogEntryFormat, entry.TimeStamp, entry.Level.ToString().ToUpper(),
                    entry.Message);


                if (entry.HasException)
                {
                    Writer.WriteException(entry.Exception, System.Console.Out);
                }
            }
            catch (Exception ex)
            {
                AddEntry(LogLevel.Error, ex, ex.Message);
            }
        }
    }
}