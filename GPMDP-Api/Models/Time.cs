using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GPMDP_Api.Models
{
    public class Time : Message
    {
        [JsonProperty("payload")]
        public TimeValues Values { get; set; }
    }

    public class TimeValues
    {
        [JsonProperty("current")]
        public int Current { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
