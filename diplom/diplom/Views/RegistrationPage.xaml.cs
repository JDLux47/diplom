using diplom.ComponentsInterface;
using diplom.Interface;
using diplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            EntryInterface entryInterface = new EntryInterface();
            entryInterface.OnlyNumbers(entryBalance);
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(entryName.Text) && !string.IsNullOrEmpty(entryLogin.Text) && !string.IsNullOrEmpty(entryPassword.Text))
            {
                if (entryPassword.Text.Length >= 8 && Regex.IsMatch(entryPassword.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
                {
                    bool isLoginUnique = true;
                    var users = await App.Diplomdatabase.GetUsersAsync();
                    foreach (var user in users)
                    {
                        if (entryLogin.Text == user.Login)
                            isLoginUnique = false;
                    }
                    if (isLoginUnique) 
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
                        {
                            DependencyService.Get<ICustomToast>().ShowCustomToast("Мы зарегистрировали Вас в системе!", Color.Green.ToHex(), Color.White.ToHex());
                            await Navigation.PopAsync();
                        }
                        else
                            DependencyService.Get<ICustomToast>().ShowCustomToast("К сожалению, мы не смогли Вас зарегистрировать :(", Color.Red.ToHex(), Color.White.ToHex());
                    }
                    else
                        DependencyService.Get<ICustomToast>().ShowCustomToast("Пользователь с таким логином уже существует. Пожалуйста, выберите другой логин.", Color.Red.ToHex(), Color.White.ToHex());
                }
                else
                    DependencyService.Get<ICustomToast>().ShowCustomToast("Пароль должен быть не менее 8 символов и содержать знаки, буквы английского алфавита (заглавные и прописные) и цифры.", Color.Red.ToHex(), Color.White.ToHex());
            }
            else
                DependencyService.Get<ICustomToast>().ShowCustomToast("Пожалуйста, заполните поля 'Имя', 'Логин' и 'Пароль'!", Color.Red.ToHex(), Color.White.ToHex());
        }
    }
}