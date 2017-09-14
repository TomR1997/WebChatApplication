using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebChat.Domain.Models {
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