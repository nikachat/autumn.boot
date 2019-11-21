using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Autumn.Common;
using Autumn.Extension;
using Microsoft.IdentityModel.Tokens;

namespace Autumn.AuthHelper.Auth
{
    public class JwtHelper
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJwt(JwtTokenModel tokenModel)
        {
            var claims = new List<Claim>
                {
                 /*
                   1、这里将用户的部分信息，比如 uid 存到了Claim 中，如果你想知道如何在其他地方将这个 uid从 Token 中取出来，请看下边的SerializeJwt()方法的调用。
                   2、你也可以研究下 HttpContext.User.Claims
                 */
                new Claim(ClaimTypes.Sid, tokenModel.Uid.ToString()),
                new Claim(ClaimTypes.Name,tokenModel.Name),
                new Claim(JwtRegisteredClaimNames.Iat,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                // 过期时间可自定义，注意JWT有自己的缓冲过期时间
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(Def.JwtAuthMiddleware_Exp).ToString()),
                //new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(Def.JwtAuthMiddleware_Exp)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss,Def.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud,Def.Audience),
                // 为了解决一个用户多个角色(比如：Admin,System)，用下边的方法
                //new Claim(ClaimTypes.Role,tokenModel.Role)
               };
            // 可以将一个用户的多个角色全部赋予
            claims.AddRange(tokenModel.Role.Split('|').Select(s => new Claim(ClaimTypes.Role, s)));

            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Def.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: Def.Issuer,
                claims: claims,
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return encodedJwt;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static JwtTokenModel SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);

            object name;
            object role;
            object expiration;

            jwtToken.Payload.TryGetValue(ClaimTypes.Name, out name);
            jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            jwtToken.Payload.TryGetValue(ClaimTypes.Expiration, out expiration);

            var tm = new JwtTokenModel
            {
                Uid = (jwtToken.Id).ToInt(),
                Name = name!=null? name.ToString():string.Empty,
                Expiration = expiration != null ? expiration.ToDate() : DateTime.MinValue,
                Role = role != null ? role.ToString() : string.Empty
            };
            return tm;
        }
    }

    /// <summary>
    /// 令牌
    /// </summary>
    public class JwtTokenModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Uid { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expiration { get; set; }
    }
}