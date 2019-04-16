using System;
using System.IO;
using SQLite;

namespace DataLayer
{
    public class DBHelper : IDisposable
    {
        public SQLiteConnection DB { get; set; }

        public DBHelper()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Constants.DBName);
            DB = new SQLiteConnection(dbPath);
        }

        public void Dispose()
        {
            DB.Dispose();
        }
    }
}
