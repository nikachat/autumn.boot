using AutoMapper;
using Autumn.Model;
using Autumn.FrameWork;

namespace Autumn.AutoMapper
{
    public class UserProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public UserProfile()
        {
            CreateMap<S01_User, UserModel>();
        }
    }
}
