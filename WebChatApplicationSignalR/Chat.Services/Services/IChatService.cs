using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Hosting;
using WebChat.Data;
using WebChat.Domain.Models;

//TODO Replace this with domain version later

namespace WebChat.Service.Services {
    public interface IChatService {
        IEnumerable<Chat> GetAllChats();
        IEnumerable<Chat> GetChatsByChatterId(int chatterId);

        IEnumerable<Message> GetAllMessages();
        IEnumerable<Message> GetMessagesByChatId(int chatId);

        bool CreateChat(int chatClientId, int chatSupporterId);
        void CloseChat(int chatId);
        void SendMessage(string content, int senderId, int receiverId);
        void SendMessage(string content, int chatId, int senderId, int receiverId);

        //void MarkAsRead(int chatId, int receiverId);

        //virtual GetUserData(UserData userData);

    }
}
