using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Domain.UserInfo.Applciation.Dto
{
    public class UserInfoInputDto
    {

    }


    public class UserInfoOutputDto
    {
        [BsonElement("userName")]
        public string UserName { get; set; }

        [BsonElement("account")]
        public string Account { get; set; }

        [BsonElement("pass")]
        public string Pass { get; set; }

        public int DeptId { get; set; }

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
