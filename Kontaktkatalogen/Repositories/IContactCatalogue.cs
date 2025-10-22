using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kontaktkatalogen.Models;

namespace Kontaktkatalogen.Repositories
{
    public interface IContactCatalogue
    {
        void Save(Contact contact);
        IEnumerable<Contact> GetAll();
    }
}
