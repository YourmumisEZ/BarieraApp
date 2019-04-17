using System;
using DataLayer.DataModels;
using SQLite;

namespace DataLayer.Repositories
{
    public class TelephoneRepository : IDisposable
    {
        private SQLiteConnection context;

        public TelephoneRepository()
        {
            context = new DBHelper().DB;
        }

        public void Create()
        {
            context.CreateTable<Telephone>();
        }

        public void Insert(Telephone entry)
        {
            context.Insert(entry);
        }

        public Telephone GetTelephone()
        {
            var table = context.Table<Telephone>();
            return table.Where(x => x.IsSelected == true)?.FirstOrDefault();

        }

        public void Invalidate()
        {
            var table = context.Table<Telephone>();
            foreach (var item in table)
            {
                item.IsSelected = false;
                context.Update(item);
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
