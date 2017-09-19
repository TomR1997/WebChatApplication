using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using WebChat.App.Hubs;

namespace WebChat.App.Models
{
    public class GroupChat
    {
        private IHubConnectionContext<dynamic> Clients { get; set; }

        private readonly static GroupChat groupChat = new GroupChat(GlobalHost.ConnectionManager.GetHubContext<GroupChatHub>().Clients);

        private GroupChat(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        public static GroupChat GetGroup { get { return groupChat; } }

        public void SendSystemMessage(string message)
        {
            Clients.All.publishMessage(new { Name = "System message", Msg = message, Time = DateTime.Now.ToString("yyyy--MM-dd HH:mm:ss") });
        }
    }
}