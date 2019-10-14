using System;
using System.Collections.Generic;
using System.Text;

namespace Autumn.Common
{
    /// <summary>
    /// 邮件
    /// </summary>
    public class ReceiveEmailModel
    {
        /// <summary>
        /// 收件日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 收件人邮箱
        /// </summary>
        public string FromAddress { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }
    }
}
