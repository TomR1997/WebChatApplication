using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebChat.App.App_Start.Startup))]

namespace WebChat.App.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
