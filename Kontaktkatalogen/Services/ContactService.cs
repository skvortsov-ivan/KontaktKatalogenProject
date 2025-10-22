using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kontaktkatalogen.Models;
using Kontaktkatalogen.Validators;
using Kontaktkatalogen.Repositories;
using Microsoft.Extensions.Logging;

namespace Kontaktkatalogen.Services
{

    using Microsoft.Extensions.Logging;

    public class ContactService
    {
        private readonly IContactCatalogue _catalogue;
        private readonly ContactValidator _validator;
        private readonly ILogger<ContactService> _logger;

        public ContactService(IContactCatalogue catalogue, ContactValidator validator, ILogger<ContactService> logger)
        {
            _catalogue = catalogue;
            _validator = validator;
            _logger = logger;
        }

        public void AddContact(Contact contact)
        {
            try
            {
                _validator.Validate(contact);
                _catalogue.Save(contact);
                _logger.LogInformation("Contact saved: {Name}", contact.Name);
            }
            catch (InvalidContactException ex)
            {
                _logger.LogWarning(ex, "Validation error for contact: {Email}", contact.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unknown error occurred while attempting to save contact.");
            }
        }

        public IEnumerable<Contact> FilterByDomain(string domain)
        {
            return _catalogue.GetAll().Where(c => c.Email.EndsWith(domain));
        }
    }
}
