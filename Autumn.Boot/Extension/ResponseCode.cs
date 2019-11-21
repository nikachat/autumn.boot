using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Autumn.Extension
{
    /// <summary>
    /// 系统响应
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 200,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Error = 400,
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown = 900
    }
}
