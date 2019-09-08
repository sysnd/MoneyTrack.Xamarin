using SQLite;

namespace MoneyTrack.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
