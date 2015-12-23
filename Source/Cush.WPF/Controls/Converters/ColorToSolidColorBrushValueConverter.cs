using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Cush.WPF.Controls.Converters
{
    public class ColorToSolidColorBrushValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value is Color)
                return new SolidColorBrush((Color)value);

            throw new InvalidOperationException("Unsupported type [" + value.GetType().Name +
                                                "], ColorToSolidColorBrushValueConverter.Convert()");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var brush = value as SolidColorBrush;
            if (brush != null)
                return brush.Color;

            throw new InvalidOperationException("Unsupported type [" + value.GetType().Name +
                                                "], ColorToSolidColorBrushValueConverter.ConvertBack()");
        }
    }
}