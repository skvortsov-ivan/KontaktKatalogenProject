# KontaktKatalogenProject
  
ContactCatalogue is a console-based contact management system built in C#. It allows users to add, search, and manage contacts efficiently using structured data and validation logic. The system emphasizes clean architecture, testability, and user-friendly interaction.  
  
## Features:  
  
Contact creation and validation  
Search and list contact methods  
Console menu navigation  
xUnit and Moq testing  
  
## Core Components:  
  
ContactService    
Handles all user-facing operations such as adding contacts, searching by name or tag, and displaying results.  
  
ContactValidator & ContactCatalogueValidator  
Validates individual contact as well as the catalogue.   
  
MainMenu
Provides a navigable console menu interface for selecting actions and interacting with the system.  
  
## Reflections  

Why did I choose Dictionary<int, Contact>?  
I chose Dictionary<int, Contact> because it provides fast access to contacts using a unique ID and it's a very useful information storage, especially when printing out information  

Why did I choose HashSet<string> to avoid email duplicates?  
I used HashSet<string> to store email addresses to ensure that each contact has a unique email. The TryAdd method is implemented to see whether an email already exists in the Hashset or not. If it does then an exception is thrown and the logger displays a warming in the console for the user. 

What did I learn about LINQ and testable code?  
I learned that LINQ makes it easy to filter and search through collections. By using LINQ, I was able to write code that’s both concise and easy to test. I also realized the importance of separating validation logic from all other parts of the code. This solution made it easier to do both xUnit and Moq tests. 
