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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            List<User> userList = new List<User>
            {
                App.LoggedInUser
            };
            userView.ItemsSource = userList;
            base.OnAppearing();
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AuthorizationPage());

            SecureStorage.Remove("User");

            await Navigation.PopToRootAsync();
        }
    }
}