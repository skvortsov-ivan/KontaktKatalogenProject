using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kontaktkatalogen.Models;
using Kontaktkatalogen.Repositories;
using static Kontaktkatalogen.Models.InvalidExceptions;


namespace Kontaktkatalogen.Validators
{
    public class ContactCatalogueValidator
    {
        //Exception if catalogue is empty
        public void AssertCatalogueNotEmpty(IContactCatalogue contactCatalogue)
        {
            if (contactCatalogue.Count == 0 || contactCatalogue == null)
                throw new InvalidExceptions.EmptyCatalogueException("There are no available contacts in the catalogue.");
        }

        //Exception if an a contact with the same email exists
        public void AssertContactIsUnique(IContactCatalogue contactCatalogue, Contact contact)
        {
            if (!contactCatalogue.TryAdd(contact.Email))
                throw new InvalidExceptions.DuplicateContactException($"A contact with the email '{contact.Email}' already exists.");
        }

        //Exception if no contact has the user provided tag
        public List<Contact> ValidateTag(IContactCatalogue catalogue, string searchTag)
        {
            var matchingContacts = catalogue.GetDictionary().Values
                .Where(c => c.Tags.Contains(searchTag))
                .ToList();

            if (!matchingContacts.Any())
                throw new InvalidExceptions.MissingTagException("There are no contacts with this tag.");

            return matchingContacts;
        }
    }
}
