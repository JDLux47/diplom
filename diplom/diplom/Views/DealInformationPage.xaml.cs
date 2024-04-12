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
	public partial class DealInformationPage : ContentPage
	{
		public DealInformationPage ()
		{
			InitializeComponent ();
        }

		protected override async void OnAppearing()
		{
			if(BindingContext is Deal deal)
			{
                deal = await App.Diplomdatabase.GetDealAsync(deal.Id);

				timepickerCreation.Time = deal.DateOfCreation.TimeOfDay;
				timepickerDeadline.Time = deal.Deadline.TimeOfDay; 

                var importances = await App.Diplomdatabase.GetImportancesAsync();
                pickerImportance.ItemsSource = importances;

                var statuses = await App.Diplomdatabase.GetStatusesAsync();
                pickerStatus.ItemsSource = statuses;

				pickerImportance.SelectedIndex = deal.ImportanceId - 1;
                pickerStatus.SelectedIndex = deal.StatusId - 1;
            }

            base.OnAppearing();
		}

		private async void SaveButton_Clicked(object sender, EventArgs e)
		{
            if (BindingContext is Deal deal)
            {
                bool edit = false;

                DateTime dateDe = new DateTime(datapickerDeadline.Date.Year, datapickerDeadline.Date.Month, datapickerDeadline.Date.Day, timepickerDeadline.Time.Hours, timepickerDeadline.Time.Minutes, 0);
                DateTime dateCr = new DateTime(datapickerCreation.Date.Year, datapickerCreation.Date.Month, datapickerCreation.Date.Day, timepickerCreation.Time.Hours, timepickerCreation.Time.Minutes, 0);

                if (deal.Note != entryNote.Text || deal.Name != entryName.Text || deal.Notification != checkboxNot.IsChecked ||
                    deal.ImportanceId != pickerImportance.SelectedIndex + 1 || deal.StatusId != pickerStatus.SelectedIndex + 1 || 
                    deal.Deadline != dateDe || deal.DateOfCreation != dateCr)
                    edit = true;

                if (edit)
                {
                    deal.Name = entryName.Text;
                    deal.Note = entryNote.Text;
                    deal.Notification = checkboxNot.IsChecked;
                    deal.ImportanceId = pickerImportance.SelectedIndex + 1;
                    deal.StatusId = pickerStatus.SelectedIndex + 1;
                    deal.Deadline = dateDe;
                    deal.DateOfCreation = dateCr;

                    await App.Diplomdatabase.SaveDealAsync(deal);
                    await DisplayAlert("Оповещение", "Данные задачи были изменены!", "OK");
                }
            }
        }

    }
}