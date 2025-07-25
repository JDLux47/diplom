﻿using diplom.Interface;
using diplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SQLite.SQLite3;

namespace diplom.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListOfCategoriesPage : ContentPage
	{
		public ListOfCategoriesPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            var categories = await App.Diplomdatabase.GetCategoriesAsync();

            var filteredCategories = categories.Where(category => (category.UserId == App.LoggedInUser.Id || category.UserId == 0) && category.Id != 1).ToList();

            categoriesView.ItemsSource = filteredCategories;

            base.OnAppearing();
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() != null)
            {
                var selectedCategory = (Category)e.CurrentSelection.FirstOrDefault(); // Предположим, что у вас есть класс Category для категорий
                var categories = await App.Diplomdatabase.GetCategoriesAsync();
                var filteredCategories = categories.Where(category => category.UserId == App.LoggedInUser.Id || category.UserId == 0).ToList();
                // Вывод диалогового окна с одним полем для изменения имени категории
                string newName = await DisplayPromptAsync("Изменение категории", "Введите название", "OK", "Отмена", selectedCategory.Name);

                if (newName != null)
                {
                    bool error = false;
                    foreach (Category category in filteredCategories)
                    {
                        if (category.Name == newName)
                        {
                            DependencyService.Get<ICustomToast>().ShowCustomToast("Категория с таким именем уже существует!", Color.Red.ToHex(), Color.White.ToHex());
                            error = true;
                            break;
                        }
                    }

                    if (!error)
                    {
                        selectedCategory.Name = newName;
                        await App.Diplomdatabase.SaveCategoryAsync(selectedCategory);
                    }
                }
            }

            OnAppearing();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            // Создание диалогового окна с одним полем для ввода
            string result = await DisplayPromptAsync("Добавление категории", "Введите название", "ОК", "Отмена", placeholder: "Название");
            var categories = await App.Diplomdatabase.GetCategoriesAsync();
            var filteredCategories = categories.Where(category => category.UserId == App.LoggedInUser.Id || category.UserId == 0).ToList();

            // Проверка, что пользователь ввел значение и нажал "ОК"
            if (result != null)
            {
                bool error = false;
                foreach (Category category in filteredCategories)
                {
                    if (category.Name == result)
                    {
                        DependencyService.Get<ICustomToast>().ShowCustomToast("Категория с таким именем уже существует!", Color.Red.ToHex(), Color.White.ToHex());
                        error = true;
                        break;
                    }
                }

                if (!error)
                {
                    Category newcategory = new Category
                    {
                        UserId = App.LoggedInUser.Id,
                        Name = result
                    };
                    await App.Diplomdatabase.SaveCategoryAsync(newcategory);
                }
            }

            OnAppearing();
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
		{
            var categoryToDelete = (Category)((ImageButton)sender).CommandParameter;

            var transactions = await App.Diplomdatabase.GetTransactionsAsync();

            bool result = true;

            foreach (var trans in transactions)
            {
                if (trans.CategoryId == categoryToDelete.Id)
                {
                    result = await DisplayAlert("Нарушение связей", $"Существуют транзакции с данной категорией. Удалить категорию?", "Да", "Отмена");
                    if (result)
                        DependencyService.Get<ICustomToast>().ShowCustomToast("Категории у связанных транзакций будут изменены на 'Без категории'", Color.Yellow.ToHex(), Color.Black.ToHex());
                    break;
                }
            }
            if (result)
            {
                foreach (var trans in transactions)
                {
                    if (trans.CategoryId == categoryToDelete.Id)
                    {
                        trans.CategoryId = 1;
                        await App.Diplomdatabase.SaveTransactionAsync(trans);
                    }
                }
                await App.Diplomdatabase.DeleteCategoryAsync(categoryToDelete);
                OnAppearing();
            }
        }

    }
}