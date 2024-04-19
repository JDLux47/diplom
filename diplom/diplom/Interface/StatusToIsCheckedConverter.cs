using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace diplom.Interface
{
    public class StatusToIsCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (int.TryParse(value.ToString(), out int statusId))
            {
                return statusId == 3;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Обратное преобразование не требуется, поэтому просто возвращаем null
            return null;
        }
    }
}
