using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebChatApplicationSignalR.Hubs;

namespace WebChatApplicationSignalR.Controllers {
    public class HomeController : Controller {
        public ActionResult Chat()
        {
            return View();
        }

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}