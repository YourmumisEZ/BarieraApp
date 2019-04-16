using SQLite;

namespace DataLayer.DataModels
{
    public class Telephone
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        
        public string Number { get; set; }

        public bool IsSelected { get; set; }
    }
}
