using Cush.Common.Exceptions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Cush.Common.Helpers
{
    [DebuggerStepThrough]
    public static class Expressions
    {
        /// <summary>
        ///     Gets a property name from an expression.
        ///     For example, when passed the property expression <c>()=>IsEnabled</c>,
        ///     will return "IsEnabled".
        /// </summary>
        /// <typeparam name="T">The type of the expression.</typeparam>
        /// <param name="expression">The property expression from which to get the name.</param>
        /// <returns>The name of the property expression.</returns>
        public static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            var body = expression.Body as MemberExpression;
            if (body != null)
            {
                return body.Member.Name;
            }

            var op = ((UnaryExpression) expression.Body).Operand;
            return ((MemberExpression) op).Member.Name;
        }

        /// <summary>
        ///     Gets an array of property name from an array of expressions.
        ///     For example, when passed the property expression <c>()=>IsEnabled</c>,
        ///     will return "IsEnabled".
        /// </summary>
        /// <param name="expressions">The property expressions from which to get the names.</param>
        /// <returns>The names of the property expressions.</returns>
        public static string[] GetPropertyNames(Expression<Func<object>>[] expressions)
        {
            if (null == expressions) ThrowHelper.ThrowArgumentNullException(() => expressions);
            return expressions.Select(GetPropertyName).ToArray();
        }
    }
}