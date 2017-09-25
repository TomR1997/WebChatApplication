using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup("ChatStartUp",typeof(WebChat.App.App_Start.Startup))]

namespace WebChat.App.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR(new HubConfiguration{ EnableJSONP = true });
        }
    }
}
