using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Autumn.AuthHelper.Auth;
using Microsoft.AspNetCore.Builder;
using Autumn.Extension;
using Newtonsoft.Json;
using Autumn.Model;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Autumn.AuthHelper
{
    /// <summary>
    /// Token认证
    /// </summary>
    public class JwtTokenAuth
    {
        /// <summary>
        /// 委托
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 中间件
        /// </summary>
        /// <param name="next"></param>
        public JwtTokenAuth(RequestDelegate next)
        {
            _next = next;
        }

        private void PreProceed(HttpContext next)
        {
            //Console.WriteLine($"{DateTime.Now} middleware invoke preproceed");
        }
        private void PostProceed(HttpContext next)
        {
            //Console.WriteLine($"{DateTime.Now} middleware invoke postproceed");
        }

        /// <summary>
        /// Token认证
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            PreProceed(httpContext);

            if (!RequestUrl.Contains(httpContext.Request.Path.Value.ToLower()))
            {
                //是否包含Authorization请求头
                if (!httpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    PostProceed(httpContext);
                    ResponseModel responseModel = new ResponseModel()
                    {
                        Code = ResponseCode.Error,
                        Message = MessageModel.InvalidToken
                    };
                    await ResponseUnAuth(httpContext, responseModel);
                }
                else
                {
                    var tokenHeader = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                    if (tokenHeader.Length >= 128)
                    {
                        JwtTokenModel tm = JwtHelper.SerializeJwt(tokenHeader);

                        // 是否过期
                        if (DateTime.Now.CompareTo(tm.Expiration) <= 0)
                        {
                            var claims = new List<Claim>{
                            new Claim(ClaimTypes.Sid, tm.Uid.ToString()),
                            new Claim(ClaimTypes.Name,tm.Name)};
                            claims.AddRange(tm.Role.Split('|').Select(s => new Claim(ClaimTypes.Role, s)));
                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);
                            httpContext.User = principal;

                            PostProceed(httpContext);
                            await _next(httpContext);
                        }
                        else
                        {
                            ResponseModel responseModel = new ResponseModel()
                            {
                                Code = ResponseCode.Error,
                                Message = MessageModel.ExpirationToken
                            };
                            await ResponseUnAuth(httpContext, responseModel);
                        }
                    }
                    else
                    {
                        ResponseModel responseModel = new ResponseModel()
                        {
                            Code = ResponseCode.Error,
                            Message = MessageModel.InvalidToken
                        };
                        await ResponseUnAuth(httpContext, responseModel);
                    }
                }
            }
            else
            {
                PostProceed(httpContext);
                await _next(httpContext);
            }
        }

        /// <summary>
        /// 路径过滤器
        /// 字符全部小写
        /// </summary>
        private static readonly string[] RequestUrl =
        {
            "/api/v1/oauth2/accesstoken",
            "/profiler",
            "/api/v1/notify/notify_paysucceed",
            "/api/v1/notify/notify_refundsucceed",
            "/api/v1/notify/notify_cancelsucceed",
            "/api/v1/notify/notify_unknownnotify",
            "/api/v1/notify/notify_unknowngateway"
        };

       /// <summary>
       /// 返回
       /// </summary>
       /// <param name="httpContext"></param>
       /// <param name="responseModel"></param>
       /// <returns></returns>
        private async Task ResponseUnAuth(HttpContext httpContext, ResponseModel responseModel)
        {
            httpContext.Response.StatusCode = 200;
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(responseModel));
        }
    }

    public static class MiddlewareHelpers
    {
        public static IApplicationBuilder UserJwtTokenAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JwtTokenAuth>();
        }
    }
}