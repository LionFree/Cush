﻿using System.Diagnostics.CodeAnalysis;

namespace Cush.Common.FileHandling
{
    /// <summary>
    /// Specifies the action being taken on a file.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum FileAction
    {
        Save,
        Open
    }
}