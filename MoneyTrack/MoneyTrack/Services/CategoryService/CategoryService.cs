using MoneyTrack.Data;
using MoneyTrack.Models;
using MoneyTrack.Services.CategoryService;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(CategoryService))]
namespace MoneyTrack.Services.CategoryService
{

    public class CategoryService : IDataStore<Category>
    {
        private readonly SQLiteAsyncConnection _connection;

        public CategoryService()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTableAsync<Category>();
        }

        public async Task<bool> AddAsync(Category category)
        {
            var count = await _connection.InsertAsync(category);
            return count > 0;
        }

        public async Task<bool> DeleteAsync(object category)
        {
            var count = await _connection.DeleteAsync(category);
            return count > 0;
        }

        public async Task<Category> GetAsync(int id)
        {
            return await _connection.GetAsync<Category>(id);
        }

        public async Task<IEnumerable<Category>> GetAsync(bool forceRefresh = false)
        {
            var categories = await _connection.QueryAsync<Category>("SELECT DISTINCT * FROM Category");           
            return categories;
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            var count = await _connection.UpdateAsync(category);
            return count > 0;
        }
    }
}
