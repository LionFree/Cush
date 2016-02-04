using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Cush.Common.FileHandling.Streams;
using Cush.Common.Logging;

namespace Cush.Common.FileHandling
{
    public sealed class BinaryFileOperator : FileOperator
    {
        private readonly BinaryFormatter _formatter = new BinaryFormatter();

        public BinaryFileOperator(ILogger logger) : base(logger)
        {
        }

        private MemoryStream SerializeToMemoryStream<T>(T obj)
        {
            var stream = new MemoryStream();
            return BinarySerializeToStream(obj, stream);
        }

        private object DeserializeFromStream(MemoryStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var o = _formatter.Deserialize(stream);
            return o;
        }

        private TStream BinarySerializeToStream<T, TStream>(T obj, TStream stream) where TStream : Stream
        {
            _formatter.Serialize(stream, obj);
            return stream;
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
            using (var fileStream = new CushFileStream(filePath))
            {
                using (var callbackStream = new ProgressStream(fileStream))
                {
                    using (var objectStream = SerializeToMemoryStream(obj))
                    {
                        callbackStream.BytesMoved += callback;
                        callbackStream.BytesMoved += OnProgressChanged;

                        var onePercentSize = (int) Math.Ceiling(objectStream.Length/100.0);

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

                                BinarySerializeToStream(obj, bufferedStream);
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
            T input;

            using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var callbackStream = new ProgressStream(stream))
                {
                    callbackStream.BytesRead += callback;
                    callbackStream.BytesRead += OnProgressChanged;

                    const int defaultBufferSize = 4096;
                    var onePercentSize = (int) Math.Ceiling(stream.Length/100.0);

                    using (var bufferedStream = new BufferedStream(callbackStream,
                        onePercentSize > defaultBufferSize
                            ? defaultBufferSize
                            : onePercentSize))
                    {
                        input = (T) _formatter.Deserialize(bufferedStream);
                    }
                }
            }

            return input;
        }
    }
}