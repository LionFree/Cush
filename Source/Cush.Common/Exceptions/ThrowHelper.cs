using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using Cush.Common.Helpers;
using JetBrains.Annotations;

namespace Cush.Common.Exceptions
{
    // <STRIP>
    // From Microsoft's internal System.ThrowHelper:
    //
    // This file defines an internal class used to throw exceptions in BCL code.
    // The main purpose is to reduce code size. 
    // 
    // The old way to throw an exception generates quite a lot IL code and assembly code.
    // Following is an example:
    //     C# source
    //          throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
    //     IL code:
    //          IL_0003:  ldstr      "key"
    //          IL_0008:  ldstr      "ArgumentNull_Key"
    //          IL_000d:  call       string System.Environment::GetResourceString(string)
    //          IL_0012:  newobj     instance void System.ArgumentNullException::.ctor(string,string)
    //          IL_0017:  throw
    //    which is 21bytes in IL.
    // 
    // So we want to get rid of the ldstr and call to Environment.GetResource in IL.
    // In order to do that, I created two enums: ExceptionResource, ExceptionArgument to represent the
    // argument name and resource name in a small integer. The source code will be changed to 
    //    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key, ExceptionResource.ArgumentNull_Key);
    //
    // The IL code will be 7 bytes.
    //    IL_0008:  ldc.i4.4
    //    IL_0009:  ldc.i4.4
    //    IL_000a:  call       void System.ThrowHelper::ThrowArgumentNullException(valuetype System.ExceptionArgument)
    //    IL_000f:  ldarg.0
    //
    // This will also reduce the Jitted code size a lot. 
    //
    // It is very important we do this for generic classes because we can easily generate the same code 
    // multiple times for different instantiation. 
    // 
    // ========================
    // Jit will generate the code to throw exception at the end of a method, thus we can reduce working
    // set if the user code will never throw an exception. However Jit doesn't know anything about the
    // methods in ThrowHelper, so it will not moves the instructions to the end. 
    // This is not a problem for ngened code because we will probably move the code based on profile data(hopefully.) 
    //
    // For jitted code, it doesn't make much difference. The only advantage of moving the code to the end is to 
    // improve cache locality. This doesn't make much different on newer processor like P4.
    // </STRIP>

    [DebuggerStepThrough]
    [Pure]
    public static class ThrowHelper
    {
        // Allow nulls for reference types and Nullable<U>, but not for value types.
        [AssertionMethod]
        public static void IfNullAndNullsAreIllegalThenThrow<T>(Expression<Func<T>> propertyExpression,
            Type expectedType)
        {
            var value = propertyExpression.Compile().Invoke();
            if (value != null || TypeHelper.DefaultIsNull(expectedType)) return;
            throw new ArgumentNullException(Expressions.GetPropertyName(propertyExpression));
        }

        // Allow nulls for reference types and Nullable<U>, but not for value types.
        [AssertionMethod]
        public static void IfNullAndNullsAreIllegalThenThrow<T>(object value, string argName)
        {
            // Note that default(T) is not equal to null for value types except when T is Nullable<U>. 
            if (value != null || default(T) == null) return;
            throw new ArgumentNullException(argName);
        }

        [AssertionMethod]
        public static void IfNullThenThrow<T>(Expression<Func<T>> propertyExpression)
        {
            var value = propertyExpression.Compile().Invoke();
            if (value != null) return;
            throw new ArgumentNullException(Expressions.GetPropertyName(propertyExpression));
        }
        
        [AssertionMethod]
        public static void IfNullThenThrowArgumentException<T>(Expression<Func<T>> propertyExpression, string message)
        {
            var value = propertyExpression.Compile().Invoke();
            if (value != null) return;
            throw new ArgumentException(message, Expressions.GetPropertyName(propertyExpression));
        }

        [AssertionMethod]
        public static void IfNullOrEmptyThenThrow(Expression<Func<string>> propertyExpression)
        {
            var value = propertyExpression.Compile().Invoke();
            if (!string.IsNullOrEmpty(value)) return;
            throw new ArgumentException(Strings.EXCEPTION_ParameterCannotBeNullOrEmpty,
                Expressions.GetPropertyName(propertyExpression));
        }

