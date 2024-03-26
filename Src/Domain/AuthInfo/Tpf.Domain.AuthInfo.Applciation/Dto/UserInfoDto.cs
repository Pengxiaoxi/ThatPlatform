using Newtonsoft.Json;

namespace Tpf.Domain.AuthInfo.Applciation.Dto
{
    public class UserInfoInputDto
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }
    }


    public class UserInfoOutputDto
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("deptId")]
        public int DeptId { get; set; }

        [JsonProperty("deptName")]
        public string DeptName { get; set; }

    }

    


}
