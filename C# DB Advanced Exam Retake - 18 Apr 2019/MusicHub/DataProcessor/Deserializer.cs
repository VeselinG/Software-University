namespace MusicHub.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Castle.Core.Internal;
    using Data;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.DataProcessor.ImportDtos;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data";

        private const string SuccessfullyImportedWriter
            = "Imported {0}";
        private const string SuccessfullyImportedProducerWithPhone
            = "Imported {0} with phone: {1} produces {2} albums";
        private const string SuccessfullyImportedProducerWithNoPhone
            = "Imported {0} with no phone number produces {1} albums";
        private const string SuccessfullyImportedSong
            = "Imported {0} ({1} genre) with duration {2}";
        private const string SuccessfullyImportedPerformer
            = "Imported {0} ({1} songs)";

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            var writersDto = JsonConvert.DeserializeObject<ImportWritersDto[]>(jsonString);

            var sb = new StringBuilder();

            var writers = new List<Writer>();

            foreach (var writerDto in writersDto)
            {
                if (!IsValid(writerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var currentWriter = new Writer()
                {
                    Name = writerDto.Name,
                    Pseudonym = writerDto.Pseudonym
                };

                writers.Add(currentWriter);
                sb.AppendLine(String.Format(SuccessfullyImportedWriter, currentWriter.Name));
            }

            context.Writers.AddRange(writers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {
            var producersDto = JsonConvert.DeserializeObject<ImportProducersDto[]>(jsonString);

            var sb = new StringBuilder();

            var producers = new List<Producer>();

            foreach (var producerDto in producersDto)
            {
                if (!IsValid(producerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var producer = new Producer()
                {
                    Name = producerDto.Name,
                    Pseudonym = producerDto.Pseudonym,
                    PhoneNumber = producerDto.PhoneNumber
                };

                bool isAlbumCorrect = true;

                foreach (var album in producerDto.Albums)
                {
                    if (!IsValid(album))
                    {
                        sb.AppendLine(ErrorMessage);
                        isAlbumCorrect = false;
                        break;
                    }

                    DateTime ReleaseDate;

                    bool isCorrectReleaseDate = DateTime
                    .TryParseExact(album.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ReleaseDate);

                    if (!isCorrectReleaseDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        isAlbumCorrect = false;
                        break;
                    }

                    var currentAlbum = new Album()
                    {
                        Name = album.Name,
                        ReleaseDate = ReleaseDate
                    };

                    producer.Albums.Add(currentAlbum);
                }

                if (isAlbumCorrect)
                {
                    producers.Add(producer);

                    if (!producer.PhoneNumber.IsNullOrEmpty())
                    {
                        sb.AppendLine(string.Format(SuccessfullyImportedProducerWithPhone, producer.Name, producer.PhoneNumber, producer.Albums.Count));
                    }
                    else
                    {
                        sb.AppendLine(string.Format(SuccessfullyImportedProducerWithNoPhone, producer.Name, producer.Albums.Count));
                    }
                }
            }

            context.Producers.AddRange(producers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportSongsDto[]), new XmlRootAttribute("Songs"));

            var sb = new StringBuilder();

            using (var reader = new StringReader(xmlString))
            {
                var songsDto = (ImportSongsDto[])xmlSerializer.Deserialize(reader);

                var songs = new List<Song>();

                foreach (var songDto in songsDto)
                {
                    if (!IsValid(songDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime songCreatedOn;

                    bool isSongCreatedOn = DateTime
                        .TryParseExact(songDto.CreatedOn, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out songCreatedOn);

                    if (!isSongCreatedOn)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    TimeSpan songDuration;

                    bool isSongDuration = TimeSpan
                       .TryParseExact(songDto.Duration, "c", CultureInfo.InvariantCulture, out songDuration);

                    if (!isSongDuration)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Genre songGenre;

                    var isSongGenre = Enum
                       .TryParse(songDto.Genre, out songGenre);

                    if (!isSongGenre)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }


                    Album album = null;

                    if (songDto.AlbumId.HasValue)
                    {
                        album = context
                            .Albums
                            .FirstOrDefault(a => a.Id == songDto.AlbumId);
                    }

                    Writer writer = context
                        .Writers
                        .FirstOrDefault(w => w.Id == songDto.WriterId);

                    if (writer == null || album == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var currentSong = new Song()
                    {
                        Name = songDto.Name,
                        Duration = songDuration,
                        CreatedOn = songCreatedOn,
                        Genre = songGenre,
                        AlbumId = songDto.AlbumId,
                        WriterId = songDto.WriterId,
                        Price = songDto.Price
                    };

                    songs.Add(currentSong);
                    sb.AppendLine(String.Format(SuccessfullyImportedSong, currentSong.Name, currentSong.Genre, currentSong.Duration));
                }

                context.Songs.AddRange(songs);
                context.SaveChanges();

                return sb.ToString().TrimEnd();
            }
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportPerformerDto[]), new XmlRootAttribute("Performers"));

            var sb = new StringBuilder();

            using (var reader = new StringReader(xmlString))
            {
                var performersDto = (ImportPerformerDto[])xmlSerializer.Deserialize(reader);

                var performers = new List<Performer>();

                foreach (var performerDto in performersDto)
                {
                    if (!IsValid(performerDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var currentPerformer = new Performer()
                    {
                        FirstName = performerDto.FirstName,
                        LastName = performerDto.LastName,
                        Age = performerDto.Age,
                        NetWorth = performerDto.NetWorth
                    };

                    bool isSongIdCorrect = true;

                    foreach (var song in performerDto.PerformersSongs)
                    {
                        if (!IsValid(song))
                        {
                            sb.AppendLine(ErrorMessage);
                            isSongIdCorrect = false;
                            break;
                        }

                        var findSongId = context
                            .Songs
                            .FirstOrDefault(s => s.Id == song.Id);

                        if (findSongId==null)
                        {
                            sb.AppendLine(ErrorMessage);
                            isSongIdCorrect = false;
                            break;
                        }

                        SongPerformer songPerformer = new SongPerformer()
                        {
                            Song = findSongId,
                            Performer = currentPerformer
                        };

                        currentPerformer.PerformerSongs.Add(songPerformer);
                    }

                    if (isSongIdCorrect)
                    {
                        performers.Add(currentPerformer);
                        sb.AppendLine(String.Format(SuccessfullyImportedPerformer, currentPerformer.FirstName, currentPerformer.PerformerSongs.Count));
                    }
                }

                context.Performers.AddRange(performers);
                context.SaveChanges();

                return sb.ToString().TrimEnd();
            }
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}