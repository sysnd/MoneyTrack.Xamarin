using MoneyTrack.Data;
using MoneyTrack.Models;
using MoneyTrack.Services.ExpenseService;
using System.Collections.Generic;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExpenseService))]

namespace MoneyTrack.Services.ExpenseService
{
    public class ExpenseService : IDataStore<Expense>
    {
        private readonly SQLite.SQLiteConnection _connection;
        public ExpenseService()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<Expense>();
        }

        public bool Add(Expense item)
        {
            var count = _connection.Insert(item);
            return count > 0;
        }

        public bool Delete(object item)
        {
            var count = _connection.Delete(item);
            return count > 0;
        }

        public Expense Get(int id)
        {
            return _connection.Get<Expense>(id);
        }

        public IEnumerable<Expense> Get(bool forceRefresh = false)
        {
            var expenses = _connection.Query<Expense>("SELECT DISTINCT * FROM Expense");
            return expenses;
        }

        public bool Update(Expense item)
        {
            var count = _connection.Update(item);
            return count > 0;
        }
    }
}
