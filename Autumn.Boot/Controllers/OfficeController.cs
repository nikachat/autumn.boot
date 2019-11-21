using Autumn.Controllers;
using Autumn.Extension;
using Autumn.IServices;
using Autumn.Model;
using Autumn.Model.TemplateModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Autumn.Api.Controllers
{
    /// <summary>
    /// 导入导出
    /// </summary>
    public class OfficeController : BaseController
    {
        IOfficeService _OfficeService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="officeService"></param>
        public OfficeController(IOfficeService officeService)
        {
            _OfficeService = officeService;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseModel> GetExcelExport()
        {
            List<ExcelExport_UserInfo> list = new List<ExcelExport_UserInfo>();
            ExcelExport_UserInfo excelExport_UserInfo = new ExcelExport_UserInfo();
            excelExport_UserInfo.Age = 100;
            excelExport_UserInfo.CarCode = "F10000";
            excelExport_UserInfo.Gender = "男";
            excelExport_UserInfo.IdentityNumber = "100000000000";
            excelExport_UserInfo.Mobile = "188888888";
            excelExport_UserInfo.Name = "张三";
            excelExport_UserInfo.RegisterDate = DateTime.Now;
            list.Add(excelExport_UserInfo);
            string fileName = DateTime.Now.ToString("yyMMddHHmmssfff");
            if (await _OfficeService.ExcelExport(list, fileName))
                return _ResponseModel;
            else
            {
                _ResponseModel.Code = ResponseCode.Error;
                _ResponseModel.Message = MessageModel.Error;
                return _ResponseModel;
            }
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseModel> GetExcelImport()
        {
            // 文件路径
            string fileUrl = Path.Combine(AppContext.BaseDirectory, "filename.xlsx");

            var table = await _OfficeService.ExcelImport<ExcelImport_UserInfo>(fileUrl);

            if(table!=null)
                return _ResponseModel;
            else
            {
                _ResponseModel.Code = ResponseCode.Error;
                _ResponseModel.Message = MessageModel.Error;
                return _ResponseModel;
            }
        }
    }
}