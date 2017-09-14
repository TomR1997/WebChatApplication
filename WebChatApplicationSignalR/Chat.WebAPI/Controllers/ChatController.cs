using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Chat.Data;
using Chat.Service.Services;

namespace Chat.WebAPI.Controllers {
    [Authorize]
    [RoutePrefix("Api/Chat")]
    public class ChatController : ApiController {
        private IChatService chatService = new ChatService();

        public IEnumerable<chat> GetAllChats()
        {
            return chatService.GetAllChats();
        }
    }
}
