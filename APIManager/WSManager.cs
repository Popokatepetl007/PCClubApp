﻿
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
        private static Socket _socket;
        public static void StartWS(Action connect_ready)
        {
            var socket = IO.Socket(WSManager.live_ws_url);
            Trace.WriteLine("--Socket INIT--");
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                Trace.WriteLine("--Socket Connected--");
                string regMessage = Newtonsoft.Json.JsonConvert.SerializeObject(new { compId = ProfileManager.compId, token = ClanREST.UserToken });
                socket.Emit("openSession", regMessage);
                Thread.Sleep(3000);
                connect_ready();
                
            });

            socket.On(String.Format("/topic/chat/user/{0}", ProfileManager.userID), (data) => WSManager.OnMessage(data));
            socket.On(String.Format("/manage/{0}", ProfileManager.compId), (data) => WSManager.OnCompManage(data));

            socket.Connect();
            _socket = socket;
        }

        public static void CloseWS()
        {
            Trace.WriteLine("--Socket Closed--");
            _socket.Close();
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
            dynamic jOb = JObject.Parse(data.ToString());
            foreach (var i in Enum.GetValues(typeof(ESystemAction)))
            {
                if(i.ToString() == (string)jOb.action) SystemController.ActionComputer((ESystemAction)i);
            }

        }

    }
}
