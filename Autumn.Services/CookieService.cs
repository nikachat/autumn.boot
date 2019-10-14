using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autumn.Services
{
    public class CookieService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsLogin()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies["username"] != null;
        }

    }
}
