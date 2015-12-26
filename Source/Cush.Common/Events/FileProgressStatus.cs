using System.Diagnostics.CodeAnalysis;

namespace Cush.Common
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum FileProgressStatus
    {
        Done = 0,
        Loading = 1,
        Saving = 2
    }
}