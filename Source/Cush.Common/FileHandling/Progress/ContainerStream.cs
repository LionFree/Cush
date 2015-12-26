using System.Diagnostics.CodeAnalysis;
using System.IO;
using Cush.Common.Exceptions;

namespace Cush.Common.Progress
{
    internal class ContainerStream : Stream
    {
        protected ContainerStream(Stream stream)
        {
            ThrowHelper.IfNullThenThrow(() => stream);
            ContainedStream = stream;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        protected Stream ContainedStream { get; }

        public override bool CanRead => ContainedStream.CanRead;

        public override bool CanSeek => ContainedStream.CanSeek;

        public override bool CanWrite => ContainedStream.CanWrite;
        public override long Length => ContainedStream.Length;

        public override long Position
        {
            get { return ContainedStream.Position; }
            set { ContainedStream.Position = value; }
        }

        public override void Flush()
        {
            ContainedStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return ContainedStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            ContainedStream.Write(buffer, offset, count);
        }

        public override void SetLength(long value)
        {
            ContainedStream.SetLength(value);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return ContainedStream.Seek(offset, origin);
        }
    }
}