using Android.Content;
using Android.Provider;
using diplom.Interface;
using diplom.Models;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static diplom.Views.AddDealPage;

[assembly: Dependency(typeof(CalendarService))]
namespace diplom.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddDealPage : ContentPage
	{
        private List<Importance> importances;
        private List<Status> statuses;
        DateTime date;
        public AddDealPage()
		{
			InitializeComponent ();
            Init();
        }

        private async void Init()
        {
            importances = await App.Diplomdatabase.GetImportancesAsync();
            statuses = await App.Diplomdatabase.GetStatusesAsync();

            pickerImportance.ItemsSource = importances;
            pickerImportance.SelectedIndex = 0;
            pickerStatus.ItemsSource = statuses;
            pickerStatus.SelectedIndex = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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

        private async void SaveButton_Clicked(object sender, EventArgs e)
		{
            if (entryName.Text != null && datapickerDeadline.Date != null && timepickerDeadline.Time != null)
            {
                date = new DateTime(datapickerDeadline.Date.Year, datapickerDeadline.Date.Month, datapickerDeadline.Date.Day, timepickerDeadline.Time.Hours, timepickerDeadline.Time.Minutes, 0);
                if(date > DateTime.Now)
                {
                    Deal deal = new Deal
                    {
                        Name = entryName.Text,
                        Note = entryNote.Text,
                        Notification = true,
                        DateOfCreation = DateTime.Now,
                        Deadline = date,
                        StatusId = pickerStatus.SelectedIndex + 1,
                        ImportanceId = pickerImportance.SelectedIndex + 1,
                        //OtherDealId = 0,
                        UserId = App.LoggedInUser.Id
                    };

                    await App.Diplomdatabase.SaveDealAsync(deal);
                    await Navigation.PopAsync();
                }
                else
                    await DisplayAlert("Ошибка!", "Дата выполнения должна быть позже даты создания задачи!", "OK");
            }
            else
                await DisplayAlert("Ошибка!", "Не все обязательные поля заполнены!", "ОК");
        }

        private async void Notification_Clicked(object sender, EventArgs e)
        {
            await SetNotificationAsync(date, entryName.Text);
        }
    }
}