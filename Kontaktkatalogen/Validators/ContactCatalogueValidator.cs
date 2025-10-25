using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kontaktkatalogen.Models;
using Kontaktkatalogen.Repositories;


namespace Kontaktkatalogen.Validators
{
    public class ContactCatalogueValidator
    {
        public void Validate(IContactCatalogue contactCatalogue)
        {
            if (contactCatalogue.Count == 0 || contactCatalogue == null)
                throw new InvalidExceptions.EmptyCatalogueException("There are no available contacts in the catalogue.");
        }
    }
}
