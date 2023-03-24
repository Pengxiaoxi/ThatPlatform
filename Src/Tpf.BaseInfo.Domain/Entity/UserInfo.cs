using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tpf.Common.BaseDomain.Entity;
using Tpf.Core.CommonAttributes.Database;

namespace Tpf.BaseInfo.Domain.Entity
{
    [BsonIgnoreExtraElements]
    [MongoDbCollection("tpf_baseInfo", "userInfo")]
    public class UserInfo : BaseEntity<string>
    {
        [BsonElement("userName")]
        public string UserName { get; set; }

        [BsonElement("account")]
        public string Account { get; set; }

        [BsonElement("pass")]
        public string Pass { get; set; }

        public string DeptId { get; set; }


    }
}
