using Autumn.FrameWork;
using Autumn.IServices.BASE;
using Autumn.Model;
using System.Threading.Tasks;

namespace Autumn.IServices
{
    /// <summary>
    /// IUserServices
    /// </summary>	
    public interface IUserService :IBaseService<S01_User>
	{
        Task<UserModel> GetUser(string loginName, string loginPwd="");
        Task<int> InsertUser(UserModel userModel);
        Task<bool> UpdateUser(UserModel userModel);
        Task<bool> TransExecuteUser(UserModel userModel);
    }
}
