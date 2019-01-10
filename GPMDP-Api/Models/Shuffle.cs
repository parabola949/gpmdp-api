using GPMDP_Api.Converters;
using GPMDP_Api.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPMDP_Api.Models
{
    public class Shuffle : Message
    {
        [JsonProperty("payload")]
        [JsonConverter(typeof(ShuffleTypeConverter))]
        public ShuffleType Type { get; set; }
    }
}
