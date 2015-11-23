// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System.Diagnostics.CodeAnalysis;

namespace Cush.Testing.Assertion.Constraints
{
    /// <summary>
    ///     TrueConstraint tests that the actual value is true
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class TrueConstraint : BasicConstraint
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:TrueConstraint" /> class.
        /// </summary>
        internal TrueConstraint() : base(true, "True")
        {
        }
    }
}