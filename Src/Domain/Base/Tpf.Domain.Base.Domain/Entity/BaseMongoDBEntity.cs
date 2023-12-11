using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tpf.Domain.Base.Domain.Entity
{
    /// <summary>
    /// BaseEntity<T>
    /// TODO: 考虑区分关系型数据库和非关系型数据库 BaseEntity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [BsonIgnoreExtraElements]
    //[MongoDbCollection("tpf_baseInfo", "userInfo")]
    public class BaseMongoDBEntity<T> where T : class
    {
        #region Field
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id
        {
            get;
            set;
        }


        //public string CreatedUserId { get; set; }

        //public string CreatedUserName { get; set; }

        //public DateTime CreatedDate { get; set; }

        //public string TenantId { get; set; }
        #endregion

        #region Ctor
        public BaseMongoDBEntity()
        {
            if (string.IsNullOrEmpty(this.Id))
            {
                this.Id = ObjectId.GenerateNewId().ToString();
            }
        }
        #endregion

        #region Private Method
        //private void SetId()
        //{
        //    // Config
        //    ORMEnum orm = ORMEnum.MongoDB;
        //    switch (orm)
        //    {
        //        case ORMEnum.EntityFramework:
        //            this.Id = Guid.NewGuid() as T;
        //            break;
        //        case ORMEnum.MongoDB:
        //            this.Id = ObjectId.GenerateNewId().ToString() as T;
        //            break;
        //        default:
        //            this.Id = null;
        //            break;
        //    }
        //} 
        #endregion
    }
}
