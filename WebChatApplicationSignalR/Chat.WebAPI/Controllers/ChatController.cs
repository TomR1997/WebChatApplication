using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Chat.Data;
using Chat.Service.Services;

namespace Chat.WebAPI.Controllers {
    public class ChatController : ApiController {
        private IChatService chatService = new ChatService();
        public IEnumerable<Domain.Models.Chat> Get()
        {
            var chats = chatService.GetAllChats();
            var result = new List<Domain.Models.Chat>();
            foreach (var chat in chats)
            {
                result.Add(new Domain.Models.Chat(chat.ChatId, chat.ChatClientId, chat.ChatSupporterId));
            }
            return result;
        }
    }
}
