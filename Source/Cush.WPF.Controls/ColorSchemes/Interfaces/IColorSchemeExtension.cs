using System.ComponentModel.Composition;
// ReSharper disable CheckNamespace

namespace Cush.WPF.ColorSchemes
{
    /// <summary>
    ///     Provides the interface for an importable <see cref="IColorScheme"/> extension.
    /// </summary>
    public interface IColorSchemeExtension : IColorScheme, IPartImportsSatisfiedNotification
    {
    }
}