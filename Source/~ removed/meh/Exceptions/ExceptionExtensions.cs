using System;

namespace Cush.Exceptions
{
    /// <summary>
    ///     Helper functions to unwrap <see cref="Exception" />s.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Unwraps an exception, showing the message for the exception and each inner exception.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string UnwrapException(this Exception ex)
        {
            var output = ex.Message;
            if (ex.InnerException == null) return output;

            output += Environment.NewLine;
            output += ex.InnerException.UnwrapException();
            return output;
        }
    }
}
