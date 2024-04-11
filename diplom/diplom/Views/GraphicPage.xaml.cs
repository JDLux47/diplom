using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace diplom.Views
{
    public partial class GraphicPage : ContentPage
    {
        private DonutChart donutChartRas, donutChartDoh;
        public DonutChart DonutChartRas
        {
            get => donutChartRas;
            set
            {
                donutChartRas = value;  
                OnPropertyChanged(); 
            }
        }
        public DonutChart DonutChartDoh
        {
            get => donutChartDoh;
            set
            {
                donutChartDoh = value;
                OnPropertyChanged();
            }
        }

        public List<SKColor> ExpenseColorsDictionary { get; } = new List<SKColor>
        {
            {new SKColor(255, 0, 0)}, // Красный
            {new SKColor(139, 0, 0)}, // Тёмно-красный
            {new SKColor(255, 69, 0)}, // Огненно-оранжевый
            {new SKColor(255, 165, 0) }, // Оранжевый
            {new SKColor(255, 140, 0) }, // Тёмный оранжевый
            {new SKColor(255, 215, 0) }, // Золотисто-жёлтый
            {new SKColor(255, 255, 0) }, // Ярко-жёлтый
            {new SKColor(255, 204, 0) }, // Жёлто-оранжевый
            {new SKColor(255, 218, 185)}, // Персиковый
            {new SKColor(255, 192, 203) }, // Розовый
            {new SKColor(255, 182, 193) }, // Светло-розовый
            {new SKColor(240, 230, 140) }, // Хаки
            {new SKColor(255, 218, 185) }, // Теплый персик
            {new SKColor(250, 128, 114) }, // Лососевый
            {new SKColor(216, 191, 216) }, // Пыльно-розовый
            {new SKColor(238, 173, 14) }, // Тёмно-жёлтый
            {new SKColor(188, 143, 143) }, // Тепло-коричневый
            {new SKColor(255, 248, 220) }, // Кремовый
            {new SKColor(220, 20, 60) }, // Алый
            {new SKColor(219, 112, 147) } // Малиново-красный
        };

        public List<SKColor> IncomeColorsDictionary { get; } = new List<SKColor>
        {
            new SKColor(50, 205, 50),  // Зелёный горошек
            new SKColor(144, 238, 144), // Светло-зелёный
            new SKColor(0, 128, 0),     // Тёмно-зелёный
            new SKColor(32, 178, 170),  // Морской волны
            new SKColor(46, 139, 87),   // Тёмно-зелёная листва
            new SKColor(60, 179, 113),  // Жизненно-зелёный
            new SKColor(0, 255, 127),   // Зелёный спаржи
            new SKColor(127, 255, 212), // Аквамариновый
            new SKColor(152, 251, 152), // Палладиум
            new SKColor(0, 250, 154),   // Палладиевый зелёный
            new SKColor(144, 238, 144), // Светло-зелёный
            new SKColor(173, 255, 47),  // Зелёно-жёлтый
            new SKColor(107, 142, 35),  // Зелёно-коричневый
            new SKColor(124, 252, 0)     // Ярко-зелёный
        };

        public GraphicPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            DisplayChart();
            BindingContext = this;
            base.OnAppearing();
        }

        private void GiveEntries(List<Models.Transaction> transactions, List<Models.Category> categories, int Type)
        {
            var spendingByCategory = categories.Select(category => new //список категорий и суммы потраченных на неё денег
            {
                Category = category,
                TotalSpent = transactions.Where(transaction => transaction.CategoryId == category.Id && transaction.Type == Type).Sum(transaction => transaction.Sum)
            });

            spendingByCategory = spendingByCategory.Where(category => category.TotalSpent > 0);

            var chartEntries = spendingByCategory.Select(item => new ChartEntry((float)item.TotalSpent)
            {
                Label = item.Category.Name,
                ValueLabel = item.TotalSpent.ToString(), // Преобразование суммы в строку для отображения
            }).ToList();

            for (int i = 0; i < chartEntries.Count(); i++)
            {
                chartEntries[i].Color = ExpenseColorsDictionary[i];
                chartEntries[i].TextColor = ExpenseColorsDictionary[i];
                chartEntries[i].ValueLabelColor = ExpenseColorsDictionary[i];
            }

            if (Type == -1)
            {
                for (int i = 0; i < chartEntries.Count(); i++)
                {
                    chartEntries[i].Color = ExpenseColorsDictionary[i];
                    chartEntries[i].TextColor = ExpenseColorsDictionary[i];
                    chartEntries[i].ValueLabelColor = ExpenseColorsDictionary[i];
                }
                DonutChartRas = new DonutChart { Entries = chartEntries, LabelTextSize = 30f, BackgroundColor = SKColor.Empty };
            }
            else
            {
                for (int i = 0; i < chartEntries.Count(); i++)
                {
                    chartEntries[i].Color = IncomeColorsDictionary[i];
                    chartEntries[i].TextColor = IncomeColorsDictionary[i];
                    chartEntries[i].ValueLabelColor = IncomeColorsDictionary[i];
                }
                DonutChartDoh = new DonutChart { Entries = chartEntries, LabelTextSize = 30f, BackgroundColor = SKColor.Empty };
            }   
        }

        private async void DisplayChart()
        {
            var transactions = await App.Diplomdatabase.GetTransactionsAsync();
            var categories = await App.Diplomdatabase.GetCategoriesAsync();
            
            GiveEntries(transactions, categories, -1);
            GiveEntries(transactions, categories, 1);
        }
    }
}