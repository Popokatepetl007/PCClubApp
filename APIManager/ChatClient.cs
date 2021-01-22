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

        public ChatClient()
        {
            WSManager.StartWS();
            req = new ClanREST(this);
        }

        public void MessageInput(int id)
        {
            
        }

        void IRequestChat.MessgateResult(string message)
        {
            
        }
    }
}
