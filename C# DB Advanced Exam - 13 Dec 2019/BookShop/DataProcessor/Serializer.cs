namespace BookShop.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var mostCraziestAuthors = context
                 .Authors
                 .Select(a => new
                 {
                     AuthorName = a.FirstName + ' ' + a.LastName,
                     Books = a.AuthorsBooks
                     .OrderByDescending(b => b.Book.Price)
                     .Select(ab => new
                     {
                         BookName = ab.Book.Name,
                         BookPrice = ab.Book.Price.ToString("f2")
                     })
                     .ToArray()

                 })
                 .ToArray()
                 .OrderByDescending(a => a.Books.Length)
                 .ThenBy(a => a.AuthorName)
                 .ToArray();

            var json = JsonConvert.SerializeObject(mostCraziestAuthors, Formatting.Indented);

            return json;
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            var books = context.Books
               .Where(b => b.PublishedOn < date && b.Genre == Genre.Science)
               .ToArray()
               .OrderByDescending(b => b.Pages)
               .ThenByDescending(b => b.PublishedOn)
               .Take(10)
               .Select(b => new ExportBookDto()
               {
                   Name = b.Name,
                   Pages = b.Pages,
                   Date = b.PublishedOn.ToString("d", CultureInfo.InvariantCulture)
               })
               .ToArray();

            var result = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(ExportBookDto[]), new XmlRootAttribute("Books"));
            var xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add(string.Empty, string.Empty);

            using (var writter = new StringWriter(result))
            {
                xmlSerializer.Serialize(writter, books, xmlNamespaces);
            }

            return result.ToString().TrimEnd();
        }
    }
}