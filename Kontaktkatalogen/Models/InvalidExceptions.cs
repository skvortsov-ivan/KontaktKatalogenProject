using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontaktkatalogen.Models
{

    public static class InvalidExceptions
    {

        //Contact Exceptions
        public class InvalidContactException : Exception
        {
            public InvalidContactException(string message) : base(message) { }
        }

        public class InvalidSearchNameException : Exception
        {
            public InvalidSearchNameException(string message) : base(message) { }
        }
        public class EmptyContactNameException : Exception
        {
            public EmptyContactNameException(string message) : base(message) { }
        }
        public class EmptyContactTagException : Exception
        {
            public EmptyContactTagException(string message) : base(message) { }
        }
        public class MissingTagException : Exception
        {
            public MissingTagException(string message) : base(message) { }
        }

        //Catalogue Exceptions
        public class DuplicateContactException : Exception
        {
            public DuplicateContactException(string message) : base(message) { }
        }

        public class EmptyCatalogueException : Exception
        {
            public EmptyCatalogueException(string message) : base(message) { }
        }

        public class MissingContactException : Exception
        {
            public MissingContactException(string message) : base(message) { }
        }
    }
}
