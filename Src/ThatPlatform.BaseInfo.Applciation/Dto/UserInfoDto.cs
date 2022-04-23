using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThatPlatform.BaseInfo.Applciation.Dto
{
    public class UserInfoInputDto
    {

    }


    public class UserInfoOutputDto
    {
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
