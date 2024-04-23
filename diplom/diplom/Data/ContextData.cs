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
                    new Status { Name = "Просрочено"},
                    new Status { Name = "Сделано c опозданием"}
                };
                foreach (Status p in statuses)
                {
                    await diplomdatabase.SaveStatusAsync(p);
                }

                var categories = new Category[]
                {
                    new Category { Name = "Без категории"},
                    new Category { Name = "Продукты", UserId = 1},
                    new Category { Name = "Интернет", UserId = 1},
                    new Category { Name = "Зарплата", UserId = 1},
                    new Category { Name = "Пенсия", UserId = 1},
                    new Category { Name = "Транспорт", UserId = 1},
                    new Category { Name = "Инвестиции", UserId = 1}
                };
                foreach (Category p in categories)
                {
                    await diplomdatabase.SaveCategoryAsync(p);
                }

                var transactions = new Transaction[]
                {
                    new Transaction { Sum = 1000, Type = 1, Date = new DateTime(2023, 12, 13), CategoryId = 1, UserId = 1},
                    new Transaction { Sum = 1200, Type = 1, Date = new DateTime(2023, 11, 11), CategoryId = 1, UserId = 1},
                    new Transaction { Sum = 1500, Type = -1, Date = new DateTime(2023, 10, 24), CategoryId = 1, UserId = 1},
                    new Transaction { Sum = 600, Type = -1, Date = new DateTime(2024, 01, 18), CategoryId = 2, UserId = 1},
                    new Transaction { Sum = 860, Type = -1, Date = new DateTime(2024, 02, 07), CategoryId = 2, UserId = 1 },
                    new Transaction { Sum = 450, Type = -1, Date = new DateTime(2024, 02, 11), CategoryId = 3, UserId = 1},
                    new Transaction { Sum = 350, Type = -1, Date = new DateTime(2024, 03, 27), CategoryId = 3, UserId = 1},
                    new Transaction { Sum = 10000, Type = 1, Date = new DateTime(2024, 04, 05), CategoryId = 4, UserId = 1},
                    new Transaction { Sum = 15000, Type = 1, Date = new DateTime(2024, 04, 07), CategoryId = 4, UserId = 1 },
                    new Transaction { Sum = 13000, Type = 1, Date = new DateTime(2024, 04, 11), CategoryId = 5, UserId = 1},
                    new Transaction { Sum = 400, Type = -1, Date = new DateTime(2024, 03, 25), CategoryId = 6, UserId = 1},
                    new Transaction { Sum = 190, Type = -1, Date = new DateTime(2024, 01, 14), CategoryId = 6, UserId = 1},
                    new Transaction { Sum = 1400, Type = 1, Date = new DateTime(2024, 02, 07), CategoryId = 7, UserId = 1 },
                };
                foreach (Transaction p in transactions)
                {
                    await diplomdatabase.SaveTransactionAsync(p);
                }

                var plans = new Plan[]
                {
                    new Plan { Name = "Телевизор", Sum = 25000, Deadline = new DateTime(2024, 03, 14), UserId = 1, Done = false},
                    new Plan { Name = "Машина", Sum = 750000, Deadline = new DateTime(2024, 02, 11), UserId = 1, Done = true},
                    new Plan { Name = "Отпуск", Sum = 100000, Deadline = new DateTime(2024, 03, 24), UserId = 1, Done = false}
                };
                foreach (Plan p in plans)
                {
                    await diplomdatabase.SavePlanAsync(p);
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
                        StatusId = 4,
                        UserId = 1
                    },
                    new Deal {
                        Name = "Сделать ТЗ",
                        DateOfCreation = new DateTime(2024, 03, 14, 12, 30, 0),
                        Deadline = new DateTime(2024, 03, 24, 17, 0, 0),
                        Notification = true,
                        Note = "О нет, опять тз..",
                        ImportanceId = 1,
                        StatusId = 5,
                        UserId = 1
                    },
                    new Deal {
                        Name = "Сделать зарядку",
                        DateOfCreation = new DateTime(2024, 02, 8, 8, 0, 0),
                        Deadline = new DateTime(2024, 02, 8, 9, 0, 0),
                        Notification = true,
                        Note = "Пресс, отжимания, подтягивания",
                        ImportanceId = 2,
                        StatusId = 6,
                        UserId = 1
                    },
                    new Deal {
                        Name = "Сходить на концерт",
                        DateOfCreation = new DateTime(2023, 12, 14, 17, 0, 0),
                        Deadline = new DateTime(2024, 03, 14, 19, 0, 0),
                        Notification = true,
                        Note = "Концерт Клавы Коки",
                        ImportanceId = 1,
                        StatusId = 3,
                        UserId = 1
                    },
                    new Deal {
                        Name = "Делать диплом",
                        DateOfCreation = new DateTime(2023, 11, 14, 17, 0, 0),
                        Deadline = new DateTime(2024, 05, 16, 19, 0, 0),
                        Notification = true,
                        ImportanceId = 2,
                        StatusId = 1,
                        UserId = 1
                    },
                    new Deal {
                        Name = "Сходить погулять",
                        DateOfCreation = new DateTime(2023, 10, 14, 17, 0, 0),
                        Deadline = new DateTime(2024, 05, 19, 19, 0, 0),
                        Notification = true,
                        ImportanceId = 1,
                        StatusId = 2,
                        UserId = 1
                    },
                    new Deal {
                        Name = "Отвезти машину в сервис",
                        DateOfCreation = new DateTime(2024, 04, 14, 17, 0, 0),
                        Deadline = new DateTime(2024, 05, 18, 19, 0, 0),
                        Notification = true,
                        ImportanceId = 2,
                        StatusId = 3,
                        UserId = 1
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
