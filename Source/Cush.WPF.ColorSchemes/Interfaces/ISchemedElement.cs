using System.Diagnostics.CodeAnalysis;
using Cush.WPF.Interfaces;

namespace Cush.WPF.ColorSchemes
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface ISchemedElement : IResourceContainer
    {
        /// <summary>
        ///     Gets or sets the current ColorScheme.
        /// </summary>
        IColorScheme CurrentScheme { get; set; }
    }
}