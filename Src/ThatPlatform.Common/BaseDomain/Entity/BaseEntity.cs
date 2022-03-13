using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThatPlatform.Common.BaseDomain.Entity
{
    public class BaseEntity<T> where T : class
    {
        #region Field
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id
        {
            get;
            set;
        }


        public string CreatedUserId { get; set; }

        public string CreatedUserName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string TenantId { get; set; }
        #endregion

        #region Ctor
        public BaseEntity()
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
