using DocumentFormat.OpenXml.Spreadsheet;
using EasyOffice.Attributes;
using EasyOffice.Enums;
using System;

namespace Autumn.Model.TemplateModel
{
    /// <summary>
    /// Excel导出模板
    /// </summary>
    // 表头样式
    [Header(Color = ColorEnum.BRIGHT_GREEN, FontSize = 22, IsBold = true)]
    // 自动换行
    [WrapText]
    public class ExcelExport_UserInfo
    {
        [ColName("车牌号")]
        //相同数据自动合并单元格
        [MergeCols] 
        public string CarCode { get; set; }

        [ColName("手机号")]
        public string Mobile { get; set; }

        [ColName("身份证号")]
        public string IdentityNumber { get; set; }

        [ColName("姓名")]
        public string Name { get; set; }

        [ColName("性别")]
        public string Gender { get; set; }

        [ColName("注册日期")]
        public DateTime RegisterDate { get; set; }

        [ColName("年龄")]
        public int Age { get; set; }
    }
}
