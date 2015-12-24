using System;
using System.Diagnostics.CodeAnalysis;

namespace Cush.WPF.ColorSchemes
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class ChangedEventArgs<T> : EventArgs
    {
        public ChangedEventArgs(T oldResources, T newResources)
        {
            NewItem = newResources;
            OldItem = oldResources;
        }
        public T NewItem { get; }
        public T OldItem { get; }
    }
}