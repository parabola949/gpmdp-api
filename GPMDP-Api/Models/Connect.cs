using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GPMDP_Api.Models
{
    public class Connect : Message
    {
        [JsonProperty("payload")]
        public new string Payload { get; set; }
    }
}
