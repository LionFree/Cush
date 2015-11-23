// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;

#if CLR_2_0 || CLR_4_0
using System.Collections.Generic;
#endif

namespace Cush.Testing.Assertion.Constraints
{
    /// <summary>
    ///     EqualConstraint is able to compare an actual value with the
    ///     expected value provided in its constructor. Two objects are
    ///     considered equal if both are null, or if both have the same
    ///     value. NUnit has special semantics for some object types.
    /// </summary>
    [ExcludeFromCodeCoverage, DebuggerStepThrough]
    internal class EqualConstraint : Constraint
    {
        #region Static and Instance Fields

        private readonly object _expected;
        
        /// <summary>
        ///     If true, strings in error messages will be clipped
        /// </summary>
        private bool _clipStrings = true;

        /// <summary>
        ///     NUnitEqualityComparer used to test equality.
        /// </summary>
        private readonly NUnitEqualityComparer _comparer = new NUnitEqualityComparer();

        #region Message Strings

        private static readonly string StringsDiffer_1 =
            "String lengths are both {0}. Strings differ at index {1}.";

        private static readonly string StringsDiffer_2 =
            "Expected string length {0} but was {1}. Strings differ at index {2}.";

        private static readonly string StreamsDiffer_1 =
            "Stream lengths are both {0}. Streams differ at offset {1}.";

        private static readonly string StreamsDiffer_2 =
            "Expected Stream length {0} but was {1}."; // Streams differ at offset {2}.";

        private static readonly string CollectionType_1 =
            "Expected and actual are both {0}";

        private static readonly string CollectionType_2 =
            "Expected is {0}, actual is {1}";

        private static readonly string ValuesDiffer_1 =
            "Values differ at index {0}";

        private static readonly string ValuesDiffer_2 =
            "Values differ at expected index {0}, actual index {1}";

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="EqualConstraint" /> class.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        internal EqualConstraint(object expected)
            : base(expected)
        {
            this._expected = expected;
        }

        #endregion

        #region Constraint Modifiers

        /// <summary>
        ///     Flag the constraint to ignore case and return self.
        /// </summary>
        internal EqualConstraint IgnoreCase
        {
            get
            {
                _comparer.IgnoreCase = true;
                return this;
            }
        }

        /// <summary>
        ///     Flag the constraint to suppress string clipping
        ///     and return self.
        /// </summary>
        internal EqualConstraint NoClip
        {
            get
            {
                _clipStrings = false;
                return this;
            }
        }

        /// <summary>
        ///     Flag the constraint to compare arrays as collections
        ///     and return self.
        /// </summary>
        internal EqualConstraint AsCollection
        {
            get
            {
                _comparer.CompareAsCollection = true;
                return this;
            }
        }

        ///// <summary>
        ///// Flag the constraint to use a tolerance when determining equality.
        ///// </summary>
        ///// <param name="amount">Tolerance value to be used</param>
        ///// <returns>Self.</returns>
        //internal EqualConstraint Within(object amount)
        //{
        //    if (!tolerance.IsEmpty)
        //        throw new InvalidOperationException("Within modifier may appear only once in a constraint expression");

        //    tolerance = new Tolerance(amount);
        //    return this;
        //}

        ///// <summary>
        ///// Switches the .Within() modifier to interpret its tolerance as
        ///// a distance in representable values (see remarks).
        ///// </summary>
        ///// <returns>Self.</returns>
        ///// <remarks>
        ///// Ulp stands for "unit in the last place" and describes the minimum
        ///// amount a given value can change. For any integers, an ulp is 1 whole
        ///// digit. For floating point values, the accuracy of which is better
        ///// for smaller numbers and worse for larger numbers, an ulp depends
        ///// on the size of the number. Using ulps for comparison of floating
        ///// point results instead of fixed tolerances is safer because it will
        ///// automatically compensate for the added inaccuracy of larger numbers.
        ///// </remarks>
        //internal EqualConstraint Ulps
        //{
        //    get
        //    {
        //        tolerance = tolerance.Ulps;
        //        return this;
        //    }
        //}

