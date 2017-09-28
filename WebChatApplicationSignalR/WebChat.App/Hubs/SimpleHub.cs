using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace WebChat.App.Hubs
{
    public class SimpleHub : Hub
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public void SendMsg(string msg)
        {
            Clients.All.msg("Message sent to client");

            Clients.Client("connectionId").msg("Message sent to client");

            Clients.Group("groupName").msg("Message sent to client");
        }

        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}