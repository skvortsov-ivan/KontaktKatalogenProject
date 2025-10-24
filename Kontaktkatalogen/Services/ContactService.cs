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
        private readonly ContactValidator _contactValidator;
        private readonly ContactCatalogueValidator _catalogueValidator;
        private readonly ILogger<ContactService> _logger;

        public ContactService(IContactCatalogue catalogue, ContactValidator contactValidator, ContactCatalogueValidator catalogueValidator, ILogger<ContactService> logger)
        {
            _catalogue = catalogue;
            _contactValidator = contactValidator;
            _catalogueValidator = catalogueValidator;
            _logger = logger;
        }

        public void AddContact(int id, Contact contact)
        {
            try
            {
                _contactValidator.Validate(contact);
                _catalogue.Save(id, contact);
                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogInformation("Contact '{Name}' has been saved successfully.", contact.Name);
            }
            catch (InvalidExceptions.InvalidContactException ex)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogWarning("Validation failed: {Message}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogError(ex, "Unknown error occurred while attempting to save contact.");
            }
        }

        public void ListContacts()
        {
            try
            {
                _catalogueValidator.Validate(_catalogue);

                Console.WriteLine("List of saved contacts:\n");

                foreach (var entry in _catalogue.GetDictionary())
                {
                    Contact contact = entry.Value;
                    Console.WriteLine($"{entry.Key} | Name: {contact.Name} | Email: {contact.Email} | Tags: {string.Join(", ", contact.Tags)}\n");
                    Console.WriteLine("-----------------------------\n");

                }

                Console.WriteLine("Press any key to continue\n>");
                Console.ReadLine();

                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogInformation("Contact list displayed successfully");
            }
            catch (InvalidExceptions.InvalidEmptyCatalogueException ex)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogWarning("Validation failed: {Message}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogError(ex, "Unknown error occurred while attempting to list contacts.");
            }

        }

        //public IEnumerable<Contact> FilterByDomain(string domain)
        //{
        //    return _catalogue.GetAll().Where(c => c.Email.EndsWith(domain));
        //}
    }
}
