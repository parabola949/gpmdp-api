using System;
using System.Threading;
using GPMDP_Api;
using System.IO;
namespace Testing
{
    class Program
    {
        static Client c;
        static string AuthCode = null;
        static void Main(string[] args)
        {
            if (File.Exists("auth.set"))
            {
                using (var sr = new StreamReader("auth.set"))
                    AuthCode = sr.ReadLine();
            }

            c = new Client();
            c.ConnectReceived += C_ConnectReceived;
            c.TrackResultReceived += C_TrackReceived;
            c.ApiVersionReceived += C_ApiVersionReceived;
            c.Connect(AuthCode);
            while (true)
            {
                var k = Console.ReadKey(true);
                switch (k.Key)
                {
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        return;
                    case ConsoleKey.C:
                        //play / pause
                        c.PlayPause();
                        break;
                }
            }
        }

        private static void C_ApiVersionReceived(object sender, string e)
        {
            Console.WriteLine(e);
        }

        private static void C_ConnectReceived(object sender, string e)
        {
            if (e == null)
            {
                Console.Write("Please enter the code from GPMDP: ");
                var code = Console.ReadLine();
                c.SendCommand("connect", "connect", new[] { c.AppName, code });
            }
            else
            {
                AuthCode = e.ToString();
                using (var sw = new StreamWriter("auth.set"))
                    sw.Write(AuthCode);
                Console.WriteLine("Connection successful");
            }
        }

        private static void C_TrackReceived(object sender, GPMDP_Api.Models.Track e)
        {
            Console.WriteLine($"Current track: {e.artist} - {e.title} from {e.album}");
        }



    }
}
