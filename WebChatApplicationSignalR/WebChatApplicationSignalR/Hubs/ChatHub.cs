using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebChatApplicationSignalR.Hubs
{
    public class ChatHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
            //Add to database
        }

        public static void sayHello()
        {
            hubContext.Clients.All.sayHello();
        }
    }
}