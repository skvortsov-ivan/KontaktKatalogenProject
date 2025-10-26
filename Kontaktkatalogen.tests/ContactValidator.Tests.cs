using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Resource;
using Kontaktkatalogen.Validators;
using Kontaktkatalogen.Models;

namespace Kontaktkatalogen.tests
{
    public class ContactValidatorTests
    {

        //Testing ContactValidator with valid contact
        [Fact]
        public void Given_ValidContact_When_Validated_Should_NotThrowException()
        {
            var validator = new ContactValidator();
            var contact = new Contact
            {
                Name = "Test",
                Email = "test@example.com",
                Tags = new List<string> { "Work" }
            };

            //Should not throw exception
            validator.Validate(contact); // Should not throw
        }

        //Testing ContactValidator with each field being empty
        [Fact]
        public void Given_InvalidContactFields_When_Validated_Should_ThrowException()
        {
            var validator = new ContactValidator();

            var invalidName = new Contact { Name = "", Email = "test@example.com", Tags = new List<string> { "Work" } };
            var invalidEmail = new Contact { Name = "Test", Email = "", Tags = new List<string> { "Work" } };
            var invalidTags = new Contact { Name = "Test", Email = "test@example.com", Tags = new List<string>() };

            Assert.Throws<InvalidExceptions.InvalidContactException>(() => validator.Validate(invalidName));
            Assert.Throws<InvalidExceptions.InvalidContactException>(() => validator.Validate(invalidEmail));
            Assert.Throws<InvalidExceptions.InvalidContactException>(() => validator.Validate(invalidTags));
        }
    }
}
