using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Domain.AuthInfo.Applciation.Dto
{
    public class UserInfoQueryDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string? Account { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string? UserName { get; set; }


    }
}
