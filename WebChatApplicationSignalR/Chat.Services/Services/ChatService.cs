using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chat.Data;

namespace Chat.Services.Services {
    public class ChatService : IChatService {
        private ChatDbEntities db = new ChatDbEntities();

        public IEnumerable<chat> GetAllChats()
        {
            return db.chats;
        }

        public IEnumerable<chat> GetChatsByChatterId(int chatterId)
        {
            var chats = GetAllChats();
            var queryResult =
                from chat in chats
                where chat.ChatSupporterId == chatterId || chat.ChatClientId == chatterId
                select chat;
            return queryResult;
        }

        public IEnumerable<message> GetAllMessages()
        {
            return db.messages;
        }

        public IEnumerable<message> GetMessagesByChatId(int chatId)
        {
            var messages = GetAllMessages();
            var queryResult =
                from message in messages
                where message.ChatId == chatId
                select message;
            return queryResult;
        }
    }
}