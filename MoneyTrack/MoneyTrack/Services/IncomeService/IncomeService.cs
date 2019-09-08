using MoneyTrack.Data;
using MoneyTrack.Models;
using MoneyTrack.Services.IncomeService;
using System.Collections.Generic;
using Xamarin.Forms;

[assembly: Dependency(typeof(IncomeService))]
namespace MoneyTrack.Services.IncomeService
{
    public class IncomeService : IDataStore<Income>
    {
        private readonly SQLite.SQLiteConnection _connection;

        public IncomeService()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<Income>();
        }
        public bool Add(Income item)
        {
            var count = _connection.Insert(item);
            return count > 0;
        }

        public bool Delete(object income)
        {
            var count = _connection.Delete(income);
            return count > 0;
        }

        public Income Get(int id)
        {
            return _connection.Get<Income>(id);
        }

        public IEnumerable<Income> Get(bool forceRefresh = false)
        {
            var income = _connection.Query<Income>("SELECT DISTINCT * FROM Income");
            return income;
        }

        public bool Update(Income item)
        {
            var count = _connection.Update(item);
            return count > 0;
        }
    }
}
