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
	public partial class PlanInfoPage : ContentPage
	{
		public PlanInfoPage()
		{
			InitializeComponent();
		}

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is Plan plan)
            {
                await App.Diplomdatabase.SavePlanAsync(plan);
                await DisplayAlert("Оповещение", "Данные плана были изменены!", "OK");
                //можно брать план из бд и сверять
            }
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is Plan plan)
            {
                bool result = await DisplayAlert("Подтверждение", "Вы уверены, что хотите удалить данный план из базы данных?", "Да", "Отмена");

                if (result)
                {
                    await App.Diplomdatabase.DeletePlanAsync(plan);
                    await Navigation.PopAsync();
                }
            }
        }
    }
}