using System;
using System.Diagnostics.CodeAnalysis;

namespace Cush.Common
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal class FileEventArgs : EventArgs
    {
        public string Description { get; set; }
        public string Filename { get; set; }
        public string Fullpath { get; set; }
        public bool Pinned { get; set; }
        public string TargetCommand { get; set; }
    }
}
