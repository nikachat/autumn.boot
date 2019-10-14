using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autumn.Common
{
    /// <summary>
    /// Session帮助类
    /// Tips:分布式系统中请不要随意使用
    /// </summary>
    public class SessionHelper
    {
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        protected void SetSession(string key, string value)
        {
            HttpAccessor.Current.Session.SetString(key, value);
        }

        /// <summary>
        /// 获取Session
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        protected string GetSession(string key)
        {
            var value = HttpAccessor.Current.Session.GetString(key);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }
    }
}
