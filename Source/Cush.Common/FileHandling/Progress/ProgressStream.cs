using System.ComponentModel;
using System.IO;
using Cush.Common.Exceptions;
// ReSharper disable EventNeverSubscribedTo.Global
// ReSharper disable VirtualMemberNeverOverriden.Global
// ReSharper disable MemberCanBeProtected.Global

namespace Cush.Common.FileHandling.Progress
{
    /// <summary>
    ///     Wraps another stream and provides reporting for when bytes are read or written to the stream.
    /// </summary>
    public class ProgressStream : Stream
    {
        private readonly Stream _innerStream;

        /// <summary>
        ///     Creates a new ProgressStream supplying the stream for it to report on.
        /// </summary>
        /// <param name="stream">The underlying stream that will be reported on when bytes are read or written.</param>
        public ProgressStream(Stream stream)
        {
            ThrowHelper.IfNullThenThrow(() => stream);
            _innerStream = stream;
        }

        public override bool CanRead => _innerStream.CanRead;

        public override bool CanSeek => _innerStream.CanSeek;

        public override bool CanWrite => _innerStream.CanWrite;

        public override long Length => _innerStream.Length;

        public override long Position
        {
            get { return _innerStream.Position; }
            set { _innerStream.Position = value; }
        }

        /// <summary>
        ///     Raised when bytes are read from the stream.
        /// </summary>
        public event ProgressChangedEventHandler BytesRead;

        /// <summary>
        ///     Raised when bytes are written to the stream.
        /// </summary>
        public event ProgressChangedEventHandler BytesWritten;

        /// <summary>
        ///     Raised when bytes are either read or written to the stream.
        /// </summary>
        public event ProgressChangedEventHandler BytesMoved;

        protected virtual void OnBytesRead(int bytesMoved)
        {
            //if (BytesRead == null) return;
            //var args = new ProgressStreamReportEventArgs(bytesMoved, _innerStream.Length, _innerStream.Position, true);
            //var a2 = new ProgressChangedEventArgs()
            BytesRead?.Invoke(this, new ProgressChangedEventArgs((int)(Position * 100 / Length), true));
        }

        protected virtual void OnBytesWritten(int bytesMoved)
        {
            //if (BytesWritten == null) return;
            //var args = new ProgressStreamReportEventArgs(bytesMoved, _innerStream.Length, _innerStream.Position, false);
            //BytesWritten(this, args);
            BytesWritten?.Invoke(this, new ProgressChangedEventArgs((int)(Position * 100 / Length), false));
        }

        protected virtual void OnBytesMoved(int bytesMoved, bool isRead)
        {
            //if (BytesMoved == null) return;
            //var args = new ProgressStreamReportEventArgs(bytesMoved, _innerStream.Length, _innerStream.Position, isRead);
            //BytesMoved(this, args);
            BytesMoved?.Invoke(this, new ProgressChangedEventArgs((int)(Position * 100 / Length), isRead));
        }

        public override void Flush()
        {
            _innerStream.Flush();
        }


        public override int Read(byte[] buffer, int offset, int count)
        {
            var bytesRead = _innerStream.Read(buffer, offset, count);

            OnBytesRead(bytesRead);
            OnBytesMoved(bytesRead, true);

            return bytesRead;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _innerStream.Write(buffer, offset, count);

            OnBytesWritten(count);
            OnBytesMoved(count, false);
        }

        public override void SetLength(long value)
        {
            _innerStream.SetLength(value);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _innerStream.Seek(offset, origin);
        }

        public override void Close()
        {
            _innerStream.Close();
            base.Close();
        }
    }
}