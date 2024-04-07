using Newtonsoft.Json;

namespace Tpf.Domain.Base.Domain.Context
{
    public class UserContextInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("userName")]
        public string? UserName { get; set; }

        [JsonProperty("account")]
        public string? Account { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }

        [JsonProperty("deptId")]
        public string? DeptId { get; set; }

        [JsonProperty("deptName")]
        public string? DeptName { get; set; }
    }
}
