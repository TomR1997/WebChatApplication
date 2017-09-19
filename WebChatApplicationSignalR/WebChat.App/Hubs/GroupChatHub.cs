using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace WebChat.App.Hubs
{
    public class GroupChatHub : Hub
    {
        private Dictionary<string, int> onlineUsers = new Dictionary<string, int>();
        private string username;

        public override Task OnConnected()
        {
            Connect();
            return base.OnConnected();
        }

        private void Connect()
        {
            if (!onlineUsers.ContainsKey(username))
            {
                onlineUsers.Add(username, 1);

                Clients.All.publishUser(onlineUsers.Select(i => i.Key));
                Clients.All.publishMessage(FormatMessage("System message", username + " connected"));
            }
            else
            {
                onlineUsers[username] = onlineUsers[username] + 1;
            }

            Groups.Add(Context.ConnectionId, "GROUP-" + username);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            onlineUsers[username] = onlineUsers[username] - 1;

            if (onlineUsers[username] == 0)
            {
                onlineUsers.Remove(username);
                Clients.All.publishUser(onlineUsers.Select(i => i.Key));
                Clients.All.publishMessage(FormatMessage("System message", username + " disconnected"));
            }

            Groups.Remove(Context.ConnectionId, "GROUP-" + username);
            return base.OnDisconnected(stopCalled);
        }

        public void SendMessage(string user, string message)
        {
            if (user == "0")
            {
                Clients.All.publishMessage(FormatMessage(user, message));
            }
            else
            {
                Clients.Groups(new List<string> { "GROUP-" + username, "GROUP-" + user }).publishMessage(FormatMessage(username, message));
            }
        }

        private dynamic FormatMessage(string name, string msg)
        {
            return new { Name = name, Msg = HttpUtility.HtmlEncode(msg), Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        }
    }
}