using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyTrack.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(object id);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAsync(bool forceRefresh = false);
    }
}
