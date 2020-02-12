using SQLite;

namespace MoneyTrack.Data
{
    public interface ISQLite
    {
        SQLiteAsyncConnection GetConnection();
    }
}
