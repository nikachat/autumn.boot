using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autumn.Model
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 代号
        /// </summary>
        [Required]
        public string UserCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
