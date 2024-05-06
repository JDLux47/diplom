using Android.Widget;
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
    public partial class AuthorizationPage : ContentPage
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private async void RegisterLabel_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var users = await App.Diplomdatabase.GetUsersAsync();
            bool login = false;
            //App.LoggedInUser = users.FirstOrDefault();
            if(EntryLogin.Text != null && EntryPassword.Text != null)
            {
                foreach (var user in users)
                {
                    if (user.Login == EntryLogin.Text && user.Password == EntryPassword.Text)
                    {
                        login = true;
                        App.LoggedInUser = user;
                        SecureStorage.Remove("User");
                        if (Checkbox.IsChecked)
                            await SecureStorage.SetAsync("User", user.Id.ToString());
                    }
                }
                if (login)
                    Application.Current.MainPage = new AppShell();
                else
                    DependencyService.Get<ICustomToast>().ShowCustomToast("Логин или пароль введён неверно!", Color.Red.ToHex(), Color.White.ToHex());
            }
            else
                DependencyService.Get<ICustomToast>().ShowCustomToast("Ошибка! Пожалуйста, заполните все поля", Color.Red.ToHex(), Color.White.ToHex());
        }
    }
}