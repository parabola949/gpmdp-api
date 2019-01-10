using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPMDP_Api.Models
{

    public class Queue : Message
    {
        [JsonProperty("payload")]
        new public Track[] Payload { get; set; }
    }
}

