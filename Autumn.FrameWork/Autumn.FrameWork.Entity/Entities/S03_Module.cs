//-----------------------------------------------------------------------------
//此代码由T4模板自动生成 By Jim
//生成时间 2019-10-18 09:08:45 
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//-----------------------------------------------------------------------------
using System;
using SqlDbLite;

namespace Autumn.FrameWork
{	
	/// <summary>
	/// 模块#ini
	/// </summary>
	[Serializable]
	public partial class S03_Module :BaseEntity
	{
	/// <summary>
	/// 模块Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=true, IsIdentity=false, IsNullable=false)]
	public int  S03_ModuleId { get; set; }

	/// <summary>
	/// 父模块Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true)]
	public int  ? S03_ParentModuleId { get; set; }

	/// <summary>
	/// 属性 0页面 1按钮 2列表 9其他
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false)]
	public byte  S03_Kind { get; set; }

	/// <summary>
	/// 名称
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false, Length=40)]
	public string  S03_ModuleName { get; set; }

	/// <summary>
	/// 深度 0根菜单 1一级菜单 2二级菜单 （以此类推）
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true)]
	public byte  ? S03_Depth { get; set; }

	/// <summary>
	/// 前端 Url路径/控件Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true, Length=200)]
	public string  S03_FrontRoute { get; set; }

	/// <summary>
	/// 后端 控制器/方法/路由
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true, Length=200)]
	public string  S03_BackRoute { get; set; }

	/// <summary>
	/// 是否有效 0有效 1无效
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false)]
	public byte  S03_IsValid { get; set; }

	/// <summary>
	/// 创建者Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false)]
	public int  S03_CreateId { get; set; }

	/// <summary>
	/// 创建者名称
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false, Length=20)]
	public string  S03_CreateBy { get; set; }

	/// <summary>
	/// 创建日期
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false)]
	public DateTime  S03_CreateTime { get; set; }

	/// <summary>
	/// 更新者Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true)]
	public int  ? S03_ModifyId { get; set; }

	/// <summary>
	/// 更新者名称
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true, Length=20)]
	public string  S03_ModifyBy { get; set; }

	/// <summary>
	/// 更新日期
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true)]
	public DateTime  ? S03_ModifyTime { get; set; }

	/// <summary>
	/// 删除者Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true)]
	public int  ? S03_DeleteId { get; set; }

	/// <summary>
	/// 删除者名称
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true, Length=20)]
	public string  S03_DeleteBy { get; set; }

	/// <summary>
	/// 删除日期
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true)]
	public DateTime  ? S03_DeleteTime { get; set; }

 
    }
}
	