using System;

namespace Cush.Common
{
    /// <summary>
    ///     Provides the interface for a globally-unique object.
    /// </summary>
    public interface IKeyedItem
    {
        /// <summary>
        ///     Gets or sets the globally-unique identifier of this object.
        /// </summary>
        Guid Guid { get; }
        
        /// <summary>
        ///     Gets or sets the displayname of this object.
        /// </summary>
        string DisplayName { get; }
    }
}