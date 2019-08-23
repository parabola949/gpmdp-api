using System;
using System.Threading;
using GPMDP_Api;
using GPMDP_Api.Playback;

using System.IO;
using GPMDP_Api.Enums;
using GPMDP_Api.Extras;

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


            //create client
            c = new Client();

            //set up event handlers
            c.ConnectReceived += C_ConnectReceived;
            c.TrackResultReceived += C_TrackReceived;
            c.ApiVersionReceived += C_ApiVersionReceived;
            c.LibraryReceived += C_LibraryReceived;
            
            //connect to the web socket
            c.Connect();

            //authenticate, enables controls
            c.Authenticate(AuthCode);

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
                    case ConsoleKey.A:
                        //testing random commands
                        c.RewindTenAsync();

                        break;
                    case ConsoleKey.Enter:
                        var t = c.ToggleShuffleAsync().Result;
                        Console.WriteLine(t);
                        break;
                       
                }
            }
        }

        private static void C_LibraryReceived(object sender, GPMDP_Api.Models.Contents e)
        {
            Console.WriteLine($"Library contents: {e.tracks.Length} tracks, {e.albums.Length} albums, {e.artists.Length} artists");
        }

        private static void C_ApiVersionReceived(object sender, string e)
        {
            Console.WriteLine("Api Version: " + e);
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
