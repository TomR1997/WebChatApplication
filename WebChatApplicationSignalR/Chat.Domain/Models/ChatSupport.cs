using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebChat.Domain.Models;

namespace Chat.Domain.Models {
    public class ChatSupporter : Chatter {
        #region Properties, fields
        public string Department { get; set; }

        private string department;
        #endregion
        public ChatSupporter(int chatterId, string userName, string department) : base(chatterId, userName)
        {
            this.department = department;
        }
    }
}