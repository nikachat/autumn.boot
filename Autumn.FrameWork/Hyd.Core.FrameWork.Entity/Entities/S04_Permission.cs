//-----------------------------------------------------------------------------
//此代码由T4模板自动生成 By Jim
//生成时间 2019-08-19 17:09:07 
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//-----------------------------------------------------------------------------
using System;
using SqlDbLite;

namespace Autumn.FrameWork
{	
	/// <summary>
	/// 权限
	/// </summary>
	[Serializable]
	public partial class S04_Permission :BaseEntity
	{
	/// <summary>
	/// 权限Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=true, IsIdentity=false, IsNullable=false)]
	public int  S04_PermissionId { get; set; }

	/// <summary>
	/// 权限名称
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false, Length=40)]
	public string  S04_PermissionName { get; set; }

	/// <summary>
	/// 角色Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false)]
	public int  S02_RoleId { get; set; }

	/// <summary>
	/// 模块Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false)]
	public int  S03_ModuleId { get; set; }

	/// <summary>
	/// 是否有效 0有效 1无效
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false)]
	public byte  S04_IsValid { get; set; }

	/// <summary>
	/// 创建者Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false)]
	public int  S04_CreateId { get; set; }

	/// <summary>
	/// 创建者名称
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false, Length=20)]
	public string  S04_CreateBy { get; set; }

	/// <summary>
	/// 创建日期
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false)]
	public DateTime  S04_CreateTime { get; set; }

	/// <summary>
	/// 更新者Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true)]
	public int  ? S04_ModifyId { get; set; }

	/// <summary>
	/// 更新者名称
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true, Length=20)]
	public string  S04_ModifyBy { get; set; }

	/// <summary>
	/// 更新日期
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true)]
	public DateTime  ? S04_ModifyTime { get; set; }

	/// <summary>
	/// 删除者Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true)]
	public int  ? S04_DeleteId { get; set; }

	/// <summary>
	/// 删除者名称
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true, Length=20)]
	public string  S04_DeleteBy { get; set; }

	/// <summary>
	/// 删除日期
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true)]
	public DateTime  ? S04_DeleteTime { get; set; }

 
    }
}
	