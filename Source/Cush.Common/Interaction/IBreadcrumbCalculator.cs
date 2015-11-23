using System;

namespace Cush.Common.Interaction
{
    public interface IBreadcrumbCalculator
    {
        double StripPadding(Type controlType, double width);
        bool StringFitsWithinControl(Type controlType, string path, double width);
    }
}
