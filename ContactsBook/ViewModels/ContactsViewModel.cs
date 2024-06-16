using ContactsBook.Models;
using ContactsBook.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ContactsBook.ViewModels
{
    public class ContactsViewModel : INotifyPropertyChanged
    {
        private readonly ContactsContext _database;

        public ObservableCollection<ContactBase> Contacts { get; set; }

        private ContactBase _selectedContact;
        public ContactBase SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public ContactsViewModel()
        {
            _database = new ContactsContext(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contacts.db3"));
            Contacts = new ObservableCollection<ContactBase>();
            LoadContacts();

            AddCommand = new Command(async () => await AddContact());
            DeleteCommand = new Command(async () => await DeleteContact(), () => SelectedContact != null);
        }

        private async void LoadContacts()
        {
            var contacts = await _database.GetContactsAsync();
            foreach (var contact in contacts)
            {
                Contacts.Add(contact);
            }
        }

        private async Task AddContact()
        {
            var contact = new ContactBase { Name = "New Contact", Phone = "000-000-0000" };
            await _database.SaveContactAsync(contact);
            Contacts.Add(contact);
        }

        private async Task DeleteContact()
        {
            if (SelectedContact != null)
            {
                await _database.DeleteContactAsync(SelectedContact);
                Contacts.Remove(SelectedContact);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
