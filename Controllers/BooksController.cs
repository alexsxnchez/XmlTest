using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace XmlTest.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            IList<Models.Book> booklist = new List<Models.Book>();
            //Load book.xml
            string path = Request.PathBase + "App_Data/books.xml";
            XmlDocument doc = new XmlDocument();

            if (System.IO.File.Exists(path))
            {
                doc.Load(path);
                XmlNodeList books = doc.GetElementsByTagName("book");

                foreach (XmlElement b in books)
                {
                    Models.Book book = new Models.Book();
                    book.Id = Int32.Parse(b.GetElementsByTagName("id")[0].InnerText);
                    book.Title = b.GetElementsByTagName("title")[0].InnerText;
                    book.AuthorTitle = b.GetElementsByTagName("author")[0].InnerText;
                    var authortitle = ((XmlElement)b.GetElementsByTagName("author")[0]).GetAttribute("title");
                    book.AuthorTitle = authortitle;
                    //book.MiddleName = b.GetElementsByTagName("middlename")[0].InnerText;
                    book.FirstName = b.GetElementsByTagName("firstname")[0].InnerText;
                    book.LastName = b.GetElementsByTagName("lastname")[0].InnerText;

                    booklist.Add(book);
                }
            }

            return View(booklist);
        }

        // Get: /Books/Create
        [HttpGet]
        public IActionResult Create()
        {
            var book = new Models.Book();
            return View(book);
        }
        [HttpPost]
        public IActionResult Create(Models.Book b)
        {
            //Load book.xml
            string path = Request.PathBase + "App_Data/books.xml";
            XmlDocument doc = new XmlDocument();

            if (System.IO.File.Exists(path))
            {
                //If file exists, load it and create new book.
                doc.Load(path);
                //Create a new book.
                XmlElement book = CreateBookElement(doc, b);
                //get root element.
                XmlNode root = doc.DocumentElement.AppendChild(book);
                doc.Save(path);

                return RedirectToAction("Index");
            }
            else
            {
                //If file doesn't exist, create new book.
                XmlNode dec = doc.CreateXmlDeclaration("1.0", "utf-8", "");
                doc.AppendChild(dec);
                XmlNode root = doc.CreateElement("books");

                //Creat a new book.
                XmlElement book = CreateBookElement(doc, b);
                root.AppendChild(book);
                doc.AppendChild(root);
                doc.Save(path);

                return RedirectToAction("Index");
            }
            
        }
        private XmlElement CreateBookElement(XmlDocument doc, Models.Book
             newBook)
        {
            XmlElement book = doc.CreateElement("book");
            //
            XmlNode id = doc.CreateElement("id");
            int id1 = doc.GetElementsByTagName("id").Count;
            id.InnerText = (id1 + 1).ToString();
            //
            XmlNode title = doc.CreateElement("title");
            title.InnerText = newBook.Title;

            XmlNode author = doc.CreateElement("author");
            XmlNode firstname = doc.CreateElement("firstname");
            firstname.InnerText = newBook.FirstName;
            XmlNode lastname = doc.CreateElement("lastname");
            lastname.InnerText = newBook.LastName;
            //XmlAttribute title = doc.CreateAttribute("title");
            //title.Value = newBook.AuthorTitle;
            //author.Attributes.Append(title);
            author.AppendChild(firstname);
            author.AppendChild(lastname);
            //Append all to book
            book.AppendChild(id);
            book.AppendChild(title);
            book.AppendChild(author);

            return book;

        }
    }
}
