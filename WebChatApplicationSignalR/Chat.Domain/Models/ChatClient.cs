using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebChat.Domain.Models {
    [DataContract]
    public class ChatClient : Chatter {
        #region Properties, fields
        [DataMember]
        private string branch;

        public string Branch
        {
            get { return branch; }
            set { branch = value; }
        }

        #endregion
        public ChatClient(int chatterId, string screenName, string branch) : base(chatterId, screenName)
        {
            this.branch = branch;
        }
    }
}