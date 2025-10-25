using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kontaktkatalogen.Models;
using Kontaktkatalogen.Repositories;
using Kontaktkatalogen.Services;
using Kontaktkatalogen.Validators;
using Microsoft.Extensions.Logging;
using Moq;

namespace Kontaktkatalogen.tests
{
    public class ContactServiceTests
    {

        [Fact]
        public void LogsWarning_WhenValidationFails()
        {
            int contactId = 1;
            var repoMock = new Mock<IContactCatalogue>();
            var loggerMock = new Mock<ILogger<ContactService>>();
            var contactValidator = new ContactValidator();
            var catalogueValidator = new ContactCatalogueValidator();
            var service = new ContactService(repoMock.Object, contactValidator, catalogueValidator, loggerMock.Object);

            var invalidContact = new Contact { Name = "", Email = "test@example.com" };
            service.AddContact(contactId, invalidContact);

            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Name is required.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
