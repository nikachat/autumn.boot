using System;
using System.Collections.Generic;
using System.Text;

namespace Autumn.Model
{
    /// <summary>
    /// 心跳
    /// </summary>
    public class HealthModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
    }
}
