using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebChat.Data;
using WebChat.Domain.Models;
using WebChat.Service.Services;

namespace WebChat.WebAPI.Controllers {
    [RoutePrefix("api/chat")]
    public class ChatController : ApiController {
        private readonly IChatService _chatServerService = new ChatService();

        public IEnumerable<Chat> GetAllChats()
        {
            return _chatServerService.GetAllChats();
        }

        [Route("GetChatByChatId/{id}"), HttpGet, ResponseType(typeof(Chat))]
        public IHttpActionResult GetChatByChatId(int id)
        {
            return Ok(_chatServerService.GetChatsByChatterId(id));
        }

        [Route("CloseChatByChatId/{id}"), HttpGet]
        public void CloseChatByChatId(int id)
        {
            _chatServerService.CloseChat(id);
        }

    }
}
