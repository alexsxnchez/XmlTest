using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XmlTest.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string AuthorTitle { get; set; }
        public virtual System.Xml.XmlNode? LastChild { get; }
    }
}
