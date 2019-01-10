using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GPMDP_Api.Models
{
    public class TrackResult : Message
    {
        [JsonProperty("payload")]
        public Track Track { get; set; }
    }
}
