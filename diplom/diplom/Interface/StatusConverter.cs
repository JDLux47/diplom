using diplom.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace diplom.Interface
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int statusId)
            {
                string statusName = GetStatusName(statusId); // получение названия Статуса
                return statusName;
            }

            return "Недоступно";
        }

        private string GetStatusName(int statusId)
        {
            return App.Diplomdatabase.GetStatusAsync(statusId).Result.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
