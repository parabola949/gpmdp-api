using System;
using System.Threading;
using GPMDP_Api;

namespace Testing
{
    class Program
    {
        static Client c;
        static void Main(string[] args)
        {
            c = new Client();
            c.CodeRequired += C_CodeRequired;
            c.TrackReceived += C_TrackReceived;
            c.QueueReceived += C_QueueReceived1;
            c.Connect();
            Thread.Sleep(-1);
        }

        private static void C_QueueReceived1(object sender, GPMDP_Api.Models.Track[] e)
        {
            throw new NotImplementedException();
        }

        private static void C_TrackReceived(object sender, GPMDP_Api.Models.Track e)
        {
            Console.WriteLine($"Current track: {e.artist} - {e.title} from {e.album}");
        }

        private static void C_CodeRequired(object sender, EventArgs e)
        {
            Console.Write("Please enter the code from GPMDP: ");
            var code = Console.ReadLine();
            c.SendCommand("connect", "connect", new[] { c.AppName, code });
        }

        private static void C_QueueReceived(object sender, GPMDP_Api.Models.Queue e)
        {
            
        }


    }
}
