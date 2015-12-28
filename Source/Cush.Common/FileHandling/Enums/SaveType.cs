using System.Diagnostics.CodeAnalysis;

namespace Cush.Common.FileHandling
{
    /// <summary>
    /// Specifies whether the save is a regular Save or a Save-As.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum SaveType
    {
        Save,
        SaveAs
    }
}
