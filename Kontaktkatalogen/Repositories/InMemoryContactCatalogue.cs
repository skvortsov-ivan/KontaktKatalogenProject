using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kontaktkatalogen.Models;

namespace Kontaktkatalogen.Repositories
{
    public class InMemoryContactCatalogue : IContactCatalogue
    {
        private List<Contact> _contacts = new List<Contact>();

        public void Save(Contact contact)
        {
            _contacts.Add(contact);
        }

        public IEnumerable<Contact> GetAll()
        {
            return _contacts;
        }
    }
}
