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

        public ChatMessage(string textMessage)
        {
            this.textMessage = textMessage;
        }

        public ChatMessage(dynamic jObj)
        {
            this.textMessage = jObj.content;
            this.timestamp = jObj.timestamp;
            this.id = jObj.id;
            this.chatId = jObj.chatId;
            this.userId = jObj.userId;
        }

        public string Text
        { get => this.textMessage; }

        public string TimeStamp
        { get => this.timestamp; }

        public int Id
        { get => this.id; }

    }
}
