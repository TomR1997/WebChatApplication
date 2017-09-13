using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Chat.Data;

namespace Chat.Services.Services {
    public interface IChatService {
        IEnumerable<chat> GetAllChats();
        IEnumerable<chat> GetChatsByChatterId(int chatterId);

        IEnumerable<message> GetAllMessages();
        IEnumerable<message> GetMessagesByChatId(int chatId);

        void CreateChat(int chatClientId, int chatSupporterId);
        void CloseChat(int chatId);
        void SendMessage(string content, int chatId, int chatClientId, int chatSupporterId);


    }
}
