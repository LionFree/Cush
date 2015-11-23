using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime;

namespace Cush.Common.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed class __DynamicallyInvokableAttribute : Attribute
    {
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public __DynamicallyInvokableAttribute()
        {
        }
    }
}