using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace diplom.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SecondGraphicPage : ContentPage
	{
        private BarChart barChartRas, barChartDoh;
        public BarChart BarChartRas
        {
            get => barChartRas;
            set
            {
                barChartRas = value;
                OnPropertyChanged();
            }
        }
        public BarChart BarChartDoh
        {
            get => barChartDoh;
            set
            {
                barChartDoh = value;
                OnPropertyChanged();
            }
        }

        public SecondGraphicPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            DisplayChartAsync(-1);
            DisplayChartAsync(1);
            BindingContext = this;
            base.OnAppearing();
        }

        private async void DisplayChartAsync(int Type)
        {
            var transactions = await App.Diplomdatabase.GetTransactionsAsync();

            DateTime currentDate = DateTime.Now;
            DateTime startDate = currentDate.AddMonths(-5); // Начиная с текущего месяца, предыдущие 5 месяцев

            var groupedTransactions = transactions
                                      .Where(t => t.Type == Type && t.Date >= startDate) // Учитываем только последние 6 месяцев
                                      .GroupBy(t => new { t.Date.Year, t.Date.Month })
                                      .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month) // Сортировка по возрастанию
                                      .Select(g => new { g.Key.Year, g.Key.Month, TotalSum = g.Sum(t => t.Sum) })
                                      .ToList();

            var entries = new List<ChartEntry>();

            if (Type != 1)
            {
                foreach (var group in groupedTransactions)
                {
                    // Добавление каждого месяца как точки данных на диаграмме
                    entries.Add(new ChartEntry((float)group.TotalSum)
                    {
                        Label = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Month)} {group.Year}",
                        ValueLabel = group.TotalSum.ToString(),
                        Color = SKColors.Red,
                    });
                }
                BarChartRas = new BarChart { Entries = entries, LabelTextSize = 30f, LabelOrientation = Orientation.Horizontal, ValueLabelOrientation = Orientation.Horizontal, BackgroundColor = SKColors.Transparent };
            }
            else
            {
                foreach (var group in groupedTransactions)
                {
                    // Добавление каждого месяца как точки данных на диаграмме
                    entries.Add(new ChartEntry((float)group.TotalSum)
                    {
                        Label = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Month)} {group.Year}",
                        ValueLabel = group.TotalSum.ToString(),
                        Color = SKColors.SpringGreen,
                    });
                }
                BarChartDoh = new BarChart { Entries = entries, LabelTextSize = 30f, LabelOrientation = Orientation.Horizontal, ValueLabelOrientation = Orientation.Horizontal, BackgroundColor = SKColors.Transparent };
            }
        }
    }
}