namespace BookShop.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Book")]
    public class ImportBookDto
    {
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        [XmlElement("Name")]
        public string Name { get; set; }


        [Required]
        [Range(1, 3)]
        [XmlElement("Genre")]
        public int Genre { get; set; }


        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        [XmlElement("Price")]
        public decimal Price { get; set; }


        [Range(50, 5000)]
        [XmlElement("Pages")]
        public int Pages { get; set; }


        [Required]
        [XmlElement("PublishedOn")]
        public string PublishedOn { get; set; }
    }
}
