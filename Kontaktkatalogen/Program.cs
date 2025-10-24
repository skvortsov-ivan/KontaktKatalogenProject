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
            var catalogue = new InMemoryContactCatalogue();
            var validator = new ContactValidator();

            var service = new ContactService(catalogue, validator, contactLogger);  
            
            //Launching the main menu
            MainMenu.Menu(service);
        }
    }
}
