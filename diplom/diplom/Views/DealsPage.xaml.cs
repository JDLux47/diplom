using Android.Views;
using diplom.Interface;
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
    public partial class DealsPage : ContentPage
    {

        public bool ShowAll = false, timer = false, SortByD, SortByT, SortByP;
        List<Deal> deals;

        public DealsPage()
        {
            InitializeComponent();
            Appearing += (sender, e) =>
            {
                Device.StartTimer(TimeSpan.FromMinutes(1), () =>
                {
                    timer = true;   
                    OnAppearing();
                    return true; // Возвращаем true для повторного запуска таймера
                });
            };
        }

        private async void ChangeStatuses()
        {
            deals = await App.Diplomdatabase.GetDealsAsync(); // Получаем все дела

            if (!ShowAll)
                deals = deals.Where(deal => deal.StatusId != 3 && deal.StatusId != 4 && deal.StatusId != 5).ToList();

            if (timer)
            {
                foreach (var deal in deals)
                {
                    if (deal.Deadline - DateTime.Now <= TimeSpan.FromMinutes(1) && deal.StatusId != 3 && deal.StatusId != 4)
                    {
                        deal.StatusId = 5;
                        await App.Diplomdatabase.SaveDealAsync(deal);
                    }
                }
                timer = false;
            }

            dealsView.ItemsSource = deals;
        }

        protected override void OnAppearing()
        {
            ChangeStatuses();
            base.OnAppearing();
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Deal selectedItem)
            {
                DealInformationPage dealInformationPage = new DealInformationPage()
                {
                    BindingContext = selectedItem
                };
                
                await Navigation.PushAsync(dealInformationPage);
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddDealPage());
        }

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkbox) // Проверка, что отправитель события - это CheckBox
            {
                if (checkbox.Parent is Grid grid) // Получение родительского элемента CheckBox, который у вас является Grid
                {
                    if (grid.BindingContext is Deal deal) // Получение объекта данных элемента внутри Grid, которого вы хотите изменить
                    {
                        deal.StatusId = 3;
                        await App.Diplomdatabase.SaveDealAsync(deal);
                        OnAppearing();
                    }
                }
            }
        }

        private void SortByTasks(object sender, EventArgs e)
        {
            if (SortByT)
            {
                deals = deals.OrderBy(deal => deal.Name).ToList();
                SortByT = false;
            }
            else
            {
                deals = deals.OrderByDescending(deal => deal.Name).ToList();
                SortByT = true;
            }

            dealsView.ItemsSource = deals;
        }

        private void SortByDeadline(object sender, EventArgs e)
        {
            if (SortByD)
            {
                deals = deals.OrderBy(deal => deal.Deadline).ToList();
                SortByD = false;
            }
            else
            {
                deals = deals.OrderByDescending(deal => deal.Deadline).ToList();
                SortByD = true;
            }

            dealsView.ItemsSource = deals;
        }

        private void SortByPriority(object sender, EventArgs e)
        {
            if (SortByP)
            {
                deals = deals.OrderBy(deal => deal.ImportanceId).ToList();
                SortByP = false;
            }
            else
            {
                deals = deals.OrderByDescending(deal => deal.ImportanceId).ToList();
                SortByP = true;
            }

            dealsView.ItemsSource = deals;
        }

        private void ShowButton_Clicked(Object sender, EventArgs e)
        {
            if (ShowAll) ShowAll = false;
            else ShowAll = true;
            OnAppearing();
        }
    }
}