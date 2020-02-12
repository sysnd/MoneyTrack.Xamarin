using MoneyTrack.Data;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteBase))]
namespace MoneyTrack.Data
{
    public class SQLiteBase : ISQLite
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var fileName = "MoneyTrack.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);

            var connection = new SQLiteAsyncConnection(path);

            return connection;
        }
    }
}
