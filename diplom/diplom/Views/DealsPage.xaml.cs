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
        public DealsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var deals = await App.Diplomdatabase.GetDealsAsync(); // Получаем все дела

            // Фильтруем транзакции, чтобы остались только те, где Id равен 1
            var filteredDeals = deals.Where(deal => deal.UserId == App.LoggedInUser.Id).ToList();

            dealsView.ItemsSource = filteredDeals;

            base.OnAppearing();
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Deal selectedItem)
            {
                // Создание новой страницы для отображения подробной информации
                ContentPage detailPage = new ContentPage();

                // Создание макета для отображения данных объекта на новой странице
                StackLayout detailLayout = new StackLayout
                {
                    Padding = new Thickness(20)
                };

                string notification = "Выключено";
                if (selectedItem.Notification)
                       notification = "Включено";

                // Добавление элементов с подробной информацией о выбранном объекте
                Label titleLabel = new Label { Text = $"Информация о задаче: ", FontSize = 20 };
                Label nameLabel = new Label { Text = $"Название: {selectedItem.Name}", FontSize = 16 };
                Label datacreationLabel = new Label { Text = $"Дата создания: {selectedItem.DateOfCreation:D}  Время: {selectedItem.DateOfCreation:t}", FontSize = 16 };
                Label deadlineLabel = new Label { Text = $"Дата выполнения: {selectedItem.Deadline:D}  Время: {selectedItem.Deadline:t}", FontSize = 16 };
                Label noteLabel = new Label { Text = $"Заметки: {selectedItem.Note}", FontSize = 16 };
                Label notificationLabel = new Label { Text = $"Уведомление: {notification}", FontSize = 16 };
                Label importanceLabel = new Label { Text = $"Степень важности: {((ImportanceConverter)Resources["ImportanceConverter"]).Convert(selectedItem.ImportanceId, null, null, null)}", FontSize = 16 };
                Label statusLabel = new Label { Text = $"Статус выполнения: {((StatusConverter)Resources["StatusConverter"]).Convert(selectedItem.StatusId, null, null, null)}", FontSize = 16 };

                // Добавление элементов в макет
                detailLayout.Children.Add(titleLabel);
                detailLayout.Children.Add(nameLabel);
                detailLayout.Children.Add(datacreationLabel);
                detailLayout.Children.Add(deadlineLabel);
                detailLayout.Children.Add(noteLabel);
                detailLayout.Children.Add(notificationLabel);
                detailLayout.Children.Add(importanceLabel);
                detailLayout.Children.Add(statusLabel);

                // Устанавливаем Content для новой страницы
                detailPage.Content = detailLayout;

                // Навигация к новой странице
                await Navigation.PushAsync(detailPage);
            }
        }
    }
}