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
        [Fact]
        public void Given_Email_When_Validated_Should_ThrowExceptionWhenEmailIsInvalid()
        {
            var validator = new ContactValidator();
            var contact1 = new Contact { Name = "Bob", Email = "Bob[at]gmail.com" };

            //Assert.Throws<InvalidContactException>(() => validator.Validate(contact1));

            var contact2 = new Contact { Name = "Bob", Email = "" };

            Assert.Throws<InvalidContactException>(() => validator.Validate(contact2));
        }
    }
}
