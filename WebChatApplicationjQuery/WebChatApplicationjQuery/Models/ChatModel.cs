using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebChatApplicationjQuery.Models
{
    public class ChatModel
    {
        public List<ChatUser> users;
        public List<ChatMessage> chatHistory;

        public ChatModel()
        {
            users = new List<ChatUser>();
            chatHistory = new List<ChatMessage>();

            chatHistory.Add(new ChatMessage()
            {
                message="The chat started at "+DateTime.Now
            });
        }

        public class ChatUser
        {
            public string userName;
            public DateTime loggedOnTime;
            public DateTime lastPing;
        }

        public class ChatMessage
        {
            public ChatUser byUser;
            public DateTime when = DateTime.Now;
            public string message = "";
        }
    }
}