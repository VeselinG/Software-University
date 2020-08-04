namespace BookShop.DataProcessor.ImportDto
{
    using Newtonsoft.Json;
    public class AuthorsBookIdDto
    {
        [JsonProperty("Id")]
        public int? BookId { get; set; }
    }
}
