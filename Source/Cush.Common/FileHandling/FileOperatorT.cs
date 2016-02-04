using System;
using System.ComponentModel;
using System.IO;
using Cush.Common.Exceptions;
using Cush.Common.Logging;
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Cush.Common.FileHandling
{
    public abstract class FileOperator<T>
    {
        public int BufferSize = 4096;

        protected readonly ILogger Logger;

        protected FileOperator(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        ///     Reads a file into an object of Type T.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        /// <typeparam name="T">The type into which the reader will store the data.</typeparam>
        /// <param name="filePath">The path of the file to read.</param>
        /// <param name="callback">A <see cref="ProgressChangedEventHandler" /> callback that will update, e.g., a progress bar.</param>
        /// <returns>A data object of type T containing the data from the file.</returns>
        public T Read(string filePath, ProgressChangedEventHandler callback = null)
        {
            ThrowHelper.IfNullOrEmptyThenThrow(() => filePath);
            IsCancelled = false;
            Cancelled += (s, e) => { throw new OperationCanceledException(); };
            try
            {
                Logger.Trace($"Reading file: {filePath}");

                // Read the binary file.
                var input = Deserialize(filePath, callback);

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

        /// <summary>
        ///     Serializes the specified item to disk.
        /// </summary>
        /// <param name="array">The object to serialize.</param>
        /// <param name="filePath">The path of the file to write.</param>
        /// <param name="callback">A <see cref="ProgressChangedEventHandler" /> callback that will update, e.g., a progress bar.</param>
        /// <returns></returns>
        public bool Write(T array, string filePath, ProgressChangedEventHandler callback = null)
        {
            ThrowHelper.IfNullThenThrow(() => array);
            ThrowHelper.IfNullOrEmptyThenThrow(() => filePath);

            try
            {
                Logger.Trace($"Starting Save: \"{filePath}\"");

                // Write the binary file.
                Serialize(array, filePath, callback);

                Logger.Trace("  Done saving.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }
        
        /// <summary>
        ///     Serializes the specified item to disk.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="filePath">The path of the file to write.</param>
        /// <param name="callback">A <see cref="ProgressChangedEventHandler" /> callback that will update, e.g., a progress bar.</param>
        /// <returns></returns>
        protected abstract void Serialize(T obj, string filePath, ProgressChangedEventHandler callback);

        /// <summary>
        ///     Deserializes a file into an object of Type T.
        /// </summary>
        /// <exception cref="ArgumentNullException" />
        /// <typeparam name="T">The type into which the reader will store the data.</typeparam>
        /// <param name="filePath">The path of the file to read.</param>
        /// <param name="callback">A <see cref="ProgressChangedEventHandler" /> callback that will update, e.g., a progress bar.</param>
        /// <returns>A data object of type T containing the data from the file.</returns>
        protected abstract T Deserialize(string filePath, ProgressChangedEventHandler callback);

        public bool IsCancelled { get; set; }

        public event EventHandler Cancelled;

        internal void CancelFileOperations(object sender, EventArgs e)
        {
            Cancelled?.Invoke(sender, e);
        }

        public event ProgressChangedEventHandler ProgressChanged;

        protected void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }
    }
}