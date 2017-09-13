using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Chat.Data;

//TODO Replace this with domain version later

namespace Chat.Services.Services {
    public interface IChatService {
        IEnumerable<chat> GetAllChats();
        IEnumerable<chat> GetChatsByChatterId(int chatterId);

        IEnumerable<message> GetAllMessages();
        IEnumerable<message> GetMessagesByChatId(int chatId);

        void CreateChats(int chatClientId, int chatSupporterId);
        void CloseChat(int chatId);
        void SendMessage(string content, int chatId, int chatClientId, int chatSupporterId);


    }
}
