using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;
using WebChat.Data;
using WebChat.Domain.Models;

namespace WebChat.Service.Services {
    public class ChatService : IChatService {
        private ChatEntities db = new ChatEntities();

        private Status ParseStatus(string status)
        {
            Status parsedStatus = Status.Unknown;
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
                throw new InvalidCastException("Could not parse" + status.GetType() + " " + status + "to " + parsedStatus.GetType() + " " + parsedStatus);
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

            var chats = GetAllChats();
            var queryResult =
                from chat in chats
                where chat.ChatSupporter.ChatterId == chatterId || chat.ChatClient.ChatterId == chatterId
                select chat;
            return queryResult;
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
            var messages = GetAllMessages();
            var queryResult =
                from message in messages
                where message.ChatId == chatId
                select message;
            return queryResult;
        }

        private IEnumerable<Chat> GetActiveChats(int chatClientId, int chatSupporterId)
        {
            var chats = GetAllChats();
            var getActiveChats =
                from chat in chats
                where chat.Status == Status.Active && chat.ChatClient.ChatterId == chatClientId && chat.ChatSupporter.ChatterId == chatSupporterId
                select chat;
            return getActiveChats;
        }

        public bool CreateChat(int chatClientId, int chatSupporterId)
        {
            var activeChats = GetActiveChats(chatClientId, chatSupporterId);
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
                //TODO Send default message of support if it exist

                var chatSupporter = GetChatSupporterByChatterId(chatSupporterId);
                SendMessage(chatSupporter.DefaultMessage, chatSupporterId, chatClientId);
                return true;
            }
            else
            {
                return false;
            }
        }

        private ChatSupporter GetChatSupporterByChatterId(int chatterId)
        {
            var dbChatSupporters = db.chatsupporters;
            var queryResult =
                from dbChatSupporter in dbChatSupporters
                where dbChatSupporter.ChatterId == chatterId
                select dbChatSupporter;
            var result = queryResult.First();
            return new ChatSupporter(result.ChatterId, GetScreennameByChatterId(result.ChatterId), result.Department, result.DefaultMessage);
        }

        private string GetScreennameByChatterId(int chatterId)
        {
            var dbChatters = db.chatters;
            var queryResult =
                from dbChatter in dbChatters
                where dbChatter.ChatterId == chatterId
                select dbChatter;
            var result = queryResult.First();
            return result.Screenname;
        }

        private Chat GetChatByChatId(int chatId)
        {
            var chats = GetAllChats();
            var queryResult =
                from chat in chats
                where chat.Id == chatId
                select chat;
            var result = queryResult.FirstOrDefault();
            return result;
        }

        public void CloseChat(int chatId)
        {
            var chat = GetChatByChatId(chatId);
            var dbChat = new chat
            {
                ChatClientId = chat.ChatClient.ChatterId,
                ChatId = chat.Id,
                ChatSupporterId = chat.ChatSupporter.ChatterId,
                Status = Status.Closed.ToString().ToUpper()
            };
            db.chats.AddOrUpdate(dbChat);
            db.SaveChanges();
        }


        public void SendMessage(string content, int chatId, int senderId, int receiverId)
        {
            var dbMessage = new message
            {
                ChatId = chatId,
                Message1 = content,
                SenderId = senderId,
                ReceiverId = receiverId,
                Read = false,
                TimeSend = DateTime.UtcNow
            };
            db.messages.Add(dbMessage);
            db.SaveChanges();
        }

        private IEnumerable<Chat> GetAllActiveChats()
        {
            var chats = GetAllChats();
            var activeChats =
                from chat in chats
                where chat.Status == Status.Active
                select chat;
            return activeChats;
        }

        private IEnumerable<Chat> GetActiveChatByChatterIds(int a, int b)
        {
            var activeChats = GetAllActiveChats();
            var queryResult =
                from chat in activeChats
                where
                (chat.ChatClient.ChatterId == a && chat.ChatSupporter.ChatterId == b) ||
                (chat.ChatClient.ChatterId == b && chat.ChatSupporter.ChatterId == a)
                select chat;
            return queryResult;
        }

        public void SendMessage(string content, int senderId, int receiverId)
        {
            var activeChat = GetActiveChatByChatterIds(senderId, receiverId);
            int chatId;
            if (activeChat.Any())
            {
                chatId = activeChat.First().Id;
            }
            else
            {
                return;
            }

            var dbMessage = new message
            {
                ChatId = chatId,
                Message1 = content,
                SenderId = senderId,
                ReceiverId = receiverId,
                Read = false,
                TimeSend = DateTime.UtcNow
            };
            db.messages.Add(dbMessage);
            db.SaveChanges();
        }


        //TODO Fix this message and add it to the interface and api
        public void MarkAsRead(int chatId, int receiverId)
        {
            foreach (var dbMessage in db.messages)
            {
                db = new ChatEntities();
                if (dbMessage.ChatId == chatId && receiverId == dbMessage.ReceiverId && !dbMessage.Read)
                {
                    dbMessage.Read = true;
                    db.Entry(dbMessage).State = EntityState.Modified;
                    //db.messages.Attach(dbMessage);


                    var saveFailed = false;
                    do
                    {
                        saveFailed = false;
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            saveFailed = true;
                        }
                    } while (saveFailed);
                }
            }
        }
    }
}
