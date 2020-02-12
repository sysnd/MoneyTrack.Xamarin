using MoneyTrack.Data;
using MoneyTrack.Models;
using MoneyTrack.Services.ExpenseService;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExpenseService))]

namespace MoneyTrack.Services.ExpenseService
{
    public class ExpenseService : IDataStore<Expense>
    {
        private readonly SQLiteAsyncConnection _connection;
        public ExpenseService()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTableAsync<Expense>();
        }

        public async Task<bool> AddAsync(Expense expense)
        {
            var count = await _connection.InsertAsync(expense);
            return count > 0;
        }

        public async Task<bool> DeleteAsync(object expense)
        {
            var count = await _connection.DeleteAsync(expense);
            return count > 0;
        }

        public async Task<Expense> GetAsync(int id)
        {
            return await _connection.GetAsync<Expense>(id);
        }

        public async Task<IEnumerable<Expense>> GetAsync(bool forceRefresh = false)
        {
            var categories = await _connection.QueryAsync<Expense>("SELECT DISTINCT * FROM Expense");
            return categories;
        }

        public async Task<bool> UpdateAsync(Expense expense)
        {
            var count = await _connection.UpdateAsync(expense);
            return count > 0;
        }
    }
}
