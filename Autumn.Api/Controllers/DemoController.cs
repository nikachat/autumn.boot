using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Autumn.Common;
using Autumn.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autumn.Api.Controllers
{

    public class DemoController : BaseController
    {
        /// <summary>
        /// 构造
        /// </summary>
        public DemoController()
        {

        }


        [HttpPost]
        public IActionResult Apost()
        {
            CookieHelper.SetCookiesSecure("name1", "value1");
            CookieHelper.SetCookiesSecure("name2", "value2");

            //HttpAccessor.Current.Response.Headers.Add("Location","www.baidu.com");
            //HttpAccessor.Current.Response.Redirect("www.baidu.com");// = HttpStatusCode.Redirect;
            //return Ok(new { Success = true, Message = "成功" });
            return Redirect("https://www.baidu.com");
        }

        [HttpGet]
        public IActionResult Aget()
        {
            CookieHelper.SetCookiesSecure("name1", "value1");
            CookieHelper.SetCookiesSecure("name2", "value2");

            HttpAccessor.Current.Response.Headers.Add("Location", "www.baidu.com");

            return Ok(new { Success = true, Message = "成功" });
        }
    }
}