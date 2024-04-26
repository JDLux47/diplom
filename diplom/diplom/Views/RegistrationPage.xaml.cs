using diplom.Interface;
using diplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace diplom.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            Entry entryName = (Entry)FindByName("entryName");
            Entry entryLogin = (Entry)FindByName("entryLogin");
            Entry entryPassword = (Entry)FindByName("entryPassword");
            Entry entryBalance = (Entry)FindByName("entryBalance");

            if (!string.IsNullOrEmpty(entryName.Text) && !string.IsNullOrEmpty(entryLogin.Text) && !string.IsNullOrEmpty(entryPassword.Text))
            {
                User newUser = new User
                {
                    Name = entryName.Text,
                    Login = entryLogin.Text,
                    Password = entryPassword.Text,
                    Balance = Convert.ToDecimal(entryBalance.Text)
                };

                bool isSuccess = Convert.ToBoolean(await App.Diplomdatabase.SaveUserAsync(newUser)); // Вставка нового пользователя в базу данных
                if (isSuccess)
                    DependencyService.Get<ICustomToast>().ShowCustomToast("Мы зарегистрировали Вас в системе!", Color.Green.ToHex(), Color.White.ToHex());
                else
                    DependencyService.Get<ICustomToast>().ShowCustomToast("К сожалению, мы не смогли Вас зарегистрировать :(", Color.Red.ToHex(), Color.White.ToHex());
            }
            else
            {
                DependencyService.Get<ICustomToast>().ShowCustomToast("Пожалуйста, заполните поля 'Имя', 'Логин' и 'Пароль'!", Color.Red.ToHex(), Color.White.ToHex());
            }
        }
    }
}