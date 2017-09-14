using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WebChat.Domain.Models;

namespace WebChat.Domain.Models {
    [DataContract]
    public class Chat {
        #region Properties, fields
        [DataMember]
        public List<Message> messages { get; private set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ChatClient { get; set; }
        [DataMember]
        public int ChatSupporter { get; set; }
        [DataMember]
        public Status Status { get; set; }
        [DataMember]

        private int id;
        private ChatClient chatClient;
        private ChatSupporter chatSupporter;
        private Status status;

        #endregion
        public Chat(int id, ChatClient chatClient, ChatSupporter chatSupporter, Status status, List<Message> messages)
        {
            this.id = id;
            this.chatClient = chatClient;
            this.chatSupporter = chatSupporter;
            this.messages = messages;
            this.status = status;
        }
    }
}