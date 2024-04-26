using diplom.Interface;
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
    public partial class PickDatePage : ContentPage
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public PickDatePage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            StartDate = datapickerStartDate.Date;
            EndDate = datapickerEndDate.Date;
            if (StartDate != null && EndDate != null && EndDate > StartDate)
                await Navigation.PopAsync();
            else
                DependencyService.Get<ICustomToast>().ShowCustomToast("Дата начала промежутка больше даты конца промежутка", Color.Red.ToHex(), Color.White.ToHex());
        }
    }
}