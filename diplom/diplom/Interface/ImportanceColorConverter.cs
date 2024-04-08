using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace diplom.Interface
{
    public class ImportanceColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int importanceId)
            {
                switch (importanceId)
                {
                    case 1: 
                        return Color.Green; 
                    case 2: 
                        return Color.Yellow; 
                    case 3: 
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
