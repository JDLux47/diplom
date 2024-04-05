using diplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace diplom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetPage : ContentPage
    {
        public BudgetPage()
        {
            InitializeComponent();
        }

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

        }
    }
}