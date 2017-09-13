using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Domain.Models
{
    public abstract class Chatter
    {
        public int ChatterId { get; set; }
        public string UserName { get; set; }

        private int chatterId;
        private string userName;

        public Chatter(int chatterId, string userName)
        {
            this.chatterId = chatterId;
            this.userName = userName;
        }
    }
}