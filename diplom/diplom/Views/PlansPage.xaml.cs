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
	public partial class PlansPage : ContentPage
	{
		public PlansPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            var plans = await App.Diplomdatabase.GetPlansAsync();

            PlansView.ItemsSource = plans;

            base.OnAppearing();
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Plan selectedItem)
            {
                PlanInfoPage planInfoPage = new PlanInfoPage
                {
                    BindingContext = selectedItem 
                };
                await Navigation.PushAsync(planInfoPage);
            }
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPlanPage());
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}