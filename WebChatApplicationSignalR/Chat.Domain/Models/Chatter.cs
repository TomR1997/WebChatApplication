using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebChat.Domain.Models {
    //TODO Remove the commented out code?
    [DataContract]
    public abstract class Chatter {
        #region Properties, fields
        //public List<Chat> chats { get; set; }
        [DataMember]
        private int chatterId;
        [DataMember]
        private string screenName;

        public int ChatterId
        {
            get { return chatterId; }
            set { chatterId = value; }
        }

        public string ScreenName
        {
            get { return screenName; }
            set { screenName = value; }
        }

        #endregion

        public Chatter(int chatterId, string screenName)
        {
            this.chatterId = chatterId;
            this.screenName = screenName;

            //chats = new List<Chat>();
        }
    }
}