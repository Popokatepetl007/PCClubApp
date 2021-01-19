
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using System.Diagnostics;






namespace PCClubApp
{
    class WSManager
    {
        private readonly string ws_url = "ws://5.129.77.65:8123/ws/websocket";
        public void ConnectAsync()
        {
            Trace.WriteLine("---------WS Manager----------");
            WebSocket ws = new WebSocket(this.ws_url);
            ws.OnMessage += (sender, e) =>
                Trace.WriteLine("Server said: " + e);
            ws.OnError += (s, e) =>
                Trace.WriteLine(e);
            
            string subscribe = "SUBSCRIBE\nid:123\ndestination:/topic/chat/user/0\n\n\0";
            string message = "{\"clubId\": 0, \"content\": \"hello\"}";
            string send_message = String.Format("SEND\nid:123\ndestination:/app/chat/user/send\n\n{0}\n\n\0", message);
            ws.Connect();
            ws.Send(subscribe);
            ws.Send(send_message);
            
        }


        public void Stamp()
        {
            
        }



    }
}
