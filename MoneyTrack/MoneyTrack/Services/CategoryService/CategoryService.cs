using MoneyTrack.Data;
using MoneyTrack.Models;
using MoneyTrack.Services.CategoryService;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly:Dependency(typeof(CategoryService))]  
namespace MoneyTrack.Services.CategoryService
{

    public class CategoryService : IDataStore<Category>
    {
        private readonly SQLite.SQLiteConnection _connection;

        public CategoryService()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<Category>();
        }

        public bool Add(Category item)
        {
            var count = _connection.Insert(item);
            return count > 0;
        }

        public bool Delete(object cat)
        {
            var count = _connection.Delete(cat);
            return count > 0;
        }

        public Category Get(int id)
        {
            return _connection.Get<Category>(id);
        }

        public IEnumerable<Category> Get(bool forceRefresh = false)
        {
            var categories = _connection.Query<Category>("SELECT DISTINCT * FROM Category");           
            return categories;
        }

        public bool Update(Category item)
        {
            var count = _connection.Update(item);
            return count > 0;
        }
    }
}
