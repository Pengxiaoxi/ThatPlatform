using MongoDB.Bson.Serialization.Attributes;
using Tpf.Common.CommonAttributes.Database;
using Tpf.Domain.Base.Domain.Entity;

namespace Tpf.Domain.AuthInfo.Domain.Entity
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
