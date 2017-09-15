using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebChat.Domain.Models {
    [DataContract]
    public class Message {
        #region Properties, fields
        [DataMember]
        private int chatId;
        [DataMember]
        private int senderId;
        [DataMember]
        private int receiverId;
        [DataMember]
        private DateTime timeSend;
        [DataMember]
        private string _content;
        [DataMember]
        private bool read;

        public int ChatId
        {
            get { return chatId; }
            set { chatId = value; }
        }

        public int SenderId
        {
            get { return senderId; }
            set { senderId = value; }
        }

        public int ReceiverId
        {
            get { return receiverId; }
            set { receiverId = value; }
        }

        public DateTime TimeSend
        {
            get { return timeSend; }
            set { timeSend = value; }
        }

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public bool Read
        {
            get { return read; }
            set { read = value; }
        }

        #endregion

        public Message(int chatId, int senderId, int receiverId, DateTime timeSend, string _content, bool read)
        {
            this.chatId = chatId;
            this.senderId = senderId;
            this.receiverId = receiverId;
            this.timeSend = timeSend;
            this._content = _content;
            this.read = read;
        }
    }
}