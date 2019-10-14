using Autumn.FrameWork;
using Autumn.IServices;
using Autumn.Services.BASE;
using System.IO;

namespace Autumn.Services
{
    public class EntityService : BaseService<S01_User>, IEntityService
    {
        public void CreateEntity()
        {
            DbContext mycon = new DbContext();
            string path = Directory.GetCurrentDirectory();
            path = path.Substring(0, path.Length - 8);
            mycon.CreateClassFileByDBTalbe(path+ @"Autumn.FrameWork\Autumn.FrameWork.Entity\Entities", "Autumn.FrameWork", null, "", true);
        }
    }
}
