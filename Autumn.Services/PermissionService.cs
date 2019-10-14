using Autumn.Common;
using Autumn.FrameWork;
using Autumn.IRepository;
using Autumn.IServices;
using Autumn.Services.BASE;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autumn.Services
{
    public class PermissionService : BaseService<S04_Permission>, IPermissionService
    {
        readonly IPermissionRepository _dal;
        readonly IModuleRepository _moduleRepository;
        readonly IRoleRepository _roleRepository;

        // 将多个仓储接口注入
        public PermissionService(IPermissionRepository dal, IModuleRepository moduleRepository, IRoleRepository roleRepository)
        {
            _dal = dal;
            _moduleRepository = moduleRepository;
            _roleRepository = roleRepository;
            BaseDal = dal;
        }

        /// <summary>
        /// 获取全部角色权限数据
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10)]
        public async Task<List<S04_Permission>> GetPermission()
        {
            var permissions = await Query(a => a.S04_IsValid == 0);
            var roles = await _roleRepository.Query(a => a.S02_IsValid == 0);
            var modules = await _moduleRepository.Query(a => a.S03_IsValid == 0);

            if (permissions.Count > 0)
            {
                foreach (var item in permissions)
                {
                    item.S02_RoleName = roles.FirstOrDefault(d => d.S02_RoleId == item.S02_RoleId).S02_RoleName;
                    item.S03_BackRoute = modules.FirstOrDefault(d => d.S03_ModuleId == item.S03_ModuleId).S03_BackRoute;
                }
            }
            return permissions;
        }
    }
}
