﻿using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tpf.Utils;

namespace Tpf.Authentication.Jwt
{
    public class JwtHelper
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        [Obsolete]
        public static string Issue(List<Claim> claims)
        {
            // 1. 定义需要使用到的Claims
            // 入参

            // 2. SecretKey
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigHelper.GetSecurityKey32()));

            // 3. 选择加密算法
            var algorithm = SecurityAlgorithms.HmacSha256;

            // 4. 生成Credentials
            var signingCredentials = new SigningCredentials(secretKey, algorithm);

            // 5. 根据以上信息，生成token
            var jwtSecurityToken = new JwtSecurityToken(
                "tpf",     //Issuer
                "tpf",   //Audience
                claims,                          //Claims,
                DateTime.Now,                    //notBefore
                DateTime.Now.AddHours(6),    //expires
                signingCredentials               //Credentials
            );

            // 6. 将token变为string
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }


    }


    //public class JwtHelper
    //{

    //    /// <summary>
    //    /// 颁发JWT字符串
    //    /// </summary>
    //    /// <param name="tokenModel"></param>
    //    /// <returns></returns>
    //    public static string IssueJwt(TokenModelJwt tokenModel)
    //    {
    //        // 自己封装的 appsettign.json 操作类，看下文
    //        string iss = ConfigHelper.Get(new string[] { "Audience", "Issuer" });
    //        string aud = ConfigHelper.Get(new string[] { "Audience", "Audience" });
    //        string secret = ConfigHelper.Get(new string[] { "Audience", "Secret" });

    //        var claims = new List<Claim>
    //          {
    //             /*
    //             * 特别重要：
    //               1、这里将用户的部分信息，比如 uid 存到了Claim 中，如果你想知道如何在其他地方将这个 uid从 Token 中取出来，请看下边的SerializeJwt() 方法，或者在整个解决方案，搜索这个方法，看哪里使用了！
    //               2、你也可以研究下 HttpContext.User.Claims ，具体的你可以看看 Policys/PermissionHandler.cs 类中是如何使用的。
    //             */                

    //            new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ToString()),
    //            new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
    //            new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
    //            //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
    //            new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
    //            new Claim(JwtRegisteredClaimNames.Iss,iss),
    //            new Claim(JwtRegisteredClaimNames.Aud,aud),

    //            //new Claim(ClaimTypes.Role,tokenModel.Role),//为了解决一个用户多个角色(比如：Admin,System)，用下边的方法
    //           };

    //        // 可以将一个用户的多个角色全部赋予；
    //        // 作者：DX 提供技术支持；
    //        claims.AddRange(tokenModel.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));


    //        //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
    //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //        var jwt = new JwtSecurityToken(
    //            issuer: iss,
    //            claims: claims,
    //            signingCredentials: creds);

    //        var jwtHandler = new JwtSecurityTokenHandler();
    //        var encodedJwt = jwtHandler.WriteToken(jwt);

    //        return encodedJwt;
    //    }

    //    /// <summary>
    //    /// 解析
    //    /// </summary>
    //    /// <param name="jwtStr"></param>
    //    /// <returns></returns>
    //    public static TokenModelJwt SerializeJwt(string jwtStr)
    //    {
    //        var jwtHandler = new JwtSecurityTokenHandler();
    //        JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
    //        object role;
    //        try
    //        {
    //            jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine(e);
    //            throw;
    //        }
    //        var tm = new TokenModelJwt
    //        {
    //            Uid = Convert.ToInt64(jwtToken.Id),
    //            //Role = role != null ? role.ObjToString() : "",
    //        };
    //        return tm;
    //    }
    //}

    ///// <summary>
    ///// 令牌
    ///// </summary>
    //public class TokenModelJwt
    //{
    //    /// <summary>
    //    /// Id
    //    /// </summary>
    //    public long Uid { get; set; }
    //    /// <summary>
    //    /// 角色
    //    /// </summary>
    //    public string Role { get; set; }
    //    /// <summary>
    //    /// 职能
    //    /// </summary>
    //    public string Work { get; set; }

    //}


}
