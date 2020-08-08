using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MusicHub.DataProcessor.ExportDtos
{
    [XmlType("Song")]
    public class ExportSongsDto
    {
        [XmlElement("SongName")]
        public string Name { get; set; }

        [XmlElement("Writer")]
        public string WriterName { get; set; }

        [XmlElement("Performer")]
        public string PerformerName { get; set; }

        [XmlElement("AlbumProducer")]
        public string AlbumProducer { get; set; }

        [XmlElement("Duration")]
        public string Duration { get; set; }
    }
}
