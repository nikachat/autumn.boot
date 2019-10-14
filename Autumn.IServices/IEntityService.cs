using Autumn.FrameWork;
using Autumn.IServices.BASE;

namespace Autumn.IServices
{
    /// <summary>
    /// IDemoServices
    /// </summary>	
    public interface IEntityService :IBaseService<S01_User>
	{
        void CreateEntity();
    }
}
