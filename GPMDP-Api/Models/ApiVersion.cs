using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPMDP_Api.Models
{
    public class ApiVersion : Message
    {
        [JsonProperty("payload")]
        public new string Payload { get; set; }
    }
}
