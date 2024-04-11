using diplom.ComponentsInterface;
using diplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace diplom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTransactionPage : ContentPage
    {
        public EditTransactionPage()
        {
            InitializeComponent();

            EntryInterface entryInterface = new EntryInterface();
            entryInterface.OnlyNumbers(entrySum);
        }

        protected override async void OnAppearing()
        {
            List<Category> categories = await App.Diplomdatabase.GetCategoriesAsync();
            pickerCategory.ItemsSource = categories;

            if (BindingContext is Transaction transaction)
            {
                int type = 0;
                if (transaction.Type != 1)
                    type = 1;

                pickerType.SelectedIndex = type;
                entrySum.Text = transaction.Sum.ToString();
                DatePicker.Date = transaction.Date;
                pickerCategory.SelectedItem = categories.FirstOrDefault(category => category.Id == transaction.CategoryId);
            }
            base.OnAppearing();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is Transaction transaction)
            {
                List<Category> categories = await App.Diplomdatabase.GetCategoriesAsync();
                int type = 1;
                if (pickerType.SelectedIndex != 0)
                    type = -1;
                // Обновление данных транзакции на основе введенных значений
                transaction.Type = type;
                transaction.Sum = Convert.ToDecimal(entrySum.Text);
                transaction.Date = DatePicker.Date;
                transaction.CategoryId = categories[pickerCategory.SelectedIndex].Id;

                await App.Diplomdatabase.SaveTransactionAsync(transaction);

                await Navigation.PopAsync();
            }
        }
    }
}