        [AssertionMethod]
        public static void IfNullOrEmptyThenThrow(Expression<Func<string>> propertyExpression, string message)
        {
            var value = propertyExpression.Compile().Invoke();
            if (!string.IsNullOrEmpty(value)) return;
            ThrowArgumentException(propertyExpression, message);
        }

        [ContractAnnotation("=> halt")]
        public static void ThrowArgumentException<T>(Expression<Func<T>> propertyExpression, string message)
        {
            var propertyName = Expressions.GetPropertyName(propertyExpression);
            throw new ArgumentException(message, propertyName);
        }

        [ContractAnnotation("=> halt")]
        public static void ThrowWrongValueTypeArgumentException(string argName, object value, Type targetType)
        {
            throw new ArgumentException(string.Format(Strings.EXCEPTION_WrongType, value, targetType, argName));
        }

        [ContractAnnotation("=> halt")]
        public static void ThrowWrongValueTypeArgumentException<T>(Expression<Func<T>> argumentExpression,
            Type targetType)
        {
            var value = argumentExpression.Compile().Invoke();
            throw new ArgumentException(string.Format(Strings.EXCEPTION_WrongType, value, targetType,
                Expressions.GetPropertyName(argumentExpression)));
            //ThrowWrongValueTypeArgumentException(Expressions.GetPropertyName(argumentExpression), value, targetType);
        }

        [ContractAnnotation("=> halt")]
        public static void ThrowArgumentNullException(string argName)
        {
            throw new ArgumentNullException(argName);
        }

        [ContractAnnotation("=> halt")]
        public static void ThrowArgumentNullException<T>(Expression<Func<T>> propertyExpression)
        {
            throw new ArgumentNullException(Expressions.GetPropertyName(propertyExpression));
        }


        [ContractAnnotation("=> halt")]
        public static void ThrowArgumentOutOfRangeException<T>(Expression<Func<T>> expression)
        {
            throw new ArgumentOutOfRangeException(Expressions.GetPropertyName(expression));
        }

        [ContractAnnotation("=> halt")]
        public static void ThrowArgumentOutOfRangeException<T>(Expression<Func<T>> argumentExpression, string message)
        {
            throw new ArgumentOutOfRangeException(Expressions.GetPropertyName(argumentExpression), message);
        }

        [ContractAnnotation("=> halt")]
        public static void ThrowInvalidUriException<T>(Expression<Func<T>> argumentExpression)
        {
            throw new ArgumentException(Strings.Error_InvalidURI,
                Expressions.GetPropertyName(argumentExpression));
        }

        [ContractAnnotation("=> halt")]
        public static void ThrowUriDoesNotExistException<T>(Expression<Func<T>> argumentExpression, bool isLocation)
        {
            var message = isLocation ? Strings.Error_LocationDNE : Strings.Error_ResourceDNE;
            throw new ArgumentException(message, Expressions.GetPropertyName(argumentExpression));
        }

        [ContractAnnotation("=> halt")]
        public static void ThrowPathDoesNotExistException<T>(Expression<Func<T>> argumentExpression, bool isFolder)
        {
            var message = isFolder ? Strings.EXCEPTION_FolderDNE : Strings.EXCEPTION_FileDNE;
            throw new ArgumentException(message, Expressions.GetPropertyName(argumentExpression));
        }

        [ContractAnnotation("=> halt")]
        public static void ThrowArgumentOutOfRangeException(string argName, string message)
        {
            throw new ArgumentOutOfRangeException(argName, message);
        }

        [ContractAnnotation("=> halt")]
        public static void ThrowInvalidOperationException(string message)
        {
            throw new InvalidOperationException(message);
        }

        internal static class TypeHelper
        {
            internal static bool DefaultIsNull(Type type)
            {
                return !type.IsValueType || (Activator.CreateInstance(type) == null);
            }
        }
    }
}