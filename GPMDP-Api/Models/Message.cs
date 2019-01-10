using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GPMDP_Api.Models
{
    public class Message
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }
        public object Payload { get; set; }

        public Message ToObject(string data)
        {
            var m = JsonConvert.DeserializeObject<Message>(data);
            switch (m.Channel)
            {
                case "connect":
                    return JsonConvert.DeserializeObject<Connect>(data);
                case "API_VERSION":
                    return JsonConvert.DeserializeObject<ApiVersion>(data);
                case "playState":
                    return JsonConvert.DeserializeObject<PlayState>(data);
                case "track":
                    return JsonConvert.DeserializeObject<TrackResult>(data);
                case "volume":
                    return JsonConvert.DeserializeObject<Volume>(data);
                case "lyrics":
                    return JsonConvert.DeserializeObject<Lyrics>(data);
                case "time":
                    return JsonConvert.DeserializeObject<Time>(data);
                case "shuffle":
                    return JsonConvert.DeserializeObject<Shuffle>(data);
                case "rating":
                    return JsonConvert.DeserializeObject<Rating>(data);
                case "repeat":
                    return JsonConvert.DeserializeObject<Repeat>(data);
                case "playlists":
                    return JsonConvert.DeserializeObject<Playlists>(data);
                case "queue":
                    return JsonConvert.DeserializeObject<Queue>(data);
                case "search-results":
                    return JsonConvert.DeserializeObject<SearchResults>(data);
                case "library":
                    return JsonConvert.DeserializeObject<Library>(data);
            }
            return null;
        }
    }
}
