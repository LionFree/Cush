using System;
using System.Diagnostics.CodeAnalysis;

namespace Cush.Common.Helpers
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class EventExtensions
    {
        public static void Raise<TArgs>(this EventHandler<TArgs> handler, object sender, TArgs args)
        {
            var innerHandler = handler;
            innerHandler?.Invoke(sender, args);
        }

        public static void Raise<TArgs>(this EventHandler<TArgs> handler, TArgs args)
        {
            Raise(handler, null, args);
        }
    }
}