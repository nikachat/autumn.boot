using Autumn.Controllers;
using Autumn.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Autumn.UnitTest.NutController
{
    public class UserControllerShould
    {
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            var server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());

            _controller = server.Host.Services.GetService<UserController>();
        }

        [Test]
        public async Task  GetUserTest()
        {
            var res = await _controller.GetUser(1,"1");

            Assert.NotNull(res);
        }

        [Test]
        public async Task InsertUserTest()
        {
            UserModel user = new UserModel()
            {
                UserCode = "114",
                UserName = "224"
            };

            var res = await _controller.InsertUser(user);

            Assert.NotNull(res);
        }
    }
}