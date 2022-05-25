using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GameWPF.View.Converter
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = Visibility.Collapsed;
            if (value != null && targetType == typeof(Visibility) && (bool)value)
            {
                result = Visibility.Visible;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
