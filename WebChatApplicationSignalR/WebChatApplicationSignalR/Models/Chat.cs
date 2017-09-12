using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//TODO Move this to a seperate file later.
public enum ChatStatus {
    Closed, Open
}

namespace WebChatApplicationSignalR.Models {
    public class Chat {
        public int Id { get; set; }
        public ChatClient Client { get; set; }
        public ChatSupporter Supporter { get; set; }
        public ChatStatus Status { get; set; }
        public List<ChatMessage> Message { get; set; }
    }
}