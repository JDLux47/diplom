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
                var planFromDb = await App.Diplomdatabase.GetPlanAsync(plan.Id);
                if (planFromDb.Deadline.Date != plan.Deadline.Date || planFromDb.Done != plan.Done || planFromDb.Name != plan.Name || planFromDb.Sum != plan.Sum)
                {
                    await App.Diplomdatabase.SavePlanAsync(plan);
                    DependencyService.Get<ICustomToast>().ShowCustomToast("Данные плана были изменены!", Color.Green.ToHex(), Color.White.ToHex());
                }
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