using System;

namespace WebChatApplicationSignalR.Models {
    public class ChatMessage {
        public IChatter Receiver { get; set; }
        public IChatter Sender { get; set; }
        public DateTime TimeSend { get; set; }
        public string Message { get; set; }
        public bool Read { get; set; }
    }
}