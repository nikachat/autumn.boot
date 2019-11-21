using System.Threading.Tasks;
using Autumn.AuthHelper.Auth;
using Autumn.Extension;
using Autumn.IServices;
using Autumn.Model;
using Autumn.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using static Autumn.SwaggerHelper.CustomApiVersion;

namespace Autumn.Controllers.v1
{
    /// <summary>
    /// 鉴权认证
    /// </summary>
    [CustomRoute(ApiVersions.v1)]
    public class Oauth2Controller : BaseController
    {
        // 测试使用,实际开发请换成实际登录服务
        IUserService _loginServices;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="loginServices"></param>
        public Oauth2Controller(IUserService loginServices)
        {
            _loginServices = loginServices;
        }

        /// <summary>
        /// 登录获取令牌
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AccessToken")]
        public async Task<ResponseModel> AccessToken([FromBody]LoginModel model)
        {
            // 实际开发时请使用登录服务类通过数据库验证用户名密码的正确性，并取得相应的权限，此处为案例
            var user = await _loginServices.GetUser(model.UserCode, model.Password);
            
            if (user.UserId>0)
            {
                JwtTokenModel tokenModel = new JwtTokenModel();
                tokenModel.Uid = user.UserId;
                tokenModel.Name = user.UserName;
                tokenModel.Role = user.RoleIds;
                _ResponseModel.Data = JwtHelper.IssueJwt(tokenModel);
            }
            else
            {
                _ResponseModel.Code = ResponseCode.Error;
                _ResponseModel.Message = MessageModel.IncorrectLogin;
            }
            return _ResponseModel;
        }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        /// <param name="token">原令牌</param>
        /// <returns></returns>
        [HttpPost("RefreshToken")]
        public async Task<ResponseModel> RefreshToken(string token = "")
        {
            string jwtStr = string.Empty;

            if (string.IsNullOrEmpty(token))
            {
                _ResponseModel.Code = ResponseCode.Error;
                _ResponseModel.Message = MessageModel.InvalidToken;
                return _ResponseModel;
            }
            var oldTokenModel = JwtHelper.SerializeJwt(token);
            if (oldTokenModel != null)
            {
                var user = await _loginServices.GetUser(oldTokenModel.Uid.ToString());
                if (user != null)
                {
                    JwtTokenModel tokenModel = new JwtTokenModel();
                    tokenModel.Uid = user.UserId;
                    tokenModel.Name = user.UserName;
                    tokenModel.Role = user.RoleIds;
                    _ResponseModel.Data = JwtHelper.IssueJwt(tokenModel);
                    return _ResponseModel;
                }
                else
                {
                    _ResponseModel.Code = ResponseCode.Error;
                    _ResponseModel.Message = MessageModel.InvalidToken;
                    return _ResponseModel;
                }
            }
            else
            {
                _ResponseModel.Code = ResponseCode.Error;
                _ResponseModel.Message = MessageModel.InvalidToken;
                return _ResponseModel;
            }
        }
    }
}