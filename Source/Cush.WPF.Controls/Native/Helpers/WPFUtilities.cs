using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace Cush.WPF.Controls.Native.Helpers
{
    internal static class WPFUtilities
    {
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static bool IsNonNegative(this Thickness thickness)
        {
            if (!thickness.Top.IsFiniteAndNonNegative())
            {
                return false;
            }

            if (!thickness.Left.IsFiniteAndNonNegative())
            {
                return false;
            }

            if (!thickness.Bottom.IsFiniteAndNonNegative())
            {
                return false;
            }

            if (!thickness.Right.IsFiniteAndNonNegative())
            {
                return false;
            }

            return true;
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static bool IsValid(this CornerRadius cornerRadius)
        {
            if (!cornerRadius.TopLeft.IsFiniteAndNonNegative())
            {
                return false;
            }

            if (!cornerRadius.TopRight.IsFiniteAndNonNegative())
            {
                return false;
            }

            if (!cornerRadius.BottomLeft.IsFiniteAndNonNegative())
            {
                return false;
            }

            if (!cornerRadius.BottomRight.IsFiniteAndNonNegative())
            {
                return false;
            }

            return true;
        }
    }
}