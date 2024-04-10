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
        }

        public bool SortByDate = false, SortBySum = false;

        protected override async void OnAppearing()
        {
            var transactions = await App.Diplomdatabase.GetTransactionsAsync(); // Получаем все транзакции

            // Фильтруем транзакции, чтобы остались только те, где Id равен 1
            var filteredTransactions = transactions.Where(transaction => transaction.UserId == App.LoggedInUser.Id).ToList();

            transactionsView.ItemsSource = filteredTransactions;

            base.OnAppearing();
        }

        private async void OnSelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Transaction selectedItem)
            {
                TransactionInformationPage transactionInformationPage = new TransactionInformationPage
                {
                    BindingContext = selectedItem // Передача данных транзакции в качестве контекста привязки
                };
                await Navigation.PushAsync(transactionInformationPage);
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

            OnAppearing();
        }

        private async void DateHeader_Tapped(object sender, EventArgs e)
        {
            if (SortByDate)
            {
                var transactions = await App.Diplomdatabase.GetTransactionsAsync(); // Получаем все транзакции

                // Фильтруем транзакции, чтобы остались только те, где Id равен 1
                var filteredTransactions = transactions.Where(transaction => transaction.UserId == App.LoggedInUser.Id).OrderBy(transaction => transaction.Date).ToList();
                
                transactionsView.ItemsSource = filteredTransactions;

                SortByDate = false;
            }
            else
            {
                var transactions = await App.Diplomdatabase.GetTransactionsAsync(); // Получаем все транзакции

                // Фильтруем транзакции, чтобы остались только те, где Id равен 1
                var filteredTransactions = transactions.Where(transaction => transaction.UserId == App.LoggedInUser.Id).OrderByDescending(transaction => transaction.Date).ToList();

                transactionsView.ItemsSource = filteredTransactions;

                SortByDate = true;

            }
        }

        private async void CategoryHeader_Tapped(object sender, EventArgs e)
        {
            var transactions = await App.Diplomdatabase.GetTransactionsAsync(); // Получаем все транзакции

            // Фильтруем транзакции, чтобы остались только те, где Id равен 1
            var filteredTransactions = transactions.Where(transaction => transaction.UserId == App.LoggedInUser.Id).OrderBy(transaction => transaction.CategoryId).ToList();

            transactionsView.ItemsSource = filteredTransactions;
        }

        private async void SumHeader_Tapped(object sender, EventArgs e)
        {
            if (SortBySum)
            {
                var transactions = await App.Diplomdatabase.GetTransactionsAsync(); // Получаем все транзакции

                // Фильтруем транзакции, чтобы остались только те, где Id равен 1
                var filteredTransactions = transactions.Where(transaction => transaction.UserId == App.LoggedInUser.Id).OrderBy(transaction => transaction.Sum).ToList();

                transactionsView.ItemsSource = filteredTransactions;

                SortBySum = false;
            }
            else
            {
                var transactions = await App.Diplomdatabase.GetTransactionsAsync(); // Получаем все транзакции

                // Фильтруем транзакции, чтобы остались только те, где Id равен 1
                var filteredTransactions = transactions.Where(transaction => transaction.UserId == App.LoggedInUser.Id).OrderByDescending(transaction => transaction.Sum).ToList();

                transactionsView.ItemsSource = filteredTransactions;

                SortBySum = true;

            }
        }

        private async void CategoriesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListOfCategoriesPage());
        }
    }
}