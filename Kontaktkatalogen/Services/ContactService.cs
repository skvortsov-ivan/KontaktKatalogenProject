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
        //Private readonly fields for catalogue, validators and logger
        private readonly IContactCatalogue _catalogue;
        private readonly ContactValidator _contactValidator;
        private readonly ContactCatalogueValidator _catalogueValidator;
        private readonly ILogger<ContactService> _logger;

        //Constructor
        public ContactService(IContactCatalogue catalogue, ContactValidator contactValidator, ContactCatalogueValidator catalogueValidator, ILogger<ContactService> logger)
        {
            _catalogue = catalogue;
            _contactValidator = contactValidator;
            _catalogueValidator = catalogueValidator;
            _logger = logger;
        }

        //Add contact method
        public void AddContact(int id, Contact contact)
        {
            try
            {
                //Validate input
                _contactValidator.Validate(contact);

                //Check if the contact is unique in regards to email
                _catalogueValidator.AssertContactIsUnique(_catalogue, contact);

                //Save contact
                _catalogue.Save(id, contact);

                //Chosing where to print all Log data as well as logging
                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogInformation("Contact '{Name}' has been saved successfully.", contact.Name);
                
                //A must include in order to print all Log activities in the correct part of the console
                Thread.Sleep(5);
            }
            //If Validators throws an exception a log warning is issued
            catch (Exception ex) when (
            ex is InvalidExceptions.InvalidContactException ||
            ex is InvalidExceptions.DuplicateContactException)
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

        //List contacts method
        public void ListContacts()
        {
            try
            {
                //Check if catalogue is empty
                _catalogueValidator.Validate(_catalogue);

                //Print out result
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

        //Search for contact method. It includes LINQ
        public void SearchForContact()
        {
            try
            {
                //Check if catalogue is empty
                _catalogueValidator.Validate(_catalogue);

                Console.WriteLine("Please enter the name of the contact:");
                string searchName = Console.ReadLine();
                Console.Clear();

                //Throw an exception if input is empty
                if (string.IsNullOrWhiteSpace(searchName))
                    throw new InvalidExceptions.EmptyContactNameException("Search name cannot be empty.");

                //Acquiring all contacts with the provided name 
                var matchingContacts = _catalogue.GetDictionary().Values.Where(c => c.Name.Equals(searchName)).ToList();

                //Throw an exception if there are no contacts with the provided name
                if (matchingContacts == null)
                    throw new InvalidExceptions.MissingContactException("The provided contact does not exist in the catalogue.");

                //Print out result
                Console.WriteLine($"Found contacts contacts with the name: {searchName}\n");
                foreach (var entry in matchingContacts)
                {
                    Console.WriteLine($"\nName: {entry.Name} | Email: {entry.Email} | Tags: {string.Join(", ", entry.Tags)}");
                    Console.WriteLine("-----------------------------");
                }
                
                Console.WriteLine("\nPress any key to continue\n>");
                Console.ReadLine();

                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogInformation("Contact found and displayed successfully");
                Thread.Sleep(5);
            }
            catch (Exception ex) when (
            ex is InvalidExceptions.EmptyCatalogueException ||
            ex is InvalidExceptions.EmptyContactNameException ||
            ex is InvalidExceptions.MissingContactException)
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

        //Filter by tag method. It includes LINQ
        public void FilterByTag()
        {
            try
            {
                //Check if catalogue is empty
                _catalogueValidator.Validate(_catalogue);

                Console.WriteLine("Please enter the tag you would like to filter by:");
                string searchTag = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(searchTag))
                    throw new InvalidExceptions.EmptyContactTagException("Search tag cannot be empty.");

                //Check if there are any contacts with the given tag
                var matchingContacts = _catalogueValidator.ValidateTag(_catalogue, searchTag);

                //Print out result
                Console.WriteLine($"List of contacts with the tag '{searchTag}':\n");
                foreach (var entry in matchingContacts)
                {
                    Console.WriteLine($"Name: {entry.Name} | Email: {entry.Email} | Tags: {string.Join(", ", entry.Tags)}");
                }
                Console.WriteLine("Press any key to continue\n>");
                Console.ReadLine();

                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                _logger.LogInformation("Filtered by tag and displayed associated contacts successfully");
                Thread.Sleep(5);
            }
            catch (Exception ex) when (
                ex is InvalidExceptions.EmptyCatalogueException ||
                ex is InvalidExceptions.EmptyContactTagException ||
                ex is InvalidExceptions.MissingTagException)
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
    }
}
