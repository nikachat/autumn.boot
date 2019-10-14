using Autumn.Controllers;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autumn.Api.Controllers
{
    /// <summary>
    /// 防御跨站请求
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SecurityController : BaseController
    {
        private IAntiforgery antiforgery;
        public SecurityController(IAntiforgery antiforgery)
        {
            this.antiforgery = antiforgery;
        }

        [HttpGet("xsrf-token")]
        public ActionResult GetXsrfToken()
        {
            var tokens = antiforgery.GetAndStoreTokens(HttpContext);
            Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, 
                new CookieOptions { HttpOnly = false, Path = "/", IsEssential = true, SameSite = SameSiteMode.Lax });
            return Ok();
        }
    }
}