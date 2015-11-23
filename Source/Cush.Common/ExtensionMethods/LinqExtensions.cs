using System;
using System.Linq;

namespace Cush.Common
{
    public static class LinqExtensions
    {
        #region variable.In(list)

        public static bool IsIn<T>(this T source, params T[] list)
        {
            if (null == source) throw new ArgumentNullException("source");
            return list.Contains(source);
        }

        // Allows you to replace this:
        //
        //        if(reallyLongIntegerVariableName == 1 || 
        //          reallyLongIntegerVariableName == 6 || 
        //          reallyLongIntegerVariableName == 9 || 
        //          reallyLongIntegerVariableName == 11)
        //      {
        //          // do something....
        //      }
        //
        // With this:
        //
        //      if(reallyLongIntegerVariableName.IsIn(1,6,9,11))
        //      {
        //         // do something....
        //      }

        #endregion
    }
}