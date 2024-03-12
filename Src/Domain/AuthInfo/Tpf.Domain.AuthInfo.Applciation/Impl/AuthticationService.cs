using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Tpf.Authentication.Jwt;
using Tpf.Domain.AuthInfo.Applciation.Dto;
using Tpf.Domain.AuthInfo.Applciation.Svc;

namespace Tpf.Domain.AuthInfo.Applciation.Impl
{
    public class AuthticationService : IAuthticationService
    {
        private readonly JwtBearerOptions _jwtBearerOptions;
        private readonly JwtOptions _jwtOptions;
        //private readonly SigningCredentials _signingCredentials;

        public AuthticationService(IOptionsSnapshot<JwtBearerOptions> jwtBearerOptions
            , IOptionsSnapshot<JwtOptions> jwtOptions
            //, SigningCredentials signingCredentials
            )
        {
            _jwtBearerOptions = jwtBearerOptions.Get(JwtBearerDefaults.AuthenticationScheme);
            _jwtOptions = jwtOptions.Value;
            //_signingCredentials = signingCredentials;
        }

        public string CreateJwtToken(UserInfoOutputDto user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(JwtClaimTypes.Id, user.Account),
                    new Claim(JwtClaimTypes.Name, user.UserName),
                }),
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresMinutes),
                //SigningCredentials = _signingCredentials
            };

            var handler = _jwtBearerOptions.SecurityTokenValidators.OfType<JwtSecurityTokenHandler>().FirstOrDefault()
                ?? new JwtSecurityTokenHandler();
            var securityToken = handler.CreateJwtSecurityToken(tokenDescriptor);
            var token = handler.WriteToken(securityToken);

            return token;
        }
    }
}
