using System;
using System.IO;

namespace Cush.Common.Logging
{
    internal sealed class FileLogImplementation : Log
    {
        private bool _fileError;

        internal FileLogImplementation(LogConfiguration config):base(config, ExceptionWriter.Default)
        {
        }

        public override void AddEntry(LogEntry entry)
        {
            LogItems.Add(entry);
            LogEntries.Add(entry.ToString());

            if (_fileError || Configuration.FullPath == string.Empty) return;

            try
            {
                using (var writer = File.AppendText(Configuration.FullPath))
                {
                    writer.WriteLine(Strings.FileLogEntryFormat,
                        entry.TimeStamp,
                        entry.Level.ToString().ToUpper(),
                        entry.Message);

                    if (entry.HasException)
                    {
                        Writer.WriteException(entry.Exception, writer);
                    }
                }
            }
            catch (Exception ex)
            {
                _fileError = true;
                AddEntry(LogLevel.Error, ex, ex.Message);
            }
        }
    }
}