using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Domain.Models;
using Xunit;

namespace UnitTest
{
    public class ChatTest
    {
        private Chat chat;
        private readonly List<Message> messageList;
        private readonly ChatClient chatClient;
        private readonly ChatSupporter chatSupport;

        public ChatTest()
        {
            chatClient = new ChatClient(1, "clientname", "branch");
            chatSupport = new ChatSupporter(1, "supportname", "support");
            messageList = new List<Message>();
            messageList.Add(new Message(1, 1, 1, new DateTime(2017,09,12), "test", false));
        }
        [Fact]
        public void ConstructorTest()
        {
            chat = new Chat(1, chatClient, chatSupport, Status.Active, messageList);
            Assert.Equal(1, chat.Id);
            Assert.Equal(1, messageList.Count);
        }

        [Fact]
        public void ChatClientTest()
        {
            Assert.Equal(1, chatClient.ChatterId);
            Assert.Equal("clientname", chatClient.ScreenName);
            Assert.Equal("branch", chatClient.Branch);
        }

        [Fact]
        public void ChatSupporterTest()
        {
            Assert.Equal(1, chatSupport.ChatterId);
            Assert.Equal("supportname", chatSupport.ScreenName);
            Assert.Equal("support", chatSupport.Department);
        }
    }
}
