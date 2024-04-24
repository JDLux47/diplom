using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Xamarin.Forms;

namespace diplom.Interface
{
    public class DeadlineCheckForPlan : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime deadline)
            {
                TimeSpan difference = deadline - DateTime.Now;

                int totalDays = (int)difference.TotalDays;
                int years = deadline.Year - DateTime.Now.Year;
                int months = deadline.Month - DateTime.Now.Month;
                int days = deadline.Day - DateTime.Now.Day;

                // Оставшееся время
                if (totalDays <= 0)
                {
                    return "времени не осталось";
                }

                if (months < 0)
                {
                    years--;
                    months += 12;
                }

                if (days < 0)
                {
                    months--;
                    days += DateTime.DaysInMonth(deadline.Year, DateTime.Now.Month);
                }

                string durationYear = years >= 10 && years <= 20 ? $"{years} лет"
                    : years % 10 >= 5 ? $"{years} лет"
                    : years % 10 == 1 ? $"{years} год"
                    : years % 10 > 1 && years % 10 < 5 ? $"{years} года"
                    : "";

                string durationMonth = months >= 10 && months <= 20 ? $"{months} месяцев"
                                    : months % 10 >= 5 ? $"{months} месяцев"
                                    : months % 10 == 1 ? $"{months} месяц"
                                    : months % 10 > 1 && months % 10 < 5 ? $"{months} месяца"
                                    : "";

                string durationDay = days >= 10 && days <= 20 ? $"{days} дней"
                                    : days % 10 >= 5 ? $"{days} дней"
                                    : days % 10 == 1 ? $"{days} день"
                                    : days % 10 > 1 && days % 10 < 5 ? $"{days} дня"
                                    : "";

                string duration = $"{durationYear} {durationMonth} {durationDay}".Trim();

                return duration;
            }
            return "Недоступно";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
