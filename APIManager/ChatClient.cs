using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCClubApp
{
    class ChatClient : ISocketChat, IRequestChat
    {
        private ClanREST req;
        public IViewChatDelegate view_chat_delegate;

        public ChatClient(IViewChatDelegate iDelegate)
        {
            req = new ClanREST(this);
            view_chat_delegate = iDelegate;
        }

        void sendMessage(string messageText)
        {
            req.PostChatMessage(messageText);
        }

        public void MessageInput(int id)
        {
            req.GetMessgeByID(id);
        }

        public void MessgateResult(ChatMessage chatMessage)
        {
            view_chat_delegate.NewMessageEvent(chatMessage);
        }

    }
}
