using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebChat.Domain.Models {
    [DataContract]
    public class Chat {
        #region Properties, fields
        [DataMember]
        public List<Message> messages { get; private set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ChatClientId { get; set; }
        [DataMember]
        public int ChatSupportId { get; set; }

        [DataMember]
        private int id;
        [DataMember]
        private int chatClientId;
        [DataMember]
        private int chatSupportId;
        [DataMember]
        private string status;
        #endregion
        public Chat(int id, int chatClientId, int chatSupportId)
        {
            this.id = id;
            this.chatClientId = chatClientId;
            this.chatSupportId = chatSupportId;
            messages = new List<Message>();
        }
    }
}