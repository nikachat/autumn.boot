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
	/// 用户
	/// </summary>
	[Serializable]
	public partial class S01_User :BaseEntity
	{
	/// <summary>
	/// 用户Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=true, IsIdentity=false, IsNullable=false)]
	public int  S01_UserId { get; set; }

	/// <summary>
	/// 用户代号
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false, Length=20)]
	public string  S01_UserCode { get; set; }

	/// <summary>
	/// 密码 加密处理
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false, Length=100)]
	public string  S01_Password { get; set; }

	/// <summary>
	/// 多角色Id 中间用|分割开
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true, Length=50)]
	public string  S02_RoleIds { get; set; }

	/// <summary>
	/// 联系电话
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true, Length=20)]
	public string  S01_Telephone { get; set; }

	/// <summary>
	/// 电子邮箱
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true, Length=40)]
	public string  S01_Email { get; set; }

	/// <summary>
	/// 备注
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true, Length=100)]
	public string  S01_Remarks { get; set; }

	/// <summary>
	/// 是否有效 0有效 1无效
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false)]
	public byte  S01_IsValid { get; set; }

	/// <summary>
	/// 创建者Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false)]
	public int  S01_CreateId { get; set; }

	/// <summary>
	/// 创建者名称
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false, Length=20)]
	public string  S01_CreateBy { get; set; }

	/// <summary>
	/// 创建日期
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=false)]
	public DateTime  S01_CreateTime { get; set; }

	/// <summary>
	/// 更新者Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true)]
	public int  ? S01_ModifyId { get; set; }

	/// <summary>
	/// 更新者名称
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true, Length=20)]
	public string  S01_ModifyBy { get; set; }

	/// <summary>
	/// 更新日期
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true)]
	public DateTime  ? S01_ModifyTime { get; set; }

	/// <summary>
	/// 删除者Id
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true)]
	public int  ? S01_DeleteId { get; set; }

	/// <summary>
	/// 删除者名称
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true, Length=20)]
	public string  S01_DeleteBy { get; set; }

	/// <summary>
	/// 删除日期
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true)]
	public DateTime  ? S01_DeleteTime { get; set; }

	/// <summary>
	/// 用户名称
	/// </summary>
	[DbLiteColumn(IsPrimaryKey=false, IsIdentity=false, IsNullable=true, Length=20)]
	public string  S01_UserName { get; set; }

 
    }
}
	