using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThatPlatform.Common.BaseDomain.Entity;

namespace ThatPlatform.BaseInfo.Domain.Entity
{
    [BsonIgnoreExtraElements]
    public class UserInfo : BaseEntity<string>
    {
        [BsonElement("userName")]
        public string UserName { get; set; }


        public string UserPass{ get; set; }


        public string CompanyId { get; set; }

    }
}
