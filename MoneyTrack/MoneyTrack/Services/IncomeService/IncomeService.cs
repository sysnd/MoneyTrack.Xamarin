using MoneyTrack.Data;
using MoneyTrack.Models;
using MoneyTrack.Services.IncomeService;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(IncomeService))]
namespace MoneyTrack.Services.IncomeService
{
    public class IncomeService : IDataStore<Income>
    {
        private readonly SQLiteAsyncConnection _connection;

        public IncomeService()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTableAsync<Income>();
        }
        public async Task<bool> AddAsync(Income income)
        {
            var count = await _connection.InsertAsync(income);
            return count > 0;
        }

        public async Task<bool> DeleteAsync(object income)
        {
            var count = await _connection.DeleteAsync(income);
            return count > 0;
        }

        public async Task<Income> GetAsync(int id)
        {
            return await _connection.GetAsync<Income>(id);
        }

        public async Task<IEnumerable<Income>> GetAsync(bool forceRefresh = false)
        {
            var income = await _connection.QueryAsync<Income>("SELECT DISTINCT * FROM Income");
            return income;
        }

        public async Task<bool> UpdateAsync(Income income)
        {
            var count = await _connection.UpdateAsync(income);
            return count > 0;
        }
    }
}
