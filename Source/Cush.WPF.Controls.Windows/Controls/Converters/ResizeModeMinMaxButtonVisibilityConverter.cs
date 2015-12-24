using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Cush.WPF.Controls.Converters
{
    public sealed class ResizeModeMinMaxButtonVisibilityConverter : IMultiValueConverter
    {
        private static ResizeModeMinMaxButtonVisibilityConverter _instance;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static ResizeModeMinMaxButtonVisibilityConverter()
        {
        }

        private ResizeModeMinMaxButtonVisibilityConverter()
        {
        }

        public static ResizeModeMinMaxButtonVisibilityConverter Instance
        {
            get { return _instance ?? (_instance = new ResizeModeMinMaxButtonVisibilityConverter()); }
        }

        //private bool NotCushWindow(object[] values)
        //{
        //    if (values[0] == DependencyProperty.UnsetValue) return true;
        //    if(values.Length==1) return false;
        //    if (values[1] == DependencyProperty.UnsetValue) return true;
        //    if (values.Length == 2) return false;
        //    return values[2] == DependencyProperty.UnsetValue;
        //}

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var whichButton = parameter as string;
            if (values != null && !string.IsNullOrEmpty(whichButton))
            {
                // TODO: CURT: Remove Before Flight
                //if (NotCushWindow(values)) return Visibility.Visible;
                // End Remove Before Flight

                var showButton = values.Length > 0 && (bool)values[0];
                var useNoneWindowStyle = values.Length > 1 && (bool)values[1];
                var windowResizeMode = values.Length > 2 ? (ResizeMode)values[2] : ResizeMode.CanResize;

                if (whichButton == "CLOSE")
                {
                    return useNoneWindowStyle || !showButton ? Visibility.Collapsed : Visibility.Visible;
                }

                switch (windowResizeMode)
                {
                    case ResizeMode.NoResize:
                        return Visibility.Collapsed;
                    case ResizeMode.CanMinimize:
                        if (whichButton == "MIN")
                        {
                            return useNoneWindowStyle || !showButton ? Visibility.Collapsed : Visibility.Visible;
                        }
                        return Visibility.Collapsed;
                    case ResizeMode.CanResize:
                    case ResizeMode.CanResizeWithGrip:
                    default:
                        return useNoneWindowStyle || !showButton ? Visibility.Collapsed : Visibility.Visible;
                }
            }
            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return targetTypes.Select(t => DependencyProperty.UnsetValue).ToArray();
        }
    }
}
