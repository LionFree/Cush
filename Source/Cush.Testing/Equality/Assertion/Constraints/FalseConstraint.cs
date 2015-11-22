// ****************************************************************
// Copyright 2008, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System.Diagnostics.CodeAnalysis;

namespace Cush.Testing.Assertion.Constraints
{
    /// <summary>
    ///     FalseConstraint tests that the actual value is false
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class FalseConstraint : BasicConstraint
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:FalseConstraint" /> class.
        /// </summary>
        internal FalseConstraint() : base(false, "False")
        {
        }
    }
}