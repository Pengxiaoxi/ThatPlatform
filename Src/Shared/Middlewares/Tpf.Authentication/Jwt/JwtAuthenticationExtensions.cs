using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Tpf.Authentication.Jwt
{
    /// <summary>
    /// JwtAuthenticationExtensions
    /// </summary>
    public static class JwtAuthenticationExtensions
    {
        public static void AddJwtBearerAuthentication(this IServiceCollection services)
        {

            var jwtOptions = new JwtOptions()
            {

            };

            services.AddScoped<AppJwtBearerEvents>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // 也可以直接写字符串，AddAuthentication("Bearer")
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256, SecurityAlgorithms.RsaSha256 }, // 有效的签名算法列表，即验证Jwt的Header部分的alg。默认为null，即所有算法均可。

                    ValidTypes = new[] { JwtConstants.HeaderType }, // 有效的token类型列表，即验证Jwt的Header部分的typ。默认为null，即算有算法均可。

                    ValidIssuer = jwtOptions.Issuer, // 有效的签发者，即验证Jwt的Payload部分的iss。默认为null。
                    ValidateIssuer = true, // 是否验证签发者。默认为true。注意，如果设置了TokenValidationParameters.IssuerValidator，则该参数无论是何值，都会执行。

                    ValidAudience = jwtOptions.Audience, // 有效的受众，即验证Jwt的Payload部分的aud。默认为null。
                    ValidateAudience = true, // 是否验证受众。默认为true。注意，如果设置了TokenValidationParameters.AudienceValidator，则该参数无论是何值，都会执行。

                    IssuerSigningKey = jwtOptions.SymmetricSecurityKey, // 用于验证Jwt签名的密钥。对于对称加密来说，加签和验签都是使用的同一个密钥；对于非对称加密来说，使用私钥加签，然后使用公钥验签。
                    ValidateIssuerSigningKey = true, // 是否使用验证密钥验证签名。默认为false。注意，如果设置了TokenValidationParameters.IssuerSigningKeyValidator，则该参数无论是何值，都会执行。

                    ValidateLifetime = true, // 是否验证token是否在有效期内，即验证Jwt的Payload部分的nbf和exp。

                    RequireSignedTokens = true, // 是否要求token必须进行签名。默认为true，即token必须签名才可能有效。
                    RequireExpirationTime = true, // 是否要求token必须包含过期时间。默认为true，即Jwt的Payload部分必须包含exp且具有有效值。

                    NameClaimType = JwtClaimTypes.Name, // 设置 HttpContext.User.Identity.NameClaimType，便于 HttpContext.User.Identity.Name 取到正确的值
                    RoleClaimType = JwtClaimTypes.Role, // 设置 HttpContext.User.Identity.RoleClaimType，便于 HttpContext.User.Identity.IsInRole(xxx) 取到正确的值

                    ClockSkew = TimeSpan.Zero, // 设置时钟漂移，可以在验证token有效期时，允许一定的时间误差（如时间刚达到token中exp，但是允许未来5分钟内该token仍然有效）。默认为300s，即5min。本例jwt的签发和验证均是同一台服务器，所以这里就不需要设置时钟漂移了。
                };

                options.SaveToken = true; // 当token验证通过后，是否保存到 Microsoft.AspNetCore.Authentication.AuthenticationProperties，默认true。该操作发生在执行完JwtBearerEvents.TokenValidated之后。

                options.SecurityTokenValidators.Clear();
                options.SecurityTokenValidators.Add(new JwtSecurityTokenHandler()); // token验证器列表，可以指定验证token的处理器。默认含有1个JwtSecurityTokenHandler。

                options.EventsType = typeof(AppJwtBearerEvents); // 自定义的 JwtBearerEvents
            });
        }

    }
}
