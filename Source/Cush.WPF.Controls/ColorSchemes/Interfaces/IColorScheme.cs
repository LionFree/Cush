using System;
using System.Diagnostics.CodeAnalysis;
using Cush.Common;
using Cush.WPF.Interfaces;

// ReSharper disable once CheckNamespace
namespace Cush.WPF.ColorSchemes
{
    /// <summary>
    ///     Provides the interface for a generalized container of color scheme resources.
    /// </summary>
    [SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
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

        /// <summary>
        ///     Gets the the default Base.
        /// </summary>
        IKeyedResourceContainer Base { get; set; }

        /// <summary>
        /// Called whenever the Base changes.
        /// </summary>
        event EventHandler<ChangedEventArgs<IKeyedResourceContainer>> BaseChanged;

        /// <summary>
        /// Called whenever the Theme changes.
        /// </summary>
        event EventHandler<ChangedEventArgs<IKeyedResourceContainer>> ThemeChanged;

        /// <summary>
        /// Called whenever the Accent changes.
        /// </summary>
        event EventHandler<ChangedEventArgs<IKeyedResourceContainer>> AccentChanged;
    }
}