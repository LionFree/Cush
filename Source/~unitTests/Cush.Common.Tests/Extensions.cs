using System.Collections.Generic;

namespace Cush.Common.Tests
{
    public static class GAssert
    {
        //public static void AreEqual<T1, T2, T3, T4>(IDictionary<T1, T2> expected, IDictionary<T3, T4> actual)
        //{
        //    Assert.AreEqual(typeof (T1), typeof (T3), "Key types do not match.");
        //    Assert.AreEqual(typeof (T2), typeof (T4), "Value types do not match.");


        //    if (typeof(T1)==typeof(T3) && typeof(T2)==typeof(T4))
        //        AreEqual<T1,T2>(expected, actual);

        //}

        public static void AreEqual<T1, T2>(IDictionary<T1, T2> expected, IDictionary<T1, T2> actual)
        {
            NUnit.Framework.Assert.AreEqual(expected.Count, actual.Count, "Dictionaries are different lengths.");
            foreach (var item in expected)
            {
                NUnit.Framework.Assert.That(actual.ContainsKey(item.Key), "Dictionary keys do not match: {0}", item.Key.ToString());
                NUnit.Framework.Assert.AreEqual(expected[item.Key], actual[item.Key], "Dictionary values do not match.");
            }
        }
    }
}