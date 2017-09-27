using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Net;

[assembly: OwinStartup("ChatStartUp",typeof(WebChat.App.App_Start.Startup))]

namespace WebChat.App.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.MapSignalR(new HubConfiguration{EnableJSONP = true, EnableDetailedErrors=true });
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableJSONP = true,
                    EnableDetailedErrors = true
                };
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}
