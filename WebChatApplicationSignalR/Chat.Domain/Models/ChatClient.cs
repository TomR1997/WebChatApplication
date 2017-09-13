﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Domain.Models
{
    public class ChatClient : Chatter
    {
        public string Branch { get; set; }

        private string branch;

        public ChatClient(int chatterId, string userName, string branch) : base(chatterId, userName)
        {
            this.branch = branch;
        }
    }
}