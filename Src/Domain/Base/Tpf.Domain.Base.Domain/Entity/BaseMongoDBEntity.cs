using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;

namespace Tpf.Domain.Base.Domain.Entity
{
    /// <summary>
    /// BaseEntity<T>
    /// TODO: 考虑区分关系型数据库和非关系型数据库 BaseEntity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseEntity<T> where T : class
    {
        #region Field
        [JsonProperty("id")]
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
        public BaseEntity()
        {
            if (string.IsNullOrEmpty(this.Id))
            {
                this.Id = Guid.NewGuid().ToString();
            }
        }
        #endregion

        #region Extensions Method
        public void Create()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public void Modify()
        {
            
        }
        #endregion
    }

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



    public enum ORMEnum
    {
        EntityFramework = 1,

        Dapper = 2,

        SqlSuger = 3,

        MongoDB = 4,
    }

}
