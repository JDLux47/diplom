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
    public partial class Authorization : ContentPage
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void RegisterLabel_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Registration();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var users = await App.Diplomdatabase.GetUsersAsync();
            bool login = true;
            User thisuser = users.FirstOrDefault();
            //foreach (var user in users)
            //{
            //    if (user.Login == EntryLogin.Text && user.Password == EntryPassword.Text)
            //    {
            //        login = true;
            //        thisuser = user;
            //    }
            //}

            if (login)
            {
                // Сохраняем информацию о пользователе в SecureStorage
                SecureStorage.RemoveAll();
                await SecureStorage.SetAsync("Key", thisuser.Id.ToString());

                Application.Current.MainPage = new AppShell();
                await DisplayAlert("Успешная авторизация", "Добро пожаловать, " + thisuser.Name.ToString() + "!", "Приветствую!");
            }
            else
                await DisplayAlert("Неуспешная авторизация", "Нет такого пользователя!", "OK");
        }
    }
}