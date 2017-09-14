using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WebChat.Domain.Models;

namespace WebChat.Domain.Models {
    public class Chat {
        #region Properties, fields
        public List<Message> messages { get; private set; }
        public int Id { get; set; }
        public int ChatClient { get; set; }
        public int ChatSupporter { get; set; }
        public ChatStatus Status { get; set; }

        private int id;
        private ChatClient chatClient;
        private ChatSupporter chatSupporter;
        private ChatStatus status;

        #endregion
        public Chat(int id, ChatClient chatClient, ChatSupporter chatSupporter)
        {
            this.id = id;
            this.chatClient = chatClient;
            this.chatSupporter = chatSupporter;
            messages = new List<Message>();
            status = ChatStatus.Active;
        }
    }
}