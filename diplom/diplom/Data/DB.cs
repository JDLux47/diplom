using diplom.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace diplom.Data
{
    public class DB
    {
        private readonly SQLiteAsyncConnection db;

        public DB(string path)
        {
            db = new SQLiteAsyncConnection(path);

            db.CreateTableAsync<Importance>().Wait();
            db.CreateTableAsync<Status>().Wait();
            db.CreateTableAsync<Transaction>().Wait();
            db.CreateTableAsync<Deal>().Wait();
            db.CreateTableAsync<User>().Wait();
            db.CreateTableAsync<Category>().Wait();

        }

        #region User
        public Task<List<User>> GetUsersAsync() //получение всех
        {
            return db.Table<User>().ToListAsync();
        }

        public Task<User> GetUserAsync(int id) //получение одного 
        {
            return db.Table<User>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAsync(User user) //добавление или обновление строки
        {
            if (user.Id != 0)
                return db.UpdateAsync(user);
            else
                return db.InsertAsync(user);
        }

        public Task<int> DeleteUserAsync(User user) //удаление строки
        {
            return db.DeleteAsync(user);
        }
        #endregion

        #region Transaction
        public Task<List<Transaction>> GetTransactionsAsync() //получение всех
        {
            return db.Table<Transaction>().ToListAsync();
        }

        public Task<Transaction> GetTransactionAsync(int id) //получение одного 
        {
            return db.Table<Transaction>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveTransactionAsync(Transaction transaction) //добавление или обновление строки
        {
            if (transaction.Id != 0)
                return db.UpdateAsync(transaction);
            else
                return db.InsertAsync(transaction);
        }

        public Task<int> DeleteTransactionAsync(Transaction transaction) //удаление строки
        {
            return db.DeleteAsync(transaction);
        }
        #endregion

        #region Deal
        public Task<List<Deal>> GetDealsAsync() //получение всех
        {
            return db.Table<Deal>().ToListAsync();
        }

        public Task<Deal> GetDealAsync(int id) //получение одного 
        {
            return db.Table<Deal>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveDealAsync(Deal deal) //добавление или обновление строки
        {
            if (deal.Id != 0)
                return db.UpdateAsync(deal);
            else
                return db.InsertAsync(deal);
        }

        public Task<int> DeleteDealAsync(Deal deal) //удаление строки
        {
            return db.DeleteAsync(deal);
        }
        #endregion

        #region Importance
        public Task<List<Importance>> GetImportancesAsync() //получение всех
        {
            return db.Table<Importance>().ToListAsync();
        }

        public Task<Importance> GetImportanceAsync(int id) //получение одного 
        {
            return db.Table<Importance>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveImportanceAsync(Importance importance) //добавление или обновление строки
        {
            if (importance.Id != 0)
                return db.UpdateAsync(importance);
            else
                return db.InsertAsync(importance);
        }

        public Task<int> DeleteImportanceAsync(Importance importance) //удаление строки
        {
            return db.DeleteAsync(importance);
        }
        #endregion

        #region Status
        public Task<List<Status>> GetStatusesAsync() //получение всех
        {
            return db.Table<Status>().ToListAsync();
        }

        public Task<Status> GetStatusAsync(int id) //получение одного 
        {
            return db.Table<Status>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveStatusAsync(Status status) //добавление или обновление строки
        {
            if (status.Id != 0)
                return db.UpdateAsync(status);
            else
                return db.InsertAsync(status);
        }

        public Task<int> DeleteStatusAsync(Status status) //удаление строки
        {
            return db.DeleteAsync(status);
        }
        #endregion

        #region Category
        public Task<List<Category>> GetCategoriesAsync() //получение всех
        {
            return db.Table<Category>().ToListAsync();
        }

        public Task<Category> GetCategoryAsync(int id) //получение одного 
        {
            return db.Table<Category>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveCategoryAsync(Category category) //добавление или обновление строки
        {
            if (category.Id != 0)
                return db.UpdateAsync(category);
            else
                return db.InsertAsync(category);
        }

        public Task<int> DeleteCategoryAsync(Category category) //удаление строки
        {
            return db.DeleteAsync(category);
        }
        #endregion

    }
}
