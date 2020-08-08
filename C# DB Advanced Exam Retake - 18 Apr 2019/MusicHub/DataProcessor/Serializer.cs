namespace MusicHub.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using MusicHub.DataProcessor.ExportDtos;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albumInfo = context
                .Albums
                .Where(p => p.Producer.Id == producerId)
                .Select(a => new
                {
                    AlbumName = a.Name,
                    ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                    ProducerName = a.Producer.Name,
                    Songs = a.Songs
                    .Select(s => new
                    {
                        SongName = s.Name,
                        Price = s.Price.ToString("f2"),
                        Writer = s.Writer.Name
                    })
                    .OrderByDescending(n => n.SongName)
                    .ThenBy(w => w.Writer)
                    .ToArray(),
                    AlbumPrice = a.Songs.Sum(s => s.Price).ToString("f2")
                })
                 .OrderByDescending(a => a.AlbumPrice)
                 .ToArray();

            var json = JsonConvert.SerializeObject(albumInfo, Formatting.Indented);

            return json;
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songAboveDuration = context
                .Songs                
                .Where(s => s.Duration.Seconds > duration)                
                .Select(s => new ExportSongsDto()
                {
                    Name = s.Name,
                    WriterName = s.Writer.Name,
                    PerformerName = s.SongPerformers.FirstOrDefault().Performer.FirstName +
                    " " + s.SongPerformers.FirstOrDefault().Performer.LastName,
                    AlbumProducer = s.Album.Producer.Name,
                    Duration = s.Duration.ToString("c", CultureInfo.InvariantCulture)
                })
                .OrderBy(n => n.Name)
                .ThenBy(w => w.WriterName)
                .ThenBy(p => p.PerformerName)
                .ToArray();

            var result = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(ExportSongsDto[]), new XmlRootAttribute("Songs"));
            var xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add(string.Empty, string.Empty);

            using (var writter = new StringWriter(result))
            {
                xmlSerializer.Serialize(writter, songAboveDuration, xmlNamespaces);
            }

            return result.ToString().TrimEnd();
        }
    }
}