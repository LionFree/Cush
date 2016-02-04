using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Cush.Common.Exceptions;
using Cush.Common.FileHandling.Streams;
using Cush.Common.Logging;

namespace Cush.Common.FileHandling
{
    public sealed class XmlFileOperator : FileOperator
    {
        public XmlFileOperator(ILogger logger) : base(logger)
        {
        }

        /// <summary>
        ///     Serializes the specified item to disk.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="filePath">The path of the file to write.</param>
        /// <param name="callback">A <see cref="ProgressChangedEventHandler" /> callback that will update, e.g., a progress bar.</param>
        /// <returns></returns>
        protected override void Serialize<T>(T obj, string filePath, ProgressChangedEventHandler callback)
        {
            ThrowHelper.IfNullThenThrow(() => obj);
            ThrowHelper.IfNullOrEmptyThenThrow(() => filePath);

            using (var fileStream = new CushFileStream(filePath))
            {
                using (var callbackStream = new ProgressStream(fileStream))
                {
                    using (var objectStream = GetMemoryStreamFromObject(obj))
                    {
                        callbackStream.BytesMoved += callback;
                        callbackStream.BytesMoved += OnProgressChanged;

                        var onePercentSize = (int) Math.Ceiling(objectStream.Length/100.0);

                        using (var bufferedStream = new BufferedStream(callbackStream,
                            onePercentSize > BufferSize ? BufferSize : onePercentSize))
                        {
                            try
                            {
                                Cancelled += (s, e) =>
                                {
                                    IsCancelled = true;
                                    bufferedStream?.Close();
                                };

                                var serializer = new XmlSerializer(obj.GetType());
                                serializer.Serialize(bufferedStream, obj);
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
        }
        
        /// <summary>
        ///     Deserializes a file into an object of Type T.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        /// <typeparam name="T">The type into which the reader will store the data.</typeparam>
        /// <param name="filePath">The path of the file to read.</param>
        /// <param name="callback">A <see cref="ProgressChangedEventHandler" /> callback that will update, e.g., a progress bar.</param>
        /// <returns>A data object of type T containing the data from the file.</returns>
        protected override T Deserialize<T>(string filePath, ProgressChangedEventHandler callback)
        {
            ThrowHelper.IfNullOrEmptyThenThrow(() => filePath);

            using (var fileStream = new CushFileStream(filePath))
            {
                using (var callbackStream = new ProgressStream(fileStream))
                {
                    callbackStream.BytesRead += callback;
                    callbackStream.BytesRead += OnProgressChanged;

                    var onePercentSize = (int) Math.Ceiling(fileStream.Length/100.0);

                    using (var bufferedStream = new BufferedStream(callbackStream,
                        onePercentSize > BufferSize
                            ? BufferSize
                            : onePercentSize))
                    {
                        try
                        {
                            Cancelled += (s, e) =>
                            {
                                IsCancelled = true;
                                bufferedStream?.Close();
                            };

                            var serializer = new XmlSerializer(typeof (T));
                            var input = serializer.Deserialize(bufferedStream);
                            return (T) input;
                        }
                        catch (InvalidOperationException ex)
                        {
                            if (IsCancelled) return default(T);
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

        

        private Stream GetMemoryStreamFromObject<T>(T obj)
        {
            ThrowHelper.IfNullThenThrow(() => obj);
            var stream = new MemoryStream();
            try
            {
                using (
                    var xmlWriter = XmlWriter.Create(stream, new XmlWriterSettings
                    {
                        OmitXmlDeclaration = true,
                        NewLineOnAttributes = false,
                        NewLineHandling = NewLineHandling.Replace,
                        Indent = true
                    }))
                {
                    var serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(xmlWriter, obj);
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