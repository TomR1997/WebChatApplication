using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WebChat.Data;
using WebChat.Domain.Models;

//TODO Replace this with domain version later

namespace WebChat.Service.Services {
    public interface IChatService {
        IEnumerable<Chat> GetAllChats();
        IEnumerable<Chat> GetChatsByChatterId(int chatterId);

        IEnumerable<Message> GetAllMessages();
        IEnumerable<Message> GetMessagesByChatId(int chatId);

        void CreateChats(int chatClientId, int chatSupporterId);
        void CloseChat(int chatId);
        void SendMessage(string content, int chatId, int chatClientId, int chatSupporterId);
    }
}
