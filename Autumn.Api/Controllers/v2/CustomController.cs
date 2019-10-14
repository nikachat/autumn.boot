using System.Collections.Generic;
using Autumn.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using static Autumn.SwaggerHelper.CustomApiVersion;

namespace Autumn.Controllers.v2
{
    /// <summary>
    /// 版本V2
    /// </summary>
    [CustomRoute(ApiVersions.v2)]
    public class CustomController : BaseController
    {
        /************************************************/
        // 如果需要使用Http协议带名称的，比如这种 [HttpGet("Custom")]
        // 目前只能把[CustomRoute(ApiVersions.v2)] 提到 controller 的上边，做controller的特性
        // 并且去掉//[Route("api/[controller]")]路由特性，否则会有两个接口 
        /************************************************/
        [HttpGet("Custom")]
        public IEnumerable<string> Get()
        {
            return new string[] { "第二版的 Custom" };
        }
    }
}
