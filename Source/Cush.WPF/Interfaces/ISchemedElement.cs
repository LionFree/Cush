using System;
using System.Diagnostics.CodeAnalysis;
using Cush.WPF.ColorSchemes;

namespace Cush.WPF.Interfaces
{
    [SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface ISchemedElement : IResourceContainer
    {
        /// <summary>
        ///     Gets or sets the current ColorScheme.
        /// </summary>
        IColorScheme CurrentScheme { get; set; }

        /// <summary>
        ///     Occurs after the CurrentScheme changes.
        /// </summary>
        event EventHandler<ChangedEventArgs<IColorScheme>> SchemeChanged;
    }
}