using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Kontaktkatalogen.Models;
using Kontaktkatalogen.Repositories;
using Kontaktkatalogen.Services;
using Kontaktkatalogen.Validators;
using Microsoft.Extensions.Logging;
using Moq;
using static Kontaktkatalogen.Models.InvalidExceptions;

namespace Kontaktkatalogen.tests
{
    public class ContactServiceTests
    {

        //Testing ContactValidator - Validate()
        [Fact]
        public void LogsWarning_WhenContactValidationFails()
        {
            int contactId = 1;
            var repoMock = new Mock<IContactCatalogue>();
            var loggerMock = new Mock<ILogger<ContactService>>();
            var contactValidator = new ContactValidator();
            var catalogueValidator = new ContactCatalogueValidator();
            var service = new ContactService(repoMock.Object, contactValidator, catalogueValidator, loggerMock.Object);

            //Invalid empty name
            var invalidContact1 = new Contact { Name = "", Email = "test@example.com", Tags = new List<string> { "Work" } };
            //Invalid empty email
            var invalidContact2 = new Contact { Name = "Test", Email = "", Tags = new List<string> { "Work" } };
            //Invalid empty list of tags
            var invalidContact3 = new Contact { Name = "Test", Email = "test@example.com", Tags = new List<string>() };

            service.AddContact(contactId, invalidContact1);
            service.AddContact(contactId + 1, invalidContact2);
            service.AddContact(contactId + 3, invalidContact3);

            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Name is required.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);

            loggerMock.Verify(
            x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Email is required.")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);

            loggerMock.Verify(
            x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("At least one tag is required.")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
        }

        //Testing ContactCatalogueValidator - AssertCatalogueNotEmpty()
        [Fact]
        public void LogsWarning_WhenCatalogueIsEmpty()
        {
            var repoMock = new Mock<IContactCatalogue>();
            var loggerMock = new Mock<ILogger<ContactService>>();
            var contactValidator = new ContactValidator();
            var catalogueValidator = new ContactCatalogueValidator();
            var service = new ContactService(repoMock.Object, contactValidator, catalogueValidator, loggerMock.Object);

            //Simulate an empty catalogue
            repoMock.Setup(r => r.Count).Returns(0);

            service.SearchForContact();

            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("There are no available contacts in the catalogue.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        //Testing ContactCatalogueValidator - AssertContactIsUnique()
        [Fact]
        public void LogsWarning_WhenDuplicateEmailExists()
        {
            int contactId = 1;
            var repoMock = new Mock<IContactCatalogue>();
            var loggerMock = new Mock<ILogger<ContactService>>();
            var contactValidator = new ContactValidator();
            var catalogueValidator = new ContactCatalogueValidator();
            var service = new ContactService(repoMock.Object, contactValidator, catalogueValidator, loggerMock.Object);

            var contact = new Contact { Name = "Test", Email = "duplicate@example.com", Tags = new List<string> { "Work" } };

            // Simulate that TryAdd returns false (email already exists)
            repoMock.Setup(r => r.TryAdd(contact.Email)).Returns(false);

            service.AddContact(contactId, contact);

            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains($"A contact with the email '{contact.Email}' already exists.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        //Testing ContactCatalogueValidator - ValidateTag()
        [Fact]
        public void LogsWarning_WhenTagIsMissing()
        {
            var repoMock = new Mock<IContactCatalogue>();
            var loggerMock = new Mock<ILogger<ContactService>>();
            var contactValidator = new ContactValidator();
            var catalogueValidator = new ContactCatalogueValidator();

            var contact = new Contact { Name = "Test", Email = "test@example.com", Tags = new List<string> { "Work" } };
            var catalogue = new Dictionary<int, Contact> { { 1, contact } };

            repoMock.Setup(r => r.Count).Returns(1);
            repoMock.Setup(r => r.GetDictionary()).Returns(catalogue);

            try
            {
                catalogueValidator.ValidateTag(repoMock.Object, "Personal"); // "Personal" tag does not exist
            }
            catch (InvalidExceptions.MissingTagException ex)
            {
                loggerMock.Object.LogWarning("Validation failed: {Message}", ex.Message);
            }

            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("There are no contacts with this tag.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
