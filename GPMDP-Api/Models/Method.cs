using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GPMDP_Api.Models
{
    public class Command
    {
        [JsonProperty("namespace")]
        public string Namespace { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }
        [JsonProperty("arguments")]
        public object Arguments { get; set; }
    }
}
