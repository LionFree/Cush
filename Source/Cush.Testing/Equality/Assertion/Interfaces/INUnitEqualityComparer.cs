// ****************************************************************
// Copyright 2012, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

namespace Cush.Testing.Assertion.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface INUnitEqualityComparer
    {
        /// <summary>
        /// Compares two objects for equality within a tolerance
        /// </summary>
        /// <param name="x">The first object to compare</param>
        /// <param name="y">The second object to compare</param>
        /// <returns></returns>
        bool AreEqual(object x, object y);
    }

#if CLR_2_0 || CLR_4_0
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INUnitEqualityComparer<T>
    {
        /// <summary>
        /// Compares two objects of a given Type for equality within a tolerance
        /// </summary>
        /// <param name="x">The first object to compare</param>
        /// <param name="y">The second object to compare</param>
        /// <returns></returns>
        bool AreEqual(T x, T y);
    }
#endif
}
