using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using Tpf.Common.CommonAttributes.Database;
using Tpf.Domain.Base.Domain.Entity;

namespace Tpf.Domain.AuthInfo.Domain.Entity
{

    [Table("base_user")]
    public class UserInfo : BaseEntity<string>
    {
        [JsonProperty("username")]
        [Column("username")]
        public string UserName { get; set; }

        [JsonProperty("account")]
        [Column("account")]
        public string Account { get; set; }

        [JsonProperty("password")]
        [Column("password")]
        public string Password { get; set; }

        [JsonProperty("secretkey")]
        [Column("secretkey")]
        public string Secretkey { get; set; }

        [JsonProperty("phone")]
        [Column("phone")]
        public string Phone { get; set; }


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
