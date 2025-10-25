using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontaktkatalogen.Models
{
    //Contact class with the fields: name, email and a list of tags
    public class Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Tags { get; set; }
    }
}
