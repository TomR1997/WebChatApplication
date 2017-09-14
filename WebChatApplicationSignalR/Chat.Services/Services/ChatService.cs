using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebChat.Data;
using WebChat.Domain.Models;

namespace WebChat.Service.Services {
    public class ChatService : IChatService {
        private ChatDbEntities db = new ChatDbEntities();

        private Chat DbChatToChat(chat dbChat)
        {
            return new Chat(
                dbChat.ChatId,
                new ChatClient(dbChat.chatclient.ChatterId, dbChat.chatclient.chatter.Screenname, dbChat.chatclient.Branch),
                new ChatSupporter(dbChat.chatsupporter.ChatterId, dbChat.chatsupporter.chatter.Screenname, dbChat.chatsupporter.Department));
        }

        public IEnumerable<Chat> GetAllChats()
        {
            return db.chats.Select(dbChat => DbChatToChat(dbChat)).ToList();
        }

        public IEnumerable<Chat> GetChatsByChatterId(int chatterId)
        {

            //var chats = GetAllChats();
            //var queryResult =
            //    from chat in chats
            //    where chat.ChatSupporterId == chatterId || chat.ChatClientId == chatterId
            //    select chat;
            //return queryResult;
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetAllMessages()
        {
            //return db.messages;
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetMessagesByChatId(int chatId)
        {
            //var messages = GetAllMessages();
            //var queryResult =
            //    from message in messages
            //    where message.ChatId == chatId
            //    select message;
            //return queryResult;
            throw new NotImplementedException();
        }

        private IEnumerable<chat> GetActiveChats()
        {
            //var chats = GetAllChats();
            //var openChats =
            //    from chat in chats
            //    where string.Equals(chat.Status, ChatStatus.Active.ToString(), StringComparison.CurrentCultureIgnoreCase)
            //    select chat;
            //return openChats;
            throw new NotImplementedException();
        }

        public void CreateChats(int chatClientId, int chatSupporterId)
        {
            var activeChats = GetActiveChats();
            if (!activeChats.Any())
            {
                var chat = new chat
                {
                    ChatClientId = chatClientId,
                    ChatSupporterId = chatSupporterId,
                    Status = ChatStatus.Active.ToString().ToUpper()
                };
                db.chats.Add(chat);
                db.SaveChanges();
            }
            else
            {
                //TODO Change this to a custom exception
                throw new Exception("There is still a chat left open!");
            }
        }

        private chat GetChatByChatId(int chatId)
        {
            //var chats = GetAllChats();
            //var queryResult =
            //    from chat in chats
            //    where chat.ChatId == chatId
            //    select chat;
            //var result = queryResult.FirstOrDefault();
            //return result;
            throw new NotImplementedException();
        }

        public void CloseChat(int chatId)
        {
            var chat = GetChatByChatId(chatId);
            chat.Status = ChatStatus.Closed.ToString().ToUpper();
            db.chats.Attach(chat);
            db.SaveChanges();
        }

        public void SendMessage(string content, int chatId, int senderId, int receiverId)
        {
            var message = new message
            {
                Message1 = content,
                SenderId = senderId,
                ReceiverId = receiverId,
                ChatId = chatId
            };
            db.messages.Attach(message);
            db.SaveChanges();
        }
    }
}