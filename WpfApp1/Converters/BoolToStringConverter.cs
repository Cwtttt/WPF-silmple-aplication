using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp1
{
    public class BoolToStringConverter1 : IValueConverter
    {
        //public T FalseValue { get; set; }
        //public T TrueValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == false)
                return "NIE";
            else 
               return "TAK"; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;//value != null ? value.Equals(TrueValue) : false;
        }
    }
}
