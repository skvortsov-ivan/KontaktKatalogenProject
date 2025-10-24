using System.ComponentModel.DataAnnotations;
using Kontaktkatalogen.ConsoleUI;
using Kontaktkatalogen.Menu;
using Kontaktkatalogen.Repositories;
using Kontaktkatalogen.Services;
using Kontaktkatalogen.Validators;
using Microsoft.Extensions.Logging;
// Besvara:
// Varför valde du dictionary <int, Contact>?
// Hur hjälper HashSet<<string> till att undvika dubletter?
// Vad lärde du om LINQ och testbar kod?

//A dictionary was used to save contact id as key and the contact class as value to simplify the implementation of the "list contacts" feature.

namespace Kontaktkatalogen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Initiating logger, catalogue, validator and service
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            var contactLogger = loggerFactory.CreateLogger<ContactService>();
            var catalogue = new ContactCatalogue();
            var contactValidator = new ContactValidator();
            var catalogueValidator = new ContactCatalogueValidator();

            var service = new ContactService(catalogue, contactValidator, catalogueValidator, contactLogger);  
            
            //Launching the main menu
            MainMenu.Menu(service);
        }
    }
}
