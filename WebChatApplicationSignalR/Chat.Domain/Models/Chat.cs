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
        private List<Message> messages;
        [DataMember]
        private int id;
        [DataMember]
        private ChatClient chatClient;
        [DataMember]
        private ChatSupporter chatSupporter;
        [DataMember]
        private Status status;

        public List<Message> Messages
        {
            get { return messages; }
            set { messages = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public ChatClient ChatClient
        {
            get { return chatClient; }
            set { chatClient = value; }
        }

        public ChatSupporter ChatSupporter
        {
            get { return chatSupporter; }
            set { chatSupporter = value; }
        }

        public Status Status
        {
            get { return status; }
            set { status = value; }
        }

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