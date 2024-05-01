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
    public partial class ListOfDealsPage : ContentPage
    {
        public ListOfDealsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var deals = await App.Diplomdatabase.GetDealsAsync();
            if (BindingContext is Deal deal) 
            {
                deals = deals.Where(d => d.OtherDealId == 0 && d.Id != deal.Id).ToList();
            }
            dealsView.ItemsSource = deals;

            base.OnAppearing();
        }

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.BindingContext is Deal deal)
            {
                if (BindingContext is Deal d)
                {
                    if (e.Value) // Галочка установлена
                    {
                        deal.OtherDealId = d.Id;
                    }
                    else // Галочка снята, можно сбросить значение или выполнить другое действие
                    {
                        deal.OtherDealId = 0; // Сброс значения OtherDealId
                    }
                    await App.Diplomdatabase.SaveDealAsync(deal);
                }
            }
        }
    }
}