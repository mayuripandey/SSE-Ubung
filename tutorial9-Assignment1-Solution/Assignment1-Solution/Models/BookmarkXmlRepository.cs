using System.Xml.XPath;
using System.Xml.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Xml.Serialization;
using System.Linq;

namespace BookmarkService.Models
{
    public class BookmarkXmlRepository : IBookmarkRepository
    {
        private readonly string _xmlFile;

        public BookmarkXmlRepository(IHostingEnvironment env)
        {
            _xmlFile = env.ContentRootPath + "/data.xml";
        }

        public static string XmlFile = "./data.xml";

        public Bookmark[] GetAll()
        {
            var serializer = new XmlSerializer(typeof(Database));
            var fs = new FileStream(_xmlFile, FileMode.Open);
            return ((Database)serializer.Deserialize(fs)).Bookmarks;
        }

        public Bookmark GetById(int id)
        {
            XDocument doc = XDocument.Load(_xmlFile);            
            XElement bookmark = (doc.XPathSelectElement("//bookmarks/bookmark[id=" + id + "]"));

            if (bookmark == null)
            {
                return null;
            }
            
            var serializer = new XmlSerializer(typeof(Bookmark));
            return (Bookmark)serializer.Deserialize(new StringReader(bookmark.ToString()));
        }

        public int Create(Bookmark bookmark)
        {
            var serializer = new XmlSerializer(typeof(Database));
            var fs = new FileStream(_xmlFile, FileMode.Open);
            var database = (Database)serializer.Deserialize(fs);
            var bookmarks = database.Bookmarks;

            var nextId = bookmarks.Length > 0 ? (bookmarks.Select(b => b.Id).Max() + 1) : 1;
            bookmark.Id = nextId;

            database.Bookmarks = bookmarks.Append(bookmark).ToArray();

            serializer.Serialize(new FileStream(_xmlFile, FileMode.Create), database);

            return nextId;
        }

        public void Update(Bookmark bookmark)
        {
            var serializer = new XmlSerializer(typeof(Database));
            var fs = new FileStream(_xmlFile, FileMode.Open);
            var database = (Database)serializer.Deserialize(fs);
            var bookmarks = database.Bookmarks.Where(b => b.Id != bookmark.Id);

            database.Bookmarks = bookmarks.Append(bookmark).ToArray();

            serializer.Serialize(new FileStream(_xmlFile, FileMode.Create), database);
        }

        public void Delete(int id)
        {
            var serializer = new XmlSerializer(typeof(Database));
            var fs = new FileStream(_xmlFile, FileMode.Open);
            var database = (Database)serializer.Deserialize(fs);
            var bookmarks = database.Bookmarks.Where(b => b.Id != id);

            serializer.Serialize(new FileStream(_xmlFile, FileMode.Create), database);
        }

        public Bookmark[] GetByUser(string user)
        {
            return GetAll().Where(b => b.User == user).ToArray();
        }

        public Bookmark[] GetByCategory(string category)
        {
            return GetAll().Where(b => b.Category == category).ToArray();
        }
    }
}