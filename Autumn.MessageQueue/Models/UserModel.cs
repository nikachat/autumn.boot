using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autumn.MessageQueue.Models
{
    /// <summary>
    /// 测试
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// 用户代号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string RoleIds { get; set; }
        /// <summary>
        /// 请求Url
        /// </summary>
        public string Urls { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bRemark { get; set; }
    }
}
