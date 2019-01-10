using GPMDP_Api.Enums;
using GPMDP_Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using WebSocketSharp;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GPMDP_Api
{
    public partial class Client
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
            if (_ws != null)
            {
                _ws.Close();
                _ws = null;
            }
            _ws = new WebSocket($"ws://{Uri}:{Port}");
            _ws.OnMessage += _ws_OnMessage;
            _ws.OnError += _ws_OnError;
            ResultReceived += Client_ResultReceived;
            _ws.Connect();
        }

        private void Client_ResultReceived(object sender, Result e)
        {
            _results[e.RequestId] = e;
        }

        public void Connect(string authCode = null)
        {
            SendCommand("connect", "connect", new[] { AppName, authCode });
        }

        public void SendCommand(string ns, string method, params object[] args)
        {
            var c = new Command
            {
                Namespace = ns,
                Method = method,
                Arguments = args
            };
            _ws.Send(JsonConvert.SerializeObject(c));
        }

        private int reqId = 0;
        private Dictionary<int, Result> _results = new Dictionary<int, Result>();
        public async Task<string> GetCommand(string ns, string method)
        {
            reqId++;
            var thisReq = reqId;
            var c = new Command
            {
                Namespace = ns,
                Method = method,
                RequestId = thisReq
            };
            object r = null;
            string type = null;
            _results.Add(thisReq, null);
            var json = JsonConvert.SerializeObject(c, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            _ws.Send(json);
            while (_results[thisReq] == null)
            {
                await Task.Delay(25);
            }
            type = _results[thisReq].Type;
            r = _results[thisReq].Value;
            _results.Remove(thisReq);
            if (type != "error")
                return r.ToString();
            else
                throw new Exception(r.ToString());

            
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
                
                var r = JsonConvert.DeserializeObject<Result>(e.Data);
                if (r?.Namespace == "result")
                {
                    ResultReceived?.Invoke(this, r);
                }
                else //DEBUG, checking for messages we don't know about
                    Console.WriteLine(e.Data);
                return;
            }

            if (m is Connect c)
            {
                if (c.Payload == "CODE_REQUIRED")
                    ConnectReceived.Invoke(this, null);
                else if (Guid.TryParse(c.Payload, out Guid g))
                    ConnectReceived.Invoke(this, c.Payload);
                return;
            }
            MessageReceived?.Invoke(this, m);

            Type t = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x == m.GetType() && x.IsSubclassOf(typeof(Message)));
            if (t != null)
            {
                dynamic nm = Convert.ChangeType(m, t);
                var em = GetType().GetField($"{t.Name}Received", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(this);
                em?.GetType().GetMethod("Invoke").Invoke(em, new[] { this, nm.Payload });
            }
            else
            {
                Console.WriteLine(e.Data);
            }
        }

        #region Events
        public event EventHandler<Track[]> QueueReceived;
        public event EventHandler<Track> TrackResultReceived;
        public event EventHandler<string> ApiVersionReceived;
        public event EventHandler<bool> PlayStateReceived;
        public event EventHandler<int> VolumeReceived;
        public event EventHandler<string> LyricsReceived;
        public event EventHandler<TimeValues> TimeReceived;
        public event EventHandler<ShuffleType> ShuffleReceived;
        public event EventHandler<LikedValues> RatingReceived;
        public event EventHandler<RepeatType> RepeatReceived;
        public event EventHandler<Playlist[]> PlaylistsReceived;
        public event EventHandler<Results> SearchResultsReceived;
        public event EventHandler<Contents> LibraryReceived;
        public event EventHandler<string> ConnectReceived;
        public event EventHandler<Message> MessageReceived;
        internal event EventHandler<Result> ResultReceived;
        #endregion
    }
}
