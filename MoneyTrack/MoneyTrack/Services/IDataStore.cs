using System.Collections.Generic;

namespace MoneyTrack.Services
{
    public interface IDataStore<T>
    {
        bool Add(T item);
        bool Update(T item);
        bool Delete(object id);
        T Get(int id);
        IEnumerable<T> Get(bool forceRefresh = false);
    }
}
