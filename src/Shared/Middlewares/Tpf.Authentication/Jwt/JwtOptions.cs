﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Tpf.Authentication.Jwt
{
    public class JwtOptions
    {
        public const string Name = "Jwt";
        public readonly static Encoding DefaultEncoding = Encoding.UTF8;
        public readonly static double DefaultExpiresMinutes = 6 * 60;

        public string? Audience { get; set; }

        public string? Issuer { get; set; }

        public double ExpiresMinutes { get; set; } = DefaultExpiresMinutes;

        public Encoding Encoding { get; set; } = DefaultEncoding;

        public string? SymmetricSecurityKeyString { get; set; }

        public SymmetricSecurityKey SymmetricSecurityKey => new(Encoding.UTF8.GetBytes(SymmetricSecurityKeyString));

    }
}
