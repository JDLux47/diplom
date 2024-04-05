using diplom.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace diplom.Interface
{
    public class ImportanceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int importanceId)
            {
                string importanceLevel = GetImportanceLevel(importanceId); // получение названия Статуса
                return importanceLevel;
            }

            return "Недоступно";
        }

        private string GetImportanceLevel(int importanceId)
        {
            return App.Diplomdatabase.GetImportanceAsync(importanceId).Result.Level;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
