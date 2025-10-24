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

        public void Save(int id, Contact contact)
        {
            _contacts[id] = contact;
        }
        public Dictionary<int, Contact> GetDictionary()
        {
            return _contacts;
        }
    }
}
