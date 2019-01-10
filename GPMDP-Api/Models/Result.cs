using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GPMDP_Api.Models
{
    public class Result : Message
    {
        [JsonProperty("namespace")]
        public string Namespace { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("value")]
        public object Value { get; set; }
        [JsonProperty("requestId")]
        public int RequestId { get; set; }
    }
}
