using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kontaktkatalogen.Models;

namespace Kontaktkatalogen.Validators
{
    public class ContactValidator
    {
        public void Validate(Contact contact)
        {
            if (contact.Id <= 0)
                throw new InvalidContactException("Id is required.");

            if (string.IsNullOrWhiteSpace(contact.Name))
                throw new InvalidContactException("Name is required.");

            if (string.IsNullOrWhiteSpace(contact.Email))
                throw new InvalidContactException("Email is required.");

            if (contact.Tags == null || contact.Tags.Count == 0)
                throw new InvalidContactException("At least one tag is required.");
        }
    }
}
