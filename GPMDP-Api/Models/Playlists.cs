using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPMDP_Api.Models
{

    public class Playlists : Message
    {
        [JsonProperty("payload")]
        public Playlist[] Lists { get; set; }
    }

    public class Playlist
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("tracks")]
        public Track[] Tracks { get; set; }
    }

}
