using Autumn.FrameWork;
using Autumn.IServices.BASE;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autumn.IServices
{
    /// <summary>
    /// IPermissionServices
    /// </summary>	
    public interface IPermissionService : IBaseService<S04_Permission>
    {
        Task<List<S04_Permission>> GetPermission();
    }
}
