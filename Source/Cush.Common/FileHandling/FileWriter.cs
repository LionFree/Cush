using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;
using Cush.Common.Exceptions;
using Cush.Common.FileHandling.Progress;
using Cush.Common.Logging;

namespace Cush.Common.FileHandling
{
    public abstract class FileWriter
    {
        public bool IsCancelled { get; protected set; }
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
        public abstract bool Write<T>(T array, string fileName, ProgressChangedEventHandler callback);

        public event EventHandler Cancelled;

        internal void CancelFileOperations(object sender, EventArgs e)
        {
            Cancelled?.Invoke(sender, e);
        }
    }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class BinaryFileWriter : FileWriter
    {
        public BinaryFileWriter(ILogger logger) : base(logger)
        {
        }

        public override bool Write<T>(T array, string fileName, ProgressChangedEventHandler callback)
        {
            ThrowHelper.IfNullThenThrow(() => array);
            ThrowHelper.IfNullOrEmptyThenThrow(() => fileName);

            try
            {
                Logger.Trace($"Starting Save: \"{fileName}\"");

                // Write the binary file.
                SerializeBinary(array, fileName, callback);

                Logger.Trace("  Done saving.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }

        private void SerializeBinary<T>(T array, string filePath, ProgressChangedEventHandler callback)
        {
            //var serializer = new BinaryFormatter();

            // This "using" forces disposal of the Stream after it's done.
            // This will prevent the System.IO.IOException indicating that
            //   The process cannot access the file 'C:\MyPath\MyFile.bin' 
            //   because it is being used by another process.
            // This exception tends to show up when saving/reading frequently.
            //using (var writeFileStream = File.Create(fileName))
            //{
            //    serializer.Serialize(writeFileStream, array);
            //}

            // This "using" forces disposal of the Stream after it's done.
            // This will prevent the System.IO.IOException indicating that
            //   The process cannot access the file 'C:\MyPath\MyFile.bin' 
            //   because it is being used by another process.
            // This exception tends to show up when saving/reading frequently.
            using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var callbackStream = new ProgressStream(stream))
                {
                    callbackStream.BytesWritten += callback;

                    const int defaultBufferSize = 4096;
                    var size = Marshal.SizeOf(array);
                    var onePercentSize = (int) Math.Ceiling(size/100.0);

                    var serializer = new XmlSerializer(typeof (T));

                    using (var bufferedStream = new BufferedStream(callbackStream,
                        onePercentSize > defaultBufferSize
                            ? defaultBufferSize
                            : onePercentSize))
                    {
                        serializer.Serialize(bufferedStream, array);
                    }
                }
            }
        }
    }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class XmlFileWriter : FileWriter
    {
        //private readonly XmlSerializerNamespaces _blankNamespaces =
            //new XmlSerializerNamespaces(new[] {new XmlQualifiedName("", "")});

        private readonly XmlWriterSettings _defaultWriterSettings = new XmlWriterSettings
        {
            OmitXmlDeclaration = true,
            NewLineOnAttributes = false,
            NewLineHandling = NewLineHandling.Replace,
            Indent = true
        };

        private const int DefaultBufferSize = 4096;


        public XmlFileWriter(ILogger logger) : base(logger)
        {
        }

        public override bool Write<T>(T array, string filePath, ProgressChangedEventHandler callback)
        {
            ThrowHelper.IfNullThenThrow(() => array);
            ThrowHelper.IfNullOrEmptyThenThrow(() => filePath);

            IsCancelled = false;
            try
            {
                Logger.Trace($"Starting Save: \"{filePath}\"");

                using (var fileStream = new CushFileStream(filePath))
                {
                    WriteToStream(array, fileStream, callback);
                }
                
                Logger.Trace("  Done saving.");
                return true;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
        
        private void WriteToStream<T>(T array, Stream stream, ProgressChangedEventHandler callback)
        {
            ThrowHelper.IfNullThenThrow(()=>array);
            ThrowHelper.IfNullThenThrow(() => stream);
            
            using (var callbackStream = new ProgressStream(stream))
            {
                using (var objectStream = GetMemoryStreamFromObject(array))
                {
                    callbackStream.BytesMoved += callback;
                    var onePercentSize = (int)Math.Ceiling(objectStream.Length / 100.0);
                    
                    using (var bufferedStream = new BufferedStream(callbackStream,
                        onePercentSize > DefaultBufferSize ? DefaultBufferSize : onePercentSize))
                    {
                        try
                        {
                            Cancelled += (s, e) =>
                            {
                                IsCancelled = true;
                                bufferedStream?.Close();
                            }; 

                            var serializer = new XmlSerializer(array.GetType());
                            serializer.Serialize(bufferedStream, array);
                        }
                        catch (InvalidOperationException ex)
                        {
                            if (IsCancelled) return;
                            Logger.Error(ex);
                            throw;
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                            throw;
                        }
                    }
                }
            }
        }


        private Stream GetMemoryStreamFromObject<T>(T array)
        {
            ThrowHelper.IfNullThenThrow(() => array);
            var stream = new MemoryStream();
            try
            {
                using (var xmlWriter = XmlWriter.Create(stream, _defaultWriterSettings))
                {
                    var serializer = new XmlSerializer(array.GetType());
                    serializer.Serialize(xmlWriter, array);
                }
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
    }
}