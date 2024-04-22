using diplom.Models;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace diplom.Views
{
    public partial class GraphicPage : ContentPage
    {
        private DonutChart donutChartRas, donutChartDoh, donutChartDeals;
        public DonutChart DonutChartRas
        {
            get => donutChartRas;
            set
            {
                donutChartRas = value;  
                OnPropertyChanged(); 
            }
        }

        public DonutChart DonutChartDeals
        {
            get => donutChartDeals;
            set
            {
                donutChartDeals = value;
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

        public DateTime StartDate, EndDate = DateTime.Now;
        public string time="";

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
            var filteredtransactions = transactions.Where(transaction => transaction.Date >= StartDate && transaction.Date <= EndDate);

            var spendingByCategory = categories.Select(category => new //список категорий и суммы потраченных на неё денег
            {
                Category = category,
                TotalSpent = filteredtransactions.Where(transaction => transaction.CategoryId == category.Id && transaction.Type == Type).Sum(transaction => transaction.Sum)
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
                if (chartEntries.Count() != 0)
                {
                    DonutChartRas = new DonutChart { Entries = chartEntries, LabelTextSize = 30f, BackgroundColor = SKColor.Empty };
                    DiagramRas.IsVisible = true;
                    CostTitleLabel.IsVisible = true;
                    CostLabel.IsVisible = true;
                    CostLabel.Text = time;
                }
                else
                {
                    CostLabel.IsVisible = false;
                    DiagramRas.IsVisible = false;
                    CostTitleLabel.IsVisible = false;
                }
            }
            else
            {
                for (int i = 0; i < chartEntries.Count(); i++)
                {
                    chartEntries[i].Color = IncomeColorsDictionary[i];
                    chartEntries[i].TextColor = IncomeColorsDictionary[i];
                    chartEntries[i].ValueLabelColor = IncomeColorsDictionary[i];
                }

                if(chartEntries.Count() != 0)
                {
                    DonutChartDoh = new DonutChart { Entries = chartEntries, LabelTextSize = 30f, BackgroundColor = SKColor.Empty };
                    DiagramDoh.IsVisible = true;
                    IncomeTitleLabel.IsVisible = true;
                    IncomeLabel.Text = time;
                    IncomeLabel.IsVisible = true;
                }
                else
                {
                    IncomeLabel.IsVisible = false;
                    DiagramDoh.IsVisible = false;
                    IncomeTitleLabel.IsVisible = false;
                }
            }
        }

        private void DealsDisplay(List<Models.Deal> deals, List<Models.Status> statuses)
        {
            var dealsAfterStartDate = deals.Where(deal => deal.DateOfCreation >= StartDate && deal.DateOfCreation <= EndDate);

            var dealsByStatus = statuses.Select(status => new
            {
                Status = status,
                DealCount = dealsAfterStartDate.Count(deal => deal.StatusId == status.Id)
            });

            var chartEntries = dealsByStatus.Select(item => new ChartEntry(item.DealCount)
            {
                Label = item.Status.Name,
                ValueLabel = item.DealCount.ToString(),
            }).ToList();

            for (int i = 0; i < chartEntries.Count; i++)
            {
                switch (chartEntries[i].Label)
                {
                    case "В работе":
                        chartEntries[i].Color = SKColor.Parse("#EE82EE");
                        chartEntries[i].ValueLabelColor = SKColor.Parse("#EE82EE");
                        chartEntries[i].TextColor = SKColor.Parse("#EE82EE");
                        break;
                    case "Отложено":
                        chartEntries[i].Color = SKColor.Parse("#B8860B");
                        chartEntries[i].ValueLabelColor = SKColor.Parse("#B8860B");
                        chartEntries[i].TextColor = SKColor.Parse("#B8860B");
                        break;
                    case "Сделано":
                        chartEntries[i].Color = SKColor.Parse("#00FF00");
                        chartEntries[i].ValueLabelColor = SKColor.Parse("#00FF00");
                        chartEntries[i].TextColor = SKColor.Parse("#00FF00");
                        break;
                    case "Отменено":
                        chartEntries[i].Color = SKColor.Parse("#800000");
                        chartEntries[i].ValueLabelColor = SKColor.Parse("#800000");
                        chartEntries[i].TextColor = SKColor.Parse("#800000");
                        break;
                    default:
                        chartEntries[i].Color = SKColor.Parse("#FF0000");
                        chartEntries[i].ValueLabelColor = SKColor.Parse("#FF0000");
                        chartEntries[i].TextColor = SKColor.Parse("#FF0000");
                        break;
                }
            }
            if (chartEntries[0].Value != 0 || chartEntries[1].Value != 0 || chartEntries[2].Value != 0 || chartEntries[3].Value != 0 || chartEntries[4].Value != 0)
            {
                DonutChartDeals = new DonutChart { Entries = chartEntries, LabelTextSize = 30f, BackgroundColor = SKColor.Empty };
                DealsTitleLabel.IsVisible = true;
                DiagramDeals.IsVisible = true;
                DealsLabel.IsVisible = true;
                DealsLabel.Text = time;
            }
            else
            {
                DealsTitleLabel.IsVisible = false;
                DiagramDeals.IsVisible = false;
                DealsLabel.IsVisible = false;
            }
        }

        private async void DisplayChart()
        {
            var transactions = await App.Diplomdatabase.GetTransactionsAsync();
            var categories = await App.Diplomdatabase.GetCategoriesAsync();
            var deals = await App.Diplomdatabase.GetDealsAsync();
            var statuses = await App.Diplomdatabase.GetStatusesAsync();
            
            GiveEntries(transactions, categories, -1);
            GiveEntries(transactions, categories, 1);
            DealsDisplay(deals, statuses);
            Filter();
        }

        private void DayButton_Clicked(object sender, EventArgs e)
        {
            StartDate = DateTime.Now.AddDays(-1);
            EndDate = DateTime.Now;
            time = "За последние сутки";
            OnAppearing();
        }

        private void WeekButton_Clicked(object sender, EventArgs e)
        {
            StartDate = DateTime.Now.AddDays(-7);
            EndDate = DateTime.Now;
            time = "За последнюю неделю";
            OnAppearing();
        }

        private void MonthButton_Clicked(object sender, EventArgs e)
        {
            StartDate = DateTime.Now.AddDays(-30);
            EndDate = DateTime.Now;
            time = "За последний месяц";
            OnAppearing();
        }

        private void YearButton_Clicked(object sender, EventArgs e)
        {
            StartDate = DateTime.Now.AddDays(-365);
            EndDate = DateTime.Now;
            time = "За последний год";
            OnAppearing();
        }

        private void AllTimeButton_Clicked (object sender, EventArgs e)
        {
            StartDate = DateTime.MinValue;
            CostLabel.IsVisible = false;
            IncomeLabel.IsVisible = false;
            DealsLabel.IsVisible = false;
            time = "За всё время";
            OnAppearing();
        }

        private async void YourTimeButton_Clicked(object sender, EventArgs e)
        {
            PickDatePage pickDatePage = new PickDatePage();
            await Navigation.PushAsync(pickDatePage);
            pickDatePage.Disappearing += (senderObj, eventArgs) =>
            {
                    var page = senderObj as PickDatePage;
                if (page != null )
                {
                    if (page.StartDate != DateTime.MinValue && page.EndDate != DateTime.MinValue)
                    {
                        StartDate = page.StartDate;
                        EndDate = page.EndDate;
                        time = StartDate.Date.ToShortDateString() + "-" + EndDate.Date.ToShortDateString();
                        OnAppearing();
                    }
                }
            };
        }

        private void Filter()
        {
            if (!CostTitleLabel.IsVisible && !IncomeTitleLabel.IsVisible && !DealsTitleLabel.IsVisible)
            {
                CostLabel.Text = "Нет данных за указанный промежуток времени...";
                CostLabel.IsVisible = true;
            }
        }

        private async void BarButton_Clicked(object sender, EventArgs e)
        {
            SecondGraphicPage secondGraphicPage = new SecondGraphicPage();
            await Navigation.PushAsync(secondGraphicPage);
        }
    }
}