using System;
using System.Collections.Generic;
using System.Text;

namespace GPMDP_Api
{
    public partial class Client
    {
        public void PlayPause()
        {
            SendCommand("playback", "playPause");
        }

        public void GetCurrentTime()
        {
            SendCommand("playback", "getCurrentTime");
        }

        public void SetCurrentTime(long milliseconds)
        {
            SendCommand("playback", "setCurrentTime", milliseconds);
        }

        public void GetTotalTime()
        {
            SendCommand("playback", "getTotalTime");
        }

        public void Rewind()
        {
            SendCommand("playback", "rewind");
        }

        public void Forward()
        {
            SendCommand("playback", "forward");
        }
    }
}
