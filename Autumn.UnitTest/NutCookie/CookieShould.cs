using Autumn.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace Autumn.UnitTest.NutCookie
{
    public class CookieShould
    {
        //private CookieServices _service;
        private Mock<IHttpContextAccessor> httpContextAccessorMock;
        [SetUp]
        public void Init()
        {
            httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext.Request.Cookies["username"]).Returns("admin");

            //var server = new TestServer(WebHost.CreateDefaultBuilder()
            //    .ConfigureServices(u => u.AddScoped(x => httpContextAccessorMock.Object))
            //    .UseStartup<Startup>());
            //_service = server.Host.Services.GetService<CookieServices>();
        }

        [Test]
        public void LoginTest()
        {
            CookieService s = new CookieService(httpContextAccessorMock.Object);
            bool result = s.IsLogin();
            Assert.IsTrue(result);
        }
    }
}
