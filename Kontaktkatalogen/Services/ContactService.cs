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
        private static string _lastLogMessage = "";
        private static ConsoleColor _lastLogColor = ConsoleColor.Gray;

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
                //The most idiotic problem I have ever faced. For whatever reason, the LogWarning will not apprear in the correct place unless I include this pause.
                //It's specifically the first I select a menu option. I'm very curious to find out why it works this way.
                Thread.Sleep(5);
            }
            catch (InvalidExceptions.InvalidContactException ex)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogWarning("Validation failed: {Message}", ex.Message);
                Thread.Sleep(5);
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogError(ex, "Unknown error occurred while attempting to save contact.");
                Thread.Sleep(5);
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
                Thread.Sleep(5);
            }
            catch (InvalidExceptions.EmptyCatalogueException ex)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogWarning("Validation failed: {Message}", ex.Message);
                Thread.Sleep(5);
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogError(ex, "Unknown error occurred while attempting to list contacts.");
                Thread.Sleep(5);
            }

        }

        public void SearchForContact()
        {
            try
            {
                _catalogueValidator.Validate(_catalogue);

                Console.WriteLine("Please enter the name of the contact:");
                string searchName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(searchName))
                    throw new InvalidExceptions.InvalidContactException("Search name cannot be empty.");

                
                var contactMatch = _catalogue.GetDictionary().Values.FirstOrDefault(c => c.Name.Equals(searchName));
                
                if (contactMatch == null)
                    throw new InvalidExceptions.MissingContactExceptionException("The provided contact does not exist in the catalogue.");


                Console.WriteLine($"Name: {contactMatch.Name} | Email: {contactMatch.Email} | Tags: {string.Join(", ", contactMatch.Tags)}");
                Console.WriteLine("Press any key to continue\n>");
                Console.ReadLine();

                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogInformation("Contact found and displayed successfully");
                Thread.Sleep(5);
            }
            catch (InvalidExceptions.EmptyCatalogueException ex)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogWarning("Validation failed: {Message}", ex.Message);
                Thread.Sleep(5);
            }
            catch (InvalidExceptions.MissingContactExceptionException ex)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogWarning("Validation failed: {Message}", ex.Message);
                Thread.Sleep(5);
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogError(ex, "Unknown error occurred while attempting to search for a contact.");
                Thread.Sleep(5);
            }
        }

        //public IEnumerable<Contact> FilterByDomain(string domain)
        //{
        //    return _catalogue.GetAll().Where(c => c.Email.EndsWith(domain));
        //}
    }
}
