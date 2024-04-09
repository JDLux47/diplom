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
    public partial class TransactionInformationPage : ContentPage
    {
        public TransactionInformationPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            if(BindingContext is Transaction transaction)
            {
                BindingContext = await App.Diplomdatabase.GetTransactionAsync(transaction.Id);
            }
            base.OnAppearing();
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            EditTransactionPage editTransactionPage = new EditTransactionPage
            {
                BindingContext = BindingContext
            };
            await Navigation.PushAsync(editTransactionPage);
        }

    }
}