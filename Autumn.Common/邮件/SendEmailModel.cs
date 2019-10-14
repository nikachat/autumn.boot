using System;
using System.Collections.Generic;
using System.Text;

namespace Autumn.Common
{
    /// <summary>
    /// 邮件
    /// </summary>
    public class SendEmailModel
    {
        /// <summary>
        /// 发件人账户
        /// </summary>
        public string FromAccount { get; set; }

        /// <summary>
        /// 发件人密码
        /// </summary>
        public string FromPassword { get; set; }

        /// <summary>
        /// 发件人邮箱
        /// </summary>
        public string FromAddress { get; set; }

        /// <summary>
        /// 收件人邮箱
        /// </summary>
        public string ToAddress { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 发件人名称
        /// </summary>
        public string FromName { get; set; } = string.Empty;

        /// <summary>
        /// 收件人名称
        /// </summary>
        public string ToName { get; set; } = string.Empty;
    }
}
