using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Cush.WPF.Controls.Converters
{
    // Converts a hex value to a color.
    /// <summary>
    /// Converts a hex value to a color.
    /// </summary>
    [ValueConversion(typeof(Color), typeof(Color))]
    class HexToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                var hexValue = value.ToString();

                var convertFromString = ColorConverter.ConvertFromString(hexValue);
                if (convertFromString != null)
                {
                    var newColor = (Color)convertFromString;
                    return newColor;
                }
            }
            catch
            {
                Trace.WriteLine("Color failed to convert.");
            }

            return DependencyProperty.UnsetValue;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Trace.WriteLine("Color! ConvertBack");
            return DependencyProperty.UnsetValue;
        }

    }
}
