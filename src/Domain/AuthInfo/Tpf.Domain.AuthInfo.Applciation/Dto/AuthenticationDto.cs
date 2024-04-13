using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Domain.AuthInfo.Applciation.Dto
{
    public class AuthenticationDto
    {

    }

    /// <summary>
    /// LoginDto
    /// </summary>
    public class LoginInputDto
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

    }

    /// <summary>
    /// LoginOutDto
    /// </summary>
    public class LoginOutputDto
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }

    public class RegisterDto
    {        
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

    }
}

