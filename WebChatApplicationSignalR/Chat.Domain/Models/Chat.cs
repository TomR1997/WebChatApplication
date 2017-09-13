using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Domain.Models {
    public class Chat {
        #region Properties, fields
        public List<Message> messages { get; private set; }
        public int Id { get; set; }
        public int ChatClientId { get; set; }
        public int ChatSupportId { get; set; }

        private int id;
        private int chatClientId;
        private int chatSupportId;
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