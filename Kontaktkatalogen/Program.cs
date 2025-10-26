using System.ComponentModel.DataAnnotations;
using Kontaktkatalogen.ConsoleUI;
using Kontaktkatalogen.Menu;
using Kontaktkatalogen.Repositories;
using Kontaktkatalogen.Services;
using Kontaktkatalogen.Validators;
using Microsoft.Extensions.Logging;

//Contact catalogue main
namespace Kontaktkatalogen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Initiating logger, catalogue, validators and service
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
