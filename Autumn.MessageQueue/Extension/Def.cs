using Autumn.Common;
using System.Configuration;

namespace Autumn.MessageQueue.Extension
{
    /// <summary>
    /// 常量定义
    /// </summary>
    public static class Def
    {
        /// <summary>
        /// 消息队列是否使用
        /// </summary>
        public static readonly bool KafkaFlg = Appsettings.Get(new string[] { "AppSettings", "Kafka", "Enabled" }).ToBool();

        /// <summary>
        /// 消息队列地址
        /// </summary>
        public static readonly string KafkaUrl = Appsettings.Get(new string[] { "AppSettings", "Kafka", "ConnectionString" });
    }
}
