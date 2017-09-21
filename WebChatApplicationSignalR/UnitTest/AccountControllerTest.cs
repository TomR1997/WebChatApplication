using WebChat.WebAPI.Controllers;
using Xunit;
using System.Web.Http;
using System.Web.Http.Results;

namespace UnitTest
{
    public class AccountControllerTest
    {
        private AccountController accountController;

        public AccountControllerTest()
        {
            accountController = new AccountController();
        }
        [Fact]
        public void LogOutTest()
        {
            IHttpActionResult actionResult = accountController.Logout();

            Assert.IsType(typeof(OkResult), actionResult);
        }
    }
}
