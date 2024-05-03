using diplom.ComponentsInterface;
using diplom.Interface;
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

        protected override async void OnDisappearing()
        {
            await Navigation.PopAsync();
            base.OnDisappearing();
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
                bool edit = false;
                var OldType = transaction.Type;
                var OldSum = transaction.Sum;

                int type = 1;
                if (pickerType.SelectedIndex != 0)
                    type = -1;

                List<Category> categories = await App.Diplomdatabase.GetCategoriesAsync();

                if (type != transaction.Type || transaction.Sum != Convert.ToDecimal(entrySum.Text) || transaction.Date != DatePicker.Date || transaction.CategoryId != categories[pickerCategory.SelectedIndex].Id)
                    edit = true;

                if(edit)
                {
                    bool result = false;
                    if (transaction.Sum != Convert.ToDecimal(entrySum.Text) || transaction.Type != type)
                        result = await DisplayAlert("Подтверждение", "Изменить баланс в соответствии с изменяемой транзакцией?", "Да", "Нет");

                    // Обновление данных транзакции на основе введенных значений
                    transaction.Type = type;
                    transaction.Sum = Convert.ToDecimal(entrySum.Text);
                    transaction.Date = DatePicker.Date;
                    transaction.CategoryId = categories[pickerCategory.SelectedIndex].Id;

                    if (result)
                    {
                        var sum = OldSum * (-1 * OldType) + transaction.Type * transaction.Sum;
                        App.LoggedInUser.Balance = App.LoggedInUser.Balance + sum;
                        await App.Diplomdatabase.SaveUserAsync(App.LoggedInUser);
                    }

                    await App.Diplomdatabase.SaveTransactionAsync(transaction);

                    DependencyService.Get<ICustomToast>().ShowCustomToast("Данные транзакции были изменены!", Color.Green.ToHex(), Color.White.ToHex());
                }
            }
        }
    }
}