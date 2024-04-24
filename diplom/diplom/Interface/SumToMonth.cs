using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace diplom.Interface
{
    public class SumToMonth : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return null;
            if (values[0] is decimal sum && values[1] is DateTime deadline)
            {
                int totalMonths = (deadline.Year - DateTime.Now.Year) * 12 + deadline.Month - DateTime.Now.Month;

                // Уточнение количества месяцев
                if (deadline.Day < DateTime.Now.Day)
                {
                    totalMonths--;
                }

                if (totalMonths > 0)
                    sum = Math.Round((sum - App.LoggedInUser.Balance) / totalMonths, 0, MidpointRounding.AwayFromZero);
                else
                    sum = Math.Round(sum - App.LoggedInUser.Balance, 0, MidpointRounding.AwayFromZero);

                return $"Нужно откладывать по {sum}₽ в месяц, чтобы накопить до конца срока";
            }
            else 
                return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
