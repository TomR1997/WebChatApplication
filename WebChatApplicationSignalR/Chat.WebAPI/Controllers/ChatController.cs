using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebChat.Data;
using WebChat.Domain.Models;
using WebChat.Service.Services;

namespace WebChat.WebAPI.Controllers {
    public class ChatController : ApiController {
        private IChatService _chatServerService = new ChatService();

        public IEnumerable<Chat> Get()
        {
            //return _chatServerService.GetAllChats().Select(chat => new Chat(chat.ChatId, new ChatClient(chat.ChatClientId), chat.ChatSupporterId)).ToList();
            throw new NotImplementedException();
        }
    }
}
