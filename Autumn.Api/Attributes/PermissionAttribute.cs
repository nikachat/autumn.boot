using Autumn.Common;
using Autumn.Extension;
using Autumn.IServices;
using Autumn.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace Autumn.Api.Attributes
{
    /// <summary>
    /// 操作授权许可
    /// </summary>
    public class PermissionAttribute : ActionFilterAttribute
    {
        private readonly IUserService _user;
        private readonly IPermissionService _permission;

        public PermissionAttribute(IUserService user,IPermissionService permission)
        {
            _user = user;
            _permission = permission;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Def.RedisCachingFlg)
            {
                RedisHelper _redisHelper = new RedisHelper(DbNum.Permission.ToInt());

                // 此处测试为空，实际请从Redis中获取当前用户的权限集合
                var PermissionList = new List<string>();
                var currentUrl = HttpAccessor.Current.Request.Path.Value;
                // 鉴权
                if (!PermissionList.Contains(currentUrl))
                {
                    ResponseModel responseModel = new ResponseModel()
                    {
                        Code = ResponseCode.Error,
                        Message = MessageModel.LimitedAuthority
                    };
                    filterContext.HttpContext.Response.StatusCode = 200;
                    filterContext.Result = new JsonResult(responseModel);
                }
                else
                {
                    base.OnActionExecuting(filterContext);
                }
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}
