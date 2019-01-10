using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPMDP_Api.Models
{
    public class PlayState : Message
    {
        [JsonProperty("payload")]
        public bool IsPlaying { get; set; }
    }
}
