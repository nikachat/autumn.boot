using EasyOffice.Enums;
using EasyOffice.Interfaces;
using EasyOffice.Models.Excel;
using Autumn.Common;
using Autumn.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Autumn.Services
{
    /// <summary>
    /// Office导入导出
    /// </summary>
    public class OfficeService : IOfficeService
    {
        private readonly IExcelExportService _excelExportService;
        private readonly IExcelImportService _excelImportService;

        public OfficeService(IExcelExportService excelExportService, IExcelImportService excelImportService)
        {
            _excelExportService = excelExportService;
            _excelImportService = excelImportService;
        }

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="fileData">数据源</param>
        /// <param name="fileName">导出文件名称（注意唯一）</param>
        /// <param name="filePath">数据源</param>
        /// <returns></returns>
        public async Task<bool> ExcelExport<T>(List<T> fileData, string fileName,string filePath="") where T : class, new()
        {
            try
            {
                string curDir = string.Empty;
                // 默认导出路径
                if (String.IsNullOrEmpty(filePath))
                {
                    curDir = AppContext.BaseDirectory + "/Export";
                }
                else
                {
                    curDir = filePath;
                }
                // 文件夹是否存在
                if (Directory.Exists(curDir))
                {
                    Directory.CreateDirectory(curDir);
                }

                string fileUrl = Path.Combine(curDir, fileName + ".xlsx");

                var bytes = await _excelExportService.ExportAsync(new ExportOption<T>()
                {
                    // 数据源
                    Data = fileData,
                    // 数据行起始索引，默认1
                    DataRowStartIndex = 1,
                    // 导出Excel类型，默认xls
                    // ExcelType = Bayantu.Extensions.Office.Enums.ExcelTypeEnum.XLS,
                    // 表头行索引，默认0
                    HeaderRowIndex = 0,
                    // 页签名称，默认sheet1
                    SheetName = "sheet1"
                });
                File.WriteAllBytes(fileUrl, bytes);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.ErrorLog(ex.Message, ex);
                return false;
            }
        }

        /// <summary>
        /// Excel导入
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="filePath">Excel绝对路径</param>
        /// <returns></returns>
        public async Task<DataTable> ExcelImport<T>(string filePath) where T : class, new()
        {
            var _rows = await _excelImportService.ValidateAsync<T>(new ImportOption()
            {
                //Excel文件绝对地址
                FileUrl = filePath,
                //数据起始行索引，默认1第二行
                DataRowStartIndex = 1,
                //表头起始行索引，默认0第一行
                HeaderRowIndex = 0,
                //映射字典，可以将模板类与Excel列重新映射， 默认null
                MappingDictionary = null,
                //页面索引，默认0第一个页签
                SheetIndex = 0,
                //校验模式，默认StopOnFirstFailure校验错误后此行停止继续校验，Continue：校验错误后继续校验
                ValidateMode = ValidateModeEnum.Continue
            });

            //数据校验
            var errorDatas = _rows.Where(x => !x.IsValid);

            if (errorDatas.Count() > 0)
            {
                NLogHelper.ErrorLog("Excel导入验证失败。");
                return null;
            }

            //导入转成DataTable
            return await _excelImportService.ToTableAsync<T>
            (
            //文件绝对地址
            filePath,
            //页签索引，默认0
            0,
            //表头行索引，默认0
            0,
            //数据行索引，默认1
            1,
            //读取多少条数据，默认-1全部
            -1);
        }
    }
}
