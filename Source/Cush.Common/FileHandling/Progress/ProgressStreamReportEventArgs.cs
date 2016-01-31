﻿using System;
// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Cush.Common.FileHandling.Progress
{
    /// <summary>
    ///     Contains the pertinent data for a ProgressStream Report event.
    /// </summary>
    public class ProgressStreamReportEventArgs : EventArgs
    {
        /// <summary>
        ///     Default constructor for ProgressStreamReportEventArgs.
        /// </summary>
        public ProgressStreamReportEventArgs()
        {
        }

        /// <summary>
        ///     Creates a new ProgressStreamReportEventArgs initializing its members.
        /// </summary>
        /// <param name="bytesMoved">The number of bytes that were read/written to/from the stream.</param>
        /// <param name="streamLength">The total length of the stream in bytes.</param>
        /// <param name="streamPosition">The current position in the stream.</param>
        /// <param name="wasRead">True if the bytes were read from the stream, false if they were written.</param>
        public ProgressStreamReportEventArgs(int bytesMoved, long streamLength, long streamPosition, bool wasRead)
            : this()
        {
            BytesMoved = bytesMoved;
            StreamLength = streamLength;
            StreamPosition = streamPosition;
            WasRead = WasRead;
        }

        /// <summary>
        ///     The number of bytes that were read/written to/from the stream.
        /// </summary>
        public int BytesMoved { get; private set; }

        /// <summary>
        ///     The total length of the stream in bytes.
        /// </summary>
        public long StreamLength { get; private set; }

        /// <summary>
        ///     The current position in the stream.
        /// </summary>
        public long StreamPosition { get; private set; }

        /// <summary>
        ///     True if the bytes were read from the stream, false if they were written.
        /// </summary>
        public bool WasRead { get; }
    }


    /// <summary>
    ///     The delegate for handling a ProgressStream Report event.
    /// </summary>
    /// <param name="sender">The object that raised the event, should be a ProgressStream.</param>
    /// <param name="args">The arguments raised with the event.</param>
    public delegate void ProgressStreamReportDelegate(object sender, ProgressStreamReportEventArgs args);
}