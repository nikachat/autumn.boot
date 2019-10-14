using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autumn.Common
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class HttpAccessor
    {
        /// <summary>
        /// Accessor
        /// </summary>
        public static IHttpContextAccessor _Accessor = null;
        private static HttpContext _MockHttpContext = null;
        public static HttpContext Current
        {
            get
            {
                return _Accessor.HttpContext == null ? _MockHttpContext : _Accessor.HttpContext;
            }
            set
            {
                _MockHttpContext = value;
            }
        }
    }
}
