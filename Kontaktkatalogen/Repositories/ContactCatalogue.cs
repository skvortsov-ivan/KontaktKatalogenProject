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
        //Catalogue dictionary
        private Dictionary<int, Contact> _catalogue = new Dictionary<int, Contact>();

        //Help counter for storing amount of contacts in catalogue
        public int Count => _catalogue.Count;

        //Help hashset to ensure there are not duplicate emails
        private HashSet<string> _emails = new();

        //Saving contact to catalogue and associated email to the hashset
        public void Save(int id, Contact contact)
        {
            _catalogue[id] = contact;
            _emails.Add(contact.Email);
        }

        //Method that returns catalogue
        public Dictionary<int, Contact> GetDictionary()
        {
            return _catalogue;
        }

        //Method that attempts to add an email to the hashset
        public bool TryAdd(string email)
        {
            return _emails.Add(email);
        }
    }
}
