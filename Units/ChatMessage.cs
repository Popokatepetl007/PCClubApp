using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCClubApp
{
    public class ChatMessage
    {
        private string textMessage;
        private string timestamp;
        private string chatId;
        private int id;
        private int userId;
        private EMessageType message_type;

        public ChatMessage(string textMessage)
        {
            this.textMessage = textMessage;
            this.message_type = EMessageType.Sent;
        }

        public ChatMessage(dynamic jObj)
        {
            this.textMessage = jObj.content;
            this.timestamp = jObj.timestamp;
            this.id = jObj.id;
            this.chatId = jObj.chatId;
            this.userId = jObj.userId;
            this.message_type = EMessageType.Recived;
        }

        public string Text
        { get => this.textMessage; }

        public string TimeStamp
        { get => this.timestamp; }

        public int Id
        { get => this.id; }

        public EMessageType MessageType
        { get => this.message_type; }


    }

    public enum EMessageType
    {
        Sent,
        Recived
    }
}
