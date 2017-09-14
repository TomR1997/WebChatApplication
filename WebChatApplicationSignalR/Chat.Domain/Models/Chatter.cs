using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebChat.Domain.Models
{
    public abstract class Chatter
    {
        #region Properties, fields
        public List<Chat> chats { get; set; }
        public int ChatterId { get; set; }
        public string UserName { get; set; }

        private int chatterId;
        private string userName;
        #endregion
        public Chatter(int chatterId, string userName)
        {
            this.chatterId = chatterId;
            this.userName = userName;
            chats = new List<Chat>();
        }
    }
}