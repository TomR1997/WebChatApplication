﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Domain.Models
{
    public class ChatSupport : Chatter
    {
        #region Properties, fields
        public string Department { get; set; }

        private string department;
        #endregion
        public ChatSupport(int chatterId, string userName, string department) : base(chatterId, userName)
        {
            this.department = department;
        }
    }
}