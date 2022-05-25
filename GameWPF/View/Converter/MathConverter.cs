using System;
using System.Linq;
using System.Globalization;
using System.Windows.Data;

namespace GameWPF.View.Converter
{
    public class MathConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(o => o == null || !o.GetType().IsValueType))
                return 0;
            double result = System.Convert.ToDouble(values[0]);
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] != null)
                    switch (parameter as string)
                    {
                        case "+": result += System.Convert.ToDouble(values[i]); break;
                        case "-": result -= System.Convert.ToDouble(values[i]); break;
                        case "*": result *= System.Convert.ToDouble(values[i]); break;
                        case "/": result /= System.Convert.ToDouble(values[i]); break;
                    }
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
