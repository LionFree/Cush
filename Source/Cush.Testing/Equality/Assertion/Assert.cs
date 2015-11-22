// ****************************************************************
// Copyright 2009, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Cush.Testing.Assertion.Constraints;
using Cush.Testing.Assertion.Exceptions;

namespace Cush.Testing.Assertion
{
    /// <summary>
    ///     Delegate used by tests that execute code and
    ///     capture any thrown exception.
    /// </summary>
    internal delegate void TestDelegate();

    /// <summary>
    ///     The Assert class contains a collection of static methods that
    ///     implement the most common assertions used in NUnit.
    /// </summary>
    [ExcludeFromCodeCoverage, DebuggerStepThrough]
    internal class Assert
    {
        #region Constructor

        /// <summary>
        ///     We don't actually want any instances of this object, but some people
        ///     like to inherit from it to add other static methods. Hence, the
        ///     protected constructor disallows any instances of this object.
        /// </summary>
        protected Assert()
        {
        }

        #endregion

        #region Assert.That

        /// <summary>
        ///     Apply a constraint to an actual value, succeeding if the constraint
        ///     is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="actual">The actual value to test</param>
        /// <param name="expression">A Constraint expression to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        internal static void That(object actual, IResolveConstraint expression, string message, params object[] args)
        {
            var constraint = expression.Resolve();

            if (!constraint.Matches(actual))
            {
                MessageWriter writer = new TextMessageWriter(message, args);
                constraint.WriteMessageTo(writer);
                throw new AssertionException(writer.ToString());
            }
        }

        #region Boolean

        /// <summary>
        ///     Asserts that a condition is true. If the condition is false the method throws
        ///     an <see cref="AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display if the condition is false</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        internal static void That(bool condition, string message, params object[] args)
        {
            That(condition, Is.True, message, args);
        }

        /// <summary>
        ///     Asserts that a condition is true. If the condition is false the method throws
        ///     an <see cref="AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display if the condition is false</param>
        internal static void That(bool condition, string message)
        {
            That(condition, Is.True, message, null);
        }

        /// <summary>
        ///     Asserts that a condition is true. If the condition is false the method throws
        ///     an <see cref="AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        internal static void That(bool condition)
        {
            That(condition, Is.True, null, null);
        }

        #endregion

        #region ref Boolean

#if !CLR_2_0 && !CLR_4_0
        /// <summary>
        ///     Apply a constraint to a referenced boolean, succeeding if the constraint
        ///     is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="actual">The actual value to test</param>
        /// <param name="constraint">A Constraint to be applied</param>
        internal static void That(ref bool actual, IResolveConstraint constraint)
        {
            That(ref actual, constraint.Resolve(), null, null);
        }

        /// <summary>
        ///     Apply a constraint to a referenced value, succeeding if the constraint
        ///     is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="actual">The actual value to test</param>
        /// <param name="constraint">A Constraint to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        internal static void That(ref bool actual, IResolveConstraint constraint, string message)
        {
            That(ref actual, constraint.Resolve(), message, null);
        }

        /// <summary>
        ///     Apply a constraint to a referenced value, succeeding if the constraint
        ///     is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="actual">The actual value to test</param>
        /// <param name="expression">A Constraint expression to be applied</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        internal static void That(ref bool actual, IResolveConstraint expression, string message, params object[] args)
        {
            var constraint = expression.Resolve();

            //IncrementAssertCount();
            if (!constraint.Matches(ref actual))
            {
                MessageWriter writer = new TextMessageWriter(message, args);
                constraint.WriteMessageTo(writer);
                throw new AssertionException(writer.ToString());
            }
        }
#endif

        #endregion

        #endregion

        #region False

        /// <summary>
        ///     Asserts that a condition is false. If the condition is true the method throws
        ///     an <see cref="AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display in case of failure</param>
        /// <param name="args">Array of objects to be used in formatting the message</param>
        internal static void False(bool condition, string message, params object[] args)
        {
            That(condition, Is.False, message, args);
        }

        /// <summary>
        ///     Asserts that a condition is false. If the condition is true the method throws
        ///     an <see cref="AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display in case of failure</param>
        internal static void False(bool condition, string message)
        {
            That(condition, Is.False, message, null);
        }

        /// <summary>
        ///     Asserts that a condition is false. If the condition is true the method throws
        ///     an <see cref="AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        internal static void False(bool condition)
        {
            That(condition, Is.False, null, null);
        }

        /// <summary>
        ///     Asserts that a condition is false. If the condition is true the method throws
        ///     an <see cref="AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display in case of failure</param>
        /// <param name="args">Array of objects to be used in formatting the message</param>
        internal static void IsFalse(bool condition, string message, params object[] args)
        {
            That(condition, Is.False, message, args);
        }

        /// <summary>
        ///     Asserts that a condition is false. If the condition is true the method throws
        ///     an <see cref="AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display in case of failure</param>
        internal static void IsFalse(bool condition, string message)
        {
            That(condition, Is.False, message, null);
        }

        /// <summary>
        ///     Asserts that a condition is false. If the condition is true the method throws
        ///     an <see cref="AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        internal static void IsFalse(bool condition)
        {
            That(condition, Is.False, null, null);
        }

        #endregion

        #region AreEqual

        #region Objects

        /// <summary>
        ///     Verifies that two objects are equal.  Two objects are considered
        ///     equal if both are null, or if both have the same value. NUnit
        ///     has special semantics for some object types.
        ///     If they are not equal an <see cref="AssertionException" /> is thrown.
        /// </summary>
        /// <param name="expected">The value that is expected</param>
        /// <param name="actual">The actual value</param>
        /// <param name="message">The message to display in case of failure</param>
        /// <param name="args">Array of objects to be used in formatting the message</param>
        public static void AreEqual(object expected, object actual, string message, params object[] args)
        {
            That(actual, Is.EqualTo(expected), message, args);
        }

        /// <summary>
        ///     Verifies that two objects are equal.  Two objects are considered
        ///     equal if both are null, or if both have the same value. NUnit
        ///     has special semantics for some object types.
        ///     If they are not equal an <see cref="AssertionException" /> is thrown.
        /// </summary>
        /// <param name="expected">The value that is expected</param>
        /// <param name="actual">The actual value</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void AreEqual(object expected, object actual, string message)
        {
            That(actual, Is.EqualTo(expected), message, null);
        }

        /// <summary>
        ///     Verifies that two objects are equal.  Two objects are considered
        ///     equal if both are null, or if both have the same value. NUnit
        ///     has special semantics for some object types.
        ///     If they are not equal an <see cref="AssertionException" /> is thrown.
        /// </summary>
        /// <param name="expected">The value that is expected</param>
        /// <param name="actual">The actual value</param>
        public static void AreEqual(object expected, object actual)
        {
            That(actual, Is.EqualTo(expected), null, null);
        }

        #endregion

        #endregion
    }
}