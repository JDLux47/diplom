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
	public partial class AddPlanPage : ContentPage
	{
		public AddPlanPage ()
		{
			InitializeComponent ();

            EntryInterface entryInterface = new EntryInterface();
            entryInterface.OnlyNumbers(entrySum);
        }

		private async void SaveButton_Clicked(object sender, EventArgs e)
		{
            if (entrySum != null && entryName != null)
            {
                Plan plan = new Plan
                {
                    Name = entryName.Text,
                    Sum = Convert.ToDecimal(entrySum.Text),
                    Deadline = datapickerDeadline.Date,
                    Done = false,
                    UserId = App.LoggedInUser.Id
                };

                await App.Diplomdatabase.SaveUserAsync(App.LoggedInUser);
                await App.Diplomdatabase.SavePlanAsync(plan);
                await Navigation.PopAsync();
            }
            else
                await DisplayAlert("Ошибка!", "Не все обязательные поля заполнены!", "Назад");
        }

    }
}