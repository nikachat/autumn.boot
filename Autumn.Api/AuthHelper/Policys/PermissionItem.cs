namespace Autumn.AuthHelper
{
    /// <summary>
    /// 角色凭据实体
    /// </summary>
    public class PermissionItem
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public virtual int RoleId { get; set; }
        /// <summary>
        /// 请求Url
        /// </summary>
        public virtual string Url { get; set; }
    }
}
