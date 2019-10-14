using System;
using System.Collections.Generic;
using System.Text;

namespace Autumn.Common
{
    public class SMSModel
    {
        /// <summary>
        /// 主账号AccessKey的ID
        /// </summary>
        public string AccessKeyId { get; set; }
        /// <summary>
        /// 主账号AccessSecret
        /// </summary>
        public string AccessSecret { get; set; }
        /// <summary>
        /// 区域
        /// 默认杭州
        /// </summary>
        public string RegionId { get; set; } = "cn-hangzhou";
        /// <summary>
        /// 接收短信的手机号码
        /// 支持对多个手机号码发送短信，手机号码之间以英文逗号（,）分隔。上限为1000个手机号码。批量调用相对于单条调用及时性稍有延迟。
        /// </summary>
        public string PhoneNumbers { get; set; }
        /// <summary>
        /// 短信签名名称
        /// 请在控制台签名管理页面签名名称一列查看。
        /// </summary>
        public string SignName { get; set; }
        /// <summary>
        /// 短信模板ID
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        /// 短信模板变量对应的实际值
        /// JSON格式
        /// </summary>
        public string TemplateParam { get; set; }
        /// <summary>
        /// 上行短信扩展码
        /// 无特殊需要此字段的用户请忽略此字段。
        /// </summary>
        public string SmsUpExtendCode { get; set; }
        /// <summary>
        /// 外部流水扩展字段
        /// </summary>
        public string OutId { get; set; }
    }
}
