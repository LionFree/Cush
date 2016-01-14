using System.Diagnostics.CodeAnalysis;
// ReSharper disable UnusedMember.Global

namespace Cush.WPF.Controls.Native
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class NonCLSCompliantConstants
    {
        public const uint TPM_RETURNCMD = 0x0100;
        public const uint TPM_LEFTBUTTON = 0x0;
        public const uint SYSCOMMAND = 0x0112;
        public const int WM_NCLBUTTONDOWN = 0x00A1;
        public const int HT_CAPTION = 0x2;
    }
}