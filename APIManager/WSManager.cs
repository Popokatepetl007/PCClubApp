
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
            var socket = IO.Socket("http://5.129.77.65:8124");
            Trace.WriteLine("--Socket INIT--");
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                Trace.WriteLine("--Socket Connected--");
                string regMessage = Newtonsoft.Json.JsonConvert.SerializeObject(new { compId = ProfileManager.compId, token = ClanREST.UserToken });
                socket.Emit("openSession", regMessage);
            });
            
            socket.On(String.Format("/topic/chat/user/{0}", ProfileManager.userID), (data) =>
            {
                Trace.WriteLine("---Chat Message comin------");
                dynamic jOb = JObject.Parse(data.ToString());
                int c_id = jOb.id;
                soc_chat.MessageInput(c_id);
            });
            socket.Connect();
        }
    }
}
