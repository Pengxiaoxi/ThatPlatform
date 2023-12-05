using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Tpf.Common.CommonAttributes.Database;
using Tpf.Domain.Base.Domain.Entity;

namespace Tpf.Domain.AuthInfo.Domain.Entity
{
    
    
    public class UserInfo : BaseEntity<string>
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("username")]
        public string Account { get; set; }

        [JsonProperty("password")]
        public string Pass { get; set; }

        [JsonProperty("username")]
        public string DeptId { get; set; }


    }

    [BsonIgnoreExtraElements]
    [MongoDbCollection("tpf_baseInfo", "userInfo")]
    public class UserInfoMongo : BaseMongoDBEntity<string>
    {
        [JsonProperty("username")]
        [BsonElement("userName")]
        public string UserName { get; set; }

        [JsonProperty("username")]
        [BsonElement("account")]
        public string Account { get; set; }

        [BsonElement("pass")]
        public string Pass { get; set; }

        public string DeptId { get; set; }


    }
}