        ///// <summary>
        ///// Switches the .Within() modifier to interpret its tolerance as
        ///// a percentage that the actual values is allowed to deviate from
        ///// the expected value.
        ///// </summary>
        ///// <returns>Self</returns>
        //internal EqualConstraint Percent
        //{
        //    get
        //    {
        //        tolerance = tolerance.Percent;
        //        return this;
        //    }
        //}

        ///// <summary>
        ///// Causes the tolerance to be interpreted as a TimeSpan in days.
        ///// </summary>
        ///// <returns>Self</returns>
        //internal EqualConstraint Days
        //{
        //    get
        //    {
        //        tolerance = tolerance.Days;
        //        return this;
        //    }
        //}

        ///// <summary>
        ///// Causes the tolerance to be interpreted as a TimeSpan in hours.
        ///// </summary>
        ///// <returns>Self</returns>
        //internal EqualConstraint Hours
        //{
        //    get
        //    {
        //        tolerance = tolerance.Hours;
        //        return this;
        //    }
        //}

        ///// <summary>
        ///// Causes the tolerance to be interpreted as a TimeSpan in minutes.
        ///// </summary>
        ///// <returns>Self</returns>
        //internal EqualConstraint Minutes
        //{
        //    get
        //    {
        //        tolerance = tolerance.Minutes;
        //        return this;
        //    }
        //}

        ///// <summary>
        ///// Causes the tolerance to be interpreted as a TimeSpan in seconds.
        ///// </summary>
        ///// <returns>Self</returns>
        //internal EqualConstraint Seconds
        //{
        //    get
        //    {
        //        tolerance = tolerance.Seconds;
        //        return this;
        //    }
        //}

        ///// <summary>
        ///// Causes the tolerance to be interpreted as a TimeSpan in milliseconds.
        ///// </summary>
        ///// <returns>Self</returns>
        //internal EqualConstraint Milliseconds
        //{
        //    get
        //    {
        //        tolerance = tolerance.Milliseconds;
        //        return this;
        //    }
        //}

        ///// <summary>
        ///// Causes the tolerance to be interpreted as a TimeSpan in clock ticks.
        ///// </summary>
        ///// <returns>Self</returns>
        //internal EqualConstraint Ticks
        //{
        //    get
        //    {
        //        tolerance = tolerance.Ticks;
        //        return this;
        //    }
        //}

        /// <summary>
        ///     Flag the constraint to use the supplied IComparer object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        [Obsolete("Replace with 'Using'")]
        internal EqualConstraint Comparer(IComparer comparer)
        {
            return Using(comparer);
        }

        /// <summary>
        ///     Flag the constraint to use the supplied IComparer object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        internal EqualConstraint Using(IComparer comparer)
        {
            this._comparer.ExternalComparers.Add(EqualityAdapter.For(comparer));
            return this;
        }

#if CLR_2_0 || CLR_4_0
    /// <summary>
    /// Flag the constraint to use the supplied IComparer object.
    /// </summary>
    /// <param name="comparer">The IComparer object to use.</param>
    /// <returns>Self.</returns>
        internal EqualConstraint Using<T>(IComparer<T> comparer)
        {
            this.comparer.ExternalComparers.Add(EqualityAdapter.For( comparer ));
            return this;
        }

                /// <summary>
        /// Flag the constraint to use the supplied Comparison object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        internal EqualConstraint Using<T>(Comparison<T> comparer)
        {
            this.comparer.ExternalComparers.Add(EqualityAdapter.For( comparer ));
            return this;
        }

        /// <summary>
        /// Flag the constraint to use the supplied IEqualityComparer object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        internal EqualConstraint Using(IEqualityComparer comparer)
        {
            this.comparer.ExternalComparers.Add(EqualityAdapter.For(comparer));
            return this;
        }

        /// <summary>
        /// Flag the constraint to use the supplied IEqualityComparer object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        internal EqualConstraint Using<T>(IEqualityComparer<T> comparer)
        {
            this.comparer.ExternalComparers.Add(EqualityAdapter.For(comparer));
            return this;
        }
#endif

        #endregion

        #region internal Methods

        /// <summary>
        ///     Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        internal override bool Matches(object actual)
        {
            Actual = actual;

            return _comparer.AreEqual(_expected, actual);
        }

