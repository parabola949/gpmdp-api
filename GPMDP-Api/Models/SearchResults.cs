using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GPMDP_Api.Models
{

    public class SearchResults : Message
    {
        [JsonProperty("payload")]
        new public Results Payload { get; set; }
    }

    public class Results
    {
        public Album[] albums { get; set; }
        public Artist[] artists { get; set; }
        public string searchText { get; set; }
        public Track[] tracks { get; set; }
    }

    public class Album
    {
        public string albumArt { get; set; }
        public string artist { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public object[] tracks { get; set; }
    }

    public class Artist
    {
        public object[] albums { get; set; }
        public string id { get; set; }
        public string image { get; set; }
        public string name { get; set; }
    }

}
