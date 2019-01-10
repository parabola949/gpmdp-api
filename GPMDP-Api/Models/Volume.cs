using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPMDP_Api.Models
{
    public class Volume : Message
    {
        [JsonProperty("payload")]
        new public int Payload { get; set; }
    }
}