        /// <summary>
        ///     Write a failure message. Overridden to provide custom
        ///     failure messages for EqualConstraint.
        /// </summary>
        /// <param name="writer">The MessageWriter to write to</param>
        internal override void WriteMessageTo(MessageWriter writer)
        {
            DisplayDifferences(writer, _expected, Actual, 0);
        }


        /// <summary>
        ///     Write description of this constraint
        /// </summary>
        /// <param name="writer">The MessageWriter to write to</param>
        internal override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WriteExpectedValue(_expected);

            if (_comparer.IgnoreCase)
                writer.WriteModifier("ignoring case");
        }

        private void DisplayDifferences(MessageWriter writer, object expected, object actual, int depth)
        {
            if (expected is string && actual is string)
                DisplayStringDifferences(writer, (string) expected, (string) actual);
            else if (expected is ICollection && actual is ICollection)
                DisplayCollectionDifferences(writer, (ICollection) expected, (ICollection) actual, depth);
            else if (expected is IEnumerable && actual is IEnumerable)
                DisplayEnumerableDifferences(writer, (IEnumerable) expected, (IEnumerable) actual, depth);
            else if (expected is Stream && actual is Stream)
                DisplayStreamDifferences(writer, (Stream) expected, (Stream) actual, depth);
            else
                writer.DisplayDifferences(expected, actual);
        }

        #endregion

        #region DisplayStringDifferences

        private void DisplayStringDifferences(MessageWriter writer, string expected, string actual)
        {
            var mismatch = MsgUtils.FindMismatchPosition(expected, actual, 0, _comparer.IgnoreCase);

            if (expected.Length == actual.Length)
                writer.WriteMessageLine(StringsDiffer_1, expected.Length, mismatch);
            else
                writer.WriteMessageLine(StringsDiffer_2, expected.Length, actual.Length, mismatch);

            writer.DisplayStringDifferences(expected, actual, mismatch, _comparer.IgnoreCase, _clipStrings);
        }

        #endregion

        #region DisplayStreamDifferences

        private void DisplayStreamDifferences(MessageWriter writer, Stream expected, Stream actual, int depth)
        {
            if (expected.Length == actual.Length)
            {
                var fp = (FailurePoint) _comparer.FailurePoints[depth];
                long offset = fp.Position;
                writer.WriteMessageLine(StreamsDiffer_1, expected.Length, offset);
            }
            else
                writer.WriteMessageLine(StreamsDiffer_2, expected.Length, actual.Length);
        }

        #endregion

        #region DisplayCollectionDifferences

        /// <summary>
        ///     Display the failure information for two collections that did not match.
        /// </summary>
        /// <param name="writer">The MessageWriter on which to display</param>
        /// <param name="expected">The expected collection.</param>
        /// <param name="actual">The actual collection</param>
        /// <param name="depth">The depth of this failure in a set of nested collections</param>
        private void DisplayCollectionDifferences(MessageWriter writer, ICollection expected, ICollection actual,
            int depth)
        {
            DisplayTypesAndSizes(writer, expected, actual, depth);

            if (_comparer.FailurePoints.Count > depth)
            {
                var failurePoint = (FailurePoint) _comparer.FailurePoints[depth];

                DisplayFailurePoint(writer, expected, actual, failurePoint, depth);

                if (failurePoint.ExpectedHasData && failurePoint.ActualHasData)
                    DisplayDifferences(
                        writer,
                        failurePoint.ExpectedValue,
                        failurePoint.ActualValue,
                        ++depth);
                else if (failurePoint.ActualHasData)
                {
                    writer.Write("  Extra:    ");
                    writer.WriteCollectionElements(actual, failurePoint.Position, 3);
                }
                else
                {
                    writer.Write("  Missing:  ");
                    writer.WriteCollectionElements(expected, failurePoint.Position, 3);
                }
            }
        }

