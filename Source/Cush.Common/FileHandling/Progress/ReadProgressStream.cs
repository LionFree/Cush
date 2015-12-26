using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Cush.Common.Progress
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal class ReadProgressStream : ContainerStream
    {
        private int _lastProgress;

        public ReadProgressStream(Stream stream)
            : base(stream)
        {
            if (!stream.CanRead || !stream.CanSeek || stream.Length <= 0)
                throw new ArgumentException(nameof(stream));
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var amountRead = base.Read(buffer, offset, count);
            if (ProgressChanged == null) return amountRead;

            var newProgress = (int) (Position*100/Length);

            if (newProgress <= _lastProgress) return amountRead;
            _lastProgress = newProgress;

            ProgressChanged?.Invoke(this, new ProgressChangedEventArgs(_lastProgress, null));
            return amountRead;
        }

        [SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
        public event ProgressChangedEventHandler ProgressChanged;
    }
}