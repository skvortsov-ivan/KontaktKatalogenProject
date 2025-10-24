using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kontaktkatalogen.ConsoleUI;
using Kontaktkatalogen.Models;
using Kontaktkatalogen.Services;

namespace Kontaktkatalogen.Menu
{
    public class MainMenu
    {
        public static void Menu(ContactService service)
        {
            //Starting on contact ID 1
            int contactId = 1;

            //Menu options for the main menu of the contact catalogue
            List<string> menuOptions = new List<string>
            {
                "Add a contact",
                "List contacts",
                "Search for a contact",
                "Filter by tag"
            };

            //Menu options for adding tags menu
            List<string> tagMenuOptions = new List<string>
            {
                "Add a new tag"
            };

            while (true)
            {
                //This is the main menu for the contact catalogue. It's also a helping class I created to make menu creation simpler and look better
                var selectedOption = DisplayMenu.DisplayandUseMenu(menuOptions, "Welcome to the Contact Catalogue", true);
                switch (selectedOption)
                {
                    //Add a contact option
                    case 1:
                        {
                            //Resetting taglist everytime a new contact is added
                            List<string> tagList = new List<string>();
                            
                            //User contact data input
                            Console.WriteLine("Please enter the name of the contact:\n");
                            string nameInput = Console.ReadLine();
                            Console.WriteLine("\nPress Enter to continue\n>");
                            Console.Clear();
                            Console.WriteLine("Please enter the email of the contact:\n");
                            string emailInput = Console.ReadLine();
                            Console.WriteLine("\nPress Enter to continue\n>");
                            bool addingTags = true;

                            //Adding tags loop
                            while(addingTags)
                            {
                                //This is the tag menu
                                var selectedTagOption = DisplayMenu.DisplayandUseMenu(tagMenuOptions, "", false);

                                switch (selectedTagOption)
                                {
                                    case 1:
                                        {
                                            Console.WriteLine("Please enter the tag you would like to add. One at a time:\n");
                                            string tagInput = Console.ReadLine();
                                            tagList.Add(tagInput);
                                            continue;
                                        }
                                    case 0:
                                        {
                                            addingTags = false;
                                            break;
                                        }
                                }
                            }
                            service.AddContact(new Contact { Id = contactId++, Name = nameInput, Email = emailInput, Tags = tagList });
                            break;
                        }
                    //List contacts option
                    case 2:
                        {
                            //service.ListContacts();
                            break;
                        }
                    //Search for a contact option
                    case 3:
                        {
                            //service.SearchForContact();
                            break;
                        }
                    //Filter by tag option
                    case 4:
                        {
                            //service.FilterByTag();
                            break;
                        }
                    //Exit menu option
                    case 0:
                        {
                            Console.WriteLine("Thanks for using the Conctact Catalogue");
                            return;
                        }
                }
            }
        }
    }
}
