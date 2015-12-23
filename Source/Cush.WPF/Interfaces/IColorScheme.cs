using System;
using System.Diagnostics.CodeAnalysis;
using Cush.Common;
using Cush.WPF.ColorSchemes;

namespace Cush.WPF.Interfaces
{
    /// <summary>
    ///     Provides the interface for a generalized container of color scheme resources.
    /// </summary>
    [SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
    public interface IColorScheme : IKeyedItem
    {
        /// <summary>
        ///     Gets the the default Theme.
        /// </summary>
        IKeyedResourceContainer Theme { get; set; }

        /// <summary>
        ///     Gets the the default Accent.
        /// </summary>
        IKeyedResourceContainer Accent { get; set; }

        event EventHandler<ChangedEventArgs<IKeyedResourceContainer>> ThemeChanged;
        event EventHandler<ChangedEventArgs<IKeyedResourceContainer>> AccentChanged;
    }
}