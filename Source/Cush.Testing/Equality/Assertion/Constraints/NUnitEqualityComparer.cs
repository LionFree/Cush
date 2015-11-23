// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Cush.Testing.Assertion.Interfaces;

#if CLR_2_0 || CLR_4_0
using System.Collections.Generic;
#endif

namespace Cush.Testing.Assertion.Constraints
{
    /// <summary>
    ///     NUnitEqualityComparer encapsulates NUnit's handling of
    ///     equality tests between objects.
    /// </summary>
    [ExcludeFromCodeCoverage, DebuggerStepThrough]
    public class NUnitEqualityComparer : INUnitEqualityComparer
    {
        #region Static and Instance Fields

        /// <summary>
        ///     If true, all string comparisons will ignore case
        /// </summary>
        private bool _caseInsensitive;

        /// <summary>
        ///     If true, arrays will be treated as collections, allowing
        ///     those of different dimensions to be compared
        /// </summary>
        private bool _compareAsCollection;

        /// <summary>
        ///     Comparison objects used in comparisons for some constraints.
        /// </summary>
        private readonly EqualityAdapterList _externalComparers = new EqualityAdapterList();

        /// <summary>
        ///     List of points at which a failure occured.
        /// </summary>
        private FailurePointList _failurePoints;

        /// <summary>
        ///     RecursionDetector used to check for recursion when
        ///     evaluating self-referencing enumerables.
        /// </summary>
        private RecursionDetector _recursionDetector;

        //private static readonly int BUFFER_SIZE = 4096;

        #endregion

        #region Properties

        /// <summary>
        ///     Returns the default NUnitEqualityComparer
        /// </summary>
        public static NUnitEqualityComparer Default
        {
            get { return new NUnitEqualityComparer(); }
        }

        /// <summary>
        ///     Gets and sets a flag indicating whether case should
        ///     be ignored in determining equality.
        /// </summary>
        public bool IgnoreCase
        {
            get { return _caseInsensitive; }
            set { _caseInsensitive = value; }
        }

        /// <summary>
        ///     Gets and sets a flag indicating that arrays should be
        ///     compared as collections, without regard to their shape.
        /// </summary>
        public bool CompareAsCollection
        {
            get { return _compareAsCollection; }
            set { _compareAsCollection = value; }
        }

        /// <summary>
        ///     Gets the list of external comparers to be used to
        ///     test for equality. They are applied to members of
        ///     collections, in place of NUnit's own logic.
        /// </summary>
#if CLR_2_0 || CLR_4_0
        public IList<EqualityAdapter> ExternalComparers
#else
        public IList ExternalComparers
#endif
        {
            get { return _externalComparers; }
        }

