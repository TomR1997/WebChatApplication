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

        private Status ParseStatus(string status)
        {
            Status parsedStatus;
            if (string.Equals(status, Status.Active.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                parsedStatus = Status.Active;
                return parsedStatus;
            }
            else if (string.Equals(status, Status.Closed.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                parsedStatus = Status.Closed;
                return parsedStatus;
            }
            else
                //TODO Make this a custom exception
                throw new Exception("Could not parse status!");
        }


        private Chat DbChatToChat(chat dbChat)
        {
            return new Chat(
                dbChat.ChatId,
                new ChatClient(dbChat.chatclient.ChatterId, dbChat.chatclient.chatter.Screenname, dbChat.chatclient.Branch),
                new ChatSupporter(dbChat.chatsupporter.ChatterId, dbChat.chatsupporter.chatter.Screenname, dbChat.chatsupporter.Department),
                ParseStatus(dbChat.Status),
                GetMessagesByChatId(dbChat.ChatId).ToList()
                );
        }

        public IEnumerable<Chat> GetAllChats()
        {
            var chats = new List<Chat>();
            //TODO For some reason if this is converted to LINQ expression it fails, fix this
            foreach (var dbChat in db.chats)
                chats.Add(DbChatToChat(dbChat));
            return chats;
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

        private Message DbMessageToMessage(message dbMessage)
        {
            return new Message(
                dbMessage.ChatId,
                dbMessage.SenderId,
                dbMessage.ReceiverId,
                dbMessage.TimeSend,
                dbMessage.Message1,
                dbMessage.Read
                );
        }

        public IEnumerable<Message> GetAllMessages()
        {
            var dbMessages = db.messages;
            var messages = new List<Message>();
            foreach (var dbMessage in dbMessages)
            {
                messages.Add(DbMessageToMessage(dbMessage));
            }
            return messages;
        }

        public IEnumerable<Message> GetMessagesByChatId(int chatId)
        {
            //var messages = GetAllMessages();
            //var queryResult =
            //    from message in messages
            //    where message.ChatId == chatId
            //    select message;
            //return queryResult;
            var messages = GetAllMessages();
            var queryResult =
                from message in messages
                where message.ChatId == chatId
                select message;
            return queryResult;
        }

        private IEnumerable<chat> GetActiveChats()
        {
            //var chats = GetAllChats();
            //var openChats =
            //    from chat in chats
            //    where string.Equals(chat.Status, Status.Active.ToString(), StringComparison.CurrentCultureIgnoreCase)
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
                    Status = Status.Active.ToString().ToUpper()
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
            chat.Status = Status.Closed.ToString().ToUpper();
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