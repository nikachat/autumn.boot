using Autumn.Common;
using Autumn.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autumn.Controllers
{
    /// <summary>
    /// 基类
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class BaseController : Controller
    {
        public ResponseModel _ResponseModel = new ResponseModel();
    }
}