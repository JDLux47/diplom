using diplom.Interface;
using diplom.Models;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Ical.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace diplom.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DealInformationPage : ContentPage
	{
        public DealInformationPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
		{
            if (BindingContext is Deal deal)
			{
                deal = await App.Diplomdatabase.GetDealAsync(deal.Id);

				timepickerCreation.Time = deal.DateOfCreation.TimeOfDay;
				timepickerDeadline.Time = deal.Deadline.TimeOfDay; 

                var importances = await App.Diplomdatabase.GetImportancesAsync();
                pickerImportance.ItemsSource = importances;

                var statuses = await App.Diplomdatabase.GetStatusesAsync();
                pickerStatus.ItemsSource = statuses;

				pickerImportance.SelectedIndex = deal.ImportanceId - 1;
                pickerStatus.SelectedIndex = deal.StatusId - 1;

                var deals = await App.Diplomdatabase.GetDealsAsync();
                deals = deals.Where(d => d.OtherDealId == deal.Id).ToList();

                if (deals.Count > 0)
                {
                    ListDealsView.ItemsSource = deals;
                    LabelTasks.IsVisible = true;
                    ListDealsView.IsVisible = true;
                }
                else
                {
                    LabelTasks.IsVisible = false;
                    ListDealsView.IsVisible = false;
                }

                entryName.Text = deal.Name;
                entryNote.Text = deal.Note;
                datapickerCreation.Date = deal.DateOfCreation;
                datapickerDeadline.Date = deal.Deadline;
                timepickerCreation.Time = deal.DateOfCreation.TimeOfDay;
                timepickerDeadline.Time = deal.Deadline.TimeOfDay;
            }

            ListDealsView_SizeChanged(ListDealsView, EventArgs.Empty);
            base.OnAppearing();

            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (BindingContext is Deal deal)
            {
                //entryName.Text = "";
                //entryNote.Text = "";
                datapickerCreation.Date = DateTime.MinValue;
                datapickerDeadline.Date = DateTime.MinValue;
                timepickerCreation.Time = DateTime.MinValue.TimeOfDay;
                timepickerDeadline.Time = DateTime.MinValue.TimeOfDay;
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
		{
            if (BindingContext is Deal deal)
            {
                bool edit = false;

                DateTime dateDe = new DateTime(datapickerDeadline.Date.Year, datapickerDeadline.Date.Month, datapickerDeadline.Date.Day, timepickerDeadline.Time.Hours, timepickerDeadline.Time.Minutes, 0);
                DateTime dateCr = new DateTime(datapickerCreation.Date.Year, datapickerCreation.Date.Month, datapickerCreation.Date.Day, timepickerCreation.Time.Hours, timepickerCreation.Time.Minutes, 0);

                if (deal.Note != entryNote.Text || deal.Name != entryName.Text || deal.ImportanceId != pickerImportance.SelectedIndex + 1 
                    || deal.StatusId != pickerStatus.SelectedIndex + 1 || deal.Deadline != dateDe || deal.DateOfCreation != dateCr)
                    edit = true;

                if (edit)
                {
                    deal.Name = entryName.Text;
                    deal.Note = entryNote.Text;
                    deal.Notification = true;
                    deal.ImportanceId = pickerImportance.SelectedIndex + 1;
                    deal.StatusId = pickerStatus.SelectedIndex + 1;
                    deal.Deadline = dateDe;
                    deal.DateOfCreation = dateCr;

                    await App.Diplomdatabase.SaveDealAsync(deal);
                    DependencyService.Get<ICustomToast>().ShowCustomToast("Данные задачи были изменены!", Color.Green.ToHex(), Color.White.ToHex());
                }
            }
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is Deal deal)
            {
                bool result = await DisplayAlert("Подтверждение", "Вы уверены, что хотите удалить данную задачу из базы данных?", "Да", "Отмена");

                if(result)
                {
                    await App.Diplomdatabase.DeleteDealAsync(deal);
                    await Navigation.PopAsync();
                }
            }
        }

        private async Task SetNotificationAsync(DateTime date, string name)
        {
            // Создание нового события
            CalendarEvent newEvent = new CalendarEvent
            {
                Summary = name,
                DtStart = new CalDateTime(DateTime.Now), // Установите время начала события
                DtEnd = new CalDateTime(date),  // Установите время окончания события
            };

            TimeSpan difference = date - DateTime.Now;
            difference = TimeSpan.FromTicks(difference.Ticks / 3);

            // Добавление напоминания к событию
            newEvent.Alarms.Add(new Alarm
            {
                Action = AlarmAction.Display,
                Trigger = new Ical.Net.DataTypes.Trigger(TimeSpan.FromTicks(difference.Ticks)) // Установите время до события для напоминания
            });

            // Добавление события в календарную компоненту
            var calendar = new Calendar();
            calendar.Events.Add(newEvent);

            // Сериализация календаря в строку
            var serializer = new CalendarSerializer();
            var serializedCalendar = serializer.SerializeToString(calendar);

            // Сохранение календаря в файл (нужно выбрать путь к файлу на устройстве)
            string filePath = "/data/data/com.companyname.diplom/cache/calendar.ics"; // Например, путь к файлу на устройстве
            File.WriteAllText(filePath, serializedCalendar);

            // Добавление события из файла в календарь устройства
            await DependencyService.Get<ICalendar>().AddEventToCalendar(filePath, newEvent); // Предполагается, что используется DependencyService для доступа к функциональности добавления событий в календарь
        }

        private async void Notification_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is Deal deal)
                await SetNotificationAsync(deal.Deadline, entryName.Text);
        }

        private async void TaskAdd_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is Deal deal)
            {
                ListOfDealsPage listOfDealsPage = new ListOfDealsPage
                {
                    BindingContext = deal,
                };
                await Navigation.PushAsync(listOfDealsPage);
            }
            
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

        private async void ListDealsView_SizeChanged(object sender, EventArgs e)
        {
            var collectionView = sender as CollectionView;
            if (collectionView == null) return;

            var deals = await App.Diplomdatabase.GetDealsAsync();
            if (BindingContext is Deal deal)
            {
                deals = deals.Where(d => d.OtherDealId == deal.Id).ToList();
            }

            double totalHeight = deals.Count() * 35;

            collectionView.HeightRequest = totalHeight;
        }
    }
}