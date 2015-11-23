using System;

namespace Cush.Testing.RandomObjects
{
    internal sealed class LockedRandom : Random
    {
        private readonly object _lock = new object();

        public override int Next()
        {
            lock (_lock) return base.Next();
        }
    }
}