// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System.Diagnostics.CodeAnalysis;

namespace Cush.Testing.Assertion.Constraints
{
    /// <summary>
    ///     BasicConstraint is the abstract base for constraints that
    ///     perform a simple comparison to a constant value.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal abstract class BasicConstraint : Constraint
    {
        private readonly string _description;
        private readonly object _expected;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:BasicConstraint" /> class.
        /// </summary>
        /// <param name="expected">The expected.</param>
        /// <param name="description">The description.</param>
        protected BasicConstraint(object expected, string description)
        {
            _expected = expected;
            _description = description;
        }

        /// <summary>
        ///     Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        internal override bool Matches(object actual)
        {
            Actual = actual;

            if (actual == null && _expected == null)
                return true;

            if (actual == null || _expected == null)
                return false;

            return _expected.Equals(actual);
        }

        /// <summary>
        ///     Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        internal override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.Write(_description);
        }
    }
}