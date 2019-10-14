using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Autumn.Common
{
    /// <summary>
    /// 上下文处理
    /// </summary>
    public class HandleHelper
    {
        /// <summary>
        /// 当前用户Id
        /// </summary>
        public static readonly string UserId = HttpAccessor.Current.User?.FindFirst(ClaimTypes.Sid).Value;

        /// <summary>
        /// 当前用户名
        /// </summary>
        public static readonly string UserName = HttpAccessor.Current.User?.Identity.Name;
    }
}
