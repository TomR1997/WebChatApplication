using WebChat.WebAPI.Controllers;
using Xunit;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web;
using System.Web.Mvc;
using System;

namespace UnitTest
{
    public class AccountControllerTest
    {
        private readonly AccountController accountController;
       
        public AccountControllerTest()
        {
            accountController = new AccountController();
        }
        [Fact]
        public void LogOutTest()
        {
            /* test cookies
            var httpContext = new MockHttpContext();
   
            IHttpActionResult actionResult = accountController.Logout();

            Assert.IsType(typeof(OkResult), actionResult);*/
            throw new NotSupportedException();
        }
    }

    public class MockHttpContext : HttpContextBase
    {
        readonly HttpRequestBase _request;

        public MockHttpContext()
        {
            _request = new MockHttpRequest();
        }

        public override HttpRequestBase Request
        {
            get { return _request; }
        }

        class MockHttpRequest : HttpRequestBase
        {
            readonly HttpCookieCollection _cookies;

            public MockHttpRequest()
            {
                _cookies = new HttpCookieCollection();
            }

            public override HttpCookieCollection Cookies
            {
                get
                {
                    return _cookies;
                }
            }
        }
    }
}
