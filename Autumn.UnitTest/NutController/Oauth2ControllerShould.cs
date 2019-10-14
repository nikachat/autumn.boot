using Autumn.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Autumn.Controllers.v1;
using Autumn.Common;

namespace Autumn.UnitTest.NutController
{
    public class Oauth2ControllerShould: BaseController
    {
        private Oauth2Controller _controller;

        [SetUp]
        public void Setup()
        {
            var server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());

            _controller = server.Host.Services.GetService<Oauth2Controller>();
        }

        [Test]
        public async Task AccessTokenTest()
        {
            LoginModel model = new LoginModel();

            model.UserCode = "1";

            model.Password = "1";

            var res = await _controller.AccessToken(model);

            Assert.NotNull(res);
        }

        [Test]
        public async Task RefreshTokenTest()
        {
            LoginModel model = new LoginModel();

            model.UserCode = "1";

            model.Password = "1";
            
            var res = await _controller.AccessToken(model);

            HttpAccessor.Current = GetMockedHttpContext();

            var res1 = await _controller.RefreshToken(res.Data.ToString());

            Assert.NotNull(res1);
        }
    }
}