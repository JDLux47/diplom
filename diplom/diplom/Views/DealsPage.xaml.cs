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
    public partial class DealsPage : ContentPage
    {
        public DealsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var deals = await App.Diplomdatabase.GetDealsAsync(); // Получаем все дела
            int loggedInUser = Convert.ToInt32(await SecureStorage.GetAsync("Key"));

            // Фильтруем транзакции, чтобы остались только те, где Id равен 1
            var filteredDeals = deals.Where(deal => deal.UserId == loggedInUser).ToList();

            dealsView.ItemsSource = filteredDeals;

            base.OnAppearing();
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}