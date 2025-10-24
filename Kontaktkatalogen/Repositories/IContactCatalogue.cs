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
        int Count { get; }
        void Save(int id, Contact contact);
        public Dictionary<int, Contact> GetDictionary();
    }
}
