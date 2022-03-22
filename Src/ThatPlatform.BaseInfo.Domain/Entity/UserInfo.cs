﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThatPlatform.Common.BaseDomain.Entity;
using ThatPlatform.Common.Infrastructure.CommonAttributes.Database;

namespace ThatPlatform.BaseInfo.Domain.Entity
{
    [BsonIgnoreExtraElements]
    [MongoDbCollection("tpf_baseInfo", "userInfo")]
    public class UserInfo : BaseEntity<string>
    {
        [BsonElement("userName")]
        public string UserName { get; set; }


        public string UserPass{ get; set; }


        public string CompanyId { get; set; }

    }
}
