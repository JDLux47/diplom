using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace diplom.Interface
{
    public class SumCheckForPlan : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is decimal sum)
            {
                sum = sum - App.LoggedInUser.Balance;
                if (sum <= 0)
                    return 0;
                else 
                    return sum;
            }
            return "Недоступно";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
