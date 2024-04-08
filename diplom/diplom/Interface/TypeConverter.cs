using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace diplom.Interface
{
    public class TypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int type)
            {
                return type == -1 ? "Расход" : "Доход";
            }
            return "Недоступно";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
