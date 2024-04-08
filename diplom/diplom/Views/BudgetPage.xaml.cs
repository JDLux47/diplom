using diplom.Interface;
using diplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Transaction = diplom.Models.Transaction;
using TypeConverter = diplom.Interface.TypeConverter;

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
            if (e.CurrentSelection.FirstOrDefault() is Transaction selectedItem)
            {
                // Создание новой страницы для отображения подробной информации
                ContentPage detailPage = new ContentPage();

                // Создание макета для отображения данных объекта на новой странице
                StackLayout detailLayout = new StackLayout
                {
                    Padding = new Thickness(20)
                };

                // Добавление элементов с подробной информацией о выбранном объекте
                Label titleLabel = new Label { Text = $"Информация о транзакции: ", FontSize = 20 };
                Label typeLabel = new Label { Text = $"Тип: {((TypeConverter)Resources["TypeConverter"]).Convert(selectedItem.Type, null, null, null)}", FontSize = 16 };
                Label sumLabel = new Label { Text = $"Сумма: {selectedItem.Sum}", FontSize = 16 };
                Label dateLabel = new Label { Text = $"Дата: {selectedItem.Date:D}", FontSize = 16 };
                Label categoryLabel = new Label { Text = $"Категория: { ((CategoryConverter)Resources["CategoryConverter"]).Convert(selectedItem.CategoryId, null, null, null)}", FontSize = 16 };

                // Добавление элементов в макет
                detailLayout.Children.Add(titleLabel);
                detailLayout.Children.Add(typeLabel);
                detailLayout.Children.Add(sumLabel);
                detailLayout.Children.Add(dateLabel);
                detailLayout.Children.Add(categoryLabel);

                // Устанавливаем Content для новой страницы
                detailPage.Content = detailLayout;

                // Навигация к новой странице
                await Navigation.PushAsync(detailPage);
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTransactionPage());
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var transactionToDelete = (Transaction)((ImageButton)sender).CommandParameter;
            await App.Diplomdatabase.DeleteTransactionAsync(transactionToDelete);

            OnAppearing();
        }
    }
}