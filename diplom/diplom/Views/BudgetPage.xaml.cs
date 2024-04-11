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

            transactionsView.ItemsSource = transactions;

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
            var transactions = await App.Diplomdatabase.GetTransactionsAsync(); // Получаем все транзакции

            if (SortByDate)
            {
                transactions = transactions.OrderBy(transaction => transaction.Date).ToList();

                SortByDate = false;
            }
            else
            {
                transactions = transactions.OrderByDescending(transaction => transaction.Date).ToList();

                SortByDate = true;

            }

            transactionsView.ItemsSource = transactions;
        }

        private async void CategoryHeader_Tapped(object sender, EventArgs e)
        {
            var transactions = await App.Diplomdatabase.GetTransactionsAsync(); // Получаем все транзакции

            transactions = transactions.OrderBy(transaction => transaction.CategoryId).ToList();

            transactionsView.ItemsSource = transactions;
        }

        private async void SumHeader_Tapped(object sender, EventArgs e)
        {
            var transactions = await App.Diplomdatabase.GetTransactionsAsync(); // Получаем все транзакции

            if (SortBySum)
            {
                transactions = transactions.OrderBy(transaction => transaction.Sum).ToList();

                SortBySum = false;
            }
            else
            {
                transactions = transactions.OrderByDescending(transaction => transaction.Sum).ToList();

                SortBySum = true;
            }

            transactionsView.ItemsSource = transactions;
        }

        private async void CategoriesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListOfCategoriesPage());
        }
    }
}