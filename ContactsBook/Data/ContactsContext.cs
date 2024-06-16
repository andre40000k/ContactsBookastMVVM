using SQLite;
using ContactsBook.Models;

namespace ContactsBook.Data
{
    public class ContactsContext
    {
        private readonly SQLiteAsyncConnection _database;

        public ContactsContext(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ContactBase>().Wait();
        }

        public Task<List<ContactBase>> GetContactsAsync()
        {
            return _database.Table<ContactBase>().ToListAsync();
        }

        public Task<int> SaveContactAsync(ContactBase contact)
        {
            if (contact.Id != 0)
            {
                return _database.UpdateAsync(contact);
            }
            else
            {
                return _database.InsertAsync(contact);
            }
        }

        public Task<int> DeleteContactAsync(ContactBase contact)
        {
            return _database.DeleteAsync(contact);
        }
    }
}
