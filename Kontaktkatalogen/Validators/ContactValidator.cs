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
            if (string.IsNullOrWhiteSpace(contact.Name))
                throw new InvalidExceptions.InvalidContactException("Name is required.");

            if (string.IsNullOrWhiteSpace(contact.Email))
                throw new InvalidExceptions.InvalidContactException("Email is required.");

            if (contact.Tags == null || contact.Tags.Count == 0)
                throw new InvalidExceptions.InvalidContactException("At least one tag is required.");
        }
    }
}
