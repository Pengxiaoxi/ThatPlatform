//using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tpf.Utils.Guids;

namespace Tpf.Domain.Base.Domain.Entity
{
    /// <summary>
    /// BaseEntity<T>
    /// TODO: 考虑区分关系型数据库和非关系型数据库 BaseEntity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseEntity<T> where T : class //, IDelete
    {
        #region Field
        //[Key]
        [JsonProperty("id")]
        [Column("id")]
        [Key]
        public string Id
        {
            get;
            set;
        }

        //[JsonProperty("created_userid")]
        //public string CreatedUserId { get; set; }

        //[JsonProperty("created_date")]
        //public DateTime? CreatedDate { get; set; }

        //[JsonProperty("update_userid")]
        //public string UpdateUserId { get; set; }

        //[JsonProperty("update_date")]
        //public DateTime? UpdateDate { get; set; }

        //[JsonProperty("is_deleted")]
        //public bool IsDeleted {  get; set; } = false;


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
            this.Id = GuidGenerator.Create();
        }

        public void Modify()
        {
            
        }
        #endregion
    }

}
