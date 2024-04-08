using diplom.ComponentsInterface;
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
    public partial class AddTransactionPage : ContentPage
    {
        public AddTransactionPage()
        {
            InitializeComponent();

            EntryInterface entryInterface = new EntryInterface();
            entryInterface.OnlyNumbers(entrySum);
        }

        protected override async void OnAppearing()
        {
            List<Category> categories = await App.Diplomdatabase.GetCategoriesAsync();
            pickerCategory.ItemsSource = categories;

            base.OnAppearing();
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            int type = 1;
            if (pickerType.SelectedIndex != 0)
                type = -1;

            Transaction transaction = new Transaction
            {
                Date = DateTime.Now,
                Sum = Convert.ToDecimal(entrySum.Text),
                CategoryId = pickerCategory.SelectedIndex + 1,
                Type = type,
                UserId = App.LoggedInUser.Id
            };

            await App.Diplomdatabase.SaveTransactionAsync(transaction);
            await Navigation.PopAsync();
        }
    }
}