using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebChatApplicationjQuery.Models;

namespace WebChatApplicationjQuery.Controllers
{
    public class ChatController : Controller
    {
        static ChatModel chatModel;
        public ActionResult Index(string user, bool? logOn, bool? logOff, string chatMessage)
        {
            try
            {
                if (chatModel == null) chatModel = new ChatModel();

                //trim chat history if needed
                if (chatModel.chatHistory.Count > 100)
                    chatModel.chatHistory.RemoveRange(0, 90);

                if (!Request.IsAjaxRequest())
                {
                    //first time loading
                    return View(chatModel);
                }
                else if (logOn != null && (bool)logOn)
                {
                    //check if nickname already exists
                    if (chatModel.users.FirstOrDefault(u => u.userName == user) != null)
                    {
                        throw new Exception("This nickname already exists");
                    }
                    else if (chatModel.users.Count > 10)
                    {
                        throw new Exception("The room is full!");
                    }
                    else
                    {
                        #region create new user and add to lobby
                        chatModel.users.Add(new ChatModel.ChatUser()
                        {
                            userName = user,
                            loggedOnTime = DateTime.Now,
                            lastPing = DateTime.Now
                        });

                        //inform lobby of new user
                        chatModel.chatHistory.Add(new ChatModel.ChatMessage()
                        {
                            message = "User '" + user + "' logged on.",
                            when = DateTime.Now
                        });
                        #endregion

                    }

                    return PartialView("Lobby", chatModel);
                }
                else if (logOff != null && (bool)logOff)
                {
                    LogOffUser(chatModel.users.FirstOrDefault(u => u.userName == user));
                    return PartialView("Lobby", chatModel);
                }
                else
                {

                    ChatModel.ChatUser currentUser = chatModel.users.FirstOrDefault(u => u.userName == user);

                    //remember each user's last ping time
                    currentUser.lastPing = DateTime.Now;

                    #region remove inactive users
                    List<ChatModel.ChatUser> removeThese = new List<ChatModel.ChatUser>();
                    foreach (Models.ChatModel.ChatUser usr in chatModel.users)
                    {
                        TimeSpan span = DateTime.Now - usr.lastPing;
                        if (span.TotalSeconds > 15)
                            removeThese.Add(usr);
                    }
                    foreach (ChatModel.ChatUser usr in removeThese)
                    {
                        LogOffUser(usr);
                    }
                    #endregion

                    #region if there is a new message, append it to the chat
                    if (!string.IsNullOrEmpty(chatMessage))
                    {
                        chatModel.chatHistory.Add(new ChatModel.ChatMessage()
                        {
                            byUser = currentUser,
                            message = chatMessage,
                            when = DateTime.Now
                        });
                    }
                    #endregion

                    return PartialView("ChatHistory", chatModel);
                }
            }
            catch (Exception ex)
            {
                //return error to AJAX function
                Response.StatusCode = 500;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// Remove this user from the lobby and inform others that he logged off
        /// </summary>
        /// <param name="user"></param>
        public void LogOffUser(ChatModel.ChatUser user)
        {
            chatModel.users.Remove(user);
            chatModel.chatHistory.Add(new ChatModel.ChatMessage()
            {
                message = "User '" + user.userName + "' logged off.",
                when = DateTime.Now
            });
        }
    }
}