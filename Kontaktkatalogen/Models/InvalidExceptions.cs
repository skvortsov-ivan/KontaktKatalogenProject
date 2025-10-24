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

        public class InvalidEmptyCatalogueException : Exception
        {
            public InvalidEmptyCatalogueException(string message) : base(message) { }
        }
    }
}
