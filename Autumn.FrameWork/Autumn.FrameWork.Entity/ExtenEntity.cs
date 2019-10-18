using SqlDbLite;
using System;
using System.Collections.Generic;

namespace Autumn.FrameWork
{
    /// <summary>
    /// 扩展实体
    /// 当自动生成出来的实体类缺少字段时可在此添加扩展字段
    /// </summary>
    public class ExtenEntity
    {}

    public partial class S04_Permission : BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [DbLiteColumn(IsIgnore = true)]
        public string S02_RoleName { get; set; }

        /// <summary>
        /// 后端路由
        /// </summary>
        [DbLiteColumn(IsIgnore = true)]
        public string S03_BackRoute { get; set; }
    }
}
