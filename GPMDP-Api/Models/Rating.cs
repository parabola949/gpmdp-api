using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GPMDP_Api.Models
{
    public class Rating : Message
    {
        [JsonProperty("payload")]
        new public LikedValues Payload { get; set; }
    }

    public class LikedValues
    {
        [JsonProperty("liked")]
        public bool Liked { get; set; }

        [JsonProperty("disliked")]
        public bool Disliked { get; set; }
    }
}
