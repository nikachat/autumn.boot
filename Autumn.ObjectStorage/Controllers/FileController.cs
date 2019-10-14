using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Autumn.ObjectStorage.Controllers
{
    /// <summary>
    /// 文件上传下载
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public FileController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 上传附件(单个附件)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpLoad()
        {
            try
            {
                // 记录返回的附件Id
                string Id = "";
                // 本地存储文件路径
                string localFilePath = _hostingEnvironment.WebRootPath;
                // 获取上传的文件
                IFormFileCollection formFiles = Request.Form.Files;

                if (formFiles == null || formFiles.Count == 0)
                {
                    return Ok(new { Status = -1, Message = "没有上传文件", FilePath = "" });
                }
                IFormFile file = formFiles[0];
                // 获取文件名称后缀 
                string fileExtension = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);
                // 根据文件后缀自动分类存储文件夹
                string fileFolder = string.Empty;
                if (fileExtension.ToLower().Contains("doc") || fileExtension.ToLower().Contains("xls"))
                {
                    fileFolder = "/file/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                }
                else if (fileExtension.ToLower().Contains("jpg") || fileExtension.ToLower().Contains("png"))
                {
                    fileFolder = "/image/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                }
                else if (fileExtension.ToLower().Contains("mp3") || fileExtension.ToLower().Contains("avi"))
                {
                    fileFolder = "/media/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                }
                else
                {
                    fileFolder = "/other/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                }
                // 保存文件
                var stream = file.OpenReadStream();
                // 把 Stream 转换成 byte[] 
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                // 设置当前流的位置为流的开始
                stream.Seek(0, SeekOrigin.Begin);
                // 把 byte[] 写入文件 
                FileStream fs = new FileStream(localFilePath + fileFolder + file.FileName, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(bytes);
                bw.Close();
                fs.Close();
                return Ok(new { Success = true, Status = 0, Message = "上传成功", FilePath = fileFolder + file.FileName, Code = Id });
            }

            catch (Exception ex)
            {
                return Ok(new { Success = false, Status = -2, Message = "上传失败", Data = ex.Message, Code = "" });
            }
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DownLoad(string url)
        {
            try
            {
                var stream = System.IO.File.OpenRead(url);
                //var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);//推荐此方法
                string fileExt = Path.GetExtension(url);
                //获取文件的ContentType
                var provider = new FileExtensionContentTypeProvider();
                var memi = provider.Mappings[fileExt];
                return File(stream, memi, Path.GetFileName(url));
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Status = -3, Message = "下载失败", Data = ex.Message, Code = "" });
                //return NotFound();
            }
        }
    }
}