using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.DataProcessor.ImportDtos
{
    public class ImportProducersDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [RegularExpression(@"^[A-z][a-z]+\s[A-z][a-z]+$")]
        public string Pseudonym { get; set; }

        [RegularExpression(@"^\+359\s\d{3}\s\d{3}\s\d{3}$")]
        public string PhoneNumber { get; set; }

        public ImportAlbumsDto[] Albums { get; set; }
    }
}
