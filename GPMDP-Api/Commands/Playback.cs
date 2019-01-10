using GPMDP_Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPMDP_Api.Commands
{
    public static class Playback
    {
        public static void PlayPause(this Client c)
        {
            c.SendCommand("playback", "playPause");
        }

        public static TimeSpan GetCurrentTime(this Client c)
        {
            var r = c.GetCommand("playback", "getCurrentTime").Result;
            var ms = long.Parse(r.ToString());
            return TimeSpan.FromMilliseconds(ms);
        }

        public static void SetCurrentTime(this Client c, long milliseconds)
        {
            c.SendCommand("playback", "setCurrentTime", milliseconds);
        }

        public static TimeSpan GetTotalTime(this Client c)
        {
            var r = c.GetCommand("playback", "getTotalTime").Result;
            var ms = long.Parse(r.ToString());
            return TimeSpan.FromMilliseconds(ms);
        }

        public static Track GetCurrentTrack(this Client c)
        {
            var r = c.GetCommand("playback", "getCurrentTrack").Result;
            return JsonConvert.DeserializeObject<Track>(r);
        }

        public static bool IsPlaying(this Client c)
        {
            return bool.Parse(c.GetCommand("playback", "isPlaying").Result);
        }


        public static void Rewind(this Client c)
        {
            c.SendCommand("playback", "rewind");
        }

        public static void Forward(this Client c)
        {
            c.SendCommand("playback", "forward");
        }
    }
}
