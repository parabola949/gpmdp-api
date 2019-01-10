using System;
using System.Collections.Generic;
using System.Text;

namespace GPMDP_Api.Models
{
    public class Track : Message
    {
        public string album { get; set; }
        public string albumArt { get; set; }
        public string albumArtist { get; set; }
        public string albumId { get; set; }
        public string artist { get; set; }
        public string artistId { get; set; }
        public int duration { get; set; }
        public string id { get; set; }
        public int index { get; set; }
        public int playCount { get; set; }
        public string title { get; set; }
    }
}
