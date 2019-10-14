using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.IO;
using Autumn.Common;

namespace Autumn.Middlewares
{
    /// <summary>
    /// 请求和响应
    /// </summary>
    public class RequRespLogMiddle
    {
        /// <summary>
        /// 委托
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 中间件
        /// </summary>
        /// <param name="next"></param>
        public RequRespLogMiddle(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 通道中间件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext context)
        {
            // 过滤
            if (context.Request.Path.Value.ToLower().Contains("api"))
            {
                context.Request.EnableBuffering();

                Stream originalBody = context.Response.Body;

                try
                {
                    // 请求
                    RequestDataLog(context.Request);

                    using (var ms = new MemoryStream())
                    {
                        context.Response.Body = ms;

                        await _next(context);

                        // 响应
                        ResponseDataLog(context.Response, ms);

                        ms.Position = 0;
                        await ms.CopyToAsync(originalBody);
                    }
                }
                catch (Exception ex)
                {
                    // 记录
                     NLogHelper.ErrorLog(context.Response.ToString(),ex);
                }
                finally
                {
                    context.Response.Body = originalBody;
                }
            }
            else
            {
                await _next(context);
            }
        }

        /// <summary>
        /// 访问
        /// </summary>
        /// <param name="request"></param>
        private void RequestDataLog(HttpRequest request)
        {
            var sr = new StreamReader(request.Body);

            var content = $"QueryData:{request.Path + request.QueryString}\r\nBodyData:{sr.ReadToEnd()}";

            if (!string.IsNullOrEmpty(content))
            {
                Parallel.For(0, 1, e =>
                {
                    LogLock log = new LogLock();
                    LogLock.OutSql2Log("RequestResponseLog", new string[] { "Request Data:", content });

                });

                request.Body.Position = 0;
            }
        }

        /// <summary>
        /// 响应
        /// </summary>
        /// <param name="response"></param>
        /// <param name="ms"></param>
        private void ResponseDataLog(HttpResponse response, MemoryStream ms)
        {
            ms.Position = 0;
            var ResponseBody = new StreamReader(ms).ReadToEnd();

            if (!string.IsNullOrEmpty(ResponseBody))
            {
                Parallel.For(0, 1, e =>
                {
                    LogLock log = new LogLock();
                    LogLock.OutSql2Log("RequestResponseLog", new string[] { "Response Data:", ResponseBody });
                });
            }
        }
    }
}

