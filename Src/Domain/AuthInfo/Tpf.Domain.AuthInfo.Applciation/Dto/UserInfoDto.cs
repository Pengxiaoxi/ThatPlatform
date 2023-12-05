using Newtonsoft.Json;

namespace Tpf.Domain.AuthInfo.Applciation.Dto
{
    public class UserInfoInputDto
    {

    }


    public class UserInfoOutputDto
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("pass")]
        public string Pass { get; set; }

        [JsonProperty("deptId")]
        public int DeptId { get; set; }

        [JsonProperty("deptName")]
        public string DeptName { get; set; }

    }

    /// <summary>
    /// LoginDto
    /// </summary>
    public class LoginInputDto
    {
        public string Account { get; set; }

        public string Pass { get; set; }


    }

    /// <summary>
    /// LoginOutDto
    /// </summary>
    public class LoginOutputDto
    {
        public string UserName { get; set; }

        public string Account { get; set; }

        //public string Pass { get; set; }


    }


}
