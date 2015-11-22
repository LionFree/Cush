using System;

namespace Cush.Common.Interaction
{
    public abstract class BreadcrumbCalculator<TControl> : IBreadcrumbCalculator where TControl : class
    {
        protected TControl CreateAndValidateControl(Type controlType)
        {
            var control = Activator.CreateInstance(controlType) as TControl;
            if (null == control)
                ThrowHelper.ThrowArgumentException(() => controlType, "controlType must be a System.Windows.Control.");
            return control;
        }

        public double StripPadding(Type controlType, double width)
        {
            var control = CreateAndValidateControl(controlType);

            var chrome = GetChromeWidth(control);
            var padding = GetPaddingWidth(control);
            
            var stuffToRemove = padding + chrome;
            return width - stuffToRemove;
        }

        public bool StringFitsWithinControl(Type controlType, string text, double width)
        {
            var control = CreateAndValidateControl(controlType);

            var textWidth = GetFormattedTextWidth(control, text);
           
            return (textWidth < width);
        }

        protected abstract double GetChromeWidth(TControl control);
        protected abstract double GetPaddingWidth(TControl control);
        protected abstract double GetFormattedTextWidth(TControl control, string text);

    }
}
