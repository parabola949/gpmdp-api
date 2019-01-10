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
    }
}
