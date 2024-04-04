using diplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diplom.Data
{
    public class ContextData
    {
        public static async Task SeedAsync(DB diplomdatabase)
        {
            try
            {
                var importances = new Importance[]
                {
                    new Importance { Level = "Низкий"},
                    new Importance { Level = "Средний"},
                    new Importance { Level = "Высокий"}
                };
                foreach (Importance p in importances)
                {
                    await diplomdatabase.SaveImportanceAsync(p);
                }

                var statuses = new Status[]
                {
                    new Status { Name = "В работе"},
                    new Status { Name = "Отложено"},
                    new Status { Name = "Сделано"},
                    new Status { Name = "Отменено"},
                    new Status { Name = "Просрочено"}
                };
                foreach (Status p in statuses)
                {
                    await diplomdatabase.SaveStatusAsync(p);
                }

                var categories = new Category[]
                {
                    new Category { Name = "Транспорт"},
                    new Category { Name = "Продукты" },
                    new Category { Name = "Интернет" }
                };
                foreach (Category p in categories)
                {
                    await diplomdatabase.SaveCategoryAsync(p);
                }

                var transactions = new Transaction[]
                {
                    new Transaction { Sum = 150, Type = 1, Date = new DateTime(2024, 03, 14), CategoryId = 1, UserId = 1},
                    new Transaction { Sum = 1200, Type = 1, Date = new DateTime(2024, 02, 11), CategoryId = 2, UserId = 1},
                    new Transaction { Sum = 350, Type = -1, Date = new DateTime(2024, 03, 24), CategoryId = 3, UserId = 1},
                    new Transaction { Sum = 190, Type = 1, Date = new DateTime(2024, 01, 20), CategoryId = 1, UserId = 2},
                    new Transaction { Sum = 700, Type = -1, Date = new DateTime(2024, 02, 07), CategoryId = 2, UserId = 2 }
                };
                foreach (Transaction p in transactions)
                {
                    await diplomdatabase.SaveTransactionAsync(p);
                }

                var deals = new Deal[]
                {
                    new Deal {
                        Name = "Сделать лабу по программированию",
                        DateOfCreation = new DateTime(2024, 01, 24, 15, 0, 0),
                        Deadline = new DateTime(2024, 01, 27, 15, 0, 0),
                        Notification = false,
                        Note = "It's so hard",
                        ImportanceId = 2,
                        StatusId = 3,
                        UserId = 1
                    },
                    new Deal {
                        Name = "Сделать ТЗ",
                        DateOfCreation = new DateTime(2024, 03, 14, 12, 30, 0),
                        Deadline = new DateTime(2024, 03, 24, 17, 0, 0),
                        Notification = true,
                        Note = "О нет, опять тз..",
                        ImportanceId = 1,
                        StatusId = 2,
                        UserId = 1
                    },
                    new Deal {
                        Name = "Сделать зарядку",
                        DateOfCreation = new DateTime(2024, 02, 8, 8, 0, 0),
                        Deadline = new DateTime(2024, 02, 8, 9, 0, 0),
                        Notification = true,
                        Note = "Пресс, отжимания, подтягивания",
                        ImportanceId = 2,
                        StatusId = 1,
                        UserId = 1
                    },
                    new Deal {
                        Name = "Сходить на концерт",
                        DateOfCreation = new DateTime(2024, 03, 14, 17, 0, 0),
                        Deadline = new DateTime(2024, 03, 14, 19, 0, 0),
                        Notification = false,
                        Note = "Концерт Клавы Коки",
                        ImportanceId = 4,
                        StatusId = 3,
                        UserId = 2
                    }
                };
                foreach (Deal p in deals)
                {
                    await diplomdatabase.SaveDealAsync(p);
                }

                var users = new User[]
                {
                    new User{Name="Иван", Login = "Ivan123", Password = "11111111", Balance = 10000,
                        Transactions = transactions.Where(t => t.UserId == 1).ToList(),
                        Deals = deals.Where(t => t.UserId == 1).ToList()
                    },
                    new User{Name="Пётр", Login = "Petr97", Password = "22222222", Balance = 20000,
                        Transactions = transactions.Where(t => t.UserId == 2).ToList(),
                        Deals = deals.Where(t => t.UserId == 2).ToList()
                    },
                    new User{Name="Алексей", Login = "Alex20", Password = "33333333", Balance = 30000,
                        Transactions = transactions.Where(t => t.UserId == 3).ToList(),
                        Deals = deals.Where(t => t.UserId == 3).ToList()
                    }
                };
                foreach (User p in users)
                {
                    await diplomdatabase.SaveUserAsync(p);
                }

            }
            catch
            {
                throw;
            }
        }
    }
}
