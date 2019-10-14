using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using Moq;

namespace Autumn.UnitTest.NutController
{
    public class BaseController
    {
        /// <summary>
        /// 上下文
        /// </summary>
        /// <returns></returns>
        public static HttpContext GetMockedHttpContext()
        {
            var claims = new List<Claim>{
            new Claim(ClaimTypes.Sid, "1"),
            new Claim(ClaimTypes.Role, "1")};
            var context = new Mock<HttpContext>();
            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(id => id.Claims).Returns(claims);
            identity.Setup(id => id.AuthenticationType).Returns(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.Setup(id => id.IsAuthenticated).Returns(true);
            identity.Setup(id => id.Name).Returns("Autumn");
            var contextUser = new Mock<ClaimsPrincipal>();
            contextUser.Setup(ctx => ctx.Identity).Returns(identity.Object);
            context.Setup(x => x.User).Returns(contextUser.Object);
            return context.Object;
        }
    }
}
