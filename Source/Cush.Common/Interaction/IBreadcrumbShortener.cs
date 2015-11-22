using System;

namespace Cush.Common.Interaction
{
    public interface IBreadcrumbShortener
    {
        string Compact(Type controlType, string path, double width);
    }
}