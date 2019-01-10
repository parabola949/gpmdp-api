using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPMDP_Api.Models
{
    public class Lyrics : Message
    {
        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}
