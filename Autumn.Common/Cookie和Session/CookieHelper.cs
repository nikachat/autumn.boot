using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autumn.Common
{
    /// <summary>
    /// Cookie帮助类
    /// </summary>
    public class CookieHelper
    {
        /// <summary>
        /// 设置前端cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>  
        /// <param name="minutes">过期时长，单位：分钟</param>      
        public static void SetCookies(string key, string value, int minutes = 30)
        {
            HttpAccessor.Current.Response.Cookies.Append(key, value, new CookieOptions
            {
                // 重要
                IsEssential = true,
                // 过期时间
                Expires = DateTime.Now.AddMinutes(minutes)
            });
        }

        /// <summary>
        /// 设置前端cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>  
        /// <param name="minutes">过期时长，单位：分钟</param>      
        public static void SetCookiesSecure(string key, string value, int minutes = 30)
        {
            HttpAccessor.Current.Response.Cookies.Append(key, value, new CookieOptions
            {
                // HttpOnly设置为true,表明为后台只读模式,前端无法通过JS来获取cookie值,可以有效的防止XXS攻击
                HttpOnly = false,
                // Secure设置为true,就是当你的网站开启了SSL(就是https),的时候,这个cookie值才会被传递
                Secure = true,
                // 过期时间
                Expires = DateTime.Now.AddMinutes(minutes),
                // 重要
                IsEssential = true
            });
        }

        /// <summary>
        /// 删除前端指定的cookie
        /// </summary>
        /// <param name="key">键</param>
        public static void DeleteCookies(string key)
        {
            HttpAccessor.Current.Response.Cookies.Delete(key);
        }

        /// <summary>
        /// 获取前端cookies
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        public static string GetCookies(string key)
        {
            HttpAccessor.Current.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }
    }
}
