using System;
using System.Diagnostics.CodeAnalysis;

namespace Cush.Common.FileHandling
{
    /// <summary>
    /// Specifies the action being taken on a file.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global"), Flags]
    public enum FileAction
    {
        None,
        Create,
        Import,
        Save,
        Open,
        Export,
        Delete
    }
}