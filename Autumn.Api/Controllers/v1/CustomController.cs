using System.Collections.Generic;
using Autumn.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using static Autumn.SwaggerHelper.CustomApiVersion;

namespace Autumn.Controllers.v1
{
    /// <summary>
    /// 版本V1
    /// </summary>
    public class CustomController : BaseController
    {
        /************************************************/
        // 如果不需要使用Http协议带名称的，比如这种 [HttpGet]
        // 就可以按照下边的写法去写，在方法上直接加特性 [CustomRoute(ApiVersions.v1, "Custom")]
        // 反之，如果你需要http协议带名称，请看 V2 文件夹的方法
        /************************************************/
        [HttpGet]
        [CustomRoute(ApiVersions.v1, "Custom")]
        public IEnumerable<string> Get()
        {
            return new string[] { "第一版的 Custom" };
        }
    }
}
