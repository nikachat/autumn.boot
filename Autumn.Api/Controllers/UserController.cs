using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Autumn.Extension;
using Autumn.IServices;
using Autumn.Model;

namespace Autumn.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserController : BaseController
    {
        IUserService _UserServices;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="userServices"></param>
        public UserController(IUserService userServices)
        {
            _UserServices = userServices;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="CurrentPage">当前页码</param>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseModel> GetUser(int CurrentPage = 1, string UserName = "")
        {
            int totalCount = 0;
            int pageCount = 1;

            var users = await _UserServices.GetUser("1","1");
            _ResponseModel.PageCount = pageCount;
            _ResponseModel.DataCount = totalCount;
            _ResponseModel.Data = users;
            return _ResponseModel;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="userModel">测试</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel> InsertUser([FromBody] UserModel userModel)
        {
            if (await _UserServices.InsertUser(userModel) > 0)
            {
                //TODO新增成功后操作
            }
            return _ResponseModel;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="userModel">测试</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel> UpdateUser([FromBody] UserModel userModel)
        {
            if (await _UserServices.UpdateUser(userModel))
            {
                //TODO更新成功后操作
            }
            return _ResponseModel;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel> DeleteUser(int id)
        {
            if (id > 0)
            {
                var demo = await _UserServices.QueryById(id);
                // 软删除
                //demo.IsDeleted = true;
                if (await _UserServices.Update(demo))
                {
                    // TODO删除成功后操作
                }
            }
            return _ResponseModel;
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        /// <param name="demoModel">测试</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseModel> TransExecuteUser([FromBody] UserModel demoModel)
        {
            if (await _UserServices.TransExecuteUser(demoModel))
            {
                //TODO新增成功后操作
            }
            return _ResponseModel;
        }

    }
}