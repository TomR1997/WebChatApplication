using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebChat.Domain.Models {
    //TODO Remove the commented out code?

    public abstract class Chatter {
        #region Properties, fields
        //public List<Chat> chats { get; set; }
        public int ChatterId { get; set; }
        public string Screenname { get; set; }

        private int chatterId;
        private string screenName;
        #endregion

        public Chatter(int chatterId, string screenName)
        {
            this.chatterId = chatterId;
            this.screenName = screenName;

            //chats = new List<Chat>();
        }
    }
}