using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Cush.WPF.Controls.Converters
{
    class TabItemCloseButtonWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToInt32(value)*0.5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}