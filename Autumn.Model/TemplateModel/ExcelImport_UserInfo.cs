using EasyOffice.Attributes;
using EasyOffice.Constants;
using System;

namespace Autumn.Model.TemplateModel
{
    /// <summary>
    /// Excel导入模板
    /// </summary>
    public class ExcelImport_UserInfo
    {
        //对应Excel列名
        [ColName("车牌号")]
        //校验必填
        [Required]
        //正则表达式校验,RegexConstant预置了一些常用的正则表达式，也可以自定义
        [Regex(RegexConstant.CAR_CODE_REGEX)]
        //校验模板类该列数据是否重复
        [Duplication]
        public string CarCode { get; set; }

        [ColName("手机号")]
        [Regex(RegexConstant.MOBILE_CHINA_REGEX)]
        public string Mobile { get; set; }

        [ColName("身份证号")]
        [Regex(RegexConstant.IDENTITY_NUMBER_REGEX)]
        public string IdentityNumber { get; set; }

        [ColName("姓名")]
        [MaxLength(10)] //最大长度校验
        public string Name { get; set; }

        [ColName("性别")]
        [Regex(RegexConstant.GENDER_REGEX)]
        public string Gender { get; set; }

        [ColName("注册日期")]
        [DateTime] //日期校验
        public DateTime RegisterDate { get; set; }

        [ColName("年龄")]
        [Range(0, 150)] //数值范围校验
        public int Age { get; set; }
    }
}
