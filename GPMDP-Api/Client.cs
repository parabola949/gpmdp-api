using GPMDP_Api.Enums;
using GPMDP_Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WebSocketSharp;

namespace GPMDP_Api
{
    public class Client
    {
        private WebSocket _ws;
        public string Uri { get; set; }
        public int Port { get; set; }
        public string AppName { get; set; }
        public Client(string appName = "gpmdp-api", string uri = "localhost", int port = 5672)
        {
            Uri = uri;
            Port = port;
            AppName = appName;
        }

        public void Connect()
        {
            if (_ws != null)
            {
                _ws.Close();
                _ws = null;
            }
            _ws = new WebSocket($"ws://{Uri}:{Port}");
            _ws.OnMessage += _ws_OnMessage;
            _ws.OnError += _ws_OnError;
            _ws.Connect();
            SendCommand("connect", "connect", AppName);
        }

        public void SendCommand(string ns, string method, object arguments)
        {
            var c = new Command
            {
                Namespace = ns,
                Method = method,
                Arguments = arguments
            };
            _ws.Send(JsonConvert.SerializeObject(c));
        }

        private void _ws_OnError(object sender, WebSocketSharp.ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _ws_OnMessage(object sender, MessageEventArgs e)
        {
            var m = new Message().ToObject(e.Data);
            if (m == null)
            {
                Console.WriteLine(e.Data);
                Console.Read();
            }
            if (m is Connect c)
            {
                if (c.payload == "CODE_REQUIRED")
                    CodeRequired.Invoke(this, null);
                else if (Guid.TryParse(c.payload, out Guid g))
                    ConnectionSuccessful.Invoke(this, g);
                return;
            }
            MessageReceived?.Invoke(this, m);
            #region Fire events
            if (m is Queue q)
                QueueReceived?.Invoke(this, q.Tracks);
            else if (m is TrackResult t)
                TrackReceived?.Invoke(this, t.Track);
            else if (m is ApiVersion v)
                VersionReceived?.Invoke(this, v.Version);
            else if (m is PlayState ps)
                PlayStateReceived?.Invoke(this, ps.IsPlaying);
            else if (m is Volume vl)
                VolumeReceived?.Invoke(this, vl.Level);
            else if (m is Lyrics l)
                LyricsReceived?.Invoke(this, l.Payload);
            else if (m is Time ti)
                TimeReceived?.Invoke(this, ti.Values);
            else if (m is Shuffle s)
                ShuffleReceived?.Invoke(this, s.Type);
            else if (m is Rating r)
                RatingReceived?.Invoke(this, r.Values);
            else if (m is Repeat re)
                RepeatReceived?.Invoke(this, re.Type);
            else if (m is Playlists pl)
                PlaylistReceived?.Invoke(this, pl.Lists);
            else if (m is SearchResults sr)
                SearchResultsReceived?.Invoke(this, sr.Results);
            else if (m is Library li)
                LibraryReceived?.Invoke(this, li.Contents);
            #endregion
            else
                Console.WriteLine(e.Data);


            //var @switch = new Dictionary<Type, Action<Message>>
            //{
            //    { typeof(Queue), (x) => QueueReceived?.Invoke(this, (x as Queue).Tracks ) }
            //};

            //var t = m.GetType();
            //if (@switch.ContainsKey(t))
            //@switch[t](m);

        }

        #region Events
        public event EventHandler<Track[]> QueueReceived;
        public event EventHandler<Track> TrackReceived;
        public event EventHandler<string> VersionReceived;
        public event EventHandler<bool> PlayStateReceived;
        public event EventHandler<int> VolumeReceived;
        public event EventHandler<string> LyricsReceived;
        public event EventHandler<TimeValues> TimeReceived;
        public event EventHandler<ShuffleType> ShuffleReceived;
        public event EventHandler<LikedValues> RatingReceived;
        public event EventHandler<RepeatType> RepeatReceived;
        public event EventHandler<Playlist[]> PlaylistReceived;
        public event EventHandler<Results> SearchResultsReceived;
        public event EventHandler<Contents> LibraryReceived;
        public event EventHandler CodeRequired;
        public event EventHandler<Guid> ConnectionSuccessful;
        public event EventHandler<Message> MessageReceived;
        #endregion
    }
}
