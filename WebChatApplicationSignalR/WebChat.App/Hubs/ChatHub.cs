using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebChat.App.Hubs
{
    public class ChatHub : Hub<IClient>
    {
        public void sendMessage(string msg)
        {
            //log the incoming message here to confirm that its received

            //send back same message with DateTime
            Clients.All.messageReceived("Message received at: " + DateTime.Now.ToString());
        }
    }
    public interface IClient
    {
        void messageReceived(string msg);
    }

}