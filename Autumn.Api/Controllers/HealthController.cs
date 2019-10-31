using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autumn.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Autumn.Api.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    public class HealthController : BaseController
    {
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get() => Ok("ok");
    }
}