using diplom.Interface;
using diplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Transaction = diplom.Models.Transaction;
using TypeConverter = diplom.Interface.TypeConverter;

namespace diplom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetPage : ContentPage
    {
        public BudgetPage()
        {
            InitializeComponent();
            balanceToolbarItem = new ToolbarItem // Создаем новый ToolbarItem
            {
                Text = $"Баланс: {App.LoggedInUser.Balance}"
            };
            ToolbarItems.Add(balanceToolbarItem);// Добавляем созданный ToolbarItem в панель инструментов
        }

        public bool SortByDate, SortBySum, SortByCategory;
        List<Transaction> transactions;
        ToolbarItem balanceToolbarItem;

        protected override async void OnAppearing()
        {
            transactions = await App.Diplomdatabase.GetTransactionsAsync(); // Получаем все транзакции

            transactionsView.ItemsSource = transactions;

            balanceToolbarItem.Text = $"Баланс: {App.LoggedInUser.Balance}";

            base.OnAppearing();
        }

        private async void OnSelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Transaction selectedItem)
            {
                EditTransactionPage editTransactionPage = new EditTransactionPage
                {
                    BindingContext = selectedItem // Передача данных транзакции в качестве контекста привязки
                };
                await Navigation.PushAsync(editTransactionPage);
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTransactionPage());
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var transactionToDelete = (Transaction)((ImageButton)sender).CommandParameter;
            await App.Diplomdatabase.DeleteTransactionAsync(transactionToDelete);
            bool result = await DisplayAlert("Подтверждение", "Изменить баланс в соответствии с удаляемой транзакцией?", "Да", "Нет");
            if(result)
            {
                App.LoggedInUser.Balance = App.LoggedInUser.Balance - transactionToDelete.Sum * transactionToDelete.Type;
                await App.Diplomdatabase.SaveUserAsync(App.LoggedInUser);
            }
            OnAppearing();
        }

        private async void CategoriesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListOfCategoriesPage());
        }

        private void ButtonByDate_Clicked(object sender, EventArgs e)
        {
            if (SortByDate)
            {
                transactions = transactions.OrderBy(transaction => transaction.Date).ToList();
                ImageSortDate.Source = "/drawable/SortToMax";
                SortByDate = false;
            }
            else
            {
                transactions = transactions.OrderByDescending(transaction => transaction.Date).ToList();
                ImageSortDate.Source = "/drawable/SortToMin";
                SortByDate = true;

            }
            ImageSortCategory.Source = "";
            ImageSortSum.Source = "";
            transactionsView.ItemsSource = transactions;
            buttonStack.IsVisible = false;
        }
        private void ButtonByPrice_Clicked(object sender, EventArgs e)
        {
            if (SortBySum)
            {
                transactions = transactions.OrderBy(transaction => transaction.Sum).ToList();
                SortBySum = false;
                ImageSortSum.Source = "/drawable/SortToMax";
            }
            else
            {
                transactions = transactions.OrderByDescending(transaction => transaction.Sum).ToList();
                SortBySum = true;
                ImageSortSum.Source = "/drawable/SortToMin";
            }
            ImageSortCategory.Source = "";
            ImageSortDate.Source = "";
            transactionsView.ItemsSource = transactions;
            buttonStack.IsVisible = false;
        }

        private void ButtonByCategory_Clicked(object sender, EventArgs e)
        {
            if (SortByCategory)
            {
                transactions = transactions.OrderBy(transaction => transaction.CategoryId).ToList();
                SortByCategory = false;
                ImageSortCategory.Source = "/drawable/SortToMax";
            }
            else
            {
                transactions = transactions.OrderByDescending(transaction => transaction.CategoryId).ToList();
                SortByCategory = true;
                ImageSortCategory.Source = "/drawable/SortToMin";
            }
            ImageSortDate.Source = "";
            ImageSortSum.Source = "";
            transactionsView.ItemsSource = transactions;

            buttonStack.IsVisible = false;
        }

        private void OpenListButton_Clicked(object sender, EventArgs e)
        {
            if(buttonStack.IsVisible)
                buttonStack.IsVisible = false;
            else 
                buttonStack.IsVisible = true;
        }

        private void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (e.VerticalOffset >= 0) // Проверяем, что произошло вертикальное прокручивание вниз
            {
                buttonStack.IsVisible = false; // Скрываем AbsoluteLayout при прокрутке вниз
            }
            else
            {
                buttonStack.IsVisible = true; // Показываем AbsoluteLayout при прокрутке вверх или в начальном положении
            }
        }
    }
}