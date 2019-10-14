using Autumn.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Autumn.Controllers
{
    /// <summary>
    /// 自动生成实体
    /// </summary>
    public class EntityController : BaseController
    {
        IEntityService _EntityServices;

        public EntityController(IEntityService entityServices)
        {
            _EntityServices = entityServices;
        }

        /// <summary>
        /// 生成MYSQL实体类
        /// </summary>
        [HttpGet]
        public void CreateEntity()
        {
            _EntityServices.CreateEntity();
        }
    }
}