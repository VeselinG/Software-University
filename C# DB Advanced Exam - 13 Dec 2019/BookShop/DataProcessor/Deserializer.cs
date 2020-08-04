namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using BookShop.Data.Models;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportBookDto[]), new XmlRootAttribute("Books"));

            using (StringReader stringReader = new StringReader(xmlString))
            {
                ImportBookDto[] bookDtos = (ImportBookDto[])xmlSerializer.Deserialize(stringReader);

                List<Book> validBooks = new List<Book>();

                foreach (var bookDto in bookDtos)
                {
                    if (!IsValid(bookDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime publishedOn;
                    bool isDateValid = DateTime
                        .TryParseExact(bookDto.PublishedOn, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out publishedOn);
                    
                    if (!isDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Book validBook = new Book()
                    {
                        Name = bookDto.Name,
                        Genre = (Genre)bookDto.Genre,
                        Price = bookDto.Price,
                        Pages = bookDto.Pages,
                        PublishedOn = publishedOn
                    };

                    validBooks.Add(validBook);
                    sb.AppendLine(String.Format(SuccessfullyImportedBook, validBook.Name, validBook.Price));
                }

                context.Books.AddRange(validBooks);
                context.SaveChanges();
            }

            return sb.ToString().TrimEnd();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserializedAuthor = JsonConvert.DeserializeObject<AuthorDto[]>(jsonString);

            var validAuthors = new List<Author>();

            foreach (var authorDto in deserializedAuthor)
            {
                if (!IsValid(authorDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var emailAlreadyExist = validAuthors.Any(e => e.Email == authorDto.Email);

                if (emailAlreadyExist)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var author = new Author()
                {
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName,
                    Email = authorDto.Email,
                    Phone = authorDto.Phone
                };

                foreach (var book in authorDto.Books)
                {
                    if (!book.BookId.HasValue)
                    {
                        continue;
                    }

                    var findBook = context.Books.FirstOrDefault(b => b.Id == book.BookId);

                    if (findBook == null)
                    {
                        continue;
                    }

                    var bookToAdd = new AuthorBook()
                    {
                        Author = author,
                        Book = findBook
                    };

                    author.AuthorsBooks.Add(bookToAdd);
                }

                if (author.AuthorsBooks.Count==0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                validAuthors.Add(author);
                sb.AppendLine(String.Format(SuccessfullyImportedAuthor, author.FirstName + ' ' + author.LastName, author.AuthorsBooks.Count));
            }

            context.Authors.AddRange(validAuthors);
            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}