        /// <summary>
        ///     Gets the list of failure points for the last Match performed.
        ///     The list consists of objects to be interpreted by the caller.
        ///     This generally means that the caller may only make use of
        ///     objects it has placed on the list at a particular depthy.
        /// </summary>
#if CLR_2_0 || CLR_4_0
        public IList<FailurePoint> FailurePoints
#else
        public IList FailurePoints
#endif
        {
            get { return _failurePoints; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Compares two objects for equality within a tolerance, setting
        ///     the tolerance to the actual tolerance used if an empty
        ///     tolerance is supplied.
        /// </summary>
        public bool AreEqual(object expected, object actual)
        {
            _failurePoints = new FailurePointList();
            _recursionDetector = new RecursionDetector();

            return ObjectsEqual(expected, actual);
        }

        #endregion

        #region Helper Methods

        private bool ObjectsEqual(object expected, object actual)
        {
            if (expected == null && actual == null)
                return true;

            if (expected == null || actual == null)
                return false;

            if (ReferenceEquals(expected, actual))
                return true;



            var externalComparer = GetExternalComparer(expected, actual);

            if (externalComparer != null)
                return externalComparer.AreEqual(expected, actual);

            if (expected is IEnumerable && actual is IEnumerable && !(expected is string && actual is string))
                return EnumerablesEqual((IEnumerable) expected, (IEnumerable) actual);

            if (expected is string && actual is string)
                return StringsEqual((string) expected, (string) actual);

            if (expected is char && actual is char)
                return CharsEqual((char) expected, (char) actual);

#if CLR_2_0 || CLR_4_0
            if (FirstImplementsIEquatableOfSecond(xType, yType))
                return InvokeFirstIEquatableEqualsSecond(expected, actual);
            else if (FirstImplementsIEquatableOfSecond(yType, xType))
                return InvokeFirstIEquatableEqualsSecond(actual, expected);
#endif

            return expected.Equals(actual);
        }

#if CLR_2_0 || CLR_4_0
    	private static bool FirstImplementsIEquatableOfSecond(Type first, Type second)
    	{
    		Type[] equatableArguments = GetEquatableGenericArguments(first);

    		foreach (Type xEquatableArgument in equatableArguments)
    			if (xEquatableArgument.Equals(second))
    				return true;

    		return false;
    	}

    	private static Type[] GetEquatableGenericArguments(Type type)
    	{
            foreach (Type @interface in type.GetInterfaces())
                if (@interface.IsGenericType && @interface.GetGenericTypeDefinition().Equals(typeof(IEquatable<>)))
                    return @interface.GetGenericArguments();

            return new Type[0];
    	}

    	private static bool InvokeFirstIEquatableEqualsSecond(object first, object second)
    	{
    		MethodInfo equals = typeof (IEquatable<>).MakeGenericType(second.GetType()).GetMethod("Equals");

    		return (bool) equals.Invoke(first, new object[] {second});
    	}
#endif

        private EqualityAdapter GetExternalComparer(object x, object y)
        {
            foreach (EqualityAdapter adapter in _externalComparers)
                if (adapter.CanCompare(x, y))
                    return adapter;

            return null;
        }

        private bool StringsEqual(string expected, string actual)
        {
            var s1 = _caseInsensitive ? expected.ToLower() : expected;
            var s2 = _caseInsensitive ? actual.ToLower() : actual;

            return s1.Equals(s2);
        }

        private bool CharsEqual(char x, char y)
        {
            var c1 = _caseInsensitive ? char.ToLower(x) : x;
            var c2 = _caseInsensitive ? char.ToLower(y) : y;

            return c1 == c2;
        }

        private bool EnumerablesEqual(IEnumerable expected, IEnumerable actual)
        {
            if (_recursionDetector.CheckRecursion(expected, actual))
                return false;

            var expectedEnum = expected.GetEnumerator();
            var actualEnum = actual.GetEnumerator();

            int count;
            for (count = 0;; count++)
            {
                var expectedHasData = expectedEnum.MoveNext();
                var actualHasData = actualEnum.MoveNext();

                if (!expectedHasData && !actualHasData)
                    return true;

                if (expectedHasData != actualHasData ||
                    !ObjectsEqual(expectedEnum.Current, actualEnum.Current))
                {
                    var fp = new FailurePoint {Position = count, ExpectedHasData = expectedHasData};
                    if (expectedHasData)
                        fp.ExpectedValue = expectedEnum.Current;
                    fp.ActualHasData = actualHasData;
                    if (actualHasData)
                        fp.ActualValue = actualEnum.Current;
                    _failurePoints.Insert(0, fp);
                    return false;
                }
            }
        }
        
        #endregion

        #region Nested RecursionDetector class

        /// <summary>
        ///     RecursionDetector detects when a comparison
        ///     between two enumerables has reached a point
        ///     where the same objects that were previously
        ///     compared are again being compared. This allows
        ///     the caller to stop the comparison if desired.
        /// </summary>
        [DebuggerStepThrough]
        private class RecursionDetector
        {
#if CLR_2_0 || CLR_4_0
            readonly Dictionary<UnorderedReferencePair, object> table = new Dictionary<UnorderedReferencePair, object>();
#else
            private readonly Hashtable _table = new Hashtable();
#endif

            /// <summary>
            ///     Check whether two objects have previously
            ///     been compared, returning true if they have.
            ///     The two objects are remembered, so that a
            ///     second call will always return true.
            /// </summary>
            public bool CheckRecursion(IEnumerable expected, IEnumerable actual)
            {
                var pair = new UnorderedReferencePair(expected, actual);

                if (ContainsPair(pair))
                    return true;

                _table.Add(pair, null);
                return false;
            }

            private bool ContainsPair(UnorderedReferencePair pair)
            {
#if CLR_2_0 || CLR_4_0
                return table.ContainsKey(pair);
#else
                return _table.Contains(pair);
#endif
            }

#if CLR_2_0 || CLR_4_0
            class UnorderedReferencePair : IEquatable<UnorderedReferencePair>
#else
            private class UnorderedReferencePair
#endif
            {
                private readonly object first;
                private readonly object second;

                public UnorderedReferencePair(object first, object second)
                {
                    this.first = first;
                    this.second = second;
                }

                public bool Equals(UnorderedReferencePair other)
                {
                    return (ReferenceEquals(first, other.first) && ReferenceEquals(second, other.second)) ||
                           (ReferenceEquals(first, other.second) && ReferenceEquals(second, other.first));
                }

                public override bool Equals(object obj)
                {
                    if (ReferenceEquals(null, obj)) return false;
                    return obj is UnorderedReferencePair && Equals((UnorderedReferencePair) obj);
                }

                public override int GetHashCode()
                {
                    unchecked
                    {
                        return ((first != null ? first.GetHashCode() : 0)*397) ^
                               ((second != null ? second.GetHashCode() : 0)*397);
                    }
                }
            }
        }

        #endregion
    }
}