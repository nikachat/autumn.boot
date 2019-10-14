using Autumn.Model;
using System;

namespace Autumn.Extension
{
    /// <summary>
    /// 通用返回信息
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; } = "1.0.0";

        /// <summary>
        /// 响应码
        /// </summary>
        public ResponseCode Code{ get; set; } = ResponseCode.Success;

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = MessageModel.Success;

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; } = new object();

        /// <summary>
        /// 数据总数
        /// </summary>
        public int DataCount { get; set; }

        /// <summary>
        /// 分页总数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 校验码
        /// </summary>
        public string Mac { get; set; } = string.Empty;

        /// <summary>
        ///  时间戳
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
