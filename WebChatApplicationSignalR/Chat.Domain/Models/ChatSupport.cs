using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WebChat.Domain.Models;

namespace WebChat.Domain.Models {
    [DataContract]
    public class ChatSupporter : Chatter {
        #region Properties, fields
        [DataMember]
        private string department;

        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        #endregion
        public ChatSupporter(int chatterId, string screenName, string department) : base(chatterId, screenName)
        {
            this.department = department;
        }
    }
}