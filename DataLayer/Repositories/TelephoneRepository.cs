using DataLayer.DataModels;

namespace DataLayer.Repositories
{
    public class TelephoneRepository
    {
        public void Create()
        {
            using (var context = new DBHelper())
            {
                if (context.DB.Table<Telephone>().Count() == 0)
                {
                    context.DB.CreateTable<Telephone>();
                }
            }
        }

        public void Insert(Telephone entry)
        {
            using (var context = new DBHelper())
            {
                context.DB.Insert(entry);
            }
        }

        public Telephone GetTelephone()
        {
            using (var context = new DBHelper())
            {
                var table = context.DB.Table<Telephone>();
                return table.Where(x => x.IsSelected == true)?.FirstOrDefault();
            }
        }

        public void Invalidate()
        {
            using (var context = new DBHelper())
            {
                var table = context.DB.Table<Telephone>();
                foreach(var item in table)
                {
                    item.IsSelected = false;
                }
                context.DB.UpdateAll(table);
            }
        }
    }
}
