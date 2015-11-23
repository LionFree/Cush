using System;

namespace Cush.Common
{
    public static class RandomExtensions
    {

        #region Random.OneOf("John", "Ringo", "Paul")

        public static T OneOf<T>(this Random rng, params T[] things)
        {
            return things[rng.Next(things.Length)];
        }

        #endregion


    }
}