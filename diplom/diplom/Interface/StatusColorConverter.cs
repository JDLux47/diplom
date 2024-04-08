using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace diplom.Interface
{
    public class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int statusId)
            {
                switch (statusId)
                {
                    case 1:
                        return Color.Blue;
                    case 2:
                        return Color.Yellow;
                    case 3:
                        return Color.Green;
                    case 4:
                        return Color.Orange;
                    case 5:
                        return Color.Red;

                    default:
                        return Color.Black;
                }
            }

            return Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
