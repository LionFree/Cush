using System.Windows.Forms;
using Cush.Common.Interaction;

namespace Cush.Windows.Forms
{
    internal class FormsBreadcrumbCalculator : BreadcrumbCalculator<Control>
    {
        protected override double GetChromeWidth(Control control)
        {
            var screenClientRect = control.RectangleToScreen(control.ClientRectangle);

            var leftBorderWidth = screenClientRect.Left - control.Left;
            var rightBorderWidth = control.Right - screenClientRect.Right;

            return leftBorderWidth + rightBorderWidth;
        }

        protected override double GetPaddingWidth(Control control)
        {
            return control.Padding.Left + control.Padding.Right;
        }

        protected override double GetFormattedTextWidth(Control control, string text)
        {
            var stringSize = TextRenderer.MeasureText(text, control.Font);
            return stringSize.Width;
        }
    }
}