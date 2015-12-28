using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml.Serialization;
using Cush.Common.Exceptions;
using Cush.Common.FileHandling.Progress;
using Cush.Common.Logging;

namespace Cush.Common.FileHandling
{
    public abstract class FileReader
    {
        protected bool Cancelled = false;
        public bool IsCancelled => Cancelled;
        protected readonly ILogger Logger;

        protected FileReader(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        ///     Reads a file into an object of Type T.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        /// <typeparam name="T">The type into which the reader will store the data.</typeparam>
        /// <param name="filePath">The path of the file to read.</param>
        /// <param name="callback"></param>
        /// <returns>A data object of type T containing the data from the file.</returns>
        public abstract T Read<T>(string filePath, ProgressChangedEventHandler callback = null);

        internal event EventHandler CancelFileOperation;

        internal void CancelFileOperations(object sender, EventArgs e)
        {
            CancelFileOperation?.Invoke(sender, e);
        }
    }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class BinaryFileReader : FileReader
    {
        public BinaryFileReader(ILogger logger) : base(logger)
        {
        }

        public override T Read<T>(string fileName, ProgressChangedEventHandler callback = null)
        {
            ThrowHelper.IfNullOrEmptyThenThrow(() => fileName);
            Cancelled = false;
            CancelFileOperation += (s, e) => { throw new OperationCanceledException(); };
            try
            { 
                Logger.Trace($"Reading file: {fileName}");

                // Read the binary file.
                var input = DeserializeBinary<T>(fileName, callback);

                Logger.Trace("  Done reading.");
                return input;
            }
            catch (FileNotFoundException ex)
            {
                Logger.Error(ex);
                return default(T);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        private T DeserializeBinary<T>(string filePath, ProgressChangedEventHandler callback)
        {
            //CancelFileOperation += (s, e) => { throw new OperationCanceledException(); };

            T input;

            // This "using" forces disposal of the Stream after it's done.
            // This will prevent the System.IO.IOException indicating that
            //   The process cannot access the file 'C:\MyPath\MyFile.bin' 
            //   because it is being used by another process.
            // This exception tends to show up when saving/reading frequently.
            using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var callbackStream = new ReadProgressStream(stream))
                {
                    callbackStream.ProgressChanged += callback;

                    const int defaultBufferSize = 4096;
                    var onePercentSize = (int) Math.Ceiling(stream.Length/100.0);

                    var serializer = new XmlSerializer(typeof (T));

                    using (var bufferedStream = new BufferedStream(callbackStream,
                        onePercentSize > defaultBufferSize
                            ? defaultBufferSize
                            : onePercentSize))
                    {
                        input = (T) serializer.Deserialize(bufferedStream);
                    }
                }
            }

            return input;
        }
    }


    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public sealed class XmlFileReader : FileReader
    {
        public XmlFileReader(ILogger logger) : base(logger)
        {
        }

        public override T Read<T>(string filePath, ProgressChangedEventHandler callback = null)
        {
            ThrowHelper.IfNullOrEmptyThenThrow(() => filePath);
            Cancelled = false;
            try
            {
                Logger.Trace($"Reading file: {filePath}");

                T input;
                using (var reader = new StreamReader(filePath))
                {
                    input = DeserializeXml<T>(reader.BaseStream, callback);
                }

                Logger.Trace("  Done reading.");
                return input;
            }
            catch (OperationCanceledException)
            {
                return default(T);
            }
            catch (FileNotFoundException ex)
            {
                Logger.Error(ex);
                return default(T);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        private T DeserializeXml<T>(Stream stream, ProgressChangedEventHandler callback)
        {
            ThrowHelper.IfNullThenThrow(() => stream);
            using (var callbackStream = new ReadProgressStream(stream))
            {
                callbackStream.ProgressChanged += callback;

                const int defaultBufferSize = 4096;
                var onePercentSize = (int) Math.Ceiling(stream.Length/100.0);

                var serializer = new XmlSerializer(typeof (T));

                using (var bufferedStream = new BufferedStream(callbackStream,
                    onePercentSize > defaultBufferSize
                        ? defaultBufferSize
                        : onePercentSize))
                {
                    try
                    {
                        CancelFileOperation += (s, e) =>
                        {
                            Cancelled = true;
                            bufferedStream?.Close();
                        };

                        var input = serializer.Deserialize(bufferedStream);
                        return (T) input;
                    }
                    catch (InvalidOperationException ex)
                    {
                        if (Cancelled) return default(T);
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
}