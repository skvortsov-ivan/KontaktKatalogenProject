using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontaktkatalogen.Models
{

    public static class InvalidExceptions
    {
        public class InvalidContactException : Exception
        {
            public InvalidContactException(string message) : base(message) { }
        }

        public class EmptyCatalogueException : Exception
        {
            public EmptyCatalogueException(string message) : base(message) { }
        }

        public class InvalidSearchNameException : Exception
        {
            public InvalidSearchNameException(string message) : base(message) { }
        }

        public class MissingContactExceptionException : Exception
        {
            public MissingContactExceptionException(string message) : base(message) { }
        }
    }
}
