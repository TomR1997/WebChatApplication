using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace WebChat.Services.Exceptions {
    public class MessageFailedException : Exception {

        public MessageFailedException()
        {

        }

        public MessageFailedException(string message) : base(message)
        {

        }
    }
}