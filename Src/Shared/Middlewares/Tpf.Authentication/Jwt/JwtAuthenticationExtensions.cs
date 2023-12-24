using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Tpf.Authentication.Jwt
{
    public static class JwtAuthenticationExtensions
    {
        public static void AddJwtBearerAuthentication(this IServiceCollection services)
        {

            var jwtOptions = new JwtOptions()
            {

            };

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })// 也可以直接写字符串，AddAuthentication("Bearer")
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = jwtOptions.SymmetricSecurityKey,
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer, //发行人
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience, //订阅人
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,//这个是缓冲过期时间，也就是说，即使我们配置了过期时间，这里也要考虑进去，过期时间+缓冲，默认好像是7分钟，你可以直接设置为0
                    RequireExpirationTime = true,
                };
            });
        }

    }
}
