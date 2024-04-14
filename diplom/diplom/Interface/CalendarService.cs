using Android.Content;
using Android.Provider;
using Ical.Net.CalendarComponents;
using System;
using System.Collections.Generic;
using System.Text;
using static diplom.Views.AddDealPage;
using System.Threading.Tasks;

namespace diplom.Interface
{
    public class CalendarService : ICalendar
    {
        public Task AddEventToCalendar(string eventFilePath, CalendarEvent newEvent)
        {
            // Получение контекста приложения
            Context context = Android.App.Application.Context;

            // Создание интента для добавления события в календарь с флагом FLAG_ACTIVITY_NEW_TASK
            Intent intent = new Intent(Intent.ActionInsert);
            intent.SetData(CalendarContract.Events.ContentUri);
            intent.SetType("vnd.android.cursor.item/event");
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.Title, newEvent.Summary); // Установка названия события
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.Dtstart, newEvent.DtStart.Value.Ticks); // Установка времени начала события
            intent.PutExtra(CalendarContract.Events.InterfaceConsts.Dtend, newEvent.DtEnd.Value.Ticks); // Установка времени окончания события
            intent.AddFlags(ActivityFlags.NewTask); // Добавление флага FLAG_ACTIVITY_NEW_TASK

            // Запуск активити для добавления события с новым заданием (task)
            context.StartActivity(intent);

            return Task.CompletedTask;
        }
    }

    public interface ICalendar
    {
        Task AddEventToCalendar(string eventFilePath, CalendarEvent newEvent);
    }
}
