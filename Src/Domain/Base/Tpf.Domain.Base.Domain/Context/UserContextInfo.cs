namespace Tpf.Domain.Base.Domain.Context
{
    public class UserContextInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string? Id { get; set; }

        public string? UserName { get; set; }

        public string? Account { get; set; }

        public string? Phone { get; set; }

        public string? DeptId { get; set; }

        public string? DeptName { get; set; }
    }
}
