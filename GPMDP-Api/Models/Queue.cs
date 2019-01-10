using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPMDP_Api.Models
{

    public class Queue : Message
    {
        [JsonProperty("payload")]
        public Track[] Tracks { get; set; }
    }
}

