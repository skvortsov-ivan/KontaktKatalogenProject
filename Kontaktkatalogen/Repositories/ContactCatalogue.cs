using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kontaktkatalogen.Models;

namespace Kontaktkatalogen.Repositories
{
    public class ContactCatalogue : IContactCatalogue
    {
        private Dictionary<int, Contact> _contacts = new Dictionary<int, Contact>();

        public int Count => _contacts.Count;
        private HashSet<string> _names = new();
        private HashSet<string> _emails = new();

        public void Save(int id, Contact contact)
        {
            _contacts[id] = contact;
            _names.Add(contact.Name);
            _emails.Add(contact.Email);
        }
        public Dictionary<int, Contact> GetDictionary()
        {
            return _contacts;
        }
        
        public bool ContainsName(string name)
        {
            return _names.Contains(name);
        }

        public bool ContainsEmail(string email)
        {
            return _emails.Contains(email);
        }
    }
}
