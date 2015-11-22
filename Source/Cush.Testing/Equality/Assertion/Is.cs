// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************


using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Cush.Testing.Assertion.Constraints;

namespace Cush.Testing.Assertion
{
    /// <summary>
    ///     Helper class with properties and methods that supply
    ///     a number of constraints used in Asserts.
    /// </summary>
    [ExcludeFromCodeCoverage, DebuggerStepThrough]
    internal class Is
    {
        #region True

        /// <summary>
        ///     Returns a constraint that tests for True
        /// </summary>
        internal static TrueConstraint True
        {
            get { return new TrueConstraint(); }
        }

        #endregion

        #region False

        /// <summary>
        ///     Returns a constraint that tests for False
        /// </summary>
        internal static FalseConstraint False
        {
            get { return new FalseConstraint(); }
        }

        #endregion

        #region EqualTo

        /// <summary>
        ///     Returns a constraint that tests two items for equality
        /// </summary>
        public static EqualConstraint EqualTo(object expected)
        {
            return new EqualConstraint(expected);
        }

        #endregion
    }
}