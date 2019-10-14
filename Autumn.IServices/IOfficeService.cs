using EasyOffice.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Autumn.IServices
{
    public interface IOfficeService
    {
        Task<bool> ExcelExport<T>(List<T> fileData, string fileName, string filePath = "") where T : class, new();

        Task<DataTable> ExcelImport<T>(string filePath) where T : class, new();
    }
}
