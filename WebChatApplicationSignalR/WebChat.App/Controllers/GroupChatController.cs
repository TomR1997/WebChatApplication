using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebChat.App.Models;

namespace WebChat.App.Controllers
{
    public class GroupChatController : Controller
    {
        public ActionResult Index()
        {
            var username = "username.here";
            ViewBag.username = username;

            return View();
        }

        public ActionResult SendSystemMessage()
        {
            return View();
        }

        [HttpPost]
        public string SendSystemMessage(string message)
        {
            GroupChat.GetGroup.SendSystemMessage(message);
            return "s";
        }
    }
}