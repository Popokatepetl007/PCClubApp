
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using System.Diagnostics;
using PCClubApp.APIManager;
using System.Threading;


namespace PCClubApp
{
    class WSManager
    {
        private static string ws_url = "ws://5.129.77.65:8123/ws/websocket";
        private static WebSocket ws;
        public static void ConnectAsync()
        {
            Trace.WriteLine("---------WS Manager----------");
            ws = new WebSocket(ws_url);
            ws.OnMessage += (sender, e) =>
                Trace.WriteLine("Server said: " + e.Data);
            ws.OnError += (s, e) =>
                Trace.WriteLine(e);

            string connectHeader = "{\"header\": {0}}".Replace("{0}", ClanREST.UserToken);

            //string subscribe = String.Format("SUBSCRIBE\nid:123\ndestination:/topic/chat/user/2\n\n{\"header\": \"{0}\"}\n\n\0", ClanREST.UserToken);
            string tryConnect = String.Format("CONNECT\nid:123\n\n{0}\n\n\0", connectHeader);

            ws.Connect();
            Thread.Sleep(1000);
            StompMessageSerializer serializer = new StompMessageSerializer();
            var connect = new StompMessage("CONNECT");
            connect["accept-version"] = "1.2";
            //connect["host"] = "";
            connect["Authorization"] = ClanREST.UserToken;
            Trace.WriteLine(serializer.Serialize(connect));
            ws.Send(serializer.Serialize(connect));
        }


        public void Stamp()
        {
            
        }


        public static void SendMessageToChat(string textMessage)
        {
            Trace.WriteLine(textMessage);
            string message = "{\"clubId\": 3, \"content\": \"{0}\"}".Replace("{0}", textMessage);
            string send_message = String.Format("SEND\nid:123\ndestination:/app/chat/user/send\n\n{0}\n\n\0", message);
            ws.Send(send_message);
        }


    }
}
