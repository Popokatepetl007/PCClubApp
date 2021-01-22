
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using System.Diagnostics;
using PCClubApp.APIManager;
using System.Threading;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;




namespace PCClubApp
{
    class WSManager
    {
        private static string ws_url = "ws://5.129.77.65:8123/ws/websocket";
        public static ISocketChat soc_chat;
        public static void StartWS()
        {
            var socket = IO.Socket("http://10.0.0.2:8124");
            
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                Trace.WriteLine("--Socket Connected--");
                string regMessage = Newtonsoft.Json.JsonConvert.SerializeObject(new { compId = ProfileManager.compId, token = ClanREST.UserToken });
                socket.Emit("openSession", regMessage);
            });

            socket.On(String.Format("/topic/chat/user/{0}", ProfileManager.userID), (data) =>
            {
                Trace.WriteLine(data);
                dynamic jOb = JObject.Parse(data.ToString());
                soc_chat.MessageInput(jOb.id);
                
            });
            socket.Connect();
        }
    }
}
