using diplom.ComponentsInterface;
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
	public partial class AddPlanPage : ContentPage
	{
		public AddPlanPage ()
		{
			InitializeComponent ();

            EntryInterface entryInterface = new EntryInterface();
            entryInterface.OnlyNumbers(entrySum);
        }

        protected override async void OnDisappearing()
        {
            await Navigation.PopAsync();
            base.OnDisappearing();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
		{
            if (entrySum.Text != null && entryName.Text != null)
            {
                if(DateTime.Now < datapickerDeadline.Date)
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
                    DependencyService.Get<ICustomToast>().ShowCustomToast("Указанная дата уже не актуальна! Пожалуйста, укажите будущую дату", Color.Red.ToHex(), Color.White.ToHex());
            }
            else
                DependencyService.Get<ICustomToast>().ShowCustomToast("Не все обязательные поля заполнены!", Color.Red.ToHex(), Color.White.ToHex());
        }

    }
}