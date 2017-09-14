using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebChat.Domain.Models {
    public class ChatClient : Chatter {
        #region Properties, fields
        public string Branch { get; set; }

        private string branch;
        #endregion
        public ChatClient(int chatterId, string screenName, string branch) : base(chatterId, screenName)
        {
            this.branch = branch;
        }
    }
}