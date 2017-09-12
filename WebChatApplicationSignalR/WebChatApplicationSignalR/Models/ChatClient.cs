using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebChatApplicationSignalR.Models {
    public class ChatClient : IChatter {
        public int Id { get; set; }
        public string Screenname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Department { get; set; }
        public string DefaultMessage { get; set; }
    }
}