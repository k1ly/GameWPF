using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace GameWPF.View.Converter
{
    public class MultiValueEqualityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Count() > 0 && values.All(o => o == values[0] || (o != null && o.Equals(values[0])));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
