using Autumn.Common;
using Autumn.Extension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Autumn.Filters
{
    /// <summary>
    /// 全局异常日志
    /// </summary>
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        public GlobalExceptionsFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            ResponseModel responseModel = new ResponseModel();

            responseModel.Code = ResponseCode.Error;

            responseModel.Message = context.Exception.Message;

            //自定义的操作记录日志
            if (context.Exception.GetType() == typeof(UserOperationException))
            {
                if (_env.IsDevelopment())
                {
                    //堆栈信息
                    responseModel.Data = context.Exception.StackTrace.TrimStart();
                }
                //返回异常数据
                context.Result = new BadRequestObjectResult(responseModel);
            }
            else
            {
                if (_env.IsDevelopment())
                {
                    //堆栈信息
                    responseModel.Data = context.Exception.StackTrace.TrimStart();
                }
                context.Result = new InternalServerErrorObjectResult(responseModel);
            }
            //日志记录
            NLogHelper.ErrorLog(responseModel.Message, context.Exception);
        }
    }

    /// <summary>
    /// 响应码
    /// </summary>
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }

    /// <summary>
    /// 返回
    /// </summary>
    public class JsonErrorResponse
    {
        /// <summary>
        /// 生产环境的消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 开发环境的消息
        /// </summary>
        public string DevelopmentMessage { get; set; }
    }

    /// <summary>
    /// 操作日志
    /// </summary>
    public class UserOperationException : Exception
    {
        public UserOperationException() { }

        public UserOperationException(string message) : base(message) { }

        public UserOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
