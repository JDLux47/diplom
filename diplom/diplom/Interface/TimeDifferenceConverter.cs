using diplom.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace diplom.Interface
{
    public class TimeDifferenceConverter : IValueConverter
    {
        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values is DateTime deadline)
            {
                TimeSpan difference = deadline - DateTime.Now;
                int days = difference.Days;
                int hours = difference.Hours;
                int minutes = difference.Minutes;

                if (days > 3)
                {
                    return $"{days} {GetDaysText(days)}";
                }
                else if (days < 3)
                {
                    if (days == 0 && hours >= 1)
                    {
                        if (hours == 1 && minutes > 0)
                        {
                            return $"{hours} {GetHoursText(hours)} {minutes} {GetMinutesText(minutes)}";
                        }
                        else if (hours == 1 && minutes == 0)
                        {
                            return $"{hours} {GetHoursText(hours)}";
                        }
                        else
                        {
                            return $"{hours} {GetHoursText(hours)} {minutes} {GetMinutesText(minutes)}";
                        }
                    }
                    else if (days < 1 && hours < 1)
                    {
                        if (minutes > 0 && hours < 1)
                            return $"{minutes} {GetMinutesText(minutes)}";
                        else
                            return $"Время вышло";
                    }
                    else
                    {
                        return $"{days} {GetDaysText(days)} {hours} {GetHoursText(hours)}";
                    }
                }
                else
                {
                    return $"{days} {GetDaysText(days)}";
                }
            }
            return "";
        }

        private string GetDaysText(int days)
        {
            if (days % 10 == 1 && days != 11)
                return "день";
            else if ((days % 10 >= 2 && days % 10 <= 4) && !(days >= 12 && days <= 14))
                return "дня";
            else
                return "дней";
        }

        private string GetHoursText(int hours)
        {
            if (hours % 10 == 1 && hours != 11)
                return "час";
            else if ((hours % 10 >= 2 && hours % 10 <= 4) && !(hours >= 12 && hours <= 14))
                return "часа";
            else
                return "часов";
        }

        private string GetMinutesText(int minutes)
        {
            if (minutes == 1)
                return "минута";
            else if (minutes >= 2 && minutes <= 4)
                return "минуты";
            else
                return "минут";
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
