using SQLite;

namespace ContactsBook.Models
{
    public class ContactBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}