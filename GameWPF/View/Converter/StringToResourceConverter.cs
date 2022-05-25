using System;
using System.Windows;
using System.Windows.Data;

namespace GameWPF.View.Converter
{
    public class StringToResource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Application.Current.FindResource(string.Format(parameter as string ?? "{0}", value as string));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
