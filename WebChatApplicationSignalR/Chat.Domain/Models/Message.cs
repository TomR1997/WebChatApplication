using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebChat.Domain.Models
{
    public class Message
    {
        #region Properties, fields
        public int ChatId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime TimeSend { get; set; }
        public string MessageContent { get; set; }
        public bool Read { get; set; }

        private int chatId;
        private int senderId;
        private int receiverId;
        private DateTime timeSend;
        private string messageContent;
        private bool read;
        #endregion

        public Message(int chatId, int senderId, int receiverId, DateTime timeSend, string messageContent, bool read)
        {
            this.chatId = chatId;
            this.senderId = senderId;
            this.receiverId = receiverId;
            this.timeSend = timeSend;
            this.messageContent = messageContent;
            this.read = read;
        }     
    }
}