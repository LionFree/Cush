using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using Cush.Common.Exceptions;
using Cush.Common.Logging;

namespace Cush.Common.FileHandling
{
    public abstract class FileWriter
    {
        protected readonly ILogger Logger;

        protected FileWriter(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        ///     Serializes the specified array to disk.
        /// </summary>
        /// <param name="array">The object to serialize.</param>
        /// <param name="fileName">The filename to write.</param>
        /// <param name="callback">A <see cref="ProgressChangedEventHandler" /> callback that will update, e.g., a progress bar.</param>
        /// <returns></returns>
        public abstract bool Write(object array, string fileName, ProgressChangedEventHandler callback);
    }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class BinaryFileWriter : FileWriter
    {
        public BinaryFileWriter(ILogger logger) : base(logger)
        {
        }

        public override bool Write(object array, string fileName, ProgressChangedEventHandler callback)
        {
            ThrowHelper.IfNullThenThrow(() => array);
            ThrowHelper.IfNullOrEmptyThenThrow(() => fileName);

            try
            {
                Logger.Trace($"Starting Save: \"{fileName}\"");

                var serializer = new BinaryFormatter();

                // This "using" forces disposal of the Stream after it's done.
                // This will prevent the System.IO.IOException indicating that
                //   The process cannot access the file 'C:\MyPath\MyFile.bin' 
                //   because it is being used by another process.
                // This exception tends to show up when saving/reading frequently.
                using (var writeFileStream = File.Create(fileName))
                {
                    serializer.Serialize(writeFileStream, array);
                }

                Logger.Trace("  Done saving.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }
    }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class XmlFileWriter : FileWriter
    {
        public XmlFileWriter(ILogger logger) : base(logger)
        {
        }

        public override bool Write(object array, string fileName, ProgressChangedEventHandler callback)
        {
            ThrowHelper.IfNullThenThrow(() => array);
            ThrowHelper.IfNullOrEmptyThenThrow(() => fileName);

            try
            {
                Logger.Trace($"Starting Save: \"{fileName}\"");

                var xsn = new XmlSerializerNamespaces();
                xsn.Add("", "");

                var xws = new XmlWriterSettings
                {
                    OmitXmlDeclaration = true,
                    NewLineOnAttributes = false,
                    NewLineHandling = NewLineHandling.Replace,
                    Indent = true
                };

                var x = new XmlSerializer(array.GetType());

                // This "using" forces disposal of the XmlWriter after it's done.
                // This will prevent the System.IO.IOException indicating that
                //   The process cannot access the file 'C:\MyPath\MyFile.xml' 
                //   because it is being used by another process.
                // This exception tends to show up when saving/reading frequently.
                using (var xmlWriter = XmlWriter.Create(fileName, xws))
                {
                    x.Serialize(xmlWriter, array, xsn);
                }

                Logger.Trace("  Done saving.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }
    }
}