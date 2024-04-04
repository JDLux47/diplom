using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace diplom.Interface
{
    public class CategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int categoryId)
            {
                string categoryName = GetCategoryName(categoryId); // получение названия категории
                return categoryName;
            }

            return "Недоступно";
        }

        private string GetCategoryName(int categoryId)
        {
            return App.Diplomdatabase.GetCategoryAsync(categoryId).Result.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
