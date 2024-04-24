using Android.Hardware.Camera2;
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
	public partial class PlansPage : ContentPage
	{
		public PlansPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            var plans = await App.Diplomdatabase.GetPlansAsync();

            IncomeToMonthCheckAsync();

            PlansView.ItemsSource = plans;

            base.OnAppearing();
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Plan selectedItem)
            {
                PlanInfoPage planInfoPage = new PlanInfoPage
                {
                    BindingContext = selectedItem 
                };
                await Navigation.PushAsync(planInfoPage);
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPlanPage());
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private async void IncomeToMonthCheckAsync()
        {
            var transactions = await App.Diplomdatabase.GetTransactionsAsync();

            // Группируем транзакции, исключая текущий месяц
            var groupedTransactions = transactions.Where(t => t.Date.Year != DateTime.Now.Year || t.Date.Month != DateTime.Now.Month)
                                                  .GroupBy(t => new { t.Date.Year, t.Date.Month });

            decimal totalProfit = 0;
            int totalMonths = 0;
            foreach (var group in groupedTransactions)
            {
                var totalIncome = group.Where(t => t.Type == 1).Sum(t => t.Sum);
                var totalExpense = group.Where(t => t.Type == -1).Sum(t => t.Sum);

                decimal monthlyProfit = totalIncome - totalExpense;

                totalProfit += monthlyProfit;
                totalMonths++;
            }

            decimal averageProfitPerMonth = Math.Round(totalMonths > 0 ? totalProfit / totalMonths : 0, 0, MidpointRounding.AwayFromZero);

            labelMonthProfit.Text = "Ваш среднемесячный заработок с учётом всех расходов и доходов: " + averageProfitPerMonth + "₽";
        }
    }
}