        /// <summary>
        ///     Displays a single line showing the types and sizes of the expected
        ///     and actual enumerations, collections or arrays. If both are identical,
        ///     the value is only shown once.
        /// </summary>
        /// <param name="writer">The MessageWriter on which to display</param>
        /// <param name="expected">The expected collection or array</param>
        /// <param name="actual">The actual collection or array</param>
        /// <param name="indent">The indentation level for the message line</param>
        private void DisplayTypesAndSizes(MessageWriter writer, IEnumerable expected, IEnumerable actual, int indent)
        {
            var sExpected = MsgUtils.GetTypeRepresentation(expected);
            if (expected is ICollection && !(expected is Array))
                sExpected += string.Format(" with {0} elements", ((ICollection) expected).Count);

            var sActual = MsgUtils.GetTypeRepresentation(actual);
            if (actual is ICollection && !(actual is Array))
                sActual += string.Format(" with {0} elements", ((ICollection) actual).Count);

            if (sExpected == sActual)
                writer.WriteMessageLine(indent, CollectionType_1, sExpected);
            else
                writer.WriteMessageLine(indent, CollectionType_2, sExpected, sActual);
        }

        /// <summary>
        ///     Displays a single line showing the point in the expected and actual
        ///     arrays at which the comparison failed. If the arrays have different
        ///     structures or dimensions, both values are shown.
        /// </summary>
        /// <param name="writer">The MessageWriter on which to display</param>
        /// <param name="expected">The expected array</param>
        /// <param name="actual">The actual array</param>
        /// <param name="failurePoint">Index of the failure point in the underlying collections</param>
        /// <param name="indent">The indentation level for the message line</param>
        private void DisplayFailurePoint(MessageWriter writer, IEnumerable expected, IEnumerable actual,
            FailurePoint failurePoint, int indent)
        {
            var expectedArray = expected as Array;
            var actualArray = actual as Array;

            var expectedRank = expectedArray != null ? expectedArray.Rank : 1;
            var actualRank = actualArray != null ? actualArray.Rank : 1;

            var useOneIndex = expectedRank == actualRank;

            if (expectedArray != null && actualArray != null)
                for (var r = 1; r < expectedRank && useOneIndex; r++)
                    if (expectedArray.GetLength(r) != actualArray.GetLength(r))
                        useOneIndex = false;

            var expectedIndices = MsgUtils.GetArrayIndicesFromCollectionIndex(expected, failurePoint.Position);
            if (useOneIndex)
            {
                writer.WriteMessageLine(indent, ValuesDiffer_1, MsgUtils.GetArrayIndicesAsString(expectedIndices));
            }
            else
            {
                var actualIndices = MsgUtils.GetArrayIndicesFromCollectionIndex(actual, failurePoint.Position);
                writer.WriteMessageLine(indent, ValuesDiffer_2,
                    MsgUtils.GetArrayIndicesAsString(expectedIndices), MsgUtils.GetArrayIndicesAsString(actualIndices));
            }
        }

        private static object GetValueFromCollection(ICollection collection, int index)
        {
            var array = collection as Array;

            if (array != null && array.Rank > 1)
                return array.GetValue(MsgUtils.GetArrayIndicesFromCollectionIndex(array, index));

            if (collection is IList)
                return ((IList) collection)[index];

            foreach (var obj in collection)
                if (--index < 0)
                    return obj;

            return null;
        }

        #endregion

        #region DisplayEnumerableDifferences

        /// <summary>
        ///     Display the failure information for two IEnumerables that did not match.
        /// </summary>
        /// <param name="writer">The MessageWriter on which to display</param>
        /// <param name="expected">The expected enumeration.</param>
        /// <param name="actual">The actual enumeration</param>
        /// <param name="depth">The depth of this failure in a set of nested collections</param>
        private void DisplayEnumerableDifferences(MessageWriter writer, IEnumerable expected, IEnumerable actual,
            int depth)
        {
            DisplayTypesAndSizes(writer, expected, actual, depth);

            if (_comparer.FailurePoints.Count > depth)
            {
                var failurePoint = (FailurePoint) _comparer.FailurePoints[depth];

                DisplayFailurePoint(writer, expected, actual, failurePoint, depth);

                if (failurePoint.ExpectedHasData && failurePoint.ActualHasData)
                    DisplayDifferences(
                        writer,
                        failurePoint.ExpectedValue,
                        failurePoint.ActualValue,
                        ++depth);
                //else if (failurePoint.ActualHasData)
                //{
                //    writer.Write("  Extra:    ");
                //    writer.WriteCollectionElements(actual, failurePoint.Position, 3);
                //}
                //else
                //{
                //    writer.Write("  Missing:  ");
                //    writer.WriteCollectionElements(expected, failurePoint.Position, 3);
                //}
            }
        }

        #endregion
    }
}