using Android.Views;
using diplom.Interface;
using diplom.Models;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace diplom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DealsPage : ContentPage
    {
        bool ShowAll = false, timer = false, SortByDeadline, SortByTask, SortByPriority, SortByStatus;
        string SortField = "Deadline";

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

        private List<Deal> Deals_ShowAll(List<Deal> deals)
        {
            if (!ShowAll)
                deals = deals.Where(deal => deal.StatusId == 1 || deal.StatusId == 2).ToList();
            else
                deals = deals.Where(deal => deal.StatusId != 1 && deal.StatusId != 2).ToList();

            return deals;
        }

        private async void ChangeStatuses()
        {
            var deals = await App.Diplomdatabase.GetDealsAsync(); // Получаем все дела

            deals = Deals_ShowAll(deals);

            if (timer)
            {
                foreach (var deal in deals)
                {
                    if (deal.Deadline - DateTime.Now <= TimeSpan.FromMinutes(1) && deal.StatusId != 3 && deal.StatusId != 4 && deal.StatusId != 6)
                    {
                        deal.StatusId = 5;
                        await App.Diplomdatabase.SaveDealAsync(deal);
                    }
                }
                timer = false;
            }
        }

        protected override void OnAppearing()
        {
            ChangeStatuses();
            Sort(SortField, true);
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
            if (sender is CheckBox checkbox && checkbox.BindingContext is Deal deal)
            {
                // Проверяем, изменилось ли значение
                if (checkbox.IsChecked && (deal.StatusId != 3 || deal.StatusId != 6))
                {
                    if (deal.Deadline >= DateTime.Now)
                        deal.StatusId = 3; // Устанавливаем 3 только если до этого было другое значение
                    else 
                        deal.StatusId = 6;

                    await App.Diplomdatabase.SaveDealAsync(deal);

                    // Обновляем только чекбокс, без вызова OnAppearing()
                    checkbox.BindingContext = null;
                    checkbox.BindingContext = deal;
                }
            }
        }

        private async void Sort(string field, bool time)
        {
            var deals = await App.Diplomdatabase.GetDealsAsync();
            deals = Deals_ShowAll(deals);

            if (time)
            {
                if (field == "Task") SortByTask = !SortByTask;
                else if (field == "Deadline") SortByDeadline = !SortByDeadline;
                else if (field == "Priority") SortByPriority = !SortByPriority;
                else if (field == "Status") SortByStatus = !SortByStatus;
            }
                
            switch (field)
            {
                case "Task":
                    if (SortByTask)
                    {
                        deals = deals.OrderBy(deal => deal.Name).ToList();
                        SortByTask = false;
                        ImageSortTask.Source = "/drawable/SortToMax";
                    }
                    else
                    {
                        deals = deals.OrderByDescending(deal => deal.Name).ToList();
                        SortByTask = true;
                        ImageSortTask.Source = "/drawable/SortToMin";
                    }
                    ImageSortDeadline.Source = "";
                    ImageSortPriority.Source = "";
                    ImageSortStatus.Source = "";
                    SortField = "Task";
                    break;

                case "Priority":
                    if (SortByPriority)
                    {
                        deals = deals.OrderBy(deal => deal.ImportanceId).ToList();
                        SortByPriority = false;
                        ImageSortPriority.Source = "/drawable/SortToMax";
                    }
                    else
                    {
                        deals = deals.OrderByDescending(deal => deal.ImportanceId).ToList();
                        SortByPriority = true;
                        ImageSortPriority.Source = "/drawable/SortToMin";
                    }
                    ImageSortDeadline.Source = "";
                    ImageSortTask.Source = "";
                    ImageSortStatus.Source = "";
                    SortField = "Priority";
                    break;

                case "Deadline":
                    if (SortByDeadline)
                    {
                        deals = deals.OrderBy(deal => deal.Deadline).ToList();
                        SortByDeadline = false;
                        ImageSortDeadline.Source = "/drawable/SortToMax";
                    }
                    else
                    {
                        deals = deals.OrderByDescending(deal => deal.Deadline).ToList();
                        SortByDeadline = true;
                        ImageSortDeadline.Source = "/drawable/SortToMin";
                    }
                    ImageSortPriority.Source = "";
                    ImageSortTask.Source = "";
                    ImageSortStatus.Source = "";
                    SortField = "Deadline";
                    break;

                case "Status":
                    if (SortByStatus)
                    {
                        deals = deals.OrderBy(deal => deal.StatusId).ToList();
                        SortByStatus = false;
                        ImageSortStatus.Source = "/drawable/SortToMax";
                    }
                    else
                    {
                        deals = deals.OrderByDescending(deal => deal.StatusId).ToList();
                        SortByStatus = true;
                        ImageSortStatus.Source = "/drawable/SortToMin";
                    }
                    ImageSortPriority.Source = "";
                    ImageSortDeadline.Source = "";
                    ImageSortTask.Source = "";
                    SortField = "Status";
                    break;

                default:
                    break;
            }
            dealsView.ItemsSource = deals;
            buttonStack.IsVisible = false;
        }


        private void ShowButton_Clicked(Object sender, EventArgs e)
        {
            if (ShowAll)
            {
                ShowAll = false;
                showHideItem.Text = "Показать остальные";
            }
            else
            {
                ShowAll = true;
                showHideItem.Text = "Показать актуальные";
            }
            OnAppearing();
        }

        private void ButtonByTask_Clicked(object sender, EventArgs e)
        {
            Sort("Task", false);
        }
        private void ButtonByPriority_Clicked(object sender, EventArgs e)
        {
            Sort("Priority", false);
        }

        private void ButtonByDeadline_Clicked(object sender, EventArgs e)
        {
            Sort("Deadline", false);
        }

        private void ButtonByStatus_Clicked (object sender, EventArgs e)
        {
            Sort("Status", false);
        }

        private void OpenListButton_Clicked(object sender, EventArgs e)
        {
            if (buttonStack.IsVisible)
                buttonStack.IsVisible = false;
            else
                buttonStack.IsVisible = true;
        }

        private void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (e.VerticalOffset >= 0) // Проверяем, что произошло вертикальное прокручивание вниз
                buttonStack.IsVisible = false; // Скрываем AbsoluteLayout при прокрутке вниз
            else
                buttonStack.IsVisible = true; // Показываем AbsoluteLayout при прокрутке вверх или в начальном положении
        }

        private async void UserAccount_Clicked(object sender, EventArgs e)
        {
            ProfilePage profilePage = new ProfilePage();
            await Navigation.PushAsync(profilePage);
        }
    }
}