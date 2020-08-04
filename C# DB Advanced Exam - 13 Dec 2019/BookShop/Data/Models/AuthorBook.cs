namespace BookShop.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    public class AuthorBook
    {
        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        public virtual Book Book { get; set; }
    }
}
