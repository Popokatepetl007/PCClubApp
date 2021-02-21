
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
        private static string live_ws_url = "http://5.129.77.65:8124";
        public static string loc_ws_url = "http://10.0.0.11:8124";
        public static ISocketChat soc_chat;
        public static void StartWS()
        {
            var socket = IO.Socket(WSManager.live_ws_url);
            Trace.WriteLine("--Socket INIT--");
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                Trace.WriteLine("--Socket Connected--");
                string regMessage = Newtonsoft.Json.JsonConvert.SerializeObject(new { compId = ProfileManager.compId, token = ClanREST.UserToken });
                socket.Emit("openSession", regMessage);
            });


            socket.On(String.Format("/topic/chat/user/{0}", ProfileManager.userID), (data) => WSManager.OnMessage(data));

            socket.Connect();
        }

        private static void OnMessage(object data)
        {
            Trace.WriteLine("---Chat Message comin------");
            Trace.WriteLine(data);
            Trace.WriteLine("---------------------");
            dynamic jOb = JObject.Parse(data.ToString());
            int c_id = jOb.id;
            soc_chat.MessageInput(c_id);
        }

        private static void OnCompManage(object data)
        {

        }

    }
}
