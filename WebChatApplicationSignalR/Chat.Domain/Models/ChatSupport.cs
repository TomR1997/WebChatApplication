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
        private string defaultMessage;

        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        public string DefaultMessage
        {
            get { return defaultMessage; }
            set { defaultMessage = value; }
        }

        #endregion
        public ChatSupporter(int chatterId, string screenName, string department) : base(chatterId, screenName)
        {
            this.department = department;
            this.defaultMessage = "";
        }

        public ChatSupporter(int chatterId, string screenName, string department, string defaultMessage) : base(chatterId, screenName)
        {
            this.department = department;
            this.defaultMessage = defaultMessage;
        }
    }
}