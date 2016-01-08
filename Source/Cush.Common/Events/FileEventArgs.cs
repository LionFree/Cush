using System;
using System.Diagnostics.CodeAnalysis;
using Cush.Common.FileHandling;

namespace Cush.Common
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class FileEventArgs : EventArgs
    {
        public int FileCount { get; set; }
        public int FileIndex { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public string Fullpath { get; set; }
        public bool Pinned { get; set; }
        public string TargetCommand { get; set; }
        public MRUEntry MRUEntry { get; set; }
    }
}
