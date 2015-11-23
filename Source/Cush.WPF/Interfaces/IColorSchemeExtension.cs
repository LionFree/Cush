﻿using System.ComponentModel.Composition;

namespace Cush.WPF.Interfaces
{
    /// <summary>
    ///     Provides the interface for an importable <see cref="IColorScheme"/> extension.
    /// </summary>
    public interface IColorSchemeExtension : IColorScheme, IPartImportsSatisfiedNotification
    {
    }
}