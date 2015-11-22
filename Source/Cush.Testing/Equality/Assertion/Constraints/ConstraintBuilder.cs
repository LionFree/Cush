// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Cush.Testing.Assertion.Constraints.Operators;

#if CLR_2_0 || CLR_4_0
using System.Collections.Generic;
#endif

namespace Cush.Testing.Assertion.Constraints
{
    /// <summary>
    ///     ConstraintBuilder maintains the stacks that are used in
    ///     processing a ConstraintExpression. An OperatorStack
    ///     is used to hold operators that are waiting for their
    ///     operands to be reognized. a ConstraintStack holds
    ///     input constraints as well as the results of each
    ///     operator applied.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class ConstraintBuilder
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:ConstraintBuilder" /> class.
        /// </summary>
        internal ConstraintBuilder()
        {
            _ops = new OperatorStack(this);
            _constraints = new ConstraintStack(this);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether this instance is resolvable.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is resolvable; otherwise, <c>false</c>.
        /// </value>
        internal bool IsResolvable
        {
            get { return _lastPushed is Constraint || _lastPushed is SelfResolvingOperator; }
        }

        #endregion

        #region Nested Operator Stack Class

        /// <summary>
        ///     OperatorStack is a type-safe stack for holding ConstraintOperators
        /// </summary>
        internal class OperatorStack
        {
            private readonly ConstraintBuilder _builder;
#if CLR_2_0 || CLR_4_0
            private Stack<ConstraintOperator> stack = new Stack<ConstraintOperator>();
#else
            private readonly Stack _stack = new Stack();
#endif

            /// <summary>
            ///     Initializes a new instance of the <see cref="T:OperatorStack" /> class.
            /// </summary>
            /// <param name="builder">The builder.</param>
            internal OperatorStack(ConstraintBuilder builder)
            {
                _builder = builder;
            }

            /// <summary>
            ///     Gets a value indicating whether this <see cref="T:OpStack" /> is empty.
            /// </summary>
            /// <value><c>true</c> if empty; otherwise, <c>false</c>.</value>
            internal bool Empty
            {
                get { return _stack.Count == 0; }
            }

            /// <summary>
            ///     Gets the topmost operator without modifying the stack.
            /// </summary>
            /// <value>The top.</value>
            internal ConstraintOperator Top
            {
                get { return (ConstraintOperator) _stack.Peek(); }
            }

            /// <summary>
            ///     Pushes the specified operator onto the stack.
            /// </summary>
            /// <param name="op">The op.</param>
            internal void Push(ConstraintOperator op)
            {
                _stack.Push(op);
            }

            /// <summary>
            ///     Pops the topmost operator from the stack.
            /// </summary>
            /// <returns></returns>
            internal ConstraintOperator Pop()
            {
                return (ConstraintOperator) _stack.Pop();
            }
        }

        #endregion

        #region Nested Constraint Stack Class

        /// <summary>
        ///     ConstraintStack is a type-safe stack for holding Constraints
        /// </summary>
        internal class ConstraintStack
        {
#if CLR_2_0 || CLR_4_0
            private Stack<Constraint> stack = new Stack<Constraint>();
#else
            private readonly Stack _stack = new Stack();
#endif
            private readonly ConstraintBuilder _builder;

            /// <summary>
            ///     Initializes a new instance of the <see cref="T:ConstraintStack" /> class.
            /// </summary>
            /// <param name="builder">The builder.</param>
            internal ConstraintStack(ConstraintBuilder builder)
            {
                _builder = builder;
            }

            /// <summary>
            ///     Gets a value indicating whether this <see cref="T:ConstraintStack" /> is empty.
            /// </summary>
            /// <value><c>true</c> if empty; otherwise, <c>false</c>.</value>
            internal bool Empty
            {
                get { return _stack.Count == 0; }
            }

            /// <summary>
            ///     Gets the topmost constraint without modifying the stack.
            /// </summary>
            /// <value>The topmost constraint</value>
            internal Constraint Top
            {
                get { return (Constraint) _stack.Peek(); }
            }

            /// <summary>
            ///     Pushes the specified constraint. As a side effect,
            ///     the constraint's builder field is set to the
            ///     ConstraintBuilder owning this stack.
            /// </summary>
            /// <param name="constraint">The constraint.</param>
            internal void Push(Constraint constraint)
            {
                _stack.Push(constraint);
                constraint.SetBuilder(_builder);
            }

            /// <summary>
            ///     Pops this topmost constrait from the stack.
            ///     As a side effect, the constraint's builder
            ///     field is set to null.
            /// </summary>
            /// <returns></returns>
            internal Constraint Pop()
            {
                var constraint = (Constraint) _stack.Pop();
                constraint.SetBuilder(null);
                return constraint;
            }
        }

        #endregion

        #region Instance Fields

        private readonly OperatorStack _ops;

        private readonly ConstraintStack _constraints;

        private object _lastPushed;

        #endregion

        #region internal Methods

        /// <summary>
        ///     Appends the specified operator to the expression by first
        ///     reducing the operator stack and then pushing the new
        ///     operator on the stack.
        /// </summary>
        /// <param name="op">The operator to push.</param>
        internal void Append(ConstraintOperator op)
        {
            op.LeftContext = _lastPushed;
            if (_lastPushed is ConstraintOperator)
                SetTopOperatorRightContext(op);

            // Reduce any lower precedence operators
            ReduceOperatorStack(op.LeftPrecedence);

            _ops.Push(op);
            _lastPushed = op;
        }

        /// <summary>
        ///     Appends the specified constraint to the expresson by pushing
        ///     it on the constraint stack.
        /// </summary>
        /// <param name="constraint">The constraint to push.</param>
        internal void Append(Constraint constraint)
        {
            if (_lastPushed is ConstraintOperator)
                SetTopOperatorRightContext(constraint);

            _constraints.Push(constraint);
            _lastPushed = constraint;
            constraint.SetBuilder(this);
        }

        /// <summary>
        ///     Sets the top operator right context.
        /// </summary>
        /// <param name="rightContext">The right context.</param>
        private void SetTopOperatorRightContext(object rightContext)
        {
            // Some operators change their precedence based on
            // the right context - save current precedence.
            var oldPrecedence = _ops.Top.LeftPrecedence;

            _ops.Top.RightContext = rightContext;

            // If the precedence increased, we may be able to
            // reduce the region of the stack below the operator
            if (_ops.Top.LeftPrecedence > oldPrecedence)
            {
                var changedOp = _ops.Pop();
                ReduceOperatorStack(changedOp.LeftPrecedence);
                _ops.Push(changedOp);
            }
        }

        /// <summary>
        ///     Reduces the operator stack until the topmost item
        ///     precedence is greater than or equal to the target precedence.
        /// </summary>
        /// <param name="targetPrecedence">The target precedence.</param>
        private void ReduceOperatorStack(int targetPrecedence)
        {
            while (!_ops.Empty && _ops.Top.RightPrecedence < targetPrecedence)
                _ops.Pop().Reduce(_constraints);
        }

        /// <summary>
        ///     Resolves this instance, returning a Constraint. If the builder
        ///     is not currently in a resolvable state, an exception is thrown.
        /// </summary>
        /// <returns>The resolved constraint</returns>
        internal Constraint Resolve()
        {
            if (!IsResolvable)
                throw new InvalidOperationException("A partial expression may not be resolved");

            while (!_ops.Empty)
            {
                var op = _ops.Pop();
                op.Reduce(_constraints);
            }

            return _constraints.Pop();
        }

        #endregion
    }
}