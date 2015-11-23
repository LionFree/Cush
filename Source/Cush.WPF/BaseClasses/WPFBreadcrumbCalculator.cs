using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Cush.Common.Interaction;

namespace Cush.WPF
{
    internal class WPFBreadcrumbCalculator : BreadcrumbCalculator<Control>
    {
        protected override double GetChromeWidth(Control control)
        {
            return Math.Abs(control.ActualWidth - control.Width);
        }

        protected override double GetPaddingWidth(Control control)
        {
            return control.Padding.Left + control.Padding.Right;
        }

        protected override double GetFormattedTextWidth(Control control, string text)
        {
            var typeFace = new Typeface(control.FontFamily, control.FontStyle, control.FontWeight,
                control.FontStretch);

            var formattedText = new FormattedText(text,
                CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
                typeFace,
                control.FontSize, control.Foreground);

            return formattedText.Width;
        }
    }